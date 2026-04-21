<?php
//$db_host = 'mysql:host=localhost;dbname=...';
ini_set('display_errors','on');
error_reporting(E_ALL & ~E_NOTICE & ~E_WARNING);
$db_host = "mysql:host=webdb.nttu.edu.tw;dbname=d50";
$db_user = "d50";
$db_password = "0968578863";
try{
    $connect = new PDO($db_host,$db_user,$db_password);
    $connect->exec("SET NAMES utf8");
    $connect->setAttribute(PDO::ATTR_ERRMODE,PDO::ERRMODE_EXCEPTION);
    echo "conn success!";
}
catch(PDOException $e){
    echo "error : ".$e->getMessage();
}
?>

