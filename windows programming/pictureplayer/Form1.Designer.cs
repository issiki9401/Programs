#nullable disable
namespace pictureplayer
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // 控件宣告
        private GroupBox imageGroupBox;
        private PictureBox imagePicturebox;
        private Button nextImageBtn;
        private Button previousImageBtn;
        private GroupBox groupBox2;
        private Button loadImage;
        private Button flipHorizontalBtn;
        private Button flipVerticalBtn;
        private Button rotateLeftBtn;
        private Button rotateRightBtn;
        private Button saveBtn;
        private GroupBox draw;
        private Label penColorLabel;
        private PictureBox colorPicker;
        private Label penSizeLabel;
        private NumericUpDown penSizeNumeric;
        private Button clearBtn;
        private GroupBox slideshowGroupBox;
        private CheckBox slideshowCheckBox;
        private Label slideshowIntervalLabel;
        private NumericUpDown slideshowIntervalNumeric;
        private Label slideshowHintLabel;

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
            this.imageGroupBox = new GroupBox();
            this.imagePicturebox = new PictureBox();
            this.previousImageBtn = new Button();
            this.nextImageBtn = new Button();
            this.groupBox2 = new GroupBox();
            this.loadImage = new Button();
            this.flipHorizontalBtn = new Button();
            this.flipVerticalBtn = new Button();
            this.rotateLeftBtn = new Button();
            this.rotateRightBtn = new Button();
            this.saveBtn = new Button();
            this.draw = new GroupBox();
            this.penColorLabel = new Label();
            this.colorPicker = new PictureBox();
            this.penSizeLabel = new Label();
            this.penSizeNumeric = new NumericUpDown();
            this.clearBtn = new Button();
            this.slideshowGroupBox = new GroupBox();
            this.slideshowCheckBox = new CheckBox();
            this.slideshowIntervalLabel = new Label();
            this.slideshowIntervalNumeric = new NumericUpDown();
            this.slideshowHintLabel = new Label();
            this.imageGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imagePicturebox)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.draw.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPicker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.penSizeNumeric)).BeginInit();
            this.slideshowGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slideshowIntervalNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // imageGroupBox
            // 
            this.imageGroupBox.Controls.Add(this.imagePicturebox);
            this.imageGroupBox.Controls.Add(this.previousImageBtn);
            this.imageGroupBox.Controls.Add(this.nextImageBtn);
            this.imageGroupBox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.imageGroupBox.Location = new Point(340, 14);
            this.imageGroupBox.Name = "imageGroupBox";
            this.imageGroupBox.Size = new Size(926, 740);
            this.imageGroupBox.TabIndex = 0;
            this.imageGroupBox.TabStop = false;
            this.imageGroupBox.Text = "imageGroupBox";
            this.imageGroupBox.MouseWheel += new MouseEventHandler(this.ImageGroupBox_MouseWheel);
            // 
            // imagePicturebox
            // 
            this.imagePicturebox.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right)));
            this.imagePicturebox.BorderStyle = BorderStyle.FixedSingle;
            this.imagePicturebox.Location = new Point(24, 24);
            this.imagePicturebox.Name = "imagePicturebox";
            this.imagePicturebox.Size = new Size(878, 620);
            this.imagePicturebox.TabIndex = 0;
            this.imagePicturebox.TabStop = false;
            this.imagePicturebox.MouseDown += new MouseEventHandler(this.imagePictureBox_MouseDown);
            this.imagePicturebox.MouseMove += new MouseEventHandler(this.imagePictureBox_MouseMove);
            this.imagePicturebox.MouseUp += new MouseEventHandler(this.imagePictureBox_MouseUp);
            this.imagePicturebox.Paint += new PaintEventHandler(this.imagePictureBox_Paint);
            // 
            // previousImageBtn
            // 
            this.previousImageBtn.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.previousImageBtn.Location = new Point(48, 660);
            this.previousImageBtn.Name = "previousImageBtn";
            this.previousImageBtn.Size = new Size(180, 52);
            this.previousImageBtn.TabIndex = 1;
            this.previousImageBtn.Text = "previous";
            this.previousImageBtn.UseVisualStyleBackColor = true;
            this.previousImageBtn.Click += new EventHandler(this.previousImageBtn_Click);
            // 
            // nextImageBtn
            // 
            this.nextImageBtn.Anchor = ((AnchorStyles)((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.nextImageBtn.Location = new Point(698, 660);
            this.nextImageBtn.Name = "nextImageBtn";
            this.nextImageBtn.Size = new Size(180, 52);
            this.nextImageBtn.TabIndex = 2;
            this.nextImageBtn.Text = "next";
            this.nextImageBtn.UseVisualStyleBackColor = true;
            this.nextImageBtn.Click += new EventHandler(this.nextImageBtn_Click);
            // 
            // groupBox2 (control)
            // 
            this.groupBox2.Controls.Add(this.loadImage);
            this.groupBox2.Controls.Add(this.flipHorizontalBtn);
            this.groupBox2.Controls.Add(this.flipVerticalBtn);
            this.groupBox2.Controls.Add(this.rotateLeftBtn);
            this.groupBox2.Controls.Add(this.rotateRightBtn);
            this.groupBox2.Controls.Add(this.saveBtn);
            this.groupBox2.Location = new Point(14, 14);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(300, 250);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "control";
            // 
            // loadImage
            // 
            this.loadImage.Location = new Point(16, 28);
            this.loadImage.Name = "loadImage";
            this.loadImage.Size = new Size(268, 46);
            this.loadImage.TabIndex = 0;
            this.loadImage.Text = "Load Image";
            this.loadImage.UseVisualStyleBackColor = true;
            this.loadImage.Click += new EventHandler(this.loadImage_Click);
            // 
            // flipHorizontalBtn
            // 
            this.flipHorizontalBtn.Location = new Point(16, 84);
            this.flipHorizontalBtn.Name = "flipHorizontalBtn";
            this.flipHorizontalBtn.Size = new Size(130, 46);
            this.flipHorizontalBtn.TabIndex = 1;
            this.flipHorizontalBtn.Text = "flip Horizontal";
            this.flipHorizontalBtn.UseVisualStyleBackColor = true;
            this.flipHorizontalBtn.Click += new EventHandler(this.flipHorizontalBtn_Click);
            // 
            // flipVerticalBtn
            // 
            this.flipVerticalBtn.Location = new Point(154, 84);
            this.flipVerticalBtn.Name = "flipVerticalBtn";
            this.flipVerticalBtn.Size = new Size(130, 46);
            this.flipVerticalBtn.TabIndex = 2;
            this.flipVerticalBtn.Text = "flip Vertical";
            this.flipVerticalBtn.UseVisualStyleBackColor = true;
            this.flipVerticalBtn.Click += new EventHandler(this.flipVerticalBtn_Click);
            // 
            // rotateLeftBtn
            // 
            this.rotateLeftBtn.Location = new Point(16, 138);
            this.rotateLeftBtn.Name = "rotateLeftBtn";
            this.rotateLeftBtn.Size = new Size(130, 46);
            this.rotateLeftBtn.TabIndex = 3;
            this.rotateLeftBtn.Text = "rotate Left";
            this.rotateLeftBtn.UseVisualStyleBackColor = true;
            this.rotateLeftBtn.Click += new EventHandler(this.rotateLeftBtn_Click);
            // 
            // rotateRightBtn
            // 
            this.rotateRightBtn.Location = new Point(154, 138);
            this.rotateRightBtn.Name = "rotateRightBtn";
            this.rotateRightBtn.Size = new Size(130, 46);
            this.rotateRightBtn.TabIndex = 4;
            this.rotateRightBtn.Text = "rotate Right";
            this.rotateRightBtn.UseVisualStyleBackColor = true;
            this.rotateRightBtn.Click += new EventHandler(this.rotateRightBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.BackColor = Color.FromArgb(174, 203, 235);
            this.saveBtn.Location = new Point(16, 192);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new Size(268, 46);
            this.saveBtn.TabIndex = 5;
            this.saveBtn.Text = "save";
            this.saveBtn.UseVisualStyleBackColor = false;
            this.saveBtn.Click += new EventHandler(this.saveBtn_Click);
            // 
            // draw
            // 
            this.draw.Controls.Add(this.penColorLabel);
            this.draw.Controls.Add(this.colorPicker);
            this.draw.Controls.Add(this.penSizeLabel);
            this.draw.Controls.Add(this.penSizeNumeric);
            this.draw.Controls.Add(this.clearBtn);
            this.draw.Location = new Point(14, 280);
            this.draw.Name = "draw";
            this.draw.Size = new Size(300, 200);
            this.draw.TabIndex = 2;
            this.draw.TabStop = false;
            this.draw.Text = "draw";
            // 
            // penColorLabel
            // 
            this.penColorLabel.AutoSize = true;
            this.penColorLabel.Location = new Point(18, 48);
            this.penColorLabel.Name = "penColorLabel";
            this.penColorLabel.Size = new Size(90, 21);
            this.penColorLabel.TabIndex = 0;
            this.penColorLabel.Text = "Pen Color";
            // 
            // colorPicker
            // 
            this.colorPicker.BackColor = Color.Black;
            this.colorPicker.Location = new Point(150, 34);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Size = new Size(90, 50);
            this.colorPicker.TabIndex = 1;
            this.colorPicker.TabStop = false;
            this.colorPicker.Click += new EventHandler(this.colorPicker_Click);
            // 
            // penSizeLabel
            // 
            this.penSizeLabel.AutoSize = true;
            this.penSizeLabel.Location = new Point(18, 102);
            this.penSizeLabel.Name = "penSizeLabel";
            this.penSizeLabel.Size = new Size(90, 21);
            this.penSizeLabel.TabIndex = 2;
            this.penSizeLabel.Text = "Pen Size";
            // 
            // penSizeNumeric
            // 
            this.penSizeNumeric.Location = new Point(150, 98);
            this.penSizeNumeric.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            this.penSizeNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.penSizeNumeric.Name = "penSizeNumeric";
            this.penSizeNumeric.Size = new Size(90, 33);
            this.penSizeNumeric.TabIndex = 3;
            this.penSizeNumeric.Value = new decimal(new int[] { 2, 0, 0, 0 });
            this.penSizeNumeric.ValueChanged += new EventHandler(this.penSizeNumeric_ValueChanged);
            // 
            // clearBtn
            // 
            this.clearBtn.BackColor = Color.Red;
            this.clearBtn.FlatStyle = FlatStyle.Flat;
            this.clearBtn.ForeColor = Color.White;
            this.clearBtn.Location = new Point(16, 146);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new Size(268, 44);
            this.clearBtn.TabIndex = 4;
            this.clearBtn.Text = "clear";
            this.clearBtn.UseVisualStyleBackColor = false;
            this.clearBtn.Click += new EventHandler(this.clearBtn_Click);
            // 
            // slideshowGroupBox
            // 
            this.slideshowGroupBox.Controls.Add(this.slideshowCheckBox);
            this.slideshowGroupBox.Controls.Add(this.slideshowIntervalLabel);
            this.slideshowGroupBox.Controls.Add(this.slideshowIntervalNumeric);
            this.slideshowGroupBox.Controls.Add(this.slideshowHintLabel);
            this.slideshowGroupBox.Location = new Point(14, 488);
            this.slideshowGroupBox.Name = "slideshowGroupBox";
            this.slideshowGroupBox.Size = new Size(300, 180);
            this.slideshowGroupBox.TabIndex = 3;
            this.slideshowGroupBox.TabStop = false;
            this.slideshowGroupBox.Text = "slideshow";
            // 
            // slideshowCheckBox
            // 
            this.slideshowCheckBox.AutoSize = true;
            this.slideshowCheckBox.Location = new Point(18, 32);
            this.slideshowCheckBox.Name = "slideshowCheckBox";
            this.slideshowCheckBox.Size = new Size(130, 27);
            this.slideshowCheckBox.TabIndex = 0;
            this.slideshowCheckBox.Text = "Auto Play";
            this.slideshowCheckBox.UseVisualStyleBackColor = true;
            this.slideshowCheckBox.CheckedChanged += new EventHandler(this.slideshowCheckBox_CheckedChanged);
            // 
            // slideshowIntervalLabel
            // 
            this.slideshowIntervalLabel.AutoSize = true;
            this.slideshowIntervalLabel.Location = new Point(18, 84);
            this.slideshowIntervalLabel.Name = "slideshowIntervalLabel";
            this.slideshowIntervalLabel.Size = new Size(115, 21);
            this.slideshowIntervalLabel.TabIndex = 1;
            this.slideshowIntervalLabel.Text = "Interval (sec)";
            // 
            // slideshowIntervalNumeric
            // 
            this.slideshowIntervalNumeric.Location = new Point(180, 80);
            this.slideshowIntervalNumeric.Maximum = new decimal(new int[] { 60, 0, 0, 0 });
            this.slideshowIntervalNumeric.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.slideshowIntervalNumeric.Name = "slideshowIntervalNumeric";
            this.slideshowIntervalNumeric.Size = new Size(90, 33);
            this.slideshowIntervalNumeric.TabIndex = 2;
            this.slideshowIntervalNumeric.Value = new decimal(new int[] { 3, 0, 0, 0 });
            this.slideshowIntervalNumeric.ValueChanged += new EventHandler(this.slideshowIntervalNumeric_ValueChanged);
            // 
            // slideshowHintLabel
            // 
            this.slideshowHintLabel.AutoSize = true;
            this.slideshowHintLabel.ForeColor = Color.Gray;
            this.slideshowHintLabel.Location = new Point(18, 128);
            this.slideshowHintLabel.Name = "slideshowHintLabel";
            this.slideshowHintLabel.Size = new Size(240, 21);
            this.slideshowHintLabel.TabIndex = 3;
            this.slideshowHintLabel.Text = "Check to auto-play every N seconds";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1280, 800);
            this.Controls.Add(this.slideshowGroupBox);
            this.Controls.Add(this.imageGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.draw);
            this.MinimumSize = new Size(980, 680);
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new EventHandler(this.Form1_Load);
            this.Resize += new EventHandler(this.Form1_Resize);
            this.imageGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imagePicturebox)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.draw.ResumeLayout(false);
            this.draw.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.colorPicker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.penSizeNumeric)).EndInit();
            this.slideshowGroupBox.ResumeLayout(false);
            this.slideshowGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slideshowIntervalNumeric)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}