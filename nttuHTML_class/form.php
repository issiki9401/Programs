<?php
error_reporting(E_ALL & ~E_NOTICE & ~E_WARNING);
echo "姓:".$_POST["fname"]."<br>";
echo "名:".$_POST["lname"]."<br>";
echo "3+4=".$_POST["q1"]."<br>";
echo "密碼:".$_POST["pw"]."<br>";

$ranking2=$_POST["ranking"];
foreach($ranking2 as $k => $v){
	echo $k."=".$v."<br>";
}
?>