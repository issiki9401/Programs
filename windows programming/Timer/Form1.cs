namespace Timer
{
    public partial class Form1 : Form
    {
        // 初始化程式及變數宣告
        float x1 = 460.0F, y1 = 120.0F;   // 主時鐘中心點
        float x2, y2;
        const float pi = 3.141592654F;
        int second = DateTime.Now.Second;
        int minute = DateTime.Now.Minute;
        int hour = DateTime.Now.Hour;

        // 功能二：正數/倒數
        bool countUp = true;

        // 功能五：碼錶 (以 0.1 秒計數)
        int swTenths = 0;
        bool swRunning = false;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
        }

        // Form_Shown 程式
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(600, 600);
            paint();
        }

        // paint() 函式
        void paint()
        {
            using (Bitmap buffer = new Bitmap(Math.Max(1, this.ClientSize.Width), Math.Max(1, this.ClientSize.Height)))
            {
                using (Graphics gb = Graphics.FromImage(buffer))
                {
                    DrawAll(gb);
                }
                Graphics g = this.CreateGraphics();
                g.DrawImage(buffer, 0, 0);
                g.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Bitmap buffer = new Bitmap(Math.Max(1, this.ClientSize.Width), Math.Max(1, this.ClientSize.Height)))
            {
                using (Graphics gb = Graphics.FromImage(buffer))
                {
                    DrawAll(gb);
                }
                e.Graphics.DrawImage(buffer, 0, 0);
            }
        }

        void DrawAll(Graphics g)
        {
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Font titleFont = new Font("Times New Roman", 12, FontStyle.Bold);
            g.DrawString("時鐘", titleFont, new SolidBrush(Color.Black), new PointF(x1 - 16, 25));
            g.DrawString("碼錶", titleFont, new SolidBrush(Color.Black), new PointF(290 - 16, 25));

            // 主時鐘
            DrawClock(g, x1, y1, hour, minute, (float)second, true);
            g.DrawString(string.Format("{0:D2}:{1:D2}:{2:D2}", hour % 24, minute, second),
                new Font("Times New Roman", 12, FontStyle.Bold), new SolidBrush(Color.Black),
                new PointF(x1 - 30, y1 + 80));

            // 功能五：碼錶時鐘
            float swSec = (swTenths % 600) / 10F;
            int swMin = (swTenths / 600) % 60;
            int swHour = swTenths / 36000;
            DrawClock(g, 290F, 120F, swHour, swMin, swSec, true);
            g.DrawString(string.Format("{0:D2}:{1:D2}.{2}", swMin, (swTenths / 10) % 60, swTenths % 10),
                new Font("Times New Roman", 12, FontStyle.Bold), new SolidBrush(Color.Black),
                new PointF(290 - 30, 120 + 80));
        }

        // 畫一個時鐘: cx,cy 為中心; hh/mm/ss 為時/分/秒(秒含小數)
        void DrawClock(Graphics g, float cx, float cy, int hh, int mm, float ss, bool withNumbers)
        {
            // 錶面
            g.DrawEllipse(new Pen(Color.Black), cx - 70, cy - 70, 140, 140);

            // 功能一-4：畫出時間刻度 (60 格, 每 5 格加長加粗)
            for (int i = 0; i < 60; i++)
            {
                float tickRadian = (float)i * pi / 180F;
                float outer = 70F;
                float inner = (i % 5 == 0) ? 58F : 64F;
                float ox = cx + outer * (float)Math.Sin(6 * tickRadian);
                float oy = cy - outer * (float)Math.Cos(6 * tickRadian);
                float ix = cx + inner * (float)Math.Sin(6 * tickRadian);
                float iy = cy - inner * (float)Math.Cos(6 * tickRadian);
                g.DrawLine(new Pen(Color.Black, (i % 5 == 0) ? 2F : 1F), ix, iy, ox, oy);
            }

            // 秒針起點公式 (弧度換算)
            float secRadian = ss * pi / 180F;                                  // second
            float minRadian = (float)(mm + ss / 60.0) * pi / 180F;             // minute
            float hourRadian = (float)(hh % 12 + mm / 60.0) * pi / 180F;       // hour

            // 功能一-1：時針 寬度 10 pixel
            x2 = cx + 35 * (float)Math.Sin(30 * hourRadian);
            y2 = cy - 35 * (float)Math.Cos(30 * hourRadian);
            g.DrawLine(new Pen(Color.Black, 10F), cx, cy, x2, y2);
            DrawArrow(g, cx, cy, x2, y2, Color.Black);

            // 功能一-2：分針 寬度 5 pixel
            x2 = cx + 50 * (float)Math.Sin(6 * minRadian);
            y2 = cy - 50 * (float)Math.Cos(6 * minRadian);
            g.DrawLine(new Pen(Color.Blue, 5F), cx, cy, x2, y2);
            DrawArrow(g, cx, cy, x2, y2, Color.Blue);

            // 功能一-3：秒針 寬度 1 pixel (簡報 p.6/p.7 原始公式)
            x2 = cx + 60 * (float)Math.Sin(6 * secRadian);   // second
            y2 = cy - 60 * (float)Math.Cos(6 * secRadian);   // second
            g.DrawLine(new Pen(Color.Red, 1F), cx, cy, x2, y2);
            DrawArrow(g, cx, cy, x2, y2, Color.Red);

            // 中心點
            g.FillEllipse(new SolidBrush(Color.Black), cx - 5, cy - 5, 10, 10);

            if (withNumbers)
            {
                // 數字標示 (Times New Roman 10, Bold|Italic, 綠色)
                Font numFont = new Font("Times New Roman", 10, FontStyle.Bold | FontStyle.Italic);
                SolidBrush green = new SolidBrush(Color.Green);
                g.DrawString("12", numFont, green, new PointF(cx - 8, cy - 68));
                g.DrawString("6", numFont, green, new PointF(cx - 4, cy + 56));
                g.DrawString("3", numFont, green, new PointF(cx + 52, cy - 8));
                g.DrawString("9", numFont, green, new PointF(cx - 64, cy - 8));
            }
        }

        // 功能三：畫指針上的箭頭
        void DrawArrow(Graphics g, float cx, float cy, float tipX, float tipY, Color color)
        {
            float dx = tipX - cx;
            float dy = tipY - cy;
            float len = (float)Math.Sqrt(dx * dx + dy * dy);
            if (len < 1F) return;
            dx /= len;
            dy /= len;
            float px = -dy, py = dx;
            PointF[] arrow = new PointF[]
            {
                new PointF(tipX, tipY),
                new PointF(tipX - dx * 10F + px * 4F, tipY - dy * 10F + py * 4F),
                new PointF(tipX - dx * 10F - px * 4F, tipY - dy * 10F - py * 4F)
            };
            g.FillPolygon(new SolidBrush(color), arrow);
        }

        // timer1_Tick (Interval=1000)
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (countUp)
            {
                // 正數 (0~59 循環)
                second++;
                if (second % 60 == 0)
                {
                    second = 0;
                    minute++;
                    if (minute % 60 == 0)
                    {
                        minute = 0;
                        hour = (hour + 1) % 24;
                    }
                }
            }
            else
            {
                // 功能二：倒數
                second--;
                if (second < 0)
                {
                    second = 59;
                    minute--;
                    if (minute < 0)
                    {
                        minute = 59;
                        hour--;
                        if (hour < 0) hour = 23;
                    }
                }
            }
            paint();
        }

        // 碼錶計時 (timer2, Interval=100)
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (swRunning)
            {
                swTenths = (swTenths + 1) % 360000;
                paint();
            }
        }

        // 功能四：暫停 / 繼續
        private void btnPause_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
            btnPause.Text = timer1.Enabled ? "暫停" : "繼續";
        }

        // 功能二：正數 / 倒數切換
        private void btnDirection_Click(object sender, EventArgs e)
        {
            countUp = !countUp;
            btnDirection.Text = countUp ? "切換為倒數" : "切換為正數";
        }

        // 功能四：重置 (回復系統時間)
        private void btnReset_Click(object sender, EventArgs e)
        {
            second = DateTime.Now.Second;
            minute = DateTime.Now.Minute;
            hour = DateTime.Now.Hour;
            paint();
        }

        // 功能四：設定時間
        private void btnSetTime_Click(object sender, EventArgs e)
        {
            hour = (int)numH.Value;
            minute = (int)numM.Value;
            second = (int)numS.Value;
            paint();
        }

        // 功能五：碼錶 開始 / 暫停
        private void btnSwToggle_Click(object sender, EventArgs e)
        {
            swRunning = !swRunning;
            btnSwToggle.Text = swRunning ? "碼錶暫停" : "碼錶開始";
        }

        // 功能五：碼錶 重置
        private void btnSwReset_Click(object sender, EventArgs e)
        {
            swRunning = false;
            swTenths = 0;
            btnSwToggle.Text = "碼錶開始";
            paint();
        }
    }
}
