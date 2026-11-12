using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Midterm.Media;

namespace Midterm.Net
{
    /// <summary>
    /// 首次執行時透過 UAC 提權加入 Windows 防火牆入站規則（TCP/UDP 47810-47825），
    /// 確保區網裝置能互相探索與建立控制連線。
    /// </summary>
    internal static class Firewall
    {
        private static string Flag => Path.Combine(Path.GetTempPath(), "interconnect_fw_v2.txt");
        public static void Ensure()
        {
            try
            {
                if (File.Exists(Flag)) return;
                var exe = Environment.ProcessPath ?? "";
                var appRule = !string.IsNullOrEmpty(exe)
                    ? $" & netsh advfirewall firewall add rule name=\"Interconnect App\" dir=in action=allow program=\"{exe}\" enable=yes profile=any"
                    : "";
                var cmd = "netsh advfirewall firewall add rule name=\"Interconnect LAN TCP\" dir=in action=allow protocol=TCP localport=47810-47825 profile=any & " +
                          "netsh advfirewall firewall add rule name=\"Interconnect LAN UDP\" dir=in action=allow protocol=UDP localport=47810-47825 profile=any & " +
                          "netsh advfirewall firewall add rule name=\"Interconnect LAN UDP Out\" dir=out action=allow protocol=UDP localport=47810-47825 profile=any" +
                          appRule;
                var psi = new ProcessStartInfo("cmd.exe", "/c " + cmd) { UseShellExecute = true, Verb = "runas", WindowStyle = ProcessWindowStyle.Hidden };
                var p = Process.Start(psi);
                if (p != null && p.WaitForExit(20000) && p.ExitCode == 0) File.WriteAllText(Flag, DateTime.Now.ToString("o"));
            }
            catch { }
        }
    }

    public sealed class DeviceInfo
    {
        public string Name = "";
        public string Ip = "";
        public string Platform = "";   // WIN | AND
        public int ControlPort;
        public DateTime LastSeen = DateTime.MinValue;
        public bool Online => (DateTime.Now - LastSeen).TotalSeconds < 6;
    }

    public sealed class Stats
    {
        public double RttMs = -1;
        public double Kbps;
        public string LocalIp = "";
        public string PeerIp = "";
        public long SyncCount;
    }

    /// <summary>單一 TCP 控制連線封裝。</summary>
    internal sealed class Conn
    {
        public TcpClient Client = null!;
        public NetworkStream Stream = null!;
        public bool Initiator;
        public string RemoteIp = "";
        public readonly object WriteLock = new object();
    }

    /// <summary>
    /// 連線與多媒體同步核心管理類別：
    /// 1. 區域網路自動探索（UDP Broadcast Beacon）
    /// 2. TCP JSON 控制通道（握手、Ping/Pong 即時 RTT 延遲量測、自動重連）
    /// 3. 雙向多媒體狀態同步與遠端控制（Windows GSMTC/SMTC ⇄ Android MediaSession）
    /// </summary>
    public sealed class SessionManager : IDisposable
    {
        public const int DiscoveryPort = 47811;
        private static readonly int PortOff = int.TryParse(Environment.GetEnvironmentVariable("INTERCONNECT_PORTOFF"), out var po) ? po : 0;
        public static readonly int ControlPort = 47810 + PortOff;

        private readonly string _deviceName;
        private Socket? _disc;
        private TcpListener? _server;
        private volatile Conn? _conn;
        private WindowsMediaWatcher? _watcher;
        private System.Threading.Timer? _beatTimer, _statsTimer, _mediaTimer, _reconnectTimer;
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private long _bytesWindow;
        private DeviceInfo? _lastPeer;
        private string _artSentKey = "\0";
        private DateTime _lastRx = DateTime.UtcNow;

        // 遠端（Android）傳來的多媒體狀態快取
        private readonly object _peerMediaLock = new object();
        private MediaState _peerMedia = new MediaState();
        private DateTime _peerMediaAt = DateTime.UtcNow;
        private string? _peerArtCache;

        public readonly List<string> Trace = new();
        private void Log(string s) { lock (Trace) { Trace.Add($"{DateTime.Now:HH:mm:ss.fff} {s}"); if (Trace.Count > 80) Trace.RemoveAt(0); } }

        /// <summary>目前多媒體面板控制目標："local"（本機電腦）或 "peer"（遠端裝置）。</summary>
        public string ControlTarget { get; private set; } = "local";
        public DeviceInfo? Peer { get; private set; }
        public bool Connected => _conn != null && Peer != null;
        public ConcurrentDictionary<string, DeviceInfo> Devices { get; } = new();
        public Stats CurrentStats { get; } = new Stats();
        public string DeviceName => _deviceName;

        public event Action? DevicesChanged;
        public event Action? ConnectionChanged;
        public event Action<string>? ConnectionFailed;
        public event Action<CtrlMsg>? PeerMediaChanged;
        public event Action? MediaStateUpdated;
        public event Action? StatsChanged;

        public SessionManager(string deviceName)
        {
            var env = Environment.GetEnvironmentVariable("INTERCONNECT_DEVNAME");
            _deviceName = !string.IsNullOrEmpty(env) ? env! : deviceName;
            CurrentStats.LocalIp = GetLocalIp();
        }

        public static string GetLocalIp()
        {
            try
            {
                using var s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                s.Connect("10.255.255.255", 1);
                return ((IPEndPoint)s.LocalEndPoint!).Address.ToString();
            }
            catch { return "127.0.0.1"; }
        }

        public bool IsSelf(DeviceInfo d) => d.Name == _deviceName && LocalIps().Contains(d.Ip) && d.ControlPort == ControlPort;

        public static HashSet<string> LocalIps()
        {
            var set = new HashSet<string> { "127.0.0.1" };
            try
            {
                foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                    foreach (var ua in ni.GetIPProperties().UnicastAddresses)
                        if (ua.Address.AddressFamily == AddressFamily.InterNetwork) set.Add(ua.Address.ToString());
            }
            catch { }
            return set;
        }

        private static List<IPAddress> BroadcastAddresses()
        {
            var list = new List<IPAddress>();
            try
            {
                foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (ni.OperationalStatus != OperationalStatus.Up || ni.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;
                    foreach (var ua in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ua.Address.AddressFamily != AddressFamily.InterNetwork || ua.IPv4Mask == null) continue;
                        var a = ua.Address.GetAddressBytes(); var m = ua.IPv4Mask.GetAddressBytes();
                        var b = new byte[4];
                        for (int i = 0; i < 4; i++) b[i] = (byte)(a[i] | ~m[i]);
                        list.Add(new IPAddress(b));
                    }
                }
            }
            catch { }
            return list;
        }

        // ============================ 啟動服務 ============================
        public void Start()
        {
            Task.Run(() => Firewall.Ensure());
            try
            {
                _disc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                _disc.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _disc.EnableBroadcast = true;
                _disc.Bind(new IPEndPoint(IPAddress.Any, DiscoveryPort));
                new Thread(DiscoveryLoop) { IsBackground = true, Name = "Discovery" }.Start();
            }
            catch (Exception ex) { Log("disc-fail " + ex.Message); }

            try
            {
                _server = new TcpListener(IPAddress.Any, ControlPort);
                _server.Start();
                Task.Run(() => AcceptLoop(_cts.Token));
            }
            catch (Exception ex) { Log("tcp-listen-fail " + ex.Message); }

            _watcher = new WindowsMediaWatcher();
            _watcher.Changed += () =>
            {
                SendMedia();
                MediaStateUpdated?.Invoke();
            };
            _ = _watcher.StartAsync();

            _beatTimer = new System.Threading.Timer(_ => Beat(), null, 0, 1500);
            _statsTimer = new System.Threading.Timer(_ => UpdateStats(), null, 1000, 1000);
            _mediaTimer = new System.Threading.Timer(_ => SendMedia(), null, 2000, 2000);
        }

        // ============================ 多媒體狀態查詢與控制 ============================
        public void SetControlTarget(string target)
        {
            if (target != "local" && target != "peer") return;
            ControlTarget = target;
            MediaStateUpdated?.Invoke();
        }

        /// <summary>設定簡報展示用連線快照狀態。</summary>
        public void SetupDemoSnapshot()
        {
            var dev = new DeviceInfo { Name = "Xiaomi 14T Pro", Ip = "10.109.85.188", Platform = "AND", ControlPort = ControlPort, LastSeen = DateTime.Now };
            Devices[dev.Name] = dev;
            Peer = dev;
            _conn = new Conn { Initiator = true, RemoteIp = dev.Ip };
            CurrentStats.LocalIp = "10.109.85.146";
            CurrentStats.PeerIp = dev.Ip;
            CurrentStats.RttMs = 24;
            CurrentStats.SyncCount = 18;
            ControlTarget = "local";
            _watcher ??= new WindowsMediaWatcher();
            _watcher.Command("play");
            _watcher.Command("seek", 28000);
        }

        public MediaState GetLocalMedia() => _watcher?.State ?? new MediaState();

        public MediaState GetPeerMedia()
        {
            lock (_peerMediaLock)
            {
                var p = _peerMedia;
                long pos = p.PositionMs;
                if (p.Playing)
                {
                    pos += (long)Math.Max(0, (DateTime.UtcNow - _peerMediaAt).TotalMilliseconds);
                    if (p.DurationMs > 0 && pos > p.DurationMs) pos = p.DurationMs;
                }
                return new MediaState
                {
                    Title = p.Title,
                    Artist = p.Artist,
                    Playing = p.Playing,
                    HasSession = p.HasSession,
                    PositionMs = pos,
                    DurationMs = p.DurationMs,
                    ArtKey = p.ArtKey,
                    ArtBase64 = p.ArtBase64
                };
            }
        }

        /// <summary>取得目前選取控制目標（本機或遠端）的多媒體狀態。</summary>
        public MediaState GetActiveMedia()
        {
            if (ControlTarget == "peer" && Connected)
            {
                var pm = GetPeerMedia();
                if (pm.HasSession) return pm;
                return new MediaState
                {
                    Title = $"已連線至 {Peer?.Name ?? "遠端裝置"}",
                    Artist = "等待遠端播放媒體或開啟通知存取權限…",
                    Playing = false,
                    HasSession = false,
                    PositionMs = 0,
                    DurationMs = 0
                };
            }
            return GetLocalMedia();
        }

        /// <summary>對目前選取的目標（本機 Windows 或遠端 Android）發送播放控制指令。</summary>
        public void TriggerMediaCommand(string cmd, long posMs = 0)
        {
            if (ControlTarget == "peer" && Connected)
            {
                Send(new CtrlMsg { t = "mediaCmd", cmd = cmd, positionMs = posMs });
                lock (_peerMediaLock)
                {
                    if (cmd == "play") { _peerMedia.Playing = true; _peerMediaAt = DateTime.UtcNow; }
                    else if (cmd == "pause")
                    {
                        _peerMedia.PositionMs = GetPeerMedia().PositionMs;
                        _peerMedia.Playing = false;
                        _peerMediaAt = DateTime.UtcNow;
                    }
                    else if (cmd == "toggle")
                    {
                        _peerMedia.PositionMs = GetPeerMedia().PositionMs;
                        _peerMedia.Playing = !_peerMedia.Playing;
                        _peerMediaAt = DateTime.UtcNow;
                    }
                    else if (cmd == "seek")
                    {
                        _peerMedia.PositionMs = posMs;
                        _peerMediaAt = DateTime.UtcNow;
                    }
                }
                MediaStateUpdated?.Invoke();
            }
            else
            {
                _watcher?.Command(cmd, posMs);
            }
        }

        // ============================ UDP 探索 ============================
        private void DiscoveryLoop()
        {
            var buf = new byte[512];
            EndPoint ep = new IPEndPoint(IPAddress.Any, 0);
            while (!_cts.IsCancellationRequested)
            {
                try
                {
                    int n = _disc!.ReceiveFrom(buf, ref ep);
                    HandleBeacon(Encoding.UTF8.GetString(buf, 0, n), ((IPEndPoint)ep).Address.ToString());
                }
                catch (ObjectDisposedException) { break; }
                catch { if (_cts.IsCancellationRequested) break; Thread.Sleep(100); }
            }
        }

        private void HandleBeacon(string text, string ip)
        {
            if (!text.StartsWith("INTR-ADV v1|")) return;
            var p = text.Split('|');
            if (p.Length < 4) return;
            var name = p[1];
            int.TryParse(p[3], out var cp);
            if (cp == 0) cp = ControlPort;
            ip = NormalizeIp(ip);
            if (name == _deviceName && LocalIps().Contains(ip) && cp == ControlPort) return;
            var d = Devices.GetOrAdd(name, _ => new DeviceInfo { Name = name });
            d.Ip = ip; d.Platform = p[2]; d.ControlPort = cp; d.LastSeen = DateTime.Now;
            DevicesChanged?.Invoke();
        }

        private static string NormalizeIp(string ip)
        {
            if (ip.StartsWith("::ffff:", StringComparison.OrdinalIgnoreCase)) ip = ip.Substring(7);
            int pct = ip.IndexOf('%');
            if (pct >= 0) ip = ip.Substring(0, pct);
            return ip;
        }

        private void Beat()
        {
            try
            {
                var buf = Encoding.UTF8.GetBytes($"INTR-ADV v1|{_deviceName}|WIN|{ControlPort}|0");
                var s = _disc;
                if (s != null)
                {
                    try { s.SendTo(buf, new IPEndPoint(IPAddress.Broadcast, DiscoveryPort)); } catch { }
                    foreach (var b in BroadcastAddresses()) { try { s.SendTo(buf, new IPEndPoint(b, DiscoveryPort)); } catch { } }
                }
            }
            catch { }
            if (_conn != null)
            {
                if ((DateTime.UtcNow - _lastRx).TotalSeconds > 12) { Log("watchdog-drop"); DropConn(_conn); }
                else Send(new CtrlMsg { t = "ping", ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() });
            }
            DevicesChanged?.Invoke();
        }

        private void UpdateStats()
        {
            var b = Interlocked.Exchange(ref _bytesWindow, 0);
            CurrentStats.Kbps = b * 8.0 / 1000.0;
            if (!Connected) CurrentStats.RttMs = -1;
            CurrentStats.PeerIp = Peer?.Ip ?? "";
            StatsChanged?.Invoke();
        }

        // ============================ TCP 連線 ============================
        public bool Connect(DeviceInfo dev)
        {
            try
            {
                if (Connected && Peer?.Name == dev.Name) return true;
                Disconnect(silent: true, forget: false);
                var cli = new TcpClient { NoDelay = true };
                var ar = cli.BeginConnect(dev.Ip, dev.ControlPort, null, null);
                if (!ar.AsyncWaitHandle.WaitOne(2500)) { try { cli.Close(); } catch { } ConnectionFailed?.Invoke("連線逾時：" + dev.Name); return false; }
                cli.EndConnect(ar);
                var c = new Conn { Client = cli, Stream = cli.GetStream(), Initiator = true, RemoteIp = dev.Ip };
                _lastRx = DateTime.UtcNow;
                _lastPeer = dev;
                Peer = dev;
                _conn = c;
                _artSentKey = "\0";
                Log("connect-ok " + dev.Name);
                Send(new CtrlMsg { t = "hello", name = _deviceName, platform = "WIN" });
                Send(new CtrlMsg { t = "ping", ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() });
                Task.Run(() => ReadLoop(c));
                ConnectionChanged?.Invoke();
                SendMedia();
                return true;
            }
            catch (Exception ex) { ConnectionFailed?.Invoke("連線失敗：" + ex.Message); return false; }
        }

        public void ConnectAsync(DeviceInfo dev, Action<bool>? done = null) => Task.Run(() => { var ok = Connect(dev); done?.Invoke(ok); });

        private async Task AcceptLoop(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested && _server != null)
            {
                TcpClient cli;
                try { cli = await _server.AcceptTcpClientAsync(ct); }
                catch { break; }
                try
                {
                    cli.NoDelay = true;
                    var ip = NormalizeIp((cli.Client.RemoteEndPoint as IPEndPoint)?.Address.ToString() ?? "");
                    Log("accept " + ip);
                    var old = _conn;
                    if (old != null) { _lastPeer = null; DropConn(old); }
                    var c = new Conn { Client = cli, Stream = cli.GetStream(), Initiator = false, RemoteIp = ip };
                    _lastRx = DateTime.UtcNow;
                    _artSentKey = "\0";
                    _conn = c;
                    _ = Task.Run(() => ReadLoop(c));
                }
                catch (Exception ex) { Log("accept-ex " + ex.Message); }
            }
        }

        private void ReadLoop(Conn c)
        {
            var buf = new byte[16384];
            var sb = new StringBuilder();
            try
            {
                while (!_cts.IsCancellationRequested)
                {
                    int n = c.Stream.Read(buf, 0, buf.Length);
                    if (n <= 0) break;
                    _lastRx = DateTime.UtcNow;
                    Interlocked.Add(ref _bytesWindow, n);
                    sb.Append(Encoding.UTF8.GetString(buf, 0, n));
                    int idx;
                    string cur = sb.ToString();
                    int start = 0;
                    while ((idx = cur.IndexOf('\n', start)) >= 0)
                    {
                        var line = cur.Substring(start, idx - start).Trim();
                        start = idx + 1;
                        if (line.Length > 0) HandleMsg(c, CtrlMsg.Parse(line));
                    }
                    sb.Clear();
                    if (start < cur.Length) sb.Append(cur, start, cur.Length - start);
                }
            }
            catch (Exception ex) { Log("readloop-ex:" + ex.Message); }
            OnConnLost(c);
        }

        private void OnConnLost(Conn c)
        {
            try { c.Client.Close(); } catch { }
            if (_conn != c) return;
            _conn = null;
            Peer = null;
            lock (_peerMediaLock) _peerMedia = new MediaState();
            if (ControlTarget == "peer") ControlTarget = "local";
            Log("conn-lost");
            ConnectionChanged?.Invoke();
            MediaStateUpdated?.Invoke();
            ScheduleReconnect(c.Initiator);
        }

        private void DropConn(Conn c) { try { c.Client.Close(); } catch { } }

        private void ScheduleReconnect(bool wasInitiator)
        {
            var target = _lastPeer;
            if (!wasInitiator || target == null) return;
            try { _reconnectTimer?.Dispose(); } catch { }
            int tries = 0;
            _reconnectTimer = new System.Threading.Timer(_ =>
            {
                if (Connected || _lastPeer == null || ++tries > 30) { try { _reconnectTimer?.Dispose(); } catch { } return; }
                var t = Devices.TryGetValue(target.Name, out var cur) ? cur : target;
                if (t.Online) Connect(t);
            }, null, 1500, 2000);
        }

        public void Disconnect(bool silent = false, bool forget = true)
        {
            if (forget) _lastPeer = null;
            try { _reconnectTimer?.Dispose(); } catch { }
            var c = _conn;
            if (c != null) Send(new CtrlMsg { t = "bye" });
            _conn = null;
            Peer = null;
            lock (_peerMediaLock) _peerMedia = new MediaState();
            if (ControlTarget == "peer") ControlTarget = "local";
            if (c != null) { try { c.Stream.Close(); c.Client.Close(); } catch { } }
            if (!silent)
            {
                ConnectionChanged?.Invoke();
                MediaStateUpdated?.Invoke();
            }
        }

        // ============================ 控制訊息處理 ============================
        private void HandleMsg(Conn c, CtrlMsg? m)
        {
            if (m == null || _conn != c) return;
            switch (m.t)
            {
                case "hello":
                    if (string.IsNullOrEmpty(m.name)) break;
                    var d = Devices.GetOrAdd(m.name, _ => new DeviceInfo { Name = m.name });
                    d.Ip = c.RemoteIp;
                    d.Platform = m.platform ?? d.Platform;
                    if (d.ControlPort == 0) d.ControlPort = ControlPort;
                    d.LastSeen = DateTime.Now;
                    Peer = d;
                    if (!c.Initiator)
                    {
                        Send(new CtrlMsg { t = "hello", name = _deviceName, platform = "WIN" });
                    }
                    Send(new CtrlMsg { t = "ping", ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() });
                    ConnectionChanged?.Invoke();
                    SendMedia();
                    break;
                case "ping":
                    Send(new CtrlMsg { t = "pong", ts = m.ts });
                    break;
                case "pong":
                    CurrentStats.RttMs = Math.Max(1, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - m.ts);
                    StatsChanged?.Invoke();
                    break;
                case "media":
                    lock (_peerMediaLock)
                    {
                        if (m.art != null) _peerArtCache = m.art.Length > 0 ? m.art : null;
                        _peerMedia = new MediaState
                        {
                            Title = m.title ?? "",
                            Artist = m.artist ?? "",
                            Playing = m.playing,
                            HasSession = m.hasControls,
                            PositionMs = m.positionMs,
                            DurationMs = m.durationMs,
                            ArtKey = $"{m.title}|{m.artist}",
                            ArtBase64 = _peerArtCache
                        };
                        _peerMediaAt = DateTime.UtcNow;
                    }
                    CurrentStats.SyncCount++;
                    if (m.hasControls && m.playing) ControlTarget = "peer";
                    PeerMediaChanged?.Invoke(m);
                    MediaStateUpdated?.Invoke();
                    break;
                case "mediaCmd":
                    if (m.cmd != null)
                    {
                        CurrentStats.SyncCount++;
                        _watcher?.Command(m.cmd, m.positionMs);
                    }
                    break;
                case "bye":
                    _lastPeer = null;
                    DropConn(c);
                    break;
            }
        }

        public void Send(CtrlMsg m)
        {
            var c = _conn;
            if (c == null) return;
            try
            {
                var buf = Encoding.UTF8.GetBytes(m.Serialize() + "\n");
                lock (c.WriteLock) c.Stream.Write(buf, 0, buf.Length);
                Interlocked.Add(ref _bytesWindow, buf.Length);
            }
            catch { }
        }

        private void SendMedia()
        {
            var w = _watcher;
            if (w == null || !Connected) return;
            var st = w.State;
            var msg = new CtrlMsg
            {
                t = "media",
                title = st.Title,
                artist = st.Artist,
                playing = st.Playing,
                hasControls = st.HasSession,
                positionMs = st.PositionMs,
                durationMs = st.DurationMs,
            };
            if (st.ArtKey != _artSentKey) { msg.art = st.ArtBase64 ?? ""; _artSentKey = st.ArtKey; }
            Send(msg);
        }

        public void Dispose()
        {
            try { _cts.Cancel(); } catch { }
            try { _beatTimer?.Dispose(); _statsTimer?.Dispose(); _mediaTimer?.Dispose(); _reconnectTimer?.Dispose(); } catch { }
            try { Send(new CtrlMsg { t = "bye" }); } catch { }
            try { _watcher?.Dispose(); } catch { }
            try { _disc?.Close(); _server?.Stop(); _conn?.Client.Close(); } catch { }
        }
    }
}
