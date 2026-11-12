namespace Timer
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnPause = new System.Windows.Forms.Button();
            this.btnDirection = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSetTime = new System.Windows.Forms.Button();
            this.numH = new System.Windows.Forms.NumericUpDown();
            this.numM = new System.Windows.Forms.NumericUpDown();
            this.numS = new System.Windows.Forms.NumericUpDown();
            this.lblSet = new System.Windows.Forms.Label();
            this.btnSwToggle = new System.Windows.Forms.Button();
            this.btnSwReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numS)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1 (簡報 p.8 屬性: Enabled=True, Interval=1000)
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2 (碼錶, Interval=100, 由碼錶按鈕控制計時)
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 100;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnPause (功能四：暫停)
            // 
            this.btnPause.Location = new System.Drawing.Point(12, 470);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(90, 30);
            this.btnPause.Text = "暫停";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnDirection (功能二：正數&倒數)
            // 
            this.btnDirection.Location = new System.Drawing.Point(108, 470);
            this.btnDirection.Name = "btnDirection";
            this.btnDirection.Size = new System.Drawing.Size(110, 30);
            this.btnDirection.Text = "切換為倒數";
            this.btnDirection.Click += new System.EventHandler(this.btnDirection_Click);
            // 
            // btnReset (功能四：重置)
            // 
            this.btnReset.Location = new System.Drawing.Point(224, 470);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(90, 30);
            this.btnReset.Text = "重置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // lblSet (功能四：設定時間)
            // 
            this.lblSet.AutoSize = true;
            this.lblSet.Location = new System.Drawing.Point(12, 518);
            this.lblSet.Name = "lblSet";
            this.lblSet.Text = "設定時間";
            // 
            // numH
            // 
            this.numH.Location = new System.Drawing.Point(80, 514);
            this.numH.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            this.numH.Name = "numH";
            this.numH.Size = new System.Drawing.Size(45, 25);
            // 
            // numM
            // 
            this.numM.Location = new System.Drawing.Point(135, 514);
            this.numM.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            this.numM.Name = "numM";
            this.numM.Size = new System.Drawing.Size(45, 25);
            // 
            // numS
            // 
            this.numS.Location = new System.Drawing.Point(190, 514);
            this.numS.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            this.numS.Name = "numS";
            this.numS.Size = new System.Drawing.Size(45, 25);
            // 
            // btnSetTime (功能四：設定時間)
            // 
            this.btnSetTime.Location = new System.Drawing.Point(245, 510);
            this.btnSetTime.Name = "btnSetTime";
            this.btnSetTime.Size = new System.Drawing.Size(70, 30);
            this.btnSetTime.Text = "設定";
            this.btnSetTime.Click += new System.EventHandler(this.btnSetTime_Click);
            // 
            // btnSwToggle (功能五：碼錶開始/暫停)
            // 
            this.btnSwToggle.Location = new System.Drawing.Point(340, 470);
            this.btnSwToggle.Name = "btnSwToggle";
            this.btnSwToggle.Size = new System.Drawing.Size(90, 30);
            this.btnSwToggle.Text = "碼錶開始";
            this.btnSwToggle.Click += new System.EventHandler(this.btnSwToggle_Click);
            // 
            // btnSwReset (功能五：碼錶重置)
            // 
            this.btnSwReset.Location = new System.Drawing.Point(436, 470);
            this.btnSwReset.Name = "btnSwReset";
            this.btnSwReset.Size = new System.Drawing.Size(90, 30);
            this.btnSwReset.Text = "碼錶重置";
            this.btnSwReset.Click += new System.EventHandler(this.btnSwReset_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 600);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnDirection);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblSet);
            this.Controls.Add(this.numH);
            this.Controls.Add(this.numM);
            this.Controls.Add(this.numS);
            this.Controls.Add(this.btnSetTime);
            this.Controls.Add(this.btnSwToggle);
            this.Controls.Add(this.btnSwReset);
            this.Name = "Form1";
            this.Text = "指針式時鐘 Project 3";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnDirection;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSetTime;
        private System.Windows.Forms.NumericUpDown numH;
        private System.Windows.Forms.NumericUpDown numM;
        private System.Windows.Forms.NumericUpDown numS;
        private System.Windows.Forms.Label lblSet;
        private System.Windows.Forms.Button btnSwToggle;
        private System.Windows.Forms.Button btnSwReset;
    }
}
