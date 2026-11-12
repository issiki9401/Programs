using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Midterm
{
    /// <summary>Material Design 3 主題色票（支援深色與淺色主題動態切換）。</summary>
    internal static class Md3
    {
        public static bool Dark = true;
        private static readonly Color DSurface = Color.FromArgb(11, 14, 17);
        private static readonly Color DContainer = Color.FromArgb(21, 24, 28);
        private static readonly Color DContainerHigh = Color.FromArgb(30, 35, 42);
        private static readonly Color DOutline = Color.FromArgb(68, 72, 77);
        private static readonly Color DOnSurface = Color.FromArgb(230, 232, 236);
        private static readonly Color DOnVariant = Color.FromArgb(185, 190, 202);
        private static readonly Color DPrimary = Color.FromArgb(168, 199, 250);
        private static readonly Color DAccent = Color.FromArgb(192, 132, 252);
        private static readonly Color DOnPrimary = Color.FromArgb(6, 46, 111);
        private static readonly Color DPrimaryContainer = Color.FromArgb(18, 76, 172);

        private static readonly Color LSurface = Color.FromArgb(245, 247, 250);
        private static readonly Color LContainer = Color.FromArgb(255, 255, 255);
        private static readonly Color LContainerHigh = Color.FromArgb(236, 240, 246);
        private static readonly Color LOutline = Color.FromArgb(196, 199, 206);
        private static readonly Color LOnSurface = Color.FromArgb(24, 26, 30);
        private static readonly Color LOnVariant = Color.FromArgb(68, 72, 80);
        private static readonly Color LPrimary = Color.FromArgb(11, 87, 208);
        private static readonly Color LAccent = Color.FromArgb(124, 58, 237);
        private static readonly Color LOnPrimary = Color.FromArgb(255, 255, 255);
        private static readonly Color LPrimaryContainer = Color.FromArgb(211, 227, 253);

        public static Color Surface => Dark ? DSurface : LSurface;
        public static Color Container => Dark ? DContainer : LContainer;
        public static Color ContainerHigh => Dark ? DContainerHigh : LContainerHigh;
        public static Color Outline => Dark ? DOutline : LOutline;
        public static Color OnSurface => Dark ? DOnSurface : LOnSurface;
        public static Color OnVariant => Dark ? DOnVariant : LOnVariant;
        public static Color Primary => Dark ? DPrimary : LPrimary;
        public static Color Accent => Dark ? DAccent : LAccent;
        public static Color OnPrimary => Dark ? DOnPrimary : LOnPrimary;
        public static Color PrimaryContainer => Dark ? DPrimaryContainer : LPrimaryContainer;
        public static Color Error => Dark ? Color.FromArgb(242, 184, 181) : Color.FromArgb(179, 38, 30);
        public static Color ErrorContainer => Dark ? Color.FromArgb(140, 29, 24) : Color.FromArgb(249, 222, 220);
    }

    /// <summary>
    /// GDI+ 幾何向量圖標繪製工具：
    /// 運用 Pen、SolidBrush、Polygon 與 Arc 等課堂繪圖方法，不依賴外部圖片檔即可無損縮放。
    /// </summary>
    internal static class LineIcons
    {
        private static Pen P(Color c, float w) => new Pen(c, w) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round };

        public static void Door(Graphics g, RectangleF r, Color c, float w)
        {
            using var pen = P(c, w);
            float x = r.X + r.Width * 0.16f, y = r.Y + r.Height * 0.10f;
            float ww = r.Width * 0.40f, hh = r.Height * 0.80f;
            g.DrawRectangle(pen, x, y, ww, hh);
            float ay = y + hh / 2, ax0 = x + ww * 0.5f, ax1 = r.Right - r.Width * 0.10f;
            g.DrawLine(pen, ax0, ay, ax1, ay);
            g.DrawLine(pen, ax1 - r.Width * 0.16f, ay - r.Height * 0.14f, ax1, ay);
            g.DrawLine(pen, ax1 - r.Width * 0.16f, ay + r.Height * 0.14f, ax1, ay);
        }

        public static void Chevron(Graphics g, RectangleF r, Color c, float w)
        {
            using var pen = P(c, w);
            g.DrawLines(pen, new[] {
                new PointF(r.X + r.Width * 0.22f, r.Y + r.Height * 0.36f),
                new PointF(r.X + r.Width * 0.50f, r.Y + r.Height * 0.68f),
                new PointF(r.X + r.Width * 0.78f, r.Y + r.Height * 0.36f) });
        }

        public static void Minimize(Graphics g, RectangleF r, Color c, float w)
        {
            using var pen = P(c, w);
            g.DrawLine(pen, r.X + r.Width * 0.2f, r.Y + r.Height / 2, r.X + r.Width * 0.8f, r.Y + r.Height / 2);
        }

        public static void Maximize(Graphics g, RectangleF r, Color c, bool maximized, float w)
        {
            using var pen = P(c, w);
            if (!maximized)
            {
                g.DrawRectangle(pen, r.X + r.Width * 0.2f, r.Y + r.Height * 0.2f, r.Width * 0.6f, r.Height * 0.6f);
            }
            else
            {
                float o = r.Width * 0.14f;
                g.DrawRectangle(pen, r.X + r.Width * 0.2f, r.Y + r.Height * 0.3f, r.Width * 0.5f, r.Height * 0.5f);
                g.DrawLines(pen, new[] {
                    new PointF(r.X + r.Width * 0.3f + o, r.Y + r.Height * 0.3f),
                    new PointF(r.X + r.Width * 0.3f + o, r.Y + r.Height * 0.2f),
                    new PointF(r.X + r.Width * 0.8f, r.Y + r.Height * 0.2f),
                    new PointF(r.X + r.Width * 0.8f, r.Y + r.Height * 0.7f),
                    new PointF(r.X + r.Width * 0.7f, r.Y + r.Height * 0.7f) });
            }
        }

        public static void Close(Graphics g, RectangleF r, Color c, float w)
        {
            using var pen = P(c, w);
            float m = r.Width * 0.26f;
            g.DrawLine(pen, r.X + m, r.Y + m, r.Right - m, r.Bottom - m);
            g.DrawLine(pen, r.Right - m, r.Y + m, r.X + m, r.Bottom - m);
        }

        /// <summary>繪製播放三角形圖示（FillPolygon）。</summary>
        public static void Play(Graphics g, RectangleF r, Color c)
        {
            using var br = new SolidBrush(c);
            var pts = new[]
            {
                new PointF(r.X + r.Width * 0.30f, r.Y + r.Height * 0.20f),
                new PointF(r.X + r.Width * 0.30f, r.Y + r.Height * 0.80f),
                new PointF(r.X + r.Width * 0.78f, r.Y + r.Height * 0.50f)
            };
            g.FillPolygon(br, pts);
        }

        /// <summary>繪製暫停雙豎線圖示（FillRectangle）。</summary>
        public static void Pause(Graphics g, RectangleF r, Color c)
        {
            using var br = new SolidBrush(c);
            float bw = r.Width * 0.18f;
            float h = r.Height * 0.58f;
            float y = r.Y + (r.Height - h) / 2f;
            g.FillRectangle(br, r.X + r.Width * 0.26f, y, bw, h);
            g.FillRectangle(br, r.Right - r.Width * 0.26f - bw, y, bw, h);
        }

        /// <summary>繪製上一首圖示（豎線 + 向左三角形）。</summary>
        public static void Prev(Graphics g, RectangleF r, Color c)
        {
            using var br = new SolidBrush(c);
            float y0 = r.Y + r.Height * 0.24f, y1 = r.Y + r.Height * 0.76f;
            g.FillRectangle(br, r.X + r.Width * 0.20f, y0, r.Width * 0.10f, y1 - y0);
            var pts = new[]
            {
                new PointF(r.X + r.Width * 0.32f, r.Y + r.Height * 0.50f),
                new PointF(r.X + r.Width * 0.78f, y0),
                new PointF(r.X + r.Width * 0.78f, y1)
            };
            g.FillPolygon(br, pts);
        }

        /// <summary>繪製下一首圖示（向右三角形 + 豎線）。</summary>
        public static void Next(Graphics g, RectangleF r, Color c)
        {
            using var br = new SolidBrush(c);
            float y0 = r.Y + r.Height * 0.24f, y1 = r.Y + r.Height * 0.76f;
            var pts = new[]
            {
                new PointF(r.X + r.Width * 0.68f, r.Y + r.Height * 0.50f),
                new PointF(r.X + r.Width * 0.22f, y0),
                new PointF(r.X + r.Width * 0.22f, y1)
            };
            g.FillPolygon(br, pts);
            g.FillRectangle(br, r.X + r.Width * 0.70f, y0, r.Width * 0.10f, y1 - y0);
        }

        /// <summary>繪製動態黑膠唱片（當無外部封面或疊加旋轉光澤時使用）。</summary>
        public static void VinylDisc(Graphics g, RectangleF r, float angleDeg)
        {
            using (var lg = new LinearGradientBrush(r, Color.FromArgb(38, 44, 56), Color.FromArgb(16, 19, 26), angleDeg))
                g.FillEllipse(lg, r);
            using (var pen = new Pen(Color.FromArgb(60, 168, 199, 250), 1.5f))
            {
                var r1 = RectangleF.Inflate(r, -r.Width * 0.16f, -r.Height * 0.16f);
                var r2 = RectangleF.Inflate(r, -r.Width * 0.28f, -r.Height * 0.28f);
                g.DrawArc(pen, r1, angleDeg, 110);
                g.DrawArc(pen, r1, angleDeg + 180, 110);
                g.DrawArc(pen, r2, angleDeg + 45, 90);
                g.DrawArc(pen, r2, angleDeg + 225, 90);
            }
            var center = RectangleF.Inflate(r, -r.Width * 0.34f, -r.Height * 0.34f);
            using (var cBr = new LinearGradientBrush(center, Md3.Primary, Md3.Accent, angleDeg))
                g.FillEllipse(cBr, center);
            var hole = RectangleF.Inflate(r, -r.Width * 0.44f, -r.Height * 0.44f);
            using (var hBr = new SolidBrush(Md3.Container))
                g.FillEllipse(hBr, hole);
        }
    }

    internal static class Gfx
    {
        public static GraphicsPath RoundRect(RectangleF r, float rad)
        {
            var p = new GraphicsPath();
            if (r.Width <= 0 || r.Height <= 0) return p;
            float d = Math.Min(rad * 2, Math.Min(r.Width, r.Height));
            if (d <= 0) { p.AddRectangle(r); return p; }
            p.AddArc(r.X, r.Y, d, d, 180, 90);
            p.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            p.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            p.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            p.CloseFigure();
            return p;
        }
    }
}
