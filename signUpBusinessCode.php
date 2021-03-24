<?php
try
{
	include_once "connectDB.php";
	date_default_timezone_set('CET');

	// If statement checks if form has been filled
	if (!isset($_POST["email"]) || !isset($_POST["fName"]) || !isset($_POST["lName"]) || !isset($_POST["bName"]) || !isset($_POST["password"]) || !isset($_POST["repeatPassword"]))
	{
		die("Please fill in the form to continue.");
	}
	// Checks if password and repeatPassword matches
	if ($_POST["password"] != $_POST["repeatPassword"])
	{
		die("Passwords do not match.");
	}

	$email_check = $_POST["email"];
	// Checks if email already exists in database
	$sql_check_email = "SELECT Email FROM business_userinfo WHERE Email = '$email_check';";
	$result_check_email = mysqli_query($connection, $sql_check_email);

	if (mysqli_num_rows($result_check_email) > 0)
	{
		die("Email already exists in database. Try logging in!");
	}

	// Define input as variables & remove harmful characters
	$creation_date = date("Y-m-d h:i:s");
	$fName = strip_tags(trim($_POST["fName"]));
	$lName = strip_tags(trim($_POST["lName"]));
	$bName = strip_tags(trim($_POST["bName"]));
	$email = strip_tags(trim($_POST["email"]));
	$password = strip_tags(trim($_POST["password"]));

	// Array of characters to further check input for
	$remove = array("'", '"', ";", "=", ">", "/", "\\", "&", "|", "*", "%", " ");

	// Check input for harmful characters
	if (str_replace($remove, "", $fName) != $fName || str_replace($remove, "", $lName) != $lName || str_replace($remove, "", $bName) != $bName || str_replace($remove, "", $email) != $email || str_replace($remove, "", $password) != $password)
	{
		die("The inputs contain contains harmful characters!");
	}

	// Now check if input has enough characters
	if ($email == "" || $password == "" || $fName == "" || $lName == "" || $bName == "" || count_chars($password) < 8 || count_chars($email) < 10 || count_chars($fName) < 2 || count_chars($lName) < 2 || count_chars($bName) < 2)
	{
		die("First, last, and education name must be at least 2 characters, password must be at least 8 characters, and email must be at least 10 characters.");
	}

	// Hash password
	$password = password_hash($password, PASSWORD_DEFAULT);

	// SQL query is created
	$sql_create_account = "INSERT INTO business_userinfo(Email, first_name, last_name, Business_name, password, creation_date) VALUES ('$email', '$fName', '$lName', '$bName', '$password', '$creation_date');";

	//Query executes
	$result_create_account = mysqli_query($connection, $sql_create_account);

	//If succeeds
	if ($result_create_account)
	{
		// Go to Log In Page
		header("Location: logInBusiness.php");
		exit;
	}
	else
	{
		die("Error: ".$sql_create_account."<br>".mysqli_error($connection));
	}

	mysqli_close($connection);
}

catch(Exception $e)
{
	echo("Something went wrong!");
}
?>