<html>
<head>
	<link rel="stylesheet" type="text/css" href="stylesheet.css">
	<title>Sign Up</title>
</head>
<body>
	<h2>Private Account</h2><h1>Sign Up</h1>

	<form id="centered" name="signUpForm" method="POST" action="signUpPrivateCode.php">
	<input class="textBox" type="text" name="email" placeholder="Email" maxlength="40"> <br><br><br>
	<input class="textBox" type="text" name="fName" placeholder="First Name" maxlength="20"> <br><br><br>
	<input class="textBox" type="text" name="lName" placeholder="Last Name" maxlength="20"> <br><br><br>
	<input class="textBox" type="password" name="password" placeholder="Password" maxlength="20"> <br><br><br>
	<input class="textBox" type="password" name="repeatPassword" placeholder="Repeat Password" maxlength="20"><br><br>
	<input id="submitButton" type="submit" value="Create Account">
	</form>
</body>
</html>