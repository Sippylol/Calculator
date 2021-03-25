	<?php 
	//Connect to DB
	$servername = "localhost";
	$dbUsername = "root";
	$dbPassword = "";
	$dbName = "kontooplysninger";
	$connection = mysqli_connect($servername, $dbUsername, $dbPassword, $dbName);

	// Die if fails
	if (!$connection) {
		die("Could not establish connection to database.".mysqli_error());
	}
	?>