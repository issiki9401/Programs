<?php
// 1. 設定時區
date_default_timezone_set('Asia/Taipei');

// 2. 設定起始日期
$startDate = new DateTimeImmutable('2026-03-31');

// 3. 建立星期幾的中文對應表
// format('N') 會回傳 1(週一) 到 7(週日)，我們用這個數字來抓取對應的中文
$chineseDayMap = [
    1 => '一', 
    2 => '二', 
    3 => '三', 
    4 => '四', 
    5 => '五', 
    6 => '六', 
    7 => '日'
];

// 4. 使用 <pre> 標籤保持純文字排版
echo "<pre>";

// 印出表頭
echo "Date       | Day (CN) | Type\n";
echo "-------------------------------\n";

// 5. 建立迴圈 (從 0 到 14，共 15 天)
for ($i = 0; $i <= 14; $i++) {
    
    // 計算當前日期
    $currentDate = $startDate->modify("+$i days");
    
    // 取得 YYYY-MM-DD 格式
    $dateStr = $currentDate->format('Y-m-d');
    
    // 取得數字型態的星期幾 (1~7)
    $dayNum = $currentDate->format('N');
    
    // 組合出中文的星期 (例如：周 + 二 = 周二)
    $chineseDay = "周" . $chineseDayMap[$dayNum];
    
    // 判斷是否為週末 (大於等於 6 就是六、日)
    if ($dayNum >= 6) {
        $type = "[WEEKEND]";
    } else {
        $type = ""; // 平日留空
    }
    
    // 輸出結果
    echo $dateStr . " | " . $chineseDay . "     | " . $type . "\n";
}

echo "</pre>";
?>