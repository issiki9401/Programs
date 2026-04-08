<!DOCTYPE html>
<html>
<head>
    <meta charset="utf8">
    <title>NTTU臺東大學|c50測試站</title>
    <script language="javascript">
    function chkall() {
        // 1. 取得「全選」按鈕本身的狀態
        var isChecked = document.getElementById("sel").checked;
        // 2. 取得所有名稱為 mbc[] 的核取方塊
        var checkboxes = document.getElementsByName('mbc[]');
        
        // 3. 透過迴圈將所有方塊設為跟全選按鈕一樣的狀態
        for (var i = 0; i < checkboxes.length; i++) {
            checkboxes[i].checked = isChecked;
        }
    }
    </script>
</head>
<body>
    <a href="index.htm">首頁哈哈</a>
    <h1>表單多選</h1>

    <form action="form_multiple2.php" method="post">
        <input type="checkbox" id="sel" onclick="chkall()"> 
        <strong>全選 / 全不選</strong>
        <hr>

        <?php
        for ($i = 0; $i < 30; $i++) {
            // 注意：這裡將 HTML 放在 PHP 標籤外面，或者你可以用 echo
            ?>
            <input type="checkbox" value="<?php echo $i; ?>" name="mbc[]"> 
            選項 <?php echo $i; ?><br>
            <?php
        }
        ?>
        
        <br>
        <input type="submit" value="送出資料">
    </form>
</body>
</html>