$rs2=$connect->query($sql2);
$rs2->setFetchMode (PDO::FETCH_BOTH);
while($row2=$rs2->fetch()) {
	?>
	<div class="card">
	<form action="mOrder.php" method="post">
		<img src="../images/<?php echo $row2['mImg'];?>" class="card-img-top" alt="...">
		<div class="card-body">
			<h5 class="card-title"><?php echo $row2['mName'];?></h5>
			<p_class="card-text"><?php echo $row['mDetail'];?></p>
			數量:<select name='mQty []' class="form-select form-select-lg mb-3" aria-label=".form-select-lg example">
				<?php
				$i=1;
				while ($i<=10) {
					if ($i==2) {
						echo "<option value='$i' selected>$i\n";
					} else {
						echo "<option value='$1'>$i\n";
					}
					$i++;
				}
				?>
			</select>
			<p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
		</div>
	</div>
	<?php
	} ?>
</div>
<input type="submit" class="btn btn-primary" value="Submit">
</form>
</body>
</html>