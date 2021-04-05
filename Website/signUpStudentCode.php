<?php
try
{
	include_once "connectDB.php";
	date_default_timezone_set('CET');

	// If statement checks if form has been filled
	if (!isset($_POST["email"]) || !isset($_POST["fName"]) || !isset($_POST["lName"]) || !isset($_POST["eName"]) || !isset($_POST["password"]) || !isset($_POST["repeatPassword"]))
	{
		die(header("Location: signUpStudentError.php"));
	}
	// Checks if password and repeatPassword matches
	if ($_POST["password"] != $_POST["repeatPassword"])
	{
		die(header("Location: signUpStudentError.php"));
	}

	$email_check = $_POST["email"];
	// Checks if email already exists in database
	$sql_check_email = "SELECT Email FROM student_userinfo WHERE Email = '$email_check';";
	$result_check_email = mysqli_query($connection, $sql_check_email);

	if (mysqli_num_rows($result_check_email) > 0)
	{
		die(header("Location: signUpStudentError.php"));
	}

	// Define input as variables & remove harmful characters
	$creation_date = date("Y-m-d h:i:s");
	$fName = strip_tags(trim($_POST["fName"]));
	$lName = strip_tags(trim($_POST["lName"]));
	$eName = strip_tags(trim($_POST["eName"]));
	$email = strip_tags(trim($_POST["email"]));
	$password = strip_tags(trim($_POST["password"]));

	// Array of characters to further check input for
	$remove = array("'", '"', ";", "=", ">", "/", "\\", "&", "|", "*", "%", " ");

	// Check input for harmful characters
	if (str_replace($remove, "", $fName) != $fName || str_replace($remove, "", $lName) != $lName || str_replace($remove, "", $eName) != $eName || str_replace($remove, "", $email) != $email || str_replace($remove, "", $password) != $password)
	{
		die(header("Location: signUpStudentError.php"));
	}

	// Now check if input has enough characters
	if ($email == "" || $password == "" || $fName == "" || $lName == "" || $eName == "" || strlen($password) < 8 || strlen($email) < 10 || strlen($fName) < 2 || strlen($lName) < 2 || strlen($eName) < 2)
	{
		die(header("Location: signUpStudentError.php"));
	}

	// Hash password
	$password = password_hash($password, PASSWORD_DEFAULT);

	// SQL query is created
	$sql_create_account = "INSERT INTO student_userinfo(Email, first_name, last_name, Education, password, creation_date) VALUES ('$email', '$fName', '$lName', '$eName', '$password', '$creation_date');";

	//Query executes
	$result_create_account = mysqli_query($connection, $sql_create_account);

	//If succeeds
	if ($result_create_account)
	{
		// Go to Log In Page
		header("Location: logInStudent.php");
		exit;
	}
	else
	{
		die(header("Location: signUpStudentError.php"));
	}

	mysqli_close($connection);
}

catch(Exception $e)
{
	echo("Something went wrong!");
}
?>