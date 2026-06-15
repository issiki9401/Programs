<?php
ini_set('display_errors','on');
error_reporting(E_ALL & ~E_NOTICE & ~E_WARNING);
require_once("../db_visitor/01_conn.php");
$uid2=$_POST['uid'];
$pwd2=$_POST['pwd'];
$urname2=$_POST['username'];
$sql="INSERT INTO `mbr` (`uid`, `pwd`, `username`, `reg`) VALUES ('$uid2', '$pwd2', '$urname2', NOW())";
$result =$connect->exec($sql);
if($result === false){
	$msg="fail insert. <br>\n";
} else {
	echo "<a href='../index.php'>回首頁</a>";
}
?>