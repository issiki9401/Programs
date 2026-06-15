<!doctype html>
<html lang="en">
  <head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <!-- Bootstrap CSS -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">

    <title>NTTU臺東大學|c50測試站</title>

<style>
    /* 針對所有指示按鈕 */
    .carousel-indicators [data-bs-target] {
        width: 18px;               /* 星形視覺上較小，建議稍微調大一點 */
        height: 18px;
        background-color: #f8f8ff; /* Ghost White */
        opacity: 0.4;
        margin: 0 8px;
        border: none;              /* 移除 Bootstrap 預設邊框 */
        
        /* --- 核心：製作四芒星圖案 --- */
        /* 透過 8 個座標點定義出向內凹的四角星 */
        clip-path: polygon(50% 0%, 61% 39%, 100% 50%, 61% 61%, 50% 100%, 39% 61%, 0% 50%, 39% 39%);
        
        /* 動態過渡效果 */
        transition: transform 0.3s ease, opacity 0.3s ease;
        
        /* 確保無陰影 */
        box-shadow: none;
    }

    /* 當前選中的按鈕 */
    .carousel-indicators .active {
        background-color: #f8f8ff;
        opacity: 1;
        transform: scale(1.2);    /* 讓選中的星星稍微大一點，更有層次感 */
    }

    /* 鼠標懸停時的放大效果 */
    .carousel-indicators [data-bs-target]:hover {
        opacity: 0.9;
        transform: scale(1.5);    /* 懸停時放大倍率 */
        cursor: pointer;
    }
</style>
	
  </head>
  <body style="background-color:lightskyblue;">
<h1>會員註冊</h1>
<form action="mbrReg2.php" method="post">
  <div class="mb-3">
    <label for="exampleInputEmail1" class="form-label">帳號:</label>
    <input name="uid" type="uid" class="form-control" id="exampleInputEmail1" aria-describedby="emailHelp">

  </div>
  <div class="mb-3">
    <label for="exampleInputPassword1" class="form-label">密碼:</label>
    <input name="pwd" type="text" class="form-control" id="exampleInputPassword1">
  </div>
    <div class="mb-3">
    <label for="exampleInputPassword1" class="form-label">姓名:</label>
    <input name="username" type="text" class="form-control" id="exampleInputPassword1">
  </div>
  
  <input type="submit" class="btn btn-primary">
</form>
  </body>
</html>