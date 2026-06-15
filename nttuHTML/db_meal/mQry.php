<?php
// 1. 資料庫連線
$db_host = 'webdb.nttu.edu.tw'; 
$db_name = 'd50';       
$db_user = 'd50';       
$db_pass = '0968578863';   

try {
    $connect = new PDO("mysql:host={$db_host};dbname={$db_name};charset=utf8mb4", $db_user, $db_pass);
    $connect->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch (PDOException $e) {
    die("資料庫連線失敗: " . $e->getMessage());
}

// 2. 查詢資料
$sql2 = "SELECT * FROM menu"; 
?>

<!DOCTYPE html>
<html lang="zh-TW">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>點餐區</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-F3w7mX95PdgyTmZZMECAngseQB83DfGTowi0iMjiWaeVhAn4FJkqJByhZMI3AhiU" crossorigin="anonymous">
</head>
<body>

<h1 class="p-2 m-0 fw-normal">點餐區</h1>

<?php
$rs2 = $connect->query($sql2); 
?>

<form action="mOrder.php" method="post">
    
    <div class="card-group"> 
        <?php
        while ($row2 = $rs2->fetch()) {
        ?>
            <div class="card" style="border-radius: 0;">
                <input type="hidden" name="mid[]" value="<?php echo $row2['mid']; ?>">

                <img src="../images/<?php echo $row2['mImg']; ?>" class="card-img-top" alt="..." style="border-radius: 0;">
                <div class="card-body">
                    <h5 class="card-title"><?php echo $row2['mName']; ?></h5>
                    <p class="card-text"><?php echo $row2['mDetail']; ?></p>
    
                    <p class="card-text fw-bold">$<?php echo $row2['mPrice']; ?>元</p>
    
                    數量:
                    <select name='mQty[]' class="form-select mb-3">
                        <?php
                        $i = 0;
                        while ($i <= 10) {
                            if ($i == 0) {
                                echo "<option value='$i' selected>$i</option>\n";
                            } else {
                                echo "<option value='$i'>$i</option>\n";
                            }
                            $i++;
                        }
                        ?>
                    </select>
                    <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
                </div>
            </div> 
        <?php
        } 
        ?>
    </div>

    <input type="submit" class="btn btn-primary w-100 py-2" style="border-radius: 0;" value="Submit">
</form>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.1/dist/js/bootstrap.bundle.min.js" integrity="sha384-/bQdsTh/da6pkI1MST/rWKFNjaCP5gBSY4sEBT38Q/9RBh9AH40zEOg7Hlq2THRZ" crossorigin="anonymous"></script>
</body>
</html>