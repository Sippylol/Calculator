<?php
try
{
	include_once "connectDB.php";
	date_default_timezone_set('CET');

	// If statement checks if form has been filled
	if (!isset($_POST["email"]) || !isset($_POST["fName"]) || !isset($_POST["lName"]) || !isset($_POST["eName"]) || !isset($_POST["password"]) || !isset($_POST["repeatPassword"]))
	{
		die(header("Location: signUpStudentError.php"));//die("Please fill in the form to continue.");
	}
	// Checks if password and repeatPassword matches
	if ($_POST["password"] != $_POST["repeatPassword"])
	{
		die(header("Location: signUpStudentError.php"));//die("Passwords do not match.");
	}

	$email_check = $_POST["email"];
	// Checks if email already exists in database
	$sql_check_email = "SELECT Email FROM student_userinfo WHERE Email = '$email_check';";
	$result_check_email = mysqli_query($connection, $sql_check_email);

	if (mysqli_num_rows($result_check_email) > 0)
	{
		die(header("Location: signUpStudentError.php"));//die("Email already exists in database. Try logging in!");
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
		die(header("Location: signUpStudentError.php"));//die("The inputs contain contains harmful characters!");
	}

	// Now check if input has enough characters
	if ($email == "" || $password == "" || $fName == "" || $lName == "" || $eName == "" || strlen($password) < 8 || strlen($email) < 10 || strlen($fName) < 2 || strlen($lName) < 2 || strlen($eName) < 2)
	{
		die(header("Location: signUpStudentError.php"));//die("First, last, and education name must be at least 2 characters, password must be at least 8 characters, and email must be at least 10 characters.");
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
		die(header("Location: signUpStudentError.php"));//die("Error: ".$sql_create_account."<br>".mysqli_error($connection));
	}

	mysqli_close($connection);
}

catch(Exception $e)
{
	echo("Something went wrong!");
}
?>