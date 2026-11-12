namespace blockgame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            gamePanel = new Panel();
            topPanel = new Panel();
            lblCheat = new Label();
            lblStatus = new Label();
            lblTimer = new Label();
            chkCheat = new CheckBox();
            numDiffMax = new NumericUpDown();
            lblDiffTo = new Label();
            numDiffMin = new NumericUpDown();
            lblDiff = new Label();
            numMaxButton = new NumericUpDown();
            lblMax = new Label();
            btnStart = new Button();
            topPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numDiffMax).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDiffMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMaxButton).BeginInit();
            SuspendLayout();
            // 
            // gamePanel
            // 
            gamePanel.BackColor = Color.WhiteSmoke;
            gamePanel.Dock = DockStyle.Fill;
            gamePanel.Location = new Point(0, 229);
            gamePanel.Margin = new Padding(5, 5, 5, 5);
            gamePanel.Name = "gamePanel";
            gamePanel.Size = new Size(1543, 880);
            gamePanel.TabIndex = 0;
            gamePanel.Resize += gamePanel_Resize;
            // 
            // topPanel
            // 
            topPanel.Controls.Add(lblCheat);
            topPanel.Controls.Add(lblStatus);
            topPanel.Controls.Add(lblTimer);
            topPanel.Controls.Add(chkCheat);
            topPanel.Controls.Add(numDiffMax);
            topPanel.Controls.Add(lblDiffTo);
            topPanel.Controls.Add(numDiffMin);
            topPanel.Controls.Add(lblDiff);
            topPanel.Controls.Add(numMaxButton);
            topPanel.Controls.Add(lblMax);
            topPanel.Controls.Add(btnStart);
            topPanel.Dock = DockStyle.Top;
            topPanel.Location = new Point(0, 0);
            topPanel.Margin = new Padding(5, 5, 5, 5);
            topPanel.Name = "topPanel";
            topPanel.Padding = new Padding(17, 17, 17, 14);
            topPanel.Size = new Size(1543, 229);
            topPanel.TabIndex = 1;
            // 
            // lblCheat
            // 
            lblCheat.AutoSize = true;
            lblCheat.Font = new Font("Microsoft JhengHei UI", 10F, FontStyle.Italic);
            lblCheat.ForeColor = Color.Crimson;
            lblCheat.Location = new Point(17, 166);
            lblCheat.Margin = new Padding(5, 0, 5, 0);
            lblCheat.Name = "lblCheat";
            lblCheat.Size = new Size(0, 30);
            lblCheat.TabIndex = 0;
            lblCheat.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblStatus
            // 
            lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblStatus.AutoSize = true;
            lblStatus.Font = new Font("Microsoft JhengHei UI", 10F);
            lblStatus.ForeColor = Color.Black;
            lblStatus.Location = new Point(189, 109);
            lblStatus.Margin = new Padding(5, 0, 5, 0);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(109, 30);
            lblStatus.TabIndex = 1;
            lblStatus.Text = "等待開始";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblTimer
            // 
            lblTimer.AutoSize = true;
            lblTimer.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            lblTimer.ForeColor = Color.DarkSlateBlue;
            lblTimer.Location = new Point(17, 104);
            lblTimer.Margin = new Padding(5, 0, 5, 0);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(45, 36);
            lblTimer.TabIndex = 2;
            lblTimer.Text = "—";
            lblTimer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // chkCheat
            // 
            chkCheat.AutoSize = true;
            chkCheat.Font = new Font("Microsoft JhengHei UI", 10F);
            chkCheat.Location = new Point(900, 31);
            chkCheat.Margin = new Padding(5, 5, 5, 5);
            chkCheat.Name = "chkCheat";
            chkCheat.Size = new Size(135, 34);
            chkCheat.TabIndex = 3;
            chkCheat.Text = "作弊顯示";
            chkCheat.CheckedChanged += chkCheat_CheckedChanged;
            // 
            // numDiffMax
            // 
            numDiffMax.Font = new Font("Microsoft JhengHei UI", 10F);
            numDiffMax.Location = new Point(758, 31);
            numDiffMax.Margin = new Padding(5, 5, 5, 5);
            numDiffMax.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numDiffMax.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDiffMax.Name = "numDiffMax";
            numDiffMax.Size = new Size(103, 37);
            numDiffMax.TabIndex = 4;
            numDiffMax.Value = new decimal(new int[] { 90, 0, 0, 0 });
            // 
            // lblDiffTo
            // 
            lblDiffTo.AutoSize = true;
            lblDiffTo.Font = new Font("Microsoft JhengHei UI", 10F);
            lblDiffTo.Location = new Point(717, 31);
            lblDiffTo.Margin = new Padding(5, 0, 5, 0);
            lblDiffTo.Name = "lblDiffTo";
            lblDiffTo.Size = new Size(37, 30);
            lblDiffTo.TabIndex = 5;
            lblDiffTo.Text = "～";
            lblDiffTo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numDiffMin
            // 
            numDiffMin.Font = new Font("Microsoft JhengHei UI", 10F);
            numDiffMin.Location = new Point(610, 31);
            numDiffMin.Margin = new Padding(5, 5, 5, 5);
            numDiffMin.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
            numDiffMin.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numDiffMin.Name = "numDiffMin";
            numDiffMin.Size = new Size(103, 37);
            numDiffMin.TabIndex = 6;
            numDiffMin.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // lblDiff
            // 
            lblDiff.AutoSize = true;
            lblDiff.Font = new Font("Microsoft JhengHei UI", 10F);
            lblDiff.Location = new Point(523, 31);
            lblDiff.Margin = new Padding(5, 0, 5, 0);
            lblDiff.Name = "lblDiff";
            lblDiff.Size = new Size(85, 30);
            lblDiff.TabIndex = 7;
            lblDiff.Text = "色差：";
            lblDiff.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // numMaxButton
            // 
            numMaxButton.Font = new Font("Microsoft JhengHei UI", 10F);
            numMaxButton.Location = new Point(370, 31);
            numMaxButton.Margin = new Padding(5, 5, 5, 5);
            numMaxButton.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numMaxButton.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numMaxButton.Name = "numMaxButton";
            numMaxButton.Size = new Size(123, 37);
            numMaxButton.TabIndex = 8;
            numMaxButton.Value = new decimal(new int[] { 3, 0, 0, 0 });
            // 
            // lblMax
            // 
            lblMax.AutoSize = true;
            lblMax.Font = new Font("Microsoft JhengHei UI", 10F);
            lblMax.Location = new Point(240, 31);
            lblMax.Margin = new Padding(5, 0, 5, 0);
            lblMax.Name = "lblMax";
            lblMax.Size = new Size(133, 30);
            lblMax.TabIndex = 9;
            lblMax.Text = "按鈕上限：";
            lblMax.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Microsoft JhengHei UI", 12F, FontStyle.Bold);
            btnStart.Location = new Point(17, 17);
            btnStart.Margin = new Padding(5, 5, 5, 5);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(189, 66);
            btnStart.TabIndex = 10;
            btnStart.Text = "開始";
            btnStart.Click += btnStart_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(12F, 26F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1543, 1109);
            Controls.Add(gamePanel);
            Controls.Add(topPanel);
            Font = new Font("Microsoft JhengHei UI", 9F);
            Margin = new Padding(5, 5, 5, 5);
            MinimumSize = new Size(1149, 1028);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "顏色魔術 - 方格選顏色";
            Load += Form1_Load;
            Resize += Form1_Resize;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numDiffMax).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDiffMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMaxButton).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel gamePanel;
        private Panel topPanel;
        private Label lblTimer;
        private Label lblStatus;
        private Label lblCheat;
        private Button btnStart;
        private Label lblMax;
        private NumericUpDown numMaxButton;
        private Label lblDiff;
        private NumericUpDown numDiffMin;
        private Label lblDiffTo;
        private NumericUpDown numDiffMax;
        private CheckBox chkCheat;
    }
}