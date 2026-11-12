# Interconnect — 雙系統 Wi-Fi 多媒體控制與 GDI+ 視覺化儀表板（視窗程式設計期中專題）

**報告者：11411133 曾俊睿**

## 1. 專題簡介與架構演進

本專案為 **Windows (C# WinForms)** 與 **Android (Kotlin)** 跨系統無線多媒體控制橋接系統。
- **專案來源**：由本人規劃功能、繪製介面草圖並透過 Visual Studio 建立 Windows Forms 架構，結合 AI 輔助實作跨系統網路通訊與系統多媒體 API 串接。
- **功能精簡與重構說明**：
  - **原初設計**：最初嘗試同時實作「雙向 Opus 即時音訊串流」與「多媒體控制互通」，但因跨平台底層音訊擷取權限（Android `MediaProjection`）與無線環境 UDP 抖動容易造成封包遺失與操作複雜度過高。
  - **修改與優化後版本**：移除不穩定的音訊串流模組與外部音訊套件依賴（`NAudio` / `Concentus`），專注強化 **「跨系統雙向多媒體同步控制」** 與 **「Windows Forms GDI+ 自訂繪圖視覺化介面」**，使系統達到零外部套件依賴、低延遲且 100% 穩定執行。

## 2. 專案位置與產出檔案

| 元件 | 路徑 |
|---|---|
| Windows 端原始碼（Visual Studio 專案） | [Midterm.sln](file:///C:/Users/1/Documents/Github/Programs/windows%20programming/Midterm/Midterm.sln) |
| Android 端原始碼（Gradle 專案） | [InterconnectAndroid](file:///C:/Users/1/Documents/Github/Programs/windows%20programming/Midterm/InterconnectAndroid/settings.gradle.kts) |
| 全架構單一 Windows 執行檔 | [Interconnect-all-arch.exe](file:///C:/Users/1/Documents/Github/Programs/windows%20programming/Midterm/publish/Interconnect-all-arch.exe) |
| 各架構獨立執行檔 (`x64` / `x86` / `arm64`) | [publish](file:///C:/Users/1/Documents/Github/Programs/windows%20programming/Midterm/publish) |
| Android APK 安裝檔 | [Interconnect.apk](file:///C:/Users/1/Documents/Github/Programs/windows%20programming/Midterm/publish/Interconnect.apk) |
| 通訊協定規格文件 | [通訊協定規格.md](file:///C:/Users/1/Documents/Github/Programs/windows%20programming/Midterm/docs/%E9%80%9A%E8%A8%8A%E5%8D%94%E5%AE%9A%E8%A6%8F%E6%A0%BC.md) |

## 3. 運用之「視窗程式設計」課程技術重點

1. **GDI+ 自訂繪圖 (`System.Drawing` / `System.Drawing.Drawing2D`)**：
   - 覆寫 `Control.OnPaint`，搭配 `DoubleBuffered` 雙緩衝消除畫面閃爍。
   - 使用 `GraphicsPath` 與 `AddArc` 建構圓角視窗裁切 (`Form.Region`)、圓角卡片與膠囊分段按鈕。
   - 使用 `LinearGradientBrush` 繪製**即時網路延遲 (Ping RTT) 漸層填充折線圖**與**動態音律頻譜柱**。
   - 使用 `Graphics.SetClip` 實現圓角專輯封面圖片裁切，並以幾何繪圖 (`FillPolygon`, `DrawArc`, `FillEllipse`) 手繪播放、暫停、上下首向量圖示與動態旋轉黑膠唱片。
2. **事件驅動與滑鼠碰撞檢測 (Hit-Testing)**：
   - 透過 `_hits` 清單記錄每個 GDI+ 繪製元件的 `RectangleF` 邊界與 `Action` 委派，於 `OnMouseDown`、`OnMouseMove` 實現按鈕點擊、懸停高亮 (Hover) 與進度條拖曳跳轉 (Seek)。
   - 攔截 Win32 `WM_NCHITTEST` (`0x84`) 訊息實現無邊框視窗八方向邊緣縮放與標題列拖曳。
3. **計時器 (`Timer`) 與跨執行緒安全更新 (`BeginInvoke`)**：
   - 透過 60ms WinForms `Timer` 驅動黑膠唱片旋轉與頻譜律動動畫；背景 Socket 執行緒收到封包後透過 `BeginInvoke` 安全觸發主執行緒 `Invalidate()` 重繪。
