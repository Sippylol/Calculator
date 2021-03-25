<?php 
try
{
	include_once "connectDB.php";

	// Checks if input != null
	if (!isset($_POST["email"]) || !isset($_POST["password"]))
	{
		die(header("Location: logInPrivateError.php"));//die("One or more of the requirements was not fulfilled!");
	}

	// Array of characters to check input for
	$remove = array("'", '"', ";", "=", ">", "/", "\\", "&", "|", "*", "%", " ");

	// Remove harmful characters
	$email = str_replace($remove, "", strip_tags(trim($_POST["email"])));
	$password = str_replace($remove, "", strip_tags(trim($_POST["password"])));

	// Check if input is valid
	if ($email == "" || $password == "" || strlen($password) < 8 || strlen($email) < 10)
	{
		die(header("Location: logInPrivateError.php"));//die("The inputs contain contains harmful characters!");
	}

	// Check if email input exists in database
	$sql_check_email = "SELECT password, PrivateUserID FROM private_userinfo WHERE Email = '$email';";
	$result = mysqli_query($connection, $sql_check_email);
	if (mysqli_num_rows($result) <= 0)
	{
		die(header("Location: logInPrivateError.php"));//die("This email doesn't exist! Try to sign up instead");
	}

	$info = mysqli_fetch_array($result);

	if (password_verify($password, $info["password"]))
	{
		$_SESSION["PrivateUserID"] = $info["PrivateUserID"];

		// Get First name of user
		$sql_get_fName = "SELECT first_name FROM private_userinfo WHERE Email = '$email';";
		$result_fName = mysqli_query($connection, $sql_get_fName);
		$fName = mysqli_fetch_assoc($result_fName);

		// User is now logged in
		die("<h1>Welcome ".implode($fName).".</h1>");
	}
	else
	{
		die(header("Location: logInPrivateError.php"));//die("<h1>The password is incorrect!</h1>");
	}
}
catch(Exception $e)
{
	die(header("Location: logInPrivateError.php"));//die("Something went wrong!");
}
?>