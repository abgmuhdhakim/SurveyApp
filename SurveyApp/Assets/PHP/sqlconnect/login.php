<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

// Database connection
$con = new mysqli('localhost', 'root', 'root', 'survey_app');
if ($con->connect_error) {
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

// Fetch user data from the database
$query = "SELECT username, role, salt, hash FROM user WHERE username = ?";
$stmt = $con->prepare($query);
$stmt->bind_param("s", $username);
$stmt->execute();
$result = $stmt->get_result();

if ($result->num_rows == 0) {
    echo "3: Username doesn't exist";
    exit();
}

$logininfo = $result->fetch_assoc();

// Verify the password
$salt = $logininfo['salt'];
$hash = $logininfo['hash'];
$loginhash = crypt($password, $salt);

if ($hash != $loginhash) {
    echo "4: Incorrect password";
    exit();
}

// Login successful
echo "0\t" . $logininfo['role']; // Use tab as delimiter

$stmt->close();
$con->close();
?>