<!DOCTYPE html>
<html>
<head>
	<link rel="stylesheet" type="text/css" href="stylesheet.css">
	<title>Sign Up</title>
</head>
<body>
	<h2>Student Account</h2><h1>Log In</h1>
	
	<form id="centered" action="logInStudentCode.php" method="POST">
		<input class="textBoxLogIn" type="text" name="email" placeholder="Email" maxlength="40"> <br><br><br>
		<input class="textBoxLogIn" type="password" name="password" placeholder="Password" maxlength="20"> <br><br><br>
		<input id="submitButtonLogIn" type="submit" value="Log In">
	</form>

	<div id="errorBox">
		<p id="errorText">Invalid email or password! Please try again.</p>
	</div>

</body>
</html>