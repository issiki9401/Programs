using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Midterm.Media;
using Midterm.Net;

namespace Midterm
{
    /// <summary>
    /// 主視窗：採用無邊框 Material Design 3 風格，全介面由 <see cref="MainView"/> 透過 Windows Forms GDI+
    /// (<see cref="Graphics"/>、<see cref="GraphicsPath"/>、<see cref="LinearGradientBrush"/>) 依 DPI 動態繪製。
    /// </summary>
    public partial class Form1 : Form
    {
        private MainView view = null!;
        private NotifyIcon tray = null!;
        private ContextMenuStrip trayMenu = null!;
        private SessionManager session = null!;
        private MediaBridge mediaBridge = null!;
        private readonly List<double> _chartHistory = new List<double>(new double[60]);
        private System.Windows.Forms.Timer animTimer = null!;
        private double _wavePhase;

        public Form1()
        {
            InitializeComponent();
            session = new SessionManager(Environment.MachineName);
            mediaBridge = new MediaBridge();
            for (int i = 0; i < 60; i++)
                _chartHistory.Add(12 + Math.Sin(i * 0.25) * 4);
            BuildUi();
            Wire();
        }

        private float S => DeviceDpi / 96f;

        private void BuildUi()
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            AutoScaleMode = AutoScaleMode.None;
            MinimumSize = new Size((int)(400 * S), (int)(660 * S));
            Size = new Size((int)(470 * S), (int)(780 * S));
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Md3.Surface;
            Text = "Interconnect";
            ShowInTaskbar = true;

            view = new MainView(session) { Dock = DockStyle.Fill, History = _chartHistory };
            Controls.Add(view);

            trayMenu = new ContextMenuStrip();
            tray = new NotifyIcon { Text = "Interconnect — 雙系統多媒體控制橋接", Visible = true, ContextMenuStrip = trayMenu, Icon = MakeIcon() };
            Icon = tray.Icon;
            tray.DoubleClick += (s, e) => ShowMain();
            tray.MouseDown += (s, e) => BuildTrayMenu();

            // 60ms GDI+ 動畫計時器：驅動頻譜律動、黑膠唱片旋轉與進度列平滑推進
            animTimer = new System.Windows.Forms.Timer { Interval = 60 };
            animTimer.Tick += (s, e) =>
            {
                _wavePhase += 0.18;
                view.AdvanceAnimation((float)_wavePhase);
            };
            animTimer.Start();
        }

        private static Icon MakeIcon()
        {
            var bmp = new Bitmap(32, 32);
            using (var g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = SmoothingMode.AntiAlias;
                LineIcons.Door(g, new RectangleF(3, 3, 26, 26), Color.FromArgb(168, 199, 250), 2.4f);
            }
            return Icon.FromHandle(bmp.GetHicon());
        }

        private void Wire()
        {
            Load += (s, e) =>
            {
                session.Start();
                mediaBridge.Attach(Handle);
                session.PeerMediaChanged += m => BeginInvoke(new Action(() => mediaBridge.Update(m)));
                mediaBridge.CommandForwarded += (cmd, pos) => session.Send(new CtrlMsg { t = "mediaCmd", cmd = cmd, positionMs = pos });
                session.DevicesChanged += () => SafeInvalidate();
                session.ConnectionChanged += () =>
                {
                    SafeInvalidate();
                    if (!session.Connected) BeginInvoke(new Action(() => mediaBridge.Clear()));
                };
                session.MediaStateUpdated += () => SafeInvalidate();
                session.ConnectionFailed += msg => BeginInvoke(new Action(() => view.ShowToast(msg)));
                session.StatsChanged += () => BeginInvoke(new Action(() =>
                {
                    double val = session.Connected && session.CurrentStats.RttMs >= 0
                        ? session.CurrentStats.RttMs
                        : 14 + Math.Sin(_wavePhase) * 5 + Math.Cos(_wavePhase * 0.7) * 3;
                    _chartHistory.Add(Math.Max(2, val));
                    if (_chartHistory.Count > 60) _chartHistory.RemoveAt(0);
                    view.Invalidate();
                }));

                if (Environment.GetEnvironmentVariable("INTERCONNECT_AUTOCONNECT") != null)
                {
                    var at = new System.Windows.Forms.Timer { Interval = 1000 };
                    int tries = 0;
                    at.Tick += (s3, e3) =>
                    {
                        tries++;
                        if (session.Connected || tries > 20) { at.Stop(); return; }
                        var want = Environment.GetEnvironmentVariable("INTERCONNECT_PEER");
                        var dev = session.Devices.Values.FirstOrDefault(d => d.Online && (want == null || d.Name.Contains(want)));
                        if (dev != null) { session.ConnectAsync(dev); at.Stop(); }
                    };
                    at.Start();
                }
            };

            view.CloseClicked += HideToTray;
            view.MinClicked += () => WindowState = FormWindowState.Minimized;
            view.MaxClicked += () =>
            {
                if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Normal;
                else { MaximizedBounds = Screen.FromControl(this).WorkingArea; WindowState = FormWindowState.Maximized; }
                view.Maximized = WindowState == FormWindowState.Maximized;
            };
            view.ThemeClicked += pt =>
            {
                var menu = new ContextMenuStrip();
                var dark = new ToolStripMenuItem("Dark（深色主題）") { Checked = Md3.Dark };
                var light = new ToolStripMenuItem("Light（淺色主題）") { Checked = !Md3.Dark };
                dark.Click += (s2, e2) => { Md3.Dark = true; ApplyTheme(); };
                light.Click += (s2, e2) => { Md3.Dark = false; ApplyTheme(); };
                menu.Items.AddRange(new ToolStripItem[] { dark, light });
                menu.Show(view, pt);
            };
            view.DragRequested += () => { NativeMethods.ReleaseCapture(); NativeMethods.SendMessage(Handle, 0xA1, new IntPtr(2), IntPtr.Zero); };
            view.TargetRequested += target => session.SetControlTarget(target);
            view.MediaCmdRequested += (cmd, pos) => session.TriggerMediaCommand(cmd, pos);
            view.ConnectRequested += dev => session.ConnectAsync(dev, ok => SafeInvalidate());
            view.DisconnectRequested += () => { session.Disconnect(); view.Invalidate(); };

            FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing) { e.Cancel = true; HideToTray(); }
            };
        }

        private void SafeInvalidate()
        {
            try { if (IsHandleCreated) BeginInvoke(new Action(() => view.Invalidate())); } catch { }
        }

        private void ApplyTheme()
        {
            BackColor = Md3.Surface;
            view.Invalidate();
        }

        private void HideToTray() { Hide(); BuildTrayMenu(); }
        private void ShowMain() { Show(); WindowState = FormWindowState.Normal; Activate(); }

        /// <summary>供簡報匯出高解析度實機介面截圖（模擬已連線 Xiaomi 14T Pro 並同步播放 Koinoyokan_320kbps）。</summary>
        public void ExportDemoScreenshot(string outPath)
        {
            tray.Visible = false;
            session.SetupDemoSnapshot();
            System.Threading.Thread.Sleep(300);
            _chartHistory.Clear();
            var rtts = new double[] { 22, 24, 19, 25, 28, 21, 18, 23, 26, 20, 17, 22, 24, 19, 21, 25, 23, 18, 20, 24, 22, 19, 23, 27, 21, 18, 20, 22, 19, 24 };
            foreach (var r in rtts) _chartHistory.Add(r);
            view.AdvanceAnimation(2.4f);
            using var bmp = new Bitmap(Width, Height);
            using (var g = Graphics.FromImage(bmp))
            {
                view.RenderToGraphics(g);
            }
            bmp.Save(outPath, System.Drawing.Imaging.ImageFormat.Png);
            session.Dispose();
        }

        private void BuildTrayMenu()
        {
            trayMenu.Items.Clear();
            trayMenu.Items.Add(new ToolStripMenuItem("INTER connect — 多媒體控制") { Enabled = false });
            var devMenu = new ToolStripMenuItem("連線裝置");
            foreach (var d in session.Devices.Values.Where(d => !session.IsSelf(d)).OrderByDescending(d => d.Online))
            {
                var item = new ToolStripMenuItem($"{d.Name} ({d.Platform})") { Enabled = d.Online, Checked = session.Connected && session.Peer?.Name == d.Name };
                var captured = d;
                item.Click += (s, e) => session.ConnectAsync(captured, ok => SafeInvalidate());
                devMenu.DropDownItems.Add(item);
            }
            if (devMenu.DropDownItems.Count == 0) devMenu.DropDownItems.Add(new ToolStripMenuItem("尚未發現區網裝置") { Enabled = false });

            var playToggle = new ToolStripMenuItem("播放 / 暫停");
            playToggle.Click += (s, e) => session.TriggerMediaCommand("toggle");
            var nextTrack = new ToolStripMenuItem("下一首");
            nextTrack.Click += (s, e) => session.TriggerMediaCommand("next");
            var prevTrack = new ToolStripMenuItem("上一首");
            prevTrack.Click += (s, e) => session.TriggerMediaCommand("prev");

            var show = new ToolStripMenuItem("顯示主視窗");
            show.Click += (s, e) => ShowMain();
            var exit = new ToolStripMenuItem("結束程式") { ForeColor = Color.Red };
            exit.Click += (s, e) => { tray.Visible = false; session.Dispose(); mediaBridge.Dispose(); Application.Exit(); };
            trayMenu.Items.AddRange(new ToolStripItem[] { devMenu, new ToolStripSeparator(), playToggle, prevTrack, nextTrack, new ToolStripSeparator(), show, exit });
        }

        /// <summary>無邊框視窗：攔截 WM_NCHITTEST 實作八方向邊緣拖曳縮放。</summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84) // WM_NCHITTEST
            {
                base.WndProc(ref m);
                if (WindowState == FormWindowState.Maximized) return;
                var p = PointToClient(new Point(unchecked((short)(m.LParam.ToInt64() & 0xFFFF)), unchecked((short)((m.LParam.ToInt64() >> 16) & 0xFFFF))));
                int g = (int)(8 * S);
                bool l = p.X < g, r = p.X >= ClientSize.Width - g, t = p.Y < g, b = p.Y >= ClientSize.Height - g;
                if (t && l) { m.Result = new IntPtr(13); return; }
                if (t && r) { m.Result = new IntPtr(14); return; }
                if (b && l) { m.Result = new IntPtr(16); return; }
                if (b && r) { m.Result = new IntPtr(17); return; }
                if (l) { m.Result = new IntPtr(10); return; }
                if (r) { m.Result = new IntPtr(11); return; }
                if (t) { m.Result = new IntPtr(12); return; }
                if (b) { m.Result = new IntPtr(15); return; }
                return;
            }
            base.WndProc(ref m);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (WindowState == FormWindowState.Maximized) Region = null;
            else
            {
                using var path = Gfx.RoundRect(new RectangleF(0, 0, Width, Height), 20 * S);
                Region = new Region(path);
            }
            view?.Invalidate();
        }
    }

    internal static class NativeMethods
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")] public static extern bool ReleaseCapture();
        [System.Runtime.InteropServices.DllImport("user32.dll")] public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);
    }

    /// <summary>
    /// GDI+ 單一繪製表面：
    /// 1. 自訂無邊框標題列與向量圖示
    /// 2. 網路連線與即時 RTT 延遲漸層折線圖面板
    /// 3. 雙向多媒體播放控制卡片（圓角封面裁切、旋轉黑膠、動態頻譜柱、可拖曳進度條、向量播放控制鈕）
    /// 4. 區域網路探索裝置列表
    /// </summary>
    internal sealed class MainView : Control
    {
        private readonly SessionManager _s;
        private readonly List<(RectangleF r, Action a)> _hits = new();
        private readonly Dictionary<(float, bool), Font> _fonts = new();
        private float _scroll;
        private float _contentH, _viewH;
        private string? _toast;
        private DateTime _toastUntil;
        private int _hoverIdx = -1;
        private RectangleF _titleRect;
        private RectangleF _seekBarRect;
        private bool _draggingSeek;
        private float _discAngle;
        private float _animPhase;
        private readonly float[] _specHeights = new float[10];

        // 封面圖片快取（避免每次 OnPaint 重新解碼 Base64）
        private string? _cachedArtB64;
        private Bitmap? _cachedArtBmp;

        public List<double> History = new();
        public bool Maximized;

        public event Action? CloseClicked, MinClicked, MaxClicked, DragRequested, DisconnectRequested;
        public event Action<Point>? ThemeClicked;
        public event Action<string>? TargetRequested;
        public event Action<string, long>? MediaCmdRequested;
        public event Action<DeviceInfo>? ConnectRequested;

        public MainView(SessionManager s)
        {
            _s = s;
            SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
        }

        private float S => DeviceDpi / 96f;

        public void AdvanceAnimation(float phase)
        {
            _animPhase = phase;
            var media = _s.GetActiveMedia();
            if (media.Playing)
            {
                _discAngle = (_discAngle + 4.5f) % 360f;
                for (int i = 0; i < _specHeights.Length; i++)
                {
                    float target = 0.28f + 0.68f * (float)Math.Abs(Math.Sin(phase * 1.7f + i * 0.65f) * Math.Cos(phase * 0.9f - i * 0.4f));
                    _specHeights[i] = _specHeights[i] * 0.45f + target * 0.55f;
                }
                Invalidate();
            }
            else
            {
                bool any = false;
                for (int i = 0; i < _specHeights.Length; i++)
                {
                    if (_specHeights[i] > 0.14f) { _specHeights[i] *= 0.75f; any = true; }
                    else _specHeights[i] = 0.12f;
                }
                if (any) Invalidate();
            }
        }

        private Font F(float px, bool bold = false)
        {
            var k = (px * S, bold);
            if (!_fonts.TryGetValue(k, out var f))
                _fonts[k] = f = new Font("Segoe UI", px * S, bold ? FontStyle.Bold : FontStyle.Regular, GraphicsUnit.Pixel);
            return f;
        }

        public void ShowToast(string msg) { _toast = msg; _toastUntil = DateTime.Now.AddSeconds(4); Invalidate(); }

        public void RenderToGraphics(Graphics g) => OnPaint(new PaintEventArgs(g, new Rectangle(0, 0, Width, Height)));

        protected override void OnDpiChangedAfterParent(EventArgs e)
        {
            foreach (var f in _fonts.Values) f.Dispose();
            _fonts.Clear();
            base.OnDpiChangedAfterParent(e);
        }

        private static readonly StringFormat Ellipsis = new StringFormat(StringFormatFlags.NoWrap)
        {
            Trimming = StringTrimming.EllipsisCharacter,
            LineAlignment = StringAlignment.Center
        };

        private void Text_(Graphics g, string t, Font f, Color c, RectangleF r, StringAlignment al = StringAlignment.Near)
        {
            using var br = new SolidBrush(c);
            using var sf = new StringFormat(Ellipsis) { Alignment = al };
            g.DrawString(t, f, br, r, sf);
        }

        private SizeF Measure(Graphics g, string t, Font f) => g.MeasureString(t, f, 10000, StringFormat.GenericTypographic);

        private Bitmap? GetAlbumArt(string? b64)
        {
            if (string.IsNullOrEmpty(b64)) return null;
            if (b64 == _cachedArtB64 && _cachedArtBmp != null) return _cachedArtBmp;
            try
            {
                var bytes = Convert.FromBase64String(b64);
                using var ms = new MemoryStream(bytes);
                var bmp = new Bitmap(ms);
                _cachedArtBmp?.Dispose();
                _cachedArtBmp = bmp;
                _cachedArtB64 = b64;
                return _cachedArtBmp;
            }
            catch { return null; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(Md3.Surface);
            _hits.Clear();
            float s = S, W = ClientSize.Width, H = ClientSize.Height;
            float m = Math.Max(16, Math.Min(28, W / s / 22)) * s;
            float gap = 14 * s;

            // ==================== 1. 標題列 ====================
            float tbH = 50 * s;
            _titleRect = new RectangleF(0, 0, W, tbH);
            LineIcons.Door(g, new RectangleF(m, 11 * s, 28 * s, 28 * s), Md3.Primary, 2.2f * s);
            var fA = F(19, true); var fB = F(15);
            float tx = m + 36 * s;
            var szA = g.MeasureString("INTER", fA);
            Text_(g, "INTER", fA, Md3.OnSurface, new RectangleF(tx, 0, szA.Width + 16 * s, tbH));
            Text_(g, "connect", fB, Md3.OnVariant, new RectangleF(tx + szA.Width + 2 * s, 2 * s, 120 * s, tbH));

            float bs = 34 * s, bx = W - m - bs, by = (tbH - bs) / 2;
            Btn(g, new RectangleF(bx, by, bs, bs), (gg, r) => LineIcons.Close(gg, r, Md3.OnSurface, 2f * s), () => CloseClicked?.Invoke()); bx -= bs + 2 * s;
            Btn(g, new RectangleF(bx, by, bs, bs), (gg, r) => LineIcons.Maximize(gg, r, Md3.OnSurface, Maximized, 2f * s), () => MaxClicked?.Invoke()); bx -= bs + 2 * s;
            Btn(g, new RectangleF(bx, by, bs, bs), (gg, r) => LineIcons.Minimize(gg, r, Md3.OnSurface, 2f * s), () => MinClicked?.Invoke()); bx -= bs + 2 * s;
            var themeR = new RectangleF(bx, by, bs, bs);
            Btn(g, themeR, (gg, r) => LineIcons.Chevron(gg, r, Md3.OnSurface, 2f * s), () => ThemeClicked?.Invoke(new Point((int)themeR.X, (int)themeR.Bottom)));
            using (var pen = new Pen(Md3.Outline)) g.DrawLine(pen, 0, tbH - 1, W, tbH - 1);

            // ==================== 2. 網路與延遲即時折線圖監控面板 ====================
            float y = tbH + gap;
            float cw = W - 2 * m;
            float monH = 128 * s;
            var monCard = new RectangleF(m, y, cw, monH);
            using (var path = Gfx.RoundRect(monCard, 18 * s))
            {
                using var br = new SolidBrush(Md3.Container); g.FillPath(br, path);
                using var pen = new Pen(Md3.Outline, 1.2f * s); g.DrawPath(pen, path);
            }
            float pad = 14 * s;
            float chartW = (cw - 3 * pad) * 0.50f;
            var chartRect = new RectangleF(monCard.X + pad, monCard.Y + pad, chartW, monH - 2 * pad);
            var infoRect = new RectangleF(chartRect.Right + pad, monCard.Y + pad, monCard.Right - pad - (chartRect.Right + pad), monH - 2 * pad);
            DrawChart(g, chartRect);

            var st = _s.CurrentStats;
            string ipText = "IP: " + (_s.Connected && !string.IsNullOrEmpty(_s.Peer?.Ip) ? _s.Peer!.Ip : st.LocalIp);
            string statusText = _s.Connected ? $"狀態: 已連線 ({_s.Peer?.Platform})" : "狀態: 區網探索就緒";
            string syncText = $"同步事件: {st.SyncCount} 次";
            string pingLabel = _s.Connected && st.RttMs >= 0 ? $"{st.RttMs:0} ms" : "LAN Ready";

            var fi = F(12.5f);
            float lh = 20 * s;
            Text_(g, ipText, fi, Md3.OnVariant, new RectangleF(infoRect.X, infoRect.Y, infoRect.Width, lh));
            Text_(g, statusText, fi, _s.Connected ? Md3.Primary : Md3.OnVariant, new RectangleF(infoRect.X, infoRect.Y + lh, infoRect.Width, lh));
            Text_(g, syncText, fi, Md3.OnVariant, new RectangleF(infoRect.X, infoRect.Y + lh * 2, infoRect.Width, lh));
            Text_(g, pingLabel, F(21, true), Md3.Primary, new RectangleF(infoRect.X, infoRect.Y + lh * 3 + 2 * s, infoRect.Width, infoRect.Height - lh * 3));
            y = monCard.Bottom + gap;

            // ==================== 3. GDI+ 雙向多媒體同步控制面板 ====================
            float mediaH = 226 * s;
            var mediaCard = new RectangleF(m, y, cw, mediaH);
            DrawMediaCard(g, mediaCard);
            y = mediaCard.Bottom + gap;

            // ==================== 4. 區域網路裝置探索列表 ====================
            Text_(g, "區域網路裝置（點擊卡片立即連線同步）", F(13.5f, true), Md3.OnVariant, new RectangleF(m + 4 * s, y, cw, 24 * s));
            y += 28 * s;

            var area = new RectangleF(m, y, cw, Math.Max(40 * s, H - y - m * 0.6f));
            _viewH = area.Height;
            var devs = _s.Devices.Values
                .Where(d => !_s.IsSelf(d) && (d.Online || (_s.Connected && _s.Peer?.Name == d.Name)))
                .OrderByDescending(d => _s.Connected && _s.Peer?.Name == d.Name).ThenBy(d => d.Name).ToList();
            var oldClip = g.Clip;
            g.SetClip(area);
            float cy = area.Y - _scroll;
            float start = cy;
            if (devs.Count == 0)
            {
                var emptyR = new RectangleF(area.X, cy, area.Width, 64 * s);
                using (var pEmpty = Gfx.RoundRect(emptyR, 14 * s))
                {
                    using var br = new SolidBrush(Md3.Container); g.FillPath(br, pEmpty);
                    using var pen = new Pen(Md3.Outline, 1f * s) { DashStyle = DashStyle.Dash }; g.DrawPath(pen, pEmpty);
                }
                Text_(g, "UDP 廣播掃描中…請確認手機與電腦位於同一 Wi-Fi 區網", F(13), Md3.OnVariant, emptyR, StringAlignment.Center);
                cy += 72 * s;
            }
            foreach (var d in devs)
            {
                bool conn = _s.Connected && _s.Peer?.Name == d.Name;
                float ch = (conn ? 104 : 72) * s;
                DrawDevice(g, new RectangleF(area.X, cy, area.Width, ch), d, conn, area);
                cy += ch + 10 * s;
            }
            _contentH = cy - start;
            g.Clip = oldClip;
            float maxScroll = Math.Max(0, _contentH - _viewH);
            if (_scroll > maxScroll) _scroll = maxScroll;

            // ==================== 5. Toast 提示 ====================
            if (_toast != null && DateTime.Now < _toastUntil)
            {
                var tr = new RectangleF(m, H - 52 * s, cw, 38 * s);
                using var path = Gfx.RoundRect(tr, 12 * s);
                using (var br = new SolidBrush(Md3.ErrorContainer)) g.FillPath(br, path);
                Text_(g, _toast, F(13), Md3.Error, new RectangleF(tr.X + 14 * s, tr.Y, tr.Width - 28 * s, tr.Height));
            }
        }

        /// <summary>繪製 GDI+ 即時折線圖（含漸層曲線下方填色與參考格線）。</summary>
        private void DrawChart(Graphics g, RectangleF r)
        {
            using (var br = new SolidBrush(Md3.Surface))
            using (var path = Gfx.RoundRect(r, 12 * S)) g.FillPath(br, path);
            using (var pen = new Pen(Color.FromArgb(32, Md3.OnSurface), 1f))
            {
                for (int i = 1; i < 3; i++)
                    g.DrawLine(pen, r.X + 6, r.Y + r.Height * i / 3f, r.Right - 6, r.Y + r.Height * i / 3f);
            }
            if (History.Count < 2) return;
            double max = Math.Max(25.0, History.Max() * 1.2);
            var pts = new PointF[History.Count];
            for (int i = 0; i < History.Count; i++)
            {
                float x = r.X + 6 * S + i * (r.Width - 12 * S) / (History.Count - 1);
                float yy = r.Bottom - 8 * S - (float)(History[i] / max) * (r.Height - 18 * S);
                pts[i] = new PointF(x, Math.Max(r.Y + 6 * S, Math.Min(r.Bottom - 6 * S, yy)));
            }

            // 漸層面積填充（展示 GDI+ LinearGradientBrush 與 GraphicsPath）
            using (var areaPath = new GraphicsPath())
            {
                areaPath.AddLines(pts);
                areaPath.AddLine(pts[^1].X, r.Bottom - 4 * S, pts[0].X, r.Bottom - 4 * S);
                areaPath.CloseFigure();
                using var grad = new LinearGradientBrush(r, Color.FromArgb(95, Md3.Primary), Color.FromArgb(5, Md3.Primary), LinearGradientMode.Vertical);
                g.FillPath(grad, areaPath);
            }
            using var line = new Pen(Md3.Primary, 2.2f * S) { LineJoin = LineJoin.Round, StartCap = LineCap.Round, EndCap = LineCap.Round };
            g.DrawLines(line, pts);
        }

        /// <summary>繪製互動式 GDI+ 多媒體播放控制面板。</summary>
        private void DrawMediaCard(Graphics g, RectangleF card)
        {
            float s = S;
            using (var path = Gfx.RoundRect(card, 18 * s))
            {
                using var br = new SolidBrush(Md3.Container); g.FillPath(br, path);
                using var pen = new Pen(Md3.Outline, 1.2f * s); g.DrawPath(pen, path);
            }

            float pad = 16 * s;
            // 頂部列：標題 + [本機電腦 | 遠端裝置] 分段切換
            var headerR = new RectangleF(card.X + pad, card.Y + 12 * s, card.Width - 2 * pad, 30 * s);
            Text_(g, "多媒體同步控制", F(14, true), Md3.OnSurface, new RectangleF(headerR.X, headerR.Y, 140 * s, headerR.Height));

            float segW = 168 * s, segH = 28 * s;
            DrawTargetSeg(g, new RectangleF(headerR.Right - segW, headerR.Y + 1 * s, segW, segH));

            // 取得目前顯示的多媒體快照
            var media = _s.GetActiveMedia();
            float bodyY = headerR.Bottom + 10 * s;
            float artSize = 68 * s;
            var artRect = new RectangleF(card.X + pad, bodyY, artSize, artSize);

            // 繪製圓角專輯封面或動態旋轉黑膠唱片
            var bmp = GetAlbumArt(media.ArtBase64);
            if (bmp != null)
            {
                var oldClip = g.Clip;
                using var artClip = Gfx.RoundRect(artRect, 14 * s);
                g.SetClip(artClip, CombineMode.Intersect);
                g.DrawImage(bmp, artRect);
                g.Clip = oldClip;
                using var borderPen = new Pen(Md3.Outline, 1f * s);
                using var borderPath = Gfx.RoundRect(artRect, 14 * s);
                g.DrawPath(borderPen, borderPath);
            }
            else
            {
                LineIcons.VinylDisc(g, artRect, _discAngle);
            }

            // 右上角：GDI+ 動態音律頻譜柱
            float specW = 66 * s, specH = 34 * s;
            var specRect = new RectangleF(card.Right - pad - specW, bodyY + 6 * s, specW, specH);
            DrawSpectrum(g, specRect, media.Playing);

            // 曲目標題與演出者
            float textX = artRect.Right + 14 * s;
            float textW = Math.Max(60 * s, specRect.X - textX - 8 * s);
            string title = string.IsNullOrEmpty(media.Title) ? "尚未播放媒體" : media.Title;
            string artist = string.IsNullOrEmpty(media.Artist) ? "點擊下方播放鈕開始控制" : media.Artist;
            string badge = _s.ControlTarget == "peer"
                ? (_s.Connected ? $"遠端同步 · {_s.Peer?.Name}" : "遠端未連線")
                : (media.IsFallbackDemo ? "本機展示曲目 · 自動同步至手機" : "Windows 系統媒體 · GSMTC");

            Text_(g, title, F(16, true), Md3.OnSurface, new RectangleF(textX, bodyY + 2 * s, textW, 24 * s));
            Text_(g, artist, F(13), Md3.OnVariant, new RectangleF(textX, bodyY + 26 * s, textW + specW, 20 * s));
            Text_(g, badge, F(11.5f), Md3.Primary, new RectangleF(textX, bodyY + 46 * s, textW + specW, 18 * s));

            // 互動式時間軸進度條（Seek Bar）
            float seekY = artRect.Bottom + 14 * s;
            long pos = Math.Max(0, media.PositionMs);
            long dur = Math.Max(0, media.DurationMs);
            float ratio = dur > 0 ? Math.Clamp((float)pos / dur, 0f, 1f) : 0f;

            var timeFont = F(11.5f);
            float timeW = 42 * s;
            Text_(g, FormatMs(pos), timeFont, Md3.OnVariant, new RectangleF(card.X + pad, seekY - 6 * s, timeW, 18 * s), StringAlignment.Near);
            Text_(g, FormatMs(dur), timeFont, Md3.OnVariant, new RectangleF(card.Right - pad - timeW, seekY - 6 * s, timeW, 18 * s), StringAlignment.Far);

            float barX = card.X + pad + timeW + 6 * s;
            float barW = card.Width - 2 * pad - 2 * timeW - 12 * s;
            float barH = 6 * s;
            _seekBarRect = new RectangleF(barX, seekY - 8 * s, barW, 22 * s);
            var trackR = new RectangleF(barX, seekY, barW, barH);
            using (var trackPath = Gfx.RoundRect(trackR, 3 * s))
            using (var trackBr = new SolidBrush(Md3.ContainerHigh))
                g.FillPath(trackBr, trackPath);

            if (ratio > 0.005f)
            {
                var fillR = new RectangleF(barX, seekY, Math.Max(6 * s, barW * ratio), barH);
                using var fillPath = Gfx.RoundRect(fillR, 3 * s);
                using var fillBr = new LinearGradientBrush(trackR, Md3.Primary, Md3.Accent, LinearGradientMode.Horizontal);
                g.FillPath(fillBr, fillPath);
            }
            float thumbX = barX + barW * ratio;
            var thumbR = new RectangleF(thumbX - 6 * s, seekY + barH / 2 - 6 * s, 12 * s, 12 * s);
            using (var thumbBr = new SolidBrush(Md3.OnSurface))
                g.FillEllipse(thumbBr, thumbR);

            // 註冊進度條點擊跳轉
            if (dur > 0)
            {
                var seekHit = _seekBarRect;
                long totalDur = dur;
                _hits.Add((seekHit, () =>
                {
                    var pt = PointToClient(Cursor.Position);
                    float r2 = Math.Clamp((pt.X - seekHit.X) / seekHit.Width, 0f, 1f);
                    MediaCmdRequested?.Invoke("seek", (long)(r2 * totalDur));
                }));
            }

            // 底部控制按鈕列：上一首 | 播放/暫停 | 下一首
            float ctrlY = seekY + 18 * s;
            float cx = card.X + card.Width / 2f;
            float mainBtnSz = 44 * s;
            float sideBtnSz = 36 * s;
            float btnGap = 24 * s;

            var prevR = new RectangleF(cx - mainBtnSz / 2 - btnGap - sideBtnSz, ctrlY + (mainBtnSz - sideBtnSz) / 2, sideBtnSz, sideBtnSz);
            var playR = new RectangleF(cx - mainBtnSz / 2, ctrlY, mainBtnSz, mainBtnSz);
            var nextR = new RectangleF(cx + mainBtnSz / 2 + btnGap, ctrlY + (mainBtnSz - sideBtnSz) / 2, sideBtnSz, sideBtnSz);

            Btn(g, prevR, (gg, r) => LineIcons.Prev(gg, r, Md3.OnSurface), () => MediaCmdRequested?.Invoke("prev", 0));

            // 圓形主播放/暫停按鈕
            int playIdx = _hits.Count;
            bool playHover = playIdx == _hoverIdx;
            var playDrawR = playHover ? RectangleF.Inflate(playR, 2 * s, 2 * s) : playR;
            using (var grad = new LinearGradientBrush(playDrawR, Md3.Primary, Md3.Accent, 45f))
                g.FillEllipse(grad, playDrawR);
            var playInner = RectangleF.Inflate(playDrawR, -10 * s, -10 * s);
            if (media.Playing) LineIcons.Pause(g, playInner, Md3.OnPrimary);
            else LineIcons.Play(g, playInner, Md3.OnPrimary);
            _hits.Add((playR, () => MediaCmdRequested?.Invoke("toggle", 0)));

            Btn(g, nextR, (gg, r) => LineIcons.Next(gg, r, Md3.OnSurface), () => MediaCmdRequested?.Invoke("next", 0));
        }

        /// <summary>繪製 GDI+ 動態音律頻譜柱。</summary>
        private void DrawSpectrum(Graphics g, RectangleF r, bool playing)
        {
            int n = _specHeights.Length;
            float gap = 2.5f * S;
            float bw = (r.Width - gap * (n - 1)) / n;
            using var grad = new LinearGradientBrush(r, Md3.Accent, Md3.Primary, LinearGradientMode.Vertical);
            using var idleBr = new SolidBrush(Color.FromArgb(70, Md3.OnVariant));
            for (int i = 0; i < n; i++)
            {
                float hRatio = _specHeights[i] <= 0 ? 0.15f : _specHeights[i];
                float bh = Math.Max(4 * S, r.Height * hRatio);
                var barR = new RectangleF(r.X + i * (bw + gap), r.Bottom - bh, bw, bh);
                using var p = Gfx.RoundRect(barR, bw / 2);
                g.FillPath(playing ? grad : idleBr, p);
            }
        }

        /// <summary>繪製 [本機電腦 | 遠端裝置] 分段切換鈕。</summary>
        private void DrawTargetSeg(Graphics g, RectangleF r)
        {
            float rad = r.Height / 2, half = r.Width / 2;
            using var outline = Gfx.RoundRect(r, rad);
            using (var br = new SolidBrush(Md3.Surface)) g.FillPath(br, outline);
            int sel = _s.ControlTarget == "local" ? 0 : 1;
            var selR = new RectangleF(sel == 0 ? r.X : r.X + half, r.Y, half, r.Height);
            var oldClip = g.Clip;
            g.SetClip(outline, CombineMode.Intersect);
            using (var br = new SolidBrush(Md3.PrimaryContainer)) g.FillRectangle(br, selR);
            g.Clip = oldClip;
            using (var pen = new Pen(Md3.Outline, 1.2f * S))
            {
                g.DrawPath(pen, outline);
                g.DrawLine(pen, r.X + half, r.Y + 1, r.X + half, r.Bottom - 1);
            }
            for (int i = 0; i < 2; i++)
            {
                var cell = new RectangleF(r.X + i * half, r.Y, half, r.Height);
                string label = i == 0 ? "本機電腦" : "遠端裝置";
                Text_(g, label, F(12, sel == i), sel == i ? Md3.OnSurface : Md3.OnVariant, cell, StringAlignment.Center);
                string target = i == 0 ? "local" : "peer";
                _hits.Add((cell, () => TargetRequested?.Invoke(target)));
            }
        }

        private static string FormatMs(long ms)
        {
            if (ms <= 0) return "00:00";
            var ts = TimeSpan.FromMilliseconds(ms);
            return $"{(int)ts.TotalMinutes:00}:{ts.Seconds:00}";
        }

        private void Btn(Graphics g, RectangleF r, Action<Graphics, RectangleF> draw, Action click)
        {
            int idx = _hits.Count;
            if (idx == _hoverIdx)
            {
                using var br = new SolidBrush(Color.FromArgb(32, Md3.OnSurface));
                g.FillEllipse(br, r);
            }
            var inner = RectangleF.Inflate(r, -6 * S, -6 * S);
            draw(g, inner);
            _hits.Add((r, click));
        }

        private void DrawDevice(Graphics g, RectangleF r, DeviceInfo d, bool conn, RectangleF clip)
        {
            float s = S;
            using (var path = Gfx.RoundRect(r, 16 * s))
            {
                using (var br = new SolidBrush(Md3.Container)) g.FillPath(br, path);
                using var pen = new Pen(conn ? Md3.Primary : Md3.Outline, (conn ? 2f : 1.2f) * s);
                g.DrawPath(pen, path);
            }
            // 狀態圓點
            using (var dotBr = new SolidBrush(conn ? Md3.Primary : (d.Online ? Color.FromArgb(76, 175, 80) : Md3.Outline)))
                g.FillEllipse(dotBr, r.X + 18 * s, r.Y + 20 * s, 10 * s, 10 * s);

            Text_(g, d.Name, F(17, true), d.Online ? Md3.OnSurface : Md3.OnVariant, new RectangleF(r.X + 36 * s, r.Y + 10 * s, r.Width - 52 * s, 28 * s));
            string sub = (conn ? "已連線 · 多媒體雙向同步中" : d.Online ? "線上 · 點擊立即連線" : "離線") + " · " + d.Platform + " · " + d.Ip;
            Text_(g, sub, F(13), Md3.OnVariant, new RectangleF(r.X + 36 * s, r.Y + 38 * s, r.Width - 52 * s, 22 * s));
            if (conn)
            {
                var b = new RectangleF(r.Right - 136 * s, r.Bottom - 40 * s, 120 * s, 32 * s);
                using (var p2 = Gfx.RoundRect(b, 16 * s))
                {
                    using (var br2 = new SolidBrush(Md3.ContainerHigh)) g.FillPath(br2, p2);
                    using var pen2 = new Pen(Md3.Outline, 1f * s); g.DrawPath(pen2, p2);
                }
                Text_(g, "Disconnect", F(13), Md3.OnSurface, b, StringAlignment.Center);
                _hits.Add((RectangleF.Intersect(b, clip), () => DisconnectRequested?.Invoke()));
            }
            else if (d.Online)
            {
                _hits.Add((RectangleF.Intersect(r, clip), () => ConnectRequested?.Invoke(d)));
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;
            if (_seekBarRect.Contains(e.Location)) _draggingSeek = true;
            for (int i = _hits.Count - 1; i >= 0; i--)
                if (_hits[i].r.Contains(e.Location)) { _hits[i].a(); Invalidate(); return; }
            if (_titleRect.Contains(e.Location)) DragRequested?.Invoke();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _draggingSeek = false;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (_titleRect.Contains(e.Location) && !_hits.Any(h => h.r.Contains(e.Location))) MaxClicked?.Invoke();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (_draggingSeek && e.Button == MouseButtons.Left && _seekBarRect.Width > 0)
            {
                var media = _s.GetActiveMedia();
                if (media.DurationMs > 0)
                {
                    float r = Math.Clamp((e.X - _seekBarRect.X) / _seekBarRect.Width, 0f, 1f);
                    MediaCmdRequested?.Invoke("seek", (long)(r * media.DurationMs));
                    Invalidate();
                    return;
                }
            }
            int idx = -1;
            for (int i = _hits.Count - 1; i >= 0; i--) if (_hits[i].r.Contains(e.Location)) { idx = i; break; }
            if (idx != _hoverIdx) { _hoverIdx = idx; Cursor = idx >= 0 ? Cursors.Hand : Cursors.Default; Invalidate(); }
        }

        protected override void OnMouseLeave(EventArgs e) { base.OnMouseLeave(e); if (_hoverIdx != -1) { _hoverIdx = -1; Invalidate(); } }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            float max = Math.Max(0, _contentH - _viewH);
            _scroll = Math.Max(0, Math.Min(max, _scroll - e.Delta / 120f * 40 * S));
            Invalidate();
        }
    }
}
