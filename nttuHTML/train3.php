<?php
function getZhDayName(DateTimeInterface $date): string {
    // 修正 1：將字母 l 改為數字 1
    $map = [1 => '一', 2 => '二', 3 => '三', 4 => '四', 5 => '五', 6 => '六', 7 => '日'];
    return $map[(int)$date->format('N')];
}

$timezone = new DateTimeZone('Asia/Taipei');
$startDate = new DateTimeImmutable('today', $timezone);
$totalDays = 28;

// 修正 2：結尾使用分號 ; 結束這行語句
echo '<select name="weekend_date_selector" id="date-select" style="padding: 5px; font-family: sans-serif;">';

// 修正 3：使用大括號 { 開啟迴圈區塊
for ($i = 0; $i < $totalDays; $i++) {
    $currentDate = $startDate->modify("+$i days");
    
    $value = $currentDate->format('Y-m-d');
    $dayName = getZhDayName($currentDate);
    $isWeekend = ($currentDate->format('N') >= 6);
    
    $label = sprintf(
        "%s (週%s)%s",
        $value,
        $dayName,
        $isWeekend ? ' [假日]' : ''
    );

    echo " <option value=\"$value\">$label</option>" . PHP_EOL;
}

// 修正 4：補上 HTML 的 select 關閉標籤
echo '</select>';
?>