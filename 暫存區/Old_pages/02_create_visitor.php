<?php
ini_set('display_errors','on');
error_reporting(E_ALL & ~E_NOTICE & ~E_WARNING);
include_once("01_conn.php");

try{
    $sql = 'create table visitor (
            vid int auto_increment primary key,
			visitor int
            )';
//echo $sql."<br>\n";
$msg='';

$result =$connect->exec($sql);
if($result !== false){
    $msg="Successfully create table visitor. <br>\n";
} else {
    $msg="Fail to create table visitor. <br>\n";
}
if($msg != '')
    echo $msg;
}catch(PDOException $e){
    echo $e->getMessage();
}

?>