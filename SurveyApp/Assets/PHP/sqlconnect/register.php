<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

$con = mysqli_connect('localhost', 'root', 'root', 'survey_app');
if (mysqli_connect_error()) {
    echo "1: Connection failed";
    exit();
}

// Check if POST data exists
if (!isset($_POST['username']) || !isset($_POST['password'])) {
    echo "2: Missing POST data";
    exit();
}

$username = $_POST['username'];
$password = $_POST['password'];

// Check if username already exists
$usernamecheckquery = "SELECT username FROM user WHERE username = ?";
$stmt = $con->prepare($usernamecheckquery);
$stmt->bind_param("s", $username);
$stmt->execute();
$usernamecheck = $stmt->get_result();

if ($usernamecheck->num_rows > 0) {
    echo "3: Username already exists";
    exit();
}

// Hash the password
$salt = "\$5\$rounds=5000\$" . "steamedhams" . $username . "\$";
$hash = crypt($password, $salt);

// Insert new user
$insertuserquery = "INSERT INTO user (username, hash, salt) VALUES (?, ?, ?)";
$stmt = $con->prepare($insertuserquery);
$stmt->bind_param("sss", $username, $hash, $salt);
$stmt->execute();

if ($stmt->affected_rows > 0) {
    echo "0";
} else {
    echo "4: Insert user query failed";
}

$stmt->close();
$con->close();
?>