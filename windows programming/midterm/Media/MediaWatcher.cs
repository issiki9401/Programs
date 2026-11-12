using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.Control;

namespace Midterm.Media
{
    /// <summary>多媒體播放狀態快照（供本機 GDI+ 介面與跨平台 JSON 同步使用）。</summary>
    public sealed class MediaState
    {
        public string Title = "";
        public string Artist = "";
        public bool Playing;
        public bool HasSession;
        public long PositionMs;
        public long DurationMs;
        public string ArtKey = "";
        public string? ArtBase64;   // JPEG Base64 編碼封面
        public bool IsFallbackDemo; // 是否為無外部播放器時的內建展示曲目
    }

    /// <summary>
    /// 監聽並控制 Windows 系統媒體工作階段（GSMTC：Spotify、Edge/Chrome YouTube、媒體播放器等）。
    /// 若系統目前未開啟任何外部音樂播放器，自動提供內建展示曲目，確保上台報告實機操作時必定可互動控制並同步至手機。
    /// </summary>
    public sealed class WindowsMediaWatcher : IDisposable
    {
        private GlobalSystemMediaTransportControlsSessionManager? _mgr;
        private GlobalSystemMediaTransportControlsSession? _session;
        private readonly object _g = new object();
        private MediaState _state = new MediaState();
        private System.Threading.Timer? _tick;
        private int _busy;
        private string _artKey = "";
        private string? _artB64;
        private string _lastPlayingAppId = "";
        private DateTime _stateAt = DateTime.UtcNow;

        // 內建備援展示曲目清單（當電腦未開啟外部播放器時使用）
        private static readonly (string Title, string Artist, long DurationMs, Color C1, Color C2)[] DemoTracks =
        {
            ("Koinoyokan_320kbps", "DESKTOP-STUDIO · Windows Audio", 85_000, Color.FromArgb(30, 136, 229), Color.FromArgb(156, 39, 176)),
            ("Nightcall (Synthwave Mix)", "Kavinsky · LAN Media Bridge", 142_000, Color.FromArgb(255, 87, 34), Color.FromArgb(103, 58, 183)),
            ("Starlight Resonance", "GDI+ Visual Engine", 118_000, Color.FromArgb(0, 188, 212), Color.FromArgb(26, 35, 126))
        };
        private int _demoIdx = 0;
        private bool _demoPlaying = false;
        private long _demoPosMs = 3_000;
        private DateTime _demoLastTick = DateTime.UtcNow;
        private string _demoArtKey = "";
        private string? _demoArtB64;

        public event Action? Changed;

        public MediaState State { get { lock (_g) return _state; } }

        public async Task StartAsync()
        {
            try
            {
                _mgr = await GlobalSystemMediaTransportControlsSessionManager.RequestAsync();
            }
            catch { }
            _tick = new System.Threading.Timer(_ => _ = PollAsync(), null, 0, 500);
        }

        private static bool IsSelf(GlobalSystemMediaTransportControlsSession s)
        {
            try { return (s.SourceAppUserModelId ?? "").IndexOf("Interconnect", StringComparison.OrdinalIgnoreCase) >= 0; }
            catch { return false; }
        }

        private GlobalSystemMediaTransportControlsSession? Pick()
        {
            var mgr = _mgr;
            if (mgr == null) return null;
            try
            {
                var list = mgr.GetSessions().Where(s => !IsSelf(s)).ToList();
                if (list.Count == 0) return null;
                var playing = list.FirstOrDefault(s => s.GetPlaybackInfo().PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing);
                if (playing != null)
                {
                    _lastPlayingAppId = playing.SourceAppUserModelId ?? "";
                    return playing;
                }
                if (!string.IsNullOrEmpty(_lastPlayingAppId))
                {
                    var remembered = list.FirstOrDefault(s => s.SourceAppUserModelId == _lastPlayingAppId);
                    if (remembered != null) return remembered;
                }
                var cur = mgr.GetCurrentSession();
                if (cur != null && !IsSelf(cur)) return cur;
                return list[0];
            }
            catch { return null; }
        }

        private async Task PollAsync()
        {
            if (Interlocked.Exchange(ref _busy, 1) == 1) return;
            try
            {
                var s = Pick();
                lock (_g) _session = s;
                MediaState ns;
                if (s != null)
                {
                    var props = await s.TryGetMediaPropertiesAsync();
                    var info = s.GetPlaybackInfo();
                    var tl = s.GetTimelineProperties();
                    bool playing = info.PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing;
                    string key = (props.Title ?? "") + "|" + (props.Artist ?? "") + "|" + (props.AlbumTitle ?? "");
                    if (key != _artKey)
                    {
                        _artKey = key;
                        _artB64 = await FetchArtAsync(props);
                    }
                    long pos = (long)tl.Position.TotalMilliseconds;
                    if (playing) pos += (long)Math.Max(0, (DateTimeOffset.UtcNow - tl.LastUpdatedTime).TotalMilliseconds);
                    long dur = (long)(tl.EndTime - tl.StartTime).TotalMilliseconds;
                    if (dur <= 0) dur = 180_000;
                    if (pos > dur) pos = dur;
                    ns = new MediaState
                    {
                        Title = string.IsNullOrWhiteSpace(props.Title) ? "Windows Media" : props.Title!,
                        Artist = string.IsNullOrWhiteSpace(props.Artist) ? Environment.MachineName : props.Artist!,
                        Playing = playing,
                        HasSession = true,
                        PositionMs = Math.Max(0, pos),
                        DurationMs = Math.Max(0, dur),
                        ArtKey = key,
                        ArtBase64 = _artB64,
                        IsFallbackDemo = false
                    };
                }
                else
                {
                    ns = BuildDemoState();
                }

                bool changed;
                lock (_g)
                {
                    var o = _state;
                    double expected = o.PositionMs + (o.Playing ? (DateTime.UtcNow - _stateAt).TotalMilliseconds : 0);
                    changed = o.Title != ns.Title || o.Artist != ns.Artist || o.Playing != ns.Playing ||
                              o.HasSession != ns.HasSession || o.DurationMs != ns.DurationMs || o.ArtKey != ns.ArtKey ||
                              Math.Abs(expected - ns.PositionMs) > 1200;
                    _state = ns;
                    if (changed) _stateAt = DateTime.UtcNow;
                }
                if (changed) Changed?.Invoke();
            }
            catch { }
            finally { Interlocked.Exchange(ref _busy, 0); }
        }

        /// <summary>建立內建展示曲目狀態（以 GDI+ 自動產生漸層幾何封面）。</summary>
        private MediaState BuildDemoState()
        {
            var now = DateTime.UtcNow;
            if (_demoPlaying)
            {
                _demoPosMs += (long)Math.Max(0, (now - _demoLastTick).TotalMilliseconds);
                var curTrack = DemoTracks[_demoIdx];
                if (_demoPosMs >= curTrack.DurationMs)
                {
                    _demoIdx = (_demoIdx + 1) % DemoTracks.Length;
                    _demoPosMs = 0;
                }
            }
            _demoLastTick = now;

            var t = DemoTracks[_demoIdx];
            string key = $"demo|{_demoIdx}|{t.Title}";
            if (key != _demoArtKey)
            {
                _demoArtKey = key;
                _demoArtB64 = GenerateDemoCoverBase64(t.Title, t.C1, t.C2);
            }
            return new MediaState
            {
                Title = t.Title,
                Artist = t.Artist,
                Playing = _demoPlaying,
                HasSession = true,
                PositionMs = Math.Min(_demoPosMs, t.DurationMs),
                DurationMs = t.DurationMs,
                ArtKey = key,
                ArtBase64 = _demoArtB64,
                IsFallbackDemo = true
            };
        }

        /// <summary>使用 GDI+ 繪製 192x192 漸層黑膠風格專輯封面並轉為 Base64 JPEG。</summary>
        private static string? GenerateDemoCoverBase64(string title, Color c1, Color c2)
        {
            try
            {
                const int side = 192;
                using var bmp = new Bitmap(side, side);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    using (var lg = new LinearGradientBrush(new Rectangle(0, 0, side, side), c1, c2, 45f))
                        g.FillRectangle(lg, 0, 0, side, side);
                    using (var pen = new Pen(Color.FromArgb(70, 255, 255, 255), 2f))
                    {
                        g.DrawEllipse(pen, 28, 28, side - 56, side - 56);
                        g.DrawEllipse(pen, 52, 52, side - 104, side - 104);
                    }
                    using (var br = new SolidBrush(Color.FromArgb(220, 15, 18, 24)))
                        g.FillEllipse(br, 68, 68, side - 136, side - 136);
                    using (var f = new Font("Segoe UI", 18, FontStyle.Bold, GraphicsUnit.Pixel))
                    using (var tb = new SolidBrush(Color.White))
                    {
                        var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString("♪", f, tb, new RectangleF(0, 0, side, side), sf);
                    }
                }
                using var ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return Convert.ToBase64String(ms.ToArray());
            }
            catch { return null; }
        }

        /// <summary>讀取系統媒體封面並縮放為 192px JPEG（Base64）。</summary>
        private static async Task<string?> FetchArtAsync(GlobalSystemMediaTransportControlsSessionMediaProperties props)
        {
            try
            {
                var thumb = props.Thumbnail;
                if (thumb == null) return null;
                using var stream = await thumb.OpenReadAsync();
                using var ms = new MemoryStream();
                using (var ras = System.IO.WindowsRuntimeStreamExtensions.AsStreamForRead(stream)) await ras.CopyToAsync(ms);
                ms.Position = 0;
                using var bmp = new Bitmap(ms);
                const int side = 192;
                using var small = new Bitmap(side, side);
                using (var g = Graphics.FromImage(small))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    float scale = Math.Max((float)side / bmp.Width, (float)side / bmp.Height);
                    float w = bmp.Width * scale, h = bmp.Height * scale;
                    g.DrawImage(bmp, (side - w) / 2, (side - h) / 2, w, h);
                }
                using var outMs = new MemoryStream();
                small.Save(outMs, System.Drawing.Imaging.ImageFormat.Jpeg);
                return Convert.ToBase64String(outMs.ToArray());
            }
            catch { return null; }
        }

        /// <summary>執行媒體控制指令（優先控制真實系統播放器，無外部播放器時控制內建展示曲目）。</summary>
        public async void Command(string cmd, long positionMs = 0)
        {
            try
            {
                GlobalSystemMediaTransportControlsSession? s;
                lock (_g) s = _session;
                s ??= Pick();
                if (s != null)
                {
                    switch (cmd)
                    {
                        case "play": await s.TryPlayAsync(); break;
                        case "pause": await s.TryPauseAsync(); break;
                        case "next": await s.TrySkipNextAsync(); break;
                        case "prev": await s.TrySkipPreviousAsync(); break;
                        case "seek": await s.TryChangePlaybackPositionAsync(positionMs * 10000); break;
                        default:
                            if (s.GetPlaybackInfo().PlaybackStatus == GlobalSystemMediaTransportControlsSessionPlaybackStatus.Playing)
                                await s.TryPauseAsync();
                            else
                                await s.TryPlayAsync();
                            break;
                    }
                    await Task.Delay(120);
                    await PollAsync();
                    return;
                }

                // 無外部播放器時操作內建展示曲目
                _demoLastTick = DateTime.UtcNow;
                switch (cmd)
                {
                    case "play": _demoPlaying = true; break;
                    case "pause": _demoPlaying = false; break;
                    case "next":
                        _demoIdx = (_demoIdx + 1) % DemoTracks.Length;
                        _demoPosMs = 0;
                        _demoPlaying = true;
                        break;
                    case "prev":
                        if (_demoPosMs > 5000) _demoPosMs = 0;
                        else { _demoIdx = (_demoIdx - 1 + DemoTracks.Length) % DemoTracks.Length; _demoPosMs = 0; }
                        _demoPlaying = true;
                        break;
                    case "seek":
                        _demoPosMs = Math.Max(0, Math.Min(DemoTracks[_demoIdx].DurationMs, positionMs));
                        break;
                    default:
                        _demoPlaying = !_demoPlaying;
                        break;
                }
                await PollAsync();
            }
            catch { }
        }

        public void Dispose() { try { _tick?.Dispose(); } catch { } }
    }
}
