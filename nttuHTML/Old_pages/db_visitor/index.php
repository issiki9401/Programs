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

<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <div class="container-fluid">
    <a class="navbar-brand" href="#">歡迎owob</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link active" aria-current="page" href="form.htm">點表單哈哈</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="form_multiple.php">多選框框哈哈</a>
        </li>
        <li class="nav-item dropdown">
          <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-bs-toggle="dropdown" aria-expanded="false">
            其他哈哈
          </a>
          <ul class="dropdown-menu" aria-labelledby="navbarDropdown">
            <li><a class="dropdown-item" href="index2.htm">舊的次頁頁面</a></li>
			<li><a class="dropdown-item" href="booking.htm">飛機訂票系統</a></li>
			<li><hr class="dropdown-divider"></li>
            <li><a class="dropdown-item" href="train.php">訂票系統</a></li>
            <li><a class="dropdown-item" href="train2.php">訂票系統2</a></li>
			<li><a class="dropdown-item" href="train3.php">訂票系統3</a></li>
          </ul>
        </li>
        <li class="nav-item">
          <a class="nav-link disabled">Disabled</a>
        </li>
      </ul>
      <form class="d-flex">
        <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
        <button class="btn btn-outline-success" type="submit">Search</button>
      </form>
    </div>
  </div>
</nav>
      
</nav>

    <!-- Optional JavaScript; choose one of the two! -->

    <!-- Option 1: Bootstrap Bundle with Popper -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>

    <!-- Option 2: Separate Popper and Bootstrap JS -->
    <!--
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.10.1/dist/umd/popper.min.js" integrity="sha384-W8fXfP3gkOKtndU4JGtKDvXbO53Wy8SZCQHczT5FMiiqmQfUpWbYdTil/SxwZgAN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.min.js" integrity="sha384-skAcpIdS7UcVUC05LJ9Dxay8AXcDYfBJqt1CJ85S/CFujBsIzCIv+l9liuYLaMQ/" crossorigin="anonymous"></script>
    -->
	
<div class="container mt-3">
    <div class="row">
        <div class="col-12 text-end">
            <div class="badge bg-white text-dark p-2 border shadow-sm">
                <i class="bi bi-person-fill"></i> <?php include_once("db_visitor/03_visitor_update.php"); ?>
            </div>
        </div>
    </div>
</div>
<div id="carouselExampleIndicators" class="carousel slide" data-bs-ride="carousel">
	
<div id="carouselExampleIndicators" class="carousel slide" data-bs-ride="carousel">
  <div class="carousel-indicators">
    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2"></button>
    <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3"></button>
	<button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="3" aria-label="Slide 4"></button>
	<button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="4" aria-label="Slide 5"></button>
	<button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="5" aria-label="Slide 6"></button>
  </div>
  <div class="carousel-inner">
    <div class="carousel-item active" data-bs-interval="10000">
      <img src="images/01.png" class="d-block w-100" alt="..." width="1440" height="360">
    </div>
    <div class="carousel-item" data-bs-interval="10000">
      <img src="images/02.png" class="d-block w-100" alt="..." width="1440" height="360">
    </div>
    <div class="carousel-item" data-bs-interval="10000">
      <img src="images/03.png" class="d-block w-100" alt="..." width="1440" height="360">
    </div>
	<div class="carousel-item" data-bs-interval="10000">
      <img src="images/04.png" class="d-block w-100" alt="..." width="1440" height="360">
    </div>
	<div class="carousel-item" data-bs-interval="10000">
      <img src="images/05.png" class="d-block w-100" alt="..." width="1440" height="360">
    </div>
	<div class="carousel-item" data-bs-interval="10000">
      <img src="images/06.png" class="d-block w-100" alt="..." width="1440" height="360">
    </div>
  </div>
  <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Previous</span>
  </button>
  <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="visually-hidden">Next</span>
  </button>
</div>

  </body>
</html>