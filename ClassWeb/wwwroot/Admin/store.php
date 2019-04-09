<?php
//print_r($_POST);

//connecting with database
$db = new PDO('mysql:host=localhost;dbname=bitmphp5051;charset=utf8mb4', 'root', '');

//build the query

$query = "INSERT INTO `students` ( `first_name`, `last_name`, `seip`)
VALUES ( '".$_POST['first_name']."', '".$_POST['last_name']."', '".$_POST['seip']."')";
//echo $query;

// execute query

$result = $db->exec($query);
if($result){
    header("location:index.php");
    echo "Data has been saved sucessfully.";
}else{
    echo "There is an error. Please try again later.";
}
