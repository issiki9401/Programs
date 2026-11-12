using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ohmypccc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int size = 10;
        Color set_color = Color.Red;
        Graphics g;
        int i=0;

        // 作業追加
        bool gradientMode = false;           // 右鍵切換顏色漸層模式
        List<Ball> balls = new List<Ball>(); // 記錄每顆泡泡,加減號鍵才能增減球數

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            g = this.CreateGraphics();

            // 右鍵切換成顏色漸層的模式
            if (e.Button == MouseButtons.Right)
            {
                gradientMode = !gradientMode;
                RedrawBalls();
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                g.FillEllipse(new SolidBrush(set_color), e.X, e.Y, size, size);
                balls.Add(new Ball(e.X, e.Y, size, set_color)); // 記住這顆泡泡
            }

            i++;
            label1.Text = i.ToString();
        }

        Color colorRandom()
        {
            Random r = new Random();

            return Color.FromArgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MouseWheel += Form1_MouseWheel;
            label1.Text = "size = " + size.ToString();
        }

        void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                size += 5;
            }
            else
            {
                size -= 5;
            }
            label1.Text = "size = " + size.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            set_color = colorDialog1.Color;
            pictureBox1.BackColor = set_color;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (check.Checked)
            {
                set_color = colorRandom();
                pictureBox1.BackColor = set_color;
            }
        }

        // 減號鍵減少球數, 加號鍵增加球數
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Random r = new Random();
            if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.Add)
            {
                balls.Add(new Ball(r.Next(0, this.ClientSize.Width),
                                   r.Next(0, this.ClientSize.Height),
                                   size, set_color));
                i++;
                label1.Text = i.ToString();
                RedrawBalls();
            }
            else if (e.KeyCode == Keys.OemMinus || e.KeyCode == Keys.Subtract)
            {
                if (balls.Count > 0)
                {
                    balls.RemoveAt(balls.Count - 1);
                    i--;
                    label1.Text = i.ToString();
                    RedrawBalls();
                }
            }
        }

        // 重畫所有泡泡(漸層模式用漸層筆刷)
        private void RedrawBalls()
        {
            g = this.CreateGraphics();
            g.Clear(this.BackColor);
            foreach (Ball b in balls)
            {
                if (gradientMode)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(
                           new Rectangle(b.X, b.Y, b.Size, b.Size), b.Color, Color.White, 45f))
                    {
                        g.FillEllipse(brush, b.X, b.Y, b.Size, b.Size);
                    }
                }
                else
                {
                    g.FillEllipse(new SolidBrush(b.Color), b.X, b.Y, b.Size, b.Size);
                }
            }
        }

        // 自訂類別:記住一顆泡泡的位置、大小、顏色
        class Ball
        {
            public int X, Y, Size;
            public Color Color;
            public Ball(int x, int y, int s, Color c)
            {
                X = x; Y = y; Size = s; Color = c;
            }
        }
    }
}
