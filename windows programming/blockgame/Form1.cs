using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;

namespace blockgame
{
    public partial class Form1 : Form
    {
        private readonly Random rnd = new Random();

        // 遊戲狀態
        private readonly List<Button> gridButtons = new List<Button>();
        private int number = 2;          // 目前關卡 N（N x N 方格）
        private bool gameRunning = false;
        private int targetIndex = -1;    // 目標格在 gridButtons 的索引

        // 顏色與色差
        private int r, g, b;
        private int diff = 50;           // 目前這一關的色差

        // 成績
        private int roundCount = 0;      // 已過關數
        private int wrongClicks = 0;     // 點錯次數
        private int totalSeconds = 0;    // 累計花費秒數
        private int bestStage = 0;       // 最佳紀錄（歷史最高關卡)

        // 計時
        private int remainingSeconds = 30;
        private readonly System.Windows.Forms.Timer countdownTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer totalTimer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer flashTimer = new System.Windows.Forms.Timer();
        private Button? flashButton = null;
        private string? flashOriginalText = null;

        // 成績檔案位置（與專案同一資料夾）
        private static readonly string ScoresFile =
            @"C:\Users\1\Documents\Github\Programs\windows programming\blockgame\scores.txt";

        public Form1()
        {
            InitializeComponent();

            countdownTimer.Interval = 1000;
            countdownTimer.Tick += CountdownTimer_Tick;
            totalTimer.Interval = 1000;
            totalTimer.Tick += TotalTimer_Tick;
            flashTimer.Interval = 400;
            flashTimer.Tick += FlashTimer_Tick;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 視窗可自由縮放，設最小尺寸避免控制項被擠在一起
            MinimumSize = new Size(640, 600);
            LoadBest();
            UpdateStatusText();
        }

        // ===================== 開始 / 建立關卡 =====================

        private void btnStart_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            totalTimer.Stop();
            countdownTimer.Stop();
            flashTimer.Stop();
            if (flashButton != null)
            {
                flashButton.Text = flashOriginalText ?? "";
                flashButton = null;
            }

            // 讀取玩家自訂設定
            number = 2;
            roundCount = 0;
            wrongClicks = 0;
            totalSeconds = 0;
            gameRunning = true;

            NewRoundColors();
            ResetCountdown();
            BuildGrid();
            PlaceTarget();

            totalTimer.Start();
            countdownTimer.Start();
            UpdateStatusText();
        }

        // 重設一關的顏色與色差（色差在自訂上下限之間隨機）
        private void NewRoundColors()
        {
            int minDiff = Math.Max(1, (int)numDiffMin.Value);
            int maxDiff = Math.Max(minDiff, (int)numDiffMax.Value);
            diff = rnd.Next(minDiff, maxDiff + 1);
            r = rnd.Next(0, 256);
            g = rnd.Next(0, 256);
            b = rnd.Next(0, 256);
        }

        private int MaxButtonCount => Math.Max(2, (int)numMaxButton.Value);

        // 動態建立 N x N 方格按鈕
        private void BuildGrid()
        {
            ClearGrid();
            for (int i = 0; i < number; i++)
            {
                for (int j = 0; j < number; j++)
                {
                    var btn = new Button();
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderColor = Color.Gainsboro;
                    btn.FlatAppearance.MouseOverBackColor = Color.LightGoldenrodYellow;
                    btn.BackColor = Color.FromArgb(r, g, b);
                    btn.Text = "";
                    btn.Tag = i * number + j;
                    btn.Click += GridButton_Click;
                    gamePanel.Controls.Add(btn);
                    gridButtons.Add(btn);
                }
            }
            ApplyGridLayout();
        }

        private void ClearGrid()
        {
            gamePanel.Controls.Clear();
            gridButtons.Clear();
        }

        // 依 gamePanel 目前大小重新排列方格（視窗縮放時呼叫，方格跟隨縮放並置中）
        private void ApplyGridLayout()
        {
            if (gridButtons.Count == 0) return;

            const int gap = 8;                       // 方格間距，避免擠在一起
            int n = number;
            int availW = Math.Max(100, gamePanel.ClientSize.Width - 2 * gap);
            int availH = Math.Max(100, gamePanel.ClientSize.Height - 2 * gap);

            int cellW = Math.Max(20, (availW - (n - 1) * gap) / n);
            int cellH = Math.Max(20, (availH - (n - 1) * gap) / n);
            int cell = Math.Min(cellW, cellH);

            int gridW = cell * n + (n - 1) * gap;
            int gridH = cell * n + (n - 1) * gap;
            int startX = Math.Max(gap, (gamePanel.ClientSize.Width - gridW) / 2);
            int startY = Math.Max(gap, (gamePanel.ClientSize.Height - gridH) / 2);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var btn = gridButtons[i * n + j];
                    btn.Size = new Size(cell, cell);
                    btn.Location = new Point(startX + j * (cell + gap), startY + i * (cell + gap));
                }
            }
        }

        // 選定目標格：藍色頻道增減色差，其餘格子維持原色
        private void PlaceTarget()
        {
            int n = number;
            int b2 = b + diff;
            if (b2 > 255) b2 = b - diff;   // 與簡報相同：超過上限就反方向減色
            b2 = Math.Clamp(b2, 0, 255);

            targetIndex = rnd.Next(0, n * n);
            for (int i = 0; i < gridButtons.Count; i++)
            {
                gridButtons[i].BackColor = Color.FromArgb(r, g, b);
            }
            gridButtons[targetIndex].BackColor = Color.FromArgb(r, g, b2);
            UpdateCheatLabel();
        }

        // ===================== 點擊方格 =====================

        private void GridButton_Click(object? sender, EventArgs e)
        {
            if (!gameRunning || sender is not Button btn) return;

            int idx = (int)(btn.Tag ?? -1);
            if (idx == targetIndex)
            {
                OnRoundCleared();
            }
            else
            {
                wrongClicks++;
                int row = idx / number + 1;
                int col = idx % number + 1;
                lblStatus.Text = $"✗ 點錯了！（第 {row} 列 第 {col} 行 不是目標）";
                FlashButton(btn);
            }
        }

        // 過關：關卡 +1，若達自訂按鈕上限則勝利
        private void OnRoundCleared()
        {
            roundCount++;
            number++;
            if (number > MaxButtonCount)
            {
                GameOver(true);
                return;
            }
            NewRoundColors();
            ResetCountdown();
            BuildGrid();
            PlaceTarget();
            UpdateStatusText();
        }

        // ===================== 作弊顯示 =====================

        private void chkCheat_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheatLabel();
        }

        // 作弊 Label：顯示目標格位置與目前色差
        private void UpdateCheatLabel()
        {
            if (chkCheat.Checked && gameRunning && targetIndex >= 0 && targetIndex < gridButtons.Count)
            {
                int n = number;
                int row = targetIndex / n + 1;
                int col = targetIndex % n + 1;
                lblCheat.Text = $"作弊：目標 = 第 {row} 列 第 {col} 行　　色差 = {diff}（B 值 ±{diff}）";
            }
            else
            {
                lblCheat.Text = "";
            }
        }

        // ===================== 計時 =====================

        private void ResetCountdown()
        {
            // 關卡越大，時間越多：20 + N*5 秒
            remainingSeconds = 20 + number * 5;
            UpdateTimerLabel();
        }

        private void CountdownTimer_Tick(object? sender, EventArgs e)
        {
            remainingSeconds--;
            if (remainingSeconds <= 0)
            {
                remainingSeconds = 0;
                UpdateTimerLabel();
                GameOver(false);             // 倒數結束仍沒過關 → 遊戲結束
                return;
            }
            UpdateTimerLabel();
        }

        private void UpdateTimerLabel()
        {
            lblTimer.Text = remainingSeconds + " 秒";
            lblTimer.ForeColor = remainingSeconds <= 10 ? Color.Red : Color.DarkSlateBlue;
        }

        private void TotalTimer_Tick(object? sender, EventArgs e)
        {
            totalSeconds++;
            UpdateStatusText();
        }

        // ===================== 成績 =====================

        private void UpdateStatusText()
        {
            string stage = gameRunning ? $"{number}x{number}" : "—";
            lblStatus.Text = $"關卡 {stage}　已過關 {roundCount}　點錯 {wrongClicks}　總時間 {totalSeconds} 秒　最佳紀錄 {bestStage}x{bestStage}";
        }

        private void GameOver(bool win)
        {
            gameRunning = false;
            totalTimer.Stop();
            countdownTimer.Stop();
            flashTimer.Stop();

            int finalStage = Math.Min(number - 1, MaxButtonCount);   // 達成的最後一關
            string result = win ? "恭喜全部過關！" : "時間到！";
            string msg = result + $"\n\n達成關卡：{finalStage}x{finalStage}（上限 {MaxButtonCount}x{MaxButtonCount}）\n" +
                                 $"點錯次數：{wrongClicks}\n花費時間：{totalSeconds} 秒";

            RecordScore(finalStage, win, wrongClicks, totalSeconds);
            bestStage = Math.Max(bestStage, finalStage);
            lblCheat.Text = "";                 // 遊戲結束後清除作弊提示
            lblTimer.Text = win ? "完成！" : "時間到";
            lblTimer.ForeColor = win ? Color.Green : Color.Red;
            UpdateStatusText();

            MessageBox.Show(msg + "\n\n成績已記錄到：\n" + ScoresFile, "顏色魔術 - 遊戲結束",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 把成績寫入 scores.txt
        private void RecordScore(int stage, bool win, int wrong, int seconds)
        {
            try
            {
                string dir = Path.GetDirectoryName(ScoresFile) ?? "";
                if (dir.Length > 0) Directory.CreateDirectory(dir);

                string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {(win ? "勝" : "敗")} | " +
                              $"到達關卡: {stage}x{stage} | 上限: {MaxButtonCount}x{MaxButtonCount} | " +
                              $"點錯: {wrong} | 總時間: {seconds} 秒";
                File.AppendAllText(ScoresFile, line + Environment.NewLine, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法寫入成績檔：\n" + ex.Message, "檔案錯誤",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // 讀取歷史成績中的最高關卡
        private void LoadBest()
        {
            int best = 0;
            try
            {
                if (File.Exists(ScoresFile))
                {
                    foreach (string line in File.ReadAllLines(ScoresFile, Encoding.UTF8))
                    {
                        Match m = Regex.Match(line, @"到達關卡:\s*(\d+)x");
                        if (m.Success && int.TryParse(m.Groups[1].Value, out int s))
                        {
                            best = Math.Max(best, s);
                        }
                    }
                }
            }
            catch
            {
                // 讀不到就算了，不影響遊戲
            }
            bestStage = best;
        }

        // ===================== 其他 =====================

        private void FlashButton(Button btn)
        {
            flashTimer.Stop();
            if (flashButton != null && flashButton.IsHandleCreated)
            {
                flashButton.Text = flashOriginalText ?? "";
            }
            flashButton = btn;
            flashOriginalText = btn.Text;
            btn.Text = "✗";
            flashTimer.Start();
        }

        private void FlashTimer_Tick(object? sender, EventArgs e)
        {
            flashTimer.Stop();
            if (flashButton != null)
            {
                flashButton.Text = flashOriginalText ?? "";
                flashButton = null;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // 視窗縮放時重新排列方格
            ApplyGridLayout();
        }

        private void gamePanel_Resize(object sender, EventArgs e)
        {
            // 遊戲區大小改變時重新排列方格（隨視窗縮放）
            ApplyGridLayout();
        }
    }
}