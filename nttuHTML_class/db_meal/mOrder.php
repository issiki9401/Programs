<?php
ini_set('display_errors','on');
error_reporting(E_ALL & ~E_NOTICE & ~E_WARNING);
require_once("../db_visitor/01_conn.php");
$meb="3";
$mid2=$_POST['mid'];
$mQty2=$_POST['mQty'];
$tmp="";

foreach($mid2 as $idx => $vau){
    echo "index is $vau with value as $mQty2[$idx]<br>\n";
    if($mQty2[$idx]!=0){
        $tmp=$tmp . $vau . "," . $mQty2[$idx] . ",";
    }
}
echo rtrim($tmp,",");
?>