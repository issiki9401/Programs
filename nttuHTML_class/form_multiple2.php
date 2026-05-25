<!DOCTYPE html>
<html>
<head>
    <meta charset="utf8">
    <title>接收表單資料</title>
</head>
<body>

    <?php
    // 1. 檢查 $_POST 裡面有沒有 'mbc' 這個陣列資料 (使用者是否有勾選)
    if (isset($_POST['mbc']) && is_array($_POST['mbc'])) {
        
        // 2. 將接收到的陣列存入變數，方便操作
        $selected_options = $_POST['mbc'];
        
        // 3. 使用 foreach 迴圈，將陣列裡的資料一筆一筆印出來
        echo "<ul>";
        foreach ($selected_options as $value) {
            // htmlspecialchars 是一種安全機制，避免被注入惡意代碼
            echo "<li>" . htmlspecialchars($value) . "</li>";
        }
        echo "</ul>";

        // 額外功能：計算總共勾了幾個
        echo "<p>總共選擇了 <strong>" . count($selected_options) . "</strong> 個項目。</p>";

    } else {
        // 如果使用者一個都沒勾就按下送出，就會執行這裡
        echo "<p style='color: red;'>您沒有勾選任何項目</p>";
    }
    ?>
	
    <a href="javascript:history.back()">回上一頁重選</a><br>
	<a href="index.htm">回首頁uwu</a>
</body>
</html>