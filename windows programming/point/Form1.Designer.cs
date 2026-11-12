namespace point
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.Panel bottomLine;
        private System.Windows.Forms.FlowLayoutPanel flowLayout;
        private System.Windows.Forms.RadioButton radioDot;
        private System.Windows.Forms.RadioButton radioLine;
        private System.Windows.Forms.RadioButton radioFace;
        private System.Windows.Forms.Button btnDotColor;
        private System.Windows.Forms.Button btnLineColor;
        private System.Windows.Forms.Button btnFaceColor;
        private System.Windows.Forms.Label labelDotSize;
        private System.Windows.Forms.TextBox txtDotSize;
        private System.Windows.Forms.Label labelLineWidth;
        private System.Windows.Forms.TextBox txtLineWidth;
        private System.Windows.Forms.Label labelHint;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.MinimumSize = new System.Drawing.Size(760, 480);
            this.Text = "點線面";
            this.BackColor = System.Drawing.Color.White;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // ===== 上方工具列 =====
            this.topPanel = new System.Windows.Forms.Panel();
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 92;
            this.topPanel.BackColor = System.Drawing.Color.WhiteSmoke;

            // 工具列底部分隔線
            this.bottomLine = new System.Windows.Forms.Panel();
            this.bottomLine.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomLine.Height = 2;
            this.bottomLine.BackColor = System.Drawing.Color.Gainsboro;

            // 自動排版的工具列容器（視窗縮小時自動換行，避免擠在一起）
            this.flowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayout.Padding = new System.Windows.Forms.Padding(12, 12, 12, 0);
            this.flowLayout.WrapContents = true;

            // 繪圖模式 RadioButton
            this.radioDot = new System.Windows.Forms.RadioButton();
            this.radioDot.Text = "點";
            this.radioDot.Margin = new System.Windows.Forms.Padding(0, 7, 2, 0);
            this.radioDot.Checked = true;

            this.radioLine = new System.Windows.Forms.RadioButton();
            this.radioLine.Text = "線";
            this.radioLine.Margin = new System.Windows.Forms.Padding(4, 7, 2, 0);

            this.radioFace = new System.Windows.Forms.RadioButton();
            this.radioFace.Text = "面";
            this.radioFace.Margin = new System.Windows.Forms.Padding(4, 7, 6, 0);

            // 顏色按鈕（ColorDialog）
            this.btnDotColor = new System.Windows.Forms.Button();
            this.btnDotColor.Text = "點顏色";
            this.btnDotColor.Size = new System.Drawing.Size(80, 30);
            this.btnDotColor.Margin = new System.Windows.Forms.Padding(6, 3, 4, 0);
            this.btnDotColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.btnLineColor = new System.Windows.Forms.Button();
            this.btnLineColor.Text = "線顏色";
            this.btnLineColor.Size = new System.Drawing.Size(80, 30);
            this.btnLineColor.Margin = new System.Windows.Forms.Padding(4, 3, 4, 0);
            this.btnLineColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            this.btnFaceColor = new System.Windows.Forms.Button();
            this.btnFaceColor.Text = "面顏色";
            this.btnFaceColor.Size = new System.Drawing.Size(80, 30);
            this.btnFaceColor.Margin = new System.Windows.Forms.Padding(4, 3, 8, 0);
            this.btnFaceColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;

            // 點大小
            this.labelDotSize = new System.Windows.Forms.Label();
            this.labelDotSize.Text = "點大小";
            this.labelDotSize.AutoSize = true;
            this.labelDotSize.Margin = new System.Windows.Forms.Padding(10, 10, 4, 0);

            this.txtDotSize = new System.Windows.Forms.TextBox();
            this.txtDotSize.Text = "20";
            this.txtDotSize.Size = new System.Drawing.Size(50, 28);
            this.txtDotSize.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);

            // 線寬度
            this.labelLineWidth = new System.Windows.Forms.Label();
            this.labelLineWidth.Text = "線寬度";
            this.labelLineWidth.AutoSize = true;
            this.labelLineWidth.Margin = new System.Windows.Forms.Padding(10, 10, 4, 0);

            this.txtLineWidth = new System.Windows.Forms.TextBox();
            this.txtLineWidth.Text = "10";
            this.txtLineWidth.Size = new System.Drawing.Size(50, 28);
            this.txtLineWidth.Margin = new System.Windows.Forms.Padding(0, 4, 8, 0);

            // 提示文字
            this.labelHint = new System.Windows.Forms.Label();
            this.labelHint.Text = "在下方拖曳繪圖";
            this.labelHint.AutoSize = true;
            this.labelHint.Margin = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.labelHint.ForeColor = System.Drawing.Color.Gray;

            // ===== 組裝 =====
            this.flowLayout.Controls.Add(this.radioDot);
            this.flowLayout.Controls.Add(this.radioLine);
            this.flowLayout.Controls.Add(this.radioFace);
            this.flowLayout.Controls.Add(this.btnDotColor);
            this.flowLayout.Controls.Add(this.btnLineColor);
            this.flowLayout.Controls.Add(this.btnFaceColor);
            this.flowLayout.Controls.Add(this.labelDotSize);
            this.flowLayout.Controls.Add(this.txtDotSize);
            this.flowLayout.Controls.Add(this.labelLineWidth);
            this.flowLayout.Controls.Add(this.txtLineWidth);
            this.flowLayout.Controls.Add(this.labelHint);

            this.topPanel.Controls.Add(this.flowLayout);
            this.topPanel.Controls.Add(this.bottomLine);

            this.Controls.Add(this.topPanel);

            // ===== 事件綁定 =====
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);

            this.radioDot.CheckedChanged += new System.EventHandler(this.Radio_Checked);
            this.radioLine.CheckedChanged += new System.EventHandler(this.Radio_Checked);
            this.radioFace.CheckedChanged += new System.EventHandler(this.Radio_Checked);

            this.btnDotColor.Click += new System.EventHandler(this.BtnDotColor_Click);
            this.btnLineColor.Click += new System.EventHandler(this.BtnLineColor_Click);
            this.btnFaceColor.Click += new System.EventHandler(this.BtnFaceColor_Click);

            this.txtDotSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtDotSize_KeyDown);
            this.txtDotSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtDotSize_KeyPress);
            this.txtDotSize.Leave += new System.EventHandler(this.TxtDotSize_Leave);

            this.txtLineWidth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtLineWidth_KeyDown);
            this.txtLineWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtLineWidth_KeyPress);
            this.txtLineWidth.Leave += new System.EventHandler(this.TxtLineWidth_Leave);
        }

        #endregion
    }
}