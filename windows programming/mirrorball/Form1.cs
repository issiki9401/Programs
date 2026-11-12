using System.Windows.Forms;

namespace mirrorball
{
    public partial class Form1 : Form
    {
        Graphics g;
        int number = 1;
        Color color = Color.Red;
        int size = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 800;
            this.Height = 600;
            MessageBox.Show("fromWidth=" + this.Width
                + "\nfromHeight=" + this.Height).ToString();
            this.KeyPreview = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Point center = new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2);
            g = this.CreateGraphics();
            g.Clear(Color.White);
            int x = e.X - center.X;
            int y = e.Y - center.Y;
            for (int i = 0; i < number; i++)
            {
                double temp = (2 * Math.PI / 360) * i * (360 / number);

                int new_x = (int)(Math.Cos(temp) * x - Math.Sin(temp) * y);
                int new_y = (int)(Math.Sin(temp) * x + Math.Cos(temp) * y);

                new_x += center.X;
                new_y += center.Y;

                g.FillEllipse(
                    new SolidBrush(color),
                    new_x - size / 2,
                    new_y - size / 2,
                    size,
                    size
                );
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Oemplus)
            {
                number *= 2;
            }
            if (e.KeyData == Keys.OemMinus)
            {
                number /= 2;

                if (number < 1)
                {
                    number = 1;
                }
            }
            if (e.KeyData == Keys.C)
            {
                colorDialog1.ShowDialog();
                color = colorDialog1.Color;
            }
            if (e.KeyData == Keys.Up)
            {
                size += 5;
            }
            if (e.KeyData == Keys.Down)
            {
                size -= 5;

                if (size < 5)
                {
                    size = 5;
                }
            }
            label1.Text = "number = " + number.ToString();
        }
    }
}
