<?php
// connection
$db = new PDO('mysql:host=localhost;dbname=bitmphp5051;charset=utf8mb4', 'root', '');

//build query
//$query = "SELECT * FROM `students` ORDER BY id DESC LIMIT 0,5";
$query = "SELECT * FROM `students` WHERE id =".$_GET['id'];
//execution
$stmt = $db->query($query);
$student = $stmt->fetch(PDO::FETCH_ASSOC); //


$query = "SELECT course_id FROM map_students_courses WHERE student_id=".$_GET['id'];
$stmt = $db->query($query);
$course_ids = $stmt->fetchAll(PDO::FETCH_ASSOC);

/*
 * TODO
 * Join Query
 * IN
 */

$course_titles = [];
foreach($course_ids as $course_id){
    $query = "SELECT title FROM courses WHERE id=".$course_id['course_id'];
    $stmt = $db->query($query);
    $course_titles[] = $stmt->fetch(PDO::FETCH_ASSOC)['title'];
}

$courses = implode(',',$course_titles);

?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>PHP-51 Batch CRUD-01</title>

    <!-- Bootstrap -->
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
    <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>
<body>
<nav class="navbar navbar-default">
    <div class="container">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
            <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#">Brand</a>
        </div>
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav navbar-right">
                <li><a href="index.php">View all students</a></li>
                <li><a href="../course/index.php">View all Courses</a></li>
                <li><a href="../course/create.html">Insert Courses</a></li>
                <li><a href="../assign/create.php">Assign Courses</a></li>

            </ul>
        </div><!-- /.navbar-collapse -->
    </div><!-- /.container-fluid -->
</nav>
<div class="container">
    <div class="row">
        <div class=" col-md-offset-3 col-md-6">
            <dl>

                <dt>ID</dt>
                <dd><?=$student['id']?></dd>

                <dt>Full Name</dt>
                <dd><?php echo $student['first_name'].' '.$student['last_name'];?></dd>

                <dt>SEIP ID</dt>
                <dd><?=$student['seip']?></dd>

                <dt>Courses</dt>
                <dd><?=$courses?></dd>

            </dl>
        </div>
    </div>
</div>


<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="../assets/js/bootstrap.min.js"></script>
</body>
</html>
