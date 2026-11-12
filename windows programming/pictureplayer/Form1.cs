#nullable disable
using System.Drawing.Drawing2D;
namespace pictureplayer
{
    public partial class Form1 : Form
    {
        // 初始變數宣告 (第5頁)
        private List<Bitmap> imageList = new List<Bitmap>();       // 圖片存在這裡
        private List<Bitmap> originalImageList = new List<Bitmap>(); // 備份的圖片
        private int currentImageIndex = 0;

        private Size defaultImagePicturebox;  // 記住預設的 picturebox 大小

        private bool isDrawing = false;
        private PointF previousPoint;   // 上一次畫圖的「圖片像素」座標

        private Pen currentPen = new Pen(Color.Black, 2);  // 初始筆刷為黑色

        // 輪播用 Timer
        private System.Windows.Forms.Timer slideshowTimer = new System.Windows.Forms.Timer();

        // 縮放倍率：1.0 = 圖片剛好符合圖片框，滾輪放大/縮小
        private float imageZoom = 1.0f;

        // 設計基準尺寸(邏輯像素)，供不同螢幕/DPI 自動縮放參考
        private const int DESIGN_WIDTH = 1280;
        private const int DESIGN_HEIGHT = 800;

        public Form1()
        {
            InitializeComponent();

            // 記住預設的 picturebox 大小(圖片顯示與縮放比例會用到)
            defaultImagePicturebox = imagePicturebox.Size;

            // 設定輪播 Timer
            slideshowTimer.Interval = 3000; // 預設 3 秒
            slideshowTimer.Tick += slideshowTimer_Tick;
        }

        // Form1_Load 事件 (第6頁)
        private void Form1_Load(object sender, EventArgs e)
        {
            // 依目前螢幕工作區與 DPI 自動調整視窗大小，讓任何螢幕都能舒適使用：
            // 目標大小 = 設計基準(1280x800) x DPI 縮放，若螢幕放不下則以工作區為上限。
            double dpiScale = (double)DeviceDpi / 96.0; // 例:150% => 1.5
            Rectangle workArea = Screen.FromControl(this).WorkingArea;

            int targetW = Math.Min(workArea.Width, (int)(DESIGN_WIDTH * dpiScale));
            int targetH = Math.Min(workArea.Height, (int)(DESIGN_HEIGHT * dpiScale));

            this.ClientSize = new Size(targetW, targetH);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // Form1_Resize：視窗大小改變時重繪圖片(保持置中)
        private void Form1_Resize(object sender, EventArgs e)
        {
            imagePicturebox.Invalidate();
        }

        // loadImage_Click (第7頁)
        private void loadImage_Click(object sender, EventArgs e)
        {
            // 選擇載入方式：整包資料夾 or 多選圖片
            DialogResult choice = MessageBox.Show(
                "請選擇載入方式：\n\n[是] 選擇整個資料夾，載入資料夾內所有圖片\n[否] 多選圖片檔案\n[取消] 不載入",
                "Load Image", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (choice == DialogResult.Yes)
            {
                // 使用 FolderBrowserDialog 選擇資料夾
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    folderDialog.Description = "請選擇包含圖片的資料夾";
                    folderDialog.ShowNewFolderButton = false;

                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string folder = folderDialog.SelectedPath;
                        if (string.IsNullOrEmpty(folder)) return;

                        // 支援的圖片副檔名
                        string[] extensions = { "*.jpg", "*.jpeg", "*.png", "*.gif", "*.bmp" };
                        List<string> filePaths = new List<string>();

                        foreach (string ext in extensions)
                        {
                            filePaths.AddRange(System.IO.Directory.GetFiles(folder, ext));
                        }

                        // 依檔名排序，讓輪播順序固定
                        filePaths.Sort();

                        if (filePaths.Count == 0)
                        {
                            MessageBox.Show("此資料夾內沒有支援的圖片檔案。",
                                "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        LoadImageFiles(filePaths);
                    }
                }
            }
            else if (choice == DialogResult.No)
            {
                // 多選圖片檔案
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "圖像文件|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                    openFileDialog.Multiselect = true; // 允許多選

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        LoadImageFiles(openFileDialog.FileNames.ToList());
                    }
                }
            }
            // choice == Cancel：不做任何事
        }

        // 將一組圖片檔案載入清單並顯示第一張
        private void LoadImageFiles(List<string> filePaths)
        {
            if (filePaths == null || filePaths.Count == 0) return;

            // 清空舊資料，避免與新資料混在一起
            foreach (Bitmap bmp in imageList)
            {
                bmp.Dispose();
            }
            foreach (Bitmap bmp in originalImageList)
            {
                bmp.Dispose();
            }
            imageList.Clear();
            originalImageList.Clear();
            imageZoom = 1.0f;

            foreach (string fileName in filePaths)
            {
                try
                {
                    // 讀取選擇的圖像文件並添加到圖片清單中
                    Bitmap newImage = new Bitmap(fileName);
                    imageList.Add(newImage);

                    // 備份原來圖片(獨立複本，供 clear 還原使用)
                    originalImageList.Add(new Bitmap(newImage));
                }
                catch (Exception ex)
                {
                    // 遇到無法讀取的檔案就跳過，不中斷整個載入
                    System.Diagnostics.Debug.WriteLine($"載入失敗: {fileName} - {ex.Message}");
                }
            }

            if (imageList.Count > 0)
            {
                // 顯示第一張圖片
                currentImageIndex = 0;
                ShowCurrentImage();
            }
            else
            {
                MessageBox.Show("沒有成功載入任何圖片。",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ShowCurrentImage (第8頁)：重繪目前圖片(畫面由 Paint 事件負責)
        private void ShowCurrentImage()
        {
            imagePicturebox.Invalidate();
        }

        // 計算圖片在 PictureBox 中的顯示位置與大小(等比置中, 套用縮放)
        private RectangleF GetImageDisplayRect()
        {
            RectangleF client = imagePicturebox.ClientRectangle;
            if (imageList.Count == 0 || client.Width <= 0 || client.Height <= 0)
                return RectangleF.Empty;

            Bitmap img = imageList[currentImageIndex];
            if (img == null || img.Width == 0 || img.Height == 0)
                return RectangleF.Empty;

            // 先算出「剛好放進 PictureBox」的基礎比例，再乘上縮放倍率
            float ratio = Math.Min(client.Width / (float)img.Width, client.Height / (float)img.Height);
            float w = img.Width * ratio * imageZoom;
            float h = img.Height * ratio * imageZoom;
            float x = (client.Width - w) / 2f;
            float y = (client.Height - h) / 2f;

            return new RectangleF(x, y, w, h);
        }

        // 把 PictureBox 上的滑鼠座標換算成圖片像素座標
        private PointF ClientToImage(Point clientPoint)
        {
            RectangleF rect = GetImageDisplayRect();
            if (imageList.Count == 0 || rect.Width <= 0 || rect.Height <= 0)
                return PointF.Empty;

            Bitmap img = imageList[currentImageIndex];
            float px = (clientPoint.X - rect.X) / rect.Width * img.Width;
            float py = (clientPoint.Y - rect.Y) / rect.Height * img.Height;

            return new PointF(px, py);
        }

        // imagePictureBox_Paint：自繪目前圖片(等比置中, 高品質縮放)
        private void imagePictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(imagePicturebox.BackColor);

            if (imageList.Count == 0)
            {
                // 未載入圖片時的提示文字
                using (Font hintFont = new Font(this.Font.FontFamily, 14f))
                {
                    TextRenderer.DrawText(e.Graphics, "請按 Load Image 載入圖片",
                        hintFont, imagePicturebox.ClientRectangle, Color.Gray,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                }
                return;
            }

            Bitmap img = imageList[currentImageIndex];
            if (img == null) return;

            RectangleF rect = GetImageDisplayRect();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.DrawImage(img, rect);
        }

        // nextImageBtn_Click (第10頁)
        private void nextImageBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            // 切換到下一張圖片
            currentImageIndex++;
            if (currentImageIndex >= imageList.Count)
            {
                currentImageIndex = 0; // 循環顯示圖片
            }

            ShowCurrentImage();
        }

        // previousImageBtn_Click (第11頁)
        private void previousImageBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            // 切換到上一張圖片
            currentImageIndex--;
            if (currentImageIndex < 0)
            {
                currentImageIndex = imageList.Count - 1; // 循環顯示圖片
            }

            ShowCurrentImage();
        }

        // flipHorizontalBtn_Click：水平翻轉目前圖片
        private void flipHorizontalBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            imageList[currentImageIndex].RotateFlip(RotateFlipType.RotateNoneFlipX);
            ShowCurrentImage();
        }

        // flipVerticalBtn_Click：垂直翻轉目前圖片
        private void flipVerticalBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            imageList[currentImageIndex].RotateFlip(RotateFlipType.RotateNoneFlipY);
            ShowCurrentImage();
        }

        // rotateLeftBtn_Click：順時針旋轉 90 度（= 逆時針 270 度）
        private void rotateLeftBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            imageList[currentImageIndex].RotateFlip(RotateFlipType.Rotate270FlipNone);
            ShowCurrentImage();
        }

        // rotateRightBtn_Click：順時針旋轉 90 度
        private void rotateRightBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            imageList[currentImageIndex].RotateFlip(RotateFlipType.Rotate90FlipNone);
            ShowCurrentImage();
        }

        // saveBtn_Click：將目前圖片另存新檔
        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG 圖片|*.png|JPEG 圖片|*.jpg;*.jpeg|Bitmap 圖片|*.bmp|GIF 圖片|*.gif";
                saveFileDialog.FileName = "image" + (currentImageIndex + 1);

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // 依副檔名決定存檔格式
                    string ext = Path.GetExtension(saveFileDialog.FileName).ToLower();
                    System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;
                    if (ext == ".jpg" || ext == ".jpeg") format = System.Drawing.Imaging.ImageFormat.Jpeg;
                    else if (ext == ".bmp") format = System.Drawing.Imaging.ImageFormat.Bmp;
                    else if (ext == ".gif") format = System.Drawing.Imaging.ImageFormat.Gif;

                    imageList[currentImageIndex].Save(saveFileDialog.FileName, format);
                }
            }
        }

        // colorPicker_Click (第12頁)
        private void colorPicker_Click(object sender, EventArgs e)
        {
            // 彈出顏色選擇器對話框
            ColorDialog colorDialog = new ColorDialog();

            // 如果選擇了顏色，則更新筆刷顏色
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // 更新筆刷的顏色
                currentPen.Color = colorDialog.Color;

                colorPicker.BackColor = colorDialog.Color;
            }
        }

        // penSizeNumeric_ValueChanged：更新筆刷大小
        private void penSizeNumeric_ValueChanged(object sender, EventArgs e)
        {
            currentPen.Width = (float)penSizeNumeric.Value;
        }

        // clearBtn_Click：清除目前圖片上的塗鴉，還原成載入時的原始圖片
        private void clearBtn_Click(object sender, EventArgs e)
        {
            if (imageList.Count == 0) return;

            imageList[currentImageIndex] = new Bitmap(originalImageList[currentImageIndex]);
            ShowCurrentImage();
        }

        // slideshowCheckBox_CheckedChanged：勾選/取消勾選輪播
        private void slideshowCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (imageList.Count == 0)
            {
                if (slideshowCheckBox.Checked)
                {
                    slideshowCheckBox.Checked = false;
                    MessageBox.Show("請先載入圖片，再啟動輪播。", "提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            if (slideshowCheckBox.Checked)
            {
                // 依設定的秒數啟動 Timer（毫秒）
                slideshowTimer.Interval = (int)slideshowIntervalNumeric.Value * 1000;
                slideshowTimer.Start();
            }
            else
            {
                slideshowTimer.Stop();
            }
        }

        // slideshowIntervalNumeric_ValueChanged：調整輪播秒數
        private void slideshowIntervalNumeric_ValueChanged(object sender, EventArgs e)
        {
            if (slideshowTimer.Enabled)
            {
                slideshowTimer.Interval = (int)slideshowIntervalNumeric.Value * 1000;
            }
        }

        // slideshowTimer_Tick：時間到自動切到下一張
        private void slideshowTimer_Tick(object sender, EventArgs e)
        {
            nextImageBtn_Click(sender, e);
        }

        // imagePictureBox_MouseDown：開始在圖片上畫圖 (1/2) (第13頁)
        private void imagePictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (imageList.Count == 0) return;

            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                previousPoint = ClientToImage(e.Location);
            }
        }

        // imagePictureBox_MouseMove：在原始圖片上畫線 (2/2) (第14頁)
        private void imagePictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || imageList.Count == 0) return;

            PointF current = ClientToImage(e.Location);
            Bitmap img = imageList[currentImageIndex];

            // 依顯示縮放換算筆刷寬度，讓螢幕上看起來接近設定的粗細
            RectangleF rect = GetImageDisplayRect();
            float scale = (rect.Width > 0 && img.Width > 0) ? (img.Width / rect.Width) : 1f;

            using (Pen pen = new Pen(currentPen.Color, currentPen.Width * scale))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawLine(pen, previousPoint.X, previousPoint.Y, current.X, current.Y);
                }
            }

            previousPoint = current;
            imagePicturebox.Invalidate();
        }

        // imagePictureBox_MouseUp：放開滑鼠左鍵時停止畫圖
        private void imagePictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        // ImageGroupBox_MouseWheel (第15頁)：滾輪縮放圖片
        private void ImageGroupBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (imageList.Count == 0) return;

            float scaleFactor = 1.1f; // 縮放因子，可以根據需求調整

            if (e.Delta > 0)
            {
                // 滾輪向上滾動，放大圖片
                imageZoom *= scaleFactor;
                if (imageZoom > 20f) imageZoom = 20f;
            }
            else
            {
                // 滾輪向下滾動，縮小圖片
                imageZoom /= scaleFactor;
                if (imageZoom < 0.05f) imageZoom = 0.05f;
            }

            // 強制 PictureBox 重新繪製以顯示縮放效果
            imagePicturebox.Invalidate();
        }
    }
}