namespace musicgame
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
            this.Do = new System.Windows.Forms.Button();
            this.Re = new System.Windows.Forms.Button();
            this.Mi = new System.Windows.Forms.Button();
            this.Fa = new System.Windows.Forms.Button();
            this.So = new System.Windows.Forms.Button();
            this.La = new System.Windows.Forms.Button();
            this.Si = new System.Windows.Forms.Button();
            this.cmbSong = new System.Windows.Forms.ComboBox();
            this.btnAutoPlay = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnReplay = new System.Windows.Forms.Button();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnAddMp3 = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Do
            // 
            this.Do.Location = new System.Drawing.Point(240, 390);
            this.Do.Name = "Do";
            this.Do.Size = new System.Drawing.Size(80, 60);
            this.Do.TabIndex = 0;
            this.Do.Text = "Do(1)";
            this.Do.UseVisualStyleBackColor = true;
            this.Do.Click += new System.EventHandler(this.Do_Click);
            // 
            // Re
            // 
            this.Re.Location = new System.Drawing.Point(320, 390);
            this.Re.Name = "Re";
            this.Re.Size = new System.Drawing.Size(80, 60);
            this.Re.TabIndex = 1;
            this.Re.Text = "Re(2)";
            this.Re.UseVisualStyleBackColor = true;
            this.Re.Click += new System.EventHandler(this.Re_Click);
            // 
            // Mi
            // 
            this.Mi.Location = new System.Drawing.Point(400, 390);
            this.Mi.Name = "Mi";
            this.Mi.Size = new System.Drawing.Size(80, 60);
            this.Mi.TabIndex = 2;
            this.Mi.Text = "Mi(3)";
            this.Mi.UseVisualStyleBackColor = true;
            this.Mi.Click += new System.EventHandler(this.Mi_Click);
            // 
            // Fa
            // 
            this.Fa.Location = new System.Drawing.Point(480, 390);
            this.Fa.Name = "Fa";
            this.Fa.Size = new System.Drawing.Size(80, 60);
            this.Fa.TabIndex = 3;
            this.Fa.Text = "Fa(4)";
            this.Fa.UseVisualStyleBackColor = true;
            this.Fa.Click += new System.EventHandler(this.Fa_Click);
            // 
            // So
            // 
            this.So.Location = new System.Drawing.Point(560, 390);
            this.So.Name = "So";
            this.So.Size = new System.Drawing.Size(80, 60);
            this.So.TabIndex = 4;
            this.So.Text = "So(5)";
            this.So.UseVisualStyleBackColor = true;
            this.So.Click += new System.EventHandler(this.So_Click);
            // 
            // La
            // 
            this.La.Location = new System.Drawing.Point(640, 390);
            this.La.Name = "La";
            this.La.Size = new System.Drawing.Size(80, 60);
            this.La.TabIndex = 5;
            this.La.Text = "La(6)";
            this.La.UseVisualStyleBackColor = true;
            this.La.Click += new System.EventHandler(this.La_Click);
            // 
            // Si
            // 
            this.Si.Location = new System.Drawing.Point(720, 390);
            this.Si.Name = "Si";
            this.Si.Size = new System.Drawing.Size(80, 60);
            this.Si.TabIndex = 6;
            this.Si.Text = "Si(7)";
            this.Si.UseVisualStyleBackColor = true;
            this.Si.Click += new System.EventHandler(this.Si_Click);
            // 
            // cmbSong (作業5: 用 ComboBox 切換歌曲)
            // 
            this.cmbSong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSong.Location = new System.Drawing.Point(20, 40);
            this.cmbSong.Name = "cmbSong";
            this.cmbSong.Size = new System.Drawing.Size(200, 28);
            this.cmbSong.TabIndex = 7;
            this.cmbSong.SelectedIndexChanged += new System.EventHandler(this.cmbSong_SelectedIndexChanged);
            // 
            // btnAutoPlay (作業3: 結合 Timer 自動播放)
            // 
            this.btnAutoPlay.Location = new System.Drawing.Point(20, 90);
            this.btnAutoPlay.Name = "btnAutoPlay";
            this.btnAutoPlay.Size = new System.Drawing.Size(200, 40);
            this.btnAutoPlay.TabIndex = 8;
            this.btnAutoPlay.Text = "自動播放";
            this.btnAutoPlay.UseVisualStyleBackColor = true;
            this.btnAutoPlay.Click += new System.EventHandler(this.btnAutoPlay_Click);
            // 
            // btnStop (作業4: 播放完可停止/繼續使用)
            // 
            this.btnStop.Location = new System.Drawing.Point(20, 140);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(200, 40);
            this.btnStop.TabIndex = 9;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnReplay (作業4: 重新播放)
            // 
            this.btnReplay.Location = new System.Drawing.Point(20, 190);
            this.btnReplay.Name = "btnReplay";
            this.btnReplay.Size = new System.Drawing.Size(200, 40);
            this.btnReplay.TabIndex = 10;
            this.btnReplay.Text = "重新播放";
            this.btnReplay.UseVisualStyleBackColor = true;
            this.btnReplay.Click += new System.EventHandler(this.btnReplay_Click);
            // 
            // btnColor (作業6: 更改鋼琴塊顏色)
            // 
            this.btnColor.Location = new System.Drawing.Point(20, 240);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(200, 40);
            this.btnColor.TabIndex = 11;
            this.btnColor.Text = "更改鋼琴塊顏色";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnAddMp3 (附加功能: 新增 mp3 等音檔歌曲)
            // 
            this.btnAddMp3.Location = new System.Drawing.Point(20, 290);
            this.btnAddMp3.Name = "btnAddMp3";
            this.btnAddMp3.Size = new System.Drawing.Size(200, 40);
            this.btnAddMp3.TabIndex = 12;
            this.btnAddMp3.Text = "＋新增 mp3 音檔歌曲";
            this.btnAddMp3.UseVisualStyleBackColor = true;
            this.btnAddMp3.Click += new System.EventHandler(this.btnAddMp3_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(20, 345);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(200, 105);
            this.lblStatus.TabIndex = 13;
            this.lblStatus.Text = "就緒";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 660);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAddMp3);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.btnReplay);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnAutoPlay);
            this.Controls.Add(this.cmbSong);
            this.Controls.Add(this.Si);
            this.Controls.Add(this.La);
            this.Controls.Add(this.So);
            this.Controls.Add(this.Fa);
            this.Controls.Add(this.Mi);
            this.Controls.Add(this.Re);
            this.Controls.Add(this.Do);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "音樂樂譜播放器 - 別踩白塊兒";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button Do;
        private System.Windows.Forms.Button Re;
        private System.Windows.Forms.Button Mi;
        private System.Windows.Forms.Button Fa;
        private System.Windows.Forms.Button So;
        private System.Windows.Forms.Button La;
        private System.Windows.Forms.Button Si;
        private System.Windows.Forms.ComboBox cmbSong;
        private System.Windows.Forms.Button btnAutoPlay;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnReplay;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnAddMp3;
        private System.Windows.Forms.Label lblStatus;
    }
}
