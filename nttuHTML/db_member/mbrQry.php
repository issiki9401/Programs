<?php
ini_set('display_errors','on');
error_reporting(E_ALL & ~E_NOTICE & ~E_WARNING);
require_once("../db_visitor/01_conn.php");

$sql2="select * from mbr";
$connect->setAttribute(PDO::ATTR_CASE, PDO::CASE_NATURAL);
$rs2=$connect->query($sql2);
$rs2->setFetchMode(PDO::FETCH_BOTH);
echo "<table border='1' width='90%'>";
while($row2=$rs2->fetch()){
	echo "<tr>";
	echo "<td>" . $row2[0];
	echo "<td>" . $row2['uid'];
	echo "<td>" . $row2[2];
	echo "<td>" . $row2['username'];
	echo "<td>" . $row2[4];
}
echo "</table>";
echo "<a href='../index.php'>回首頁</a>";
	
?>