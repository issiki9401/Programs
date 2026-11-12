using System.IO;

namespace musicgame
{
    public partial class Form1 : Form
    {
        // ---------- 初始變數宣告 ----------
        int minX = 240, maxX = 800, minY = 30, maxY = 380;
        int count = 0;
        //int[] fq = {524,554,588,622,784,880,988};
        int[] fq = { 440, 495, 550, 587, 660, 733, 825, 895 };
        int duration = 600;
        int[] arrbee = new int[7] { 5, 3, 3, 4, 2, 2, 1 };

        // ---------- 2.完成整首小蜜蜂音樂（簡譜 49 個音，1=C） ----------
        int[] arrbeeFull = new int[49]
        {
            5,3,3, 4,2,2, 1,2,3,4, 5,5,5,
            5,3,3, 4,2,2, 1,3,5,5, 1,
            2,2,2,2, 2,3,4, 3,3,3,3, 3,4,5,
            5,3,3, 4,2,2, 1,3,5,5, 1
        };

        // ---------- 5.小星星樂譜（42 個音） ----------
        int[] arrstar = new int[42]
        {
            1,1,5,5, 6,6,5, 4,4,3,3, 2,2,1,
            5,5,4,4, 3,3,2, 5,5,4,4, 3,3,2,
            1,1,5,5, 6,6,5, 4,4,3,3, 2,2,1
        };

        // ---------- paintTable 參數化 ----------
        // 目前曲目的樂譜陣列與長度
        int[] arrCur = new int[7] { 5, 3, 3, 4, 2, 2, 1 };
        int maxCur = 7;

        // ---------- 6.可更改的鋼琴塊（樂譜黑塊）顏色 ----------
        Color blockColor = Color.Black;

        // ---------- 7.鋼琴音（Do~Si = C,D,E,F,G,A,B 的 wav，用 Windows Media Player 播放） ----------
        // 音檔放在 exe 目錄（bin\Debug\... 或 bin\Release\...）下的 Music_Notes 資料夾
        static readonly string[] NoteNames = { "C", "D", "E", "F", "G", "A", "B" };  // 對應 Do~Si
        dynamic? notePlayer = null;   // 鋼琴單音用的 WindowsMediaPlayer (WMPlayer.OCX)

        // ---------- 3.Timer 自動播放 ----------
        System.Windows.Forms.Timer tmrPlay = new System.Windows.Forms.Timer();
        int playIndex = 0;          // 目前播到第幾個音
        bool autoPlaying = false;

        // ---------- 附加功能：mp3 等音檔歌曲（同樣用 Windows Media Player 播放） ----------
        List<string> mediaFiles = new List<string>();   // 與 cmbSong 中「音檔歌曲」項目對應
        dynamic? songPlayer = null; // 整首音檔用的 WindowsMediaPlayer (WMPlayer.OCX)
        string? loadedSong = null;  // songPlayer 目前載入的檔案（用於暫停/繼續）
        bool wmpPlaying = false;

        public Form1()
        {
            InitializeComponent();

            // 3.Timer 每個 Tick 播放下一個音並讓色塊移動
            tmrPlay.Interval = 650;
            tmrPlay.Tick += TmrPlay_Tick;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(860, 660);
            Do.Location = new Point(240, 390);
            Re.Location = new Point(320, 390);
            Mi.Location = new Point(400, 390);
            Fa.Location = new Point(480, 390);
            So.Location = new Point(560, 390);
            La.Location = new Point(640, 390);
            Si.Location = new Point(720, 390);
            this.KeyPreview = true;          // KeyPress 事件可被執行

            // 5.ComboBox 歌曲清單（內建樂譜 + 之後新增的音檔）
            cmbSong.Items.Add("小蜜蜂(前7音)");
            cmbSong.Items.Add("小蜜蜂(整首)");
            cmbSong.Items.Add("小星星");
            cmbSong.SelectedIndex = 0;

            paintTable(arrCur, maxCur);
        }

        // ---------- Do_Click、Re_Click 事件（其餘五音同型補齊） ----------
        private void Do_Click(object sender, EventArgs e)
        {
            PlayNote(0);
        }

        private void Re_Click(object sender, EventArgs e)
        {
            PlayNote(1);
        }

        private void Mi_Click(object sender, EventArgs e)
        {
            PlayNote(2);
        }

        private void Fa_Click(object sender, EventArgs e)
        {
            PlayNote(3);
        }

        private void So_Click(object sender, EventArgs e)
        {
            PlayNote(4);
            StepDown();        // 按 So 鍵後樂譜落下（count++ 重畫）
        }

        private void La_Click(object sender, EventArgs e)
        {
            PlayNote(5);
        }

        private void Si_Click(object sender, EventArgs e)
        {
            PlayNote(6);
        }

        // 7.Do~Si 對應 C,D,E,F,G,A,B 的 wav，用 Windows Media Player 播放；
        // 找不到音檔時退回簡報原本的 Console.Beep
        private void PlayNote(int i)
        {
            string path = GetNotePath(NoteNames[i]);
            if (File.Exists(path))
                PlayPiano(path);
            else
                Console.Beep(fq[i], duration);
        }

        // 檔案須放在 exe 目錄（bin\Debug\... 或 bin\Release\...）下的 Music_Notes 資料夾
        private static string GetNotePath(string noteName)
        {
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.GetFullPath(directory + @"\Music_Notes\" + noteName + ".wav");
            return path;
        }

        // 建立 Windows Media Player COM 物件（WMPlayer.OCX，等同官方文件的 WindowsMediaPlayer）
        private static dynamic? CreateWmp()
        {
            Type? t = Type.GetTypeFromProgID("WMPlayer.OCX");
            return t == null ? null : Activator.CreateInstance(t);
        }

        // 用 Windows Media Player 播放一個鋼琴單音：player.URL = path; player.controls.play();
        private void PlayPiano(string path)
        {
            try
            {
                if (notePlayer == null) notePlayer = CreateWmp();
                if (notePlayer == null) return;
                notePlayer.URL = path;
                notePlayer.controls.play();
            }
            catch { }
        }

        // ---------- 按 So 鍵後，已弹過的音落下（黑鍵往下移一列） ----------
        private void StepDown()
        {
            count++;
            paintTable(arrCur, maxCur);
        }

        // ---------- Form1_KeyPress 鍵盤 1-7 ----------
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.D1)
            {
                //MessageBox.Show("press D1");
                Do_Click(sender, e);
            }
            if (e.KeyChar == (char)Keys.D2)
            {
                Re_Click(sender, e);
            }
            if (e.KeyChar == (char)Keys.D3)
            {
                Mi_Click(sender, e);
            }
            if (e.KeyChar == (char)Keys.D4)
            {
                Fa_Click(sender, e);
            }
            if (e.KeyChar == (char)Keys.D5)
            {
                So_Click(sender, e);
            }
            if (e.KeyChar == (char)Keys.D6)
            {
                La_Click(sender, e);
            }
            if (e.KeyChar == (char)Keys.D7)
            {
                Si_Click(sender, e);
            }
        }

        // ---------- paintTable(int[] arr, int max) ----------
        // 手寫畫線版本保留為註解，正式使用迴圈版本
        void paintTable(int[] arr, int max)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(Color.White);

            /* 手寫畫 7x7 表格（缺點：重複、難維護）
            g.DrawLine(new Pen(Color.Black), new Point(minX, minY), new Point(maxX, minY));
            g.DrawLine(new Pen(Color.Black), new Point(minX, minY + (1 * 50)), new Point(maxX, minY + (1 * 50)));
            g.DrawLine(new Pen(Color.Black), new Point(minX, minY + (2 * 50)), new Point(maxX, minY + (2 * 50)));
            g.DrawLine(new Pen(Color.Black), new Point(minX, minY + (3 * 50)), new Point(maxX, minY + (3 * 50)));
            g.DrawLine(new Pen(Color.Black), new Point(minX, minY), new Point(minX, maxY));
            g.DrawLine(new Pen(Color.Black), new Point(minX + (1 * 100), minY), new Point(minX + (1 * 100), maxY));
            g.DrawLine(new Pen(Color.Black), new Point(minX + (2 * 100), minY), new Point(minX + (2 * 100), maxY));
            g.DrawLine(new Pen(Color.Black), new Point(minX + (3 * 100), minY), new Point(minX + (3 * 100), maxY));
            */

            // 用迴圈畫 7x7 表格
            for (int i = 0; i <= 7; i++)
            {
                g.DrawLine(new Pen(Color.Black), new Point(minX, minY + ((380 - 30) / 7) * i), new Point(maxX, minY + ((380 - 30) / 7) * i));
                g.DrawLine(new Pen(Color.Black), new Point(minX + ((800 - 240) / 7) * i, minY), new Point(minX + ((800 - 240) / 7) * i, maxY));
            }

            // 填色音階（示範，保留為註解）
            /*
            g.FillRectangle(new SolidBrush(Color.Red), minX, minY, 100, 50);
            g.FillRectangle(new SolidBrush(Color.Green), minX + (2 * 100), minY, 100, 50);
            g.FillRectangle(new SolidBrush(Color.Blue), minX, minY + (2 * 50), 100, 50);
            */

            // 填色前 7 個音（黑塊），j 從 count 開始 → 落下效果
            for (int i = 6, j = count; i >= 0; i--)
            {
                if (j > max - 1)
                {
                }
                else
                {
                    g.FillRectangle(new SolidBrush(blockColor), minX + ((800 - 240) / 7) * (arr[j++] - 1), minY + ((380 - 30) / 7) * i, (800 - 240) / 7, (380 - 30) / 7);
                }
            }
        }

        // ---------- 3.Timer Tick → 自動播放下一個音 + 色塊移動 ----------
        private void TmrPlay_Tick(object? sender, EventArgs e)
        {
            if (playIndex >= maxCur)          // 4.播完自動停下來，程式可繼續使用
            {
                tmrPlay.Stop();
                autoPlaying = false;
                btnAutoPlay.Text = "自動播放";
                lblStatus.Text = "播放完畢！可按「重新播放」或手動彈奏";
                return;
            }
            int note = arrCur[playIndex];     // 1~7
            PlayNote(note - 1);
            playIndex++;
            count = playIndex;                // 色塊跟著落下移動
            paintTable(arrCur, maxCur);
            lblStatus.Text = $"自動播放中：第 {playIndex}/{maxCur} 音";
        }

        private void btnAutoPlay_Click(object sender, EventArgs e)
        {
            if (cmbSong.SelectedItem is string s && s.Contains("mp3"))
            {
                // 附加功能：音檔歌曲用 WMP 播放/暫停
                ToggleMediaPlay();
                return;
            }
            if (autoPlaying)                   // 播放中按 → 暫停
            {
                tmrPlay.Stop();
                autoPlaying = false;
                btnAutoPlay.Text = "自動播放(繼續)";
                lblStatus.Text = "已暫停";
                return;
            }
            if (playIndex >= maxCur) playIndex = 0;
            count = playIndex;
            paintTable(arrCur, maxCur);
            tmrPlay.Start();
            autoPlaying = true;
            btnAutoPlay.Text = "暫停";
        }

        // ---------- 4.停止 / 重新播放 ----------
        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrPlay.Stop();
            autoPlaying = false;
            btnAutoPlay.Text = "自動播放";
            StopMedia();
            playIndex = 0;
            count = 0;
            paintTable(arrCur, maxCur);
            lblStatus.Text = "已停止，可繼續手動彈奏或重新播放";
        }

        private void btnReplay_Click(object sender, EventArgs e)
        {
            StopMedia();
            playIndex = 0;
            count = 0;
            paintTable(arrCur, maxCur);
            if (cmbSong.SelectedItem is string s && s.Contains("mp3"))
            {
                ToggleMediaPlay();
                return;
            }
            tmrPlay.Start();
            autoPlaying = true;
            btnAutoPlay.Text = "暫停";
            lblStatus.Text = "重新播放中…";
        }

        // ---------- 5.ComboBox 切換歌曲 ----------
        private void cmbSong_SelectedIndexChanged(object sender, EventArgs e)
        {
            StopMedia();
            tmrPlay.Stop();
            autoPlaying = false;
            btnAutoPlay.Text = "自動播放";
            playIndex = 0;
            count = 0;

            string sel = cmbSong.SelectedItem?.ToString() ?? "";
            if (sel == "小蜜蜂(前7音)") { arrCur = arrbee; maxCur = 7; }
            else if (sel == "小蜜蜂(整首)") { arrCur = arrbeeFull; maxCur = arrbeeFull.Length; }
            else if (sel == "小星星") { arrCur = arrstar; maxCur = arrstar.Length; }
            else
            {
                // 附加功能：mp3 等音檔歌曲（格子清空，按「自動播放」用 WMP 播放音檔）
                arrCur = new int[1] { 0 };
                maxCur = 0;
                lblStatus.Text = "音檔歌曲：按「自動播放」開始播放";
            }
            paintTable(arrCur, maxCur);
            if (maxCur > 0) lblStatus.Text = $"已切換：{sel}（共 {maxCur} 音）";
        }

        // ---------- 6.更改鋼琴塊顏色 ----------
        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = blockColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                blockColor = cd.Color;
                paintTable(arrCur, maxCur);
                lblStatus.Text = "鋼琴塊顏色已更新";
            }
        }

        // ---------- 附加功能：新增 mp3 等音檔歌曲 ----------
        private void btnAddMp3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "新增 mp3 等音檔歌曲";
            ofd.Filter = "音檔|*.mp3;*.wav;*.wma;*.m4a;*.flac;*.ogg|所有檔案|*.*";
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (string f in ofd.FileNames)
                {
                    mediaFiles.Add(f);
                    cmbSong.Items.Add(Path.GetFileNameWithoutExtension(f) + "(mp3)");
                }
                cmbSong.SelectedIndex = cmbSong.Items.Count - 1;
                lblStatus.Text = $"已新增 {ofd.FileNames.Length} 首音檔歌曲";
            }
        }

        // 附加功能：用 Windows Media Player 播放/暫停 mp3 等音檔歌曲
        private void ToggleMediaPlay()
        {
            int idx = cmbSong.SelectedIndex - 3;      // 前 3 項是內建樂譜
            if (idx < 0 || idx >= mediaFiles.Count) return;
            try
            {
                if (songPlayer == null) songPlayer = CreateWmp();
                if (songPlayer == null) return;
                if (wmpPlaying)
                {
                    songPlayer.controls.pause();
                    wmpPlaying = false;
                    btnAutoPlay.Text = "自動播放(繼續)";
                    lblStatus.Text = "音檔已暫停";
                }
                else
                {
                    // 切到不同檔案才重設 URL；從暫停繼續時沿用原 URL
                    if (loadedSong != mediaFiles[idx])
                    {
                        songPlayer.URL = mediaFiles[idx];
                        loadedSong = mediaFiles[idx];
                    }
                    songPlayer.controls.play();
                    wmpPlaying = true;
                    btnAutoPlay.Text = "暫停";
                    lblStatus.Text = "音檔播放中：" + Path.GetFileName(mediaFiles[idx]);
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "音檔播放失敗：" + ex.Message;
            }
        }

        private void StopMedia()
        {
            try
            {
                if (songPlayer != null && wmpPlaying)
                {
                    songPlayer!.controls.stop();
                    wmpPlaying = false;
                    loadedSong = null;
                }
                btnAutoPlay.Text = "自動播放";
            }
            catch { }
        }
    }
}
