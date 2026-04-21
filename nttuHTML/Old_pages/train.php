<?php
// 1. 設定時區，確保日期計算正確
date_default_timezone_set('Asia/Taipei');

// 2. 設定起始日期
$startDate = new DateTimeImmutable('2026-03-31');

// 3. 使用 <pre> 標籤，讓網頁輸出時能保持純文字的排版與空格
echo "<pre>";

// 4. 建立 0 到 50 的迴圈 (共 51 天)
for ($i = 0; $i <= 50; $i++) {
    
    // 將起始日期加上 $i 天
    $currentDate = $startDate->modify("+$i days");
    
    // 取得 YYYY-MM-DD 格式的日期
    $dateStr = $currentDate->format('Y-m-d');
    
    // 取得星期幾的英文全名 (例如 Tuesday)
    $dayOfWeek = $currentDate->format('l');
    
    // 判斷是否為週末
    // format('N') 會回傳數字 1(週一) 到 7(週日)
    // 如果大於等於 6 (週六或週日)，就是 WEEKEND
    if ($currentDate->format('N') >= 6) {
        $dayType = "[WEEKEND]";
    } else {
        $dayType = "[WEEKDAY]";
    }
    
    // 輸出結果 (使用 \t 或是直接打空格來排版)
    echo $i . "  " . $dateStr . " | " . $dayOfWeek . " | " . $dayType . "<br>";
}

echo "</pre>";
?>