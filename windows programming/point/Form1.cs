using System;
using System.Drawing;
using System.Windows.Forms;

namespace point
{
    public partial class Form1 : Form
    {
        // 抓住的 "PaintEvent"：決定目前要畫點、線還是面
        private PaintEventHandler? hold_paint_event = null;

        // 起始位置 / 結束位置
        private Point start = new Point();
        private Point end = new Point();

        // 點、線、面各自的顏色
        private Color dotColor = Color.Black;
        private Color lineColor = Color.Black;
        private Color faceColor = Color.Black;

        // 點的大小（直徑 px）與線的寬度（px）
        private int dotSize = 20;
        private int lineWidth = 10;

        // 判斷滑鼠有沒有按住
        private bool Is_Mouse_Down = false;

        public Form1()
        {
            InitializeComponent();

            // 預設畫點
            hold_paint_event = Draw_Dot;

            // 讓顏色按鈕顯示目前的顏色
            RefreshColorButtons();
        }

        // ===== 繪圖區（避開上方工具列） =====
        private Rectangle GetDrawArea()
        {
            int top = topPanel.Bottom;
            return new Rectangle(0, top, ClientSize.Width, ClientSize.Height - top);
        }

        // ===== Form1_Paint：觸發被抓住的 paint event =====
        private void Form1_Paint(object? sender, PaintEventArgs e)
        {
            // 只允許在工具列下方的繪圖區作畫，避免畫到控制項上
            e.Graphics.SetClip(GetDrawArea());

            if (hold_paint_event != null)
            {
                hold_paint_event(sender, e);
            }
        }

        // ===== 三個繪圖方法（型別是 PaintEventHandler） =====

        // 畫點：在 start 畫一顆圓形
        private void Draw_Dot(object? sender, PaintEventArgs e)
        {
            using (Brush brush = new SolidBrush(dotColor))
            {
                e.Graphics.FillEllipse(brush,
                    start.X - dotSize / 2,
                    start.Y - dotSize / 2,
                    dotSize,
                    dotSize);
            }
        }

        // 畫線：從起點到終點
        private void Draw_Line(object? sender, PaintEventArgs e)
        {
            using (Pen pen = new Pen(lineColor, lineWidth))
            {
                e.Graphics.DrawLine(pen, start, end);
            }
        }

        // 畫面（矩形）：取起點終點圍成的矩形
        private void Draw_Face(object? sender, PaintEventArgs e)
        {
            int x = start.X < end.X ? start.X : end.X;   // 取得左上角的 X 座標
            int y = start.Y < end.Y ? start.Y : end.Y;   // 取得左上角的 Y 座標
            int height = Math.Abs(end.Y - start.Y);
            int width = Math.Abs(end.X - start.X);

            using (Brush brush = new SolidBrush(faceColor))
            {
                e.Graphics.FillRectangle(brush, x, y, width, height);
            }
        }

        // ===== RadioButton 切換繪圖模式 =====
        private void Radio_Checked(object? sender, EventArgs e)
        {
            if (sender is not RadioButton get_radious_button) return;
            string get_text = get_radious_button.Text;

            if (get_radious_button.Checked)
            {
                if (get_text == "點")
                {
                    hold_paint_event = Draw_Dot;
                }
                else if (get_text == "線")
                {
                    hold_paint_event = Draw_Line;
                }
                else if (get_text == "面")
                {
                    hold_paint_event = Draw_Face;
                }

                // 已畫好的內容保留，只切換接下來的繪圖行為
                Invalidate();
            }
        }

        // ===== 滑鼠點下 =====
        private void Form1_MouseDown(object? sender, MouseEventArgs e)
        {
            Is_Mouse_Down = true; // 表示滑鼠按下

            if (hold_paint_event != Draw_Dot)
            {
                start = new Point(e.X, e.Y);
                end = start;
            }
            else
            {
                start = new Point(e.X, e.Y);
                this.Invalidate(); // 觸發 Form1_Paint 事件
            }
        }

        // ===== 滑鼠鬆開 =====
        private void Form1_MouseUp(object? sender, MouseEventArgs e)
        {
            Is_Mouse_Down = false; // 表示滑鼠放開

            if (hold_paint_event != Draw_Dot) // 畫點則忽略拖曳
            {
                end = new Point(e.X, e.Y);
                this.Invalidate(); // 觸發 Form1_Paint 事件
            }
        }

        // ===== 滑鼠移動 =====
        private void Form1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (hold_paint_event == Draw_Line)
            {
                end = new Point(e.X, e.Y);
                if (Is_Mouse_Down) this.Invalidate(); // 重繪
            }
            if (hold_paint_event == Draw_Face)
            {
                end = new Point(e.X, e.Y);
                if (Is_Mouse_Down) this.Invalidate(); // 重繪
            }
        }

        // ===== 顏色變更（ColorDialog） =====
        private void BtnDotColor_Click(object? sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = dotColor;
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    dotColor = dialog.Color;
                    RefreshColorButtons();
                    Invalidate();
                }
            }
        }

        private void BtnLineColor_Click(object? sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = lineColor;
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    lineColor = dialog.Color;
                    RefreshColorButtons();
                    Invalidate();
                }
            }
        }

        private void BtnFaceColor_Click(object? sender, EventArgs e)
        {
            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = faceColor;
                dialog.FullOpen = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    faceColor = dialog.Color;
                    RefreshColorButtons();
                    Invalidate();
                }
            }
        }

        private void RefreshColorButtons()
        {
            btnDotColor.BackColor = dotColor;
            btnLineColor.BackColor = lineColor;
            btnFaceColor.BackColor = faceColor;

            btnDotColor.ForeColor = GetContrastColor(dotColor);
            btnLineColor.ForeColor = GetContrastColor(lineColor);
            btnFaceColor.ForeColor = GetContrastColor(faceColor);
        }

        // 依背景明暗決定文字顏色，深色底用白字、淺色底用黑字
        private static Color GetContrastColor(Color back)
        {
            double luminance = 0.299 * back.R + 0.587 * back.G + 0.114 * back.B;
            return luminance < 140 ? Color.White : Color.Black;
        }

        // ===== 點大小（TextBox） =====
        private void TxtDotSize_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyDotSize();
                e.SuppressKeyPress = true;
            }
        }

        private void TxtDotSize_Leave(object? sender, EventArgs e)
        {
            ApplyDotSize();
        }

        private void TxtDotSize_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // 只允許輸入數字（含退格）
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ApplyDotSize()
        {
            if (int.TryParse(txtDotSize.Text.Trim(), out int value))
            {
                value = Math.Max(1, Math.Min(200, value)); // 限制 1 ~ 200
                dotSize = value;
                txtDotSize.Text = value.ToString();
                Invalidate();
            }
            else
            {
                txtDotSize.Text = dotSize.ToString();
            }
        }

        // ===== 線寬度（TextBox） =====
        private void TxtLineWidth_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyLineWidth();
                e.SuppressKeyPress = true;
            }
        }

        private void TxtLineWidth_Leave(object? sender, EventArgs e)
        {
            ApplyLineWidth();
        }

        private void TxtLineWidth_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // 只允許輸入數字（含退格）
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void ApplyLineWidth()
        {
            if (int.TryParse(txtLineWidth.Text.Trim(), out int value))
            {
                value = Math.Max(1, Math.Min(50, value)); // 限制 1 ~ 50
                lineWidth = value;
                txtLineWidth.Text = value.ToString();
                Invalidate();
            }
            else
            {
                txtLineWidth.Text = lineWidth.ToString();
            }
        }
    }
}