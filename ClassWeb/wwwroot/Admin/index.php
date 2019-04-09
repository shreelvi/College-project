<?php
// connection
$db = new PDO('mysql:host=localhost;dbname=bitmphp5051;charset=utf8mb4', 'root', '');

//build query
//$query = "SELECT * FROM `students` ORDER BY id DESC LIMIT 0,5";
$query = "SELECT * FROM `students` ORDER BY id DESC";
//execution
$stmt = $db->query($query);
$students = $stmt->fetchAll(PDO::FETCH_ASSOC); //





$student_courses = 'PHP, HTML';
?>
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <base href="http://localhost/P8/PHP-51/Day-27_CROUD/">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>PHP-50&51 Batch Common session CRUD</title>

    <!-- Bootstrap -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet">

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
            <a class="navbar-brand" href="dashboard.html">Dashboard</a>

        </div>
        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
            <ul class="nav navbar-nav navbar-right">
                <li><a href="student/create.html">Add a Student</a></li>
                <li><a href="course/index.php">View all Courses</a></li>
                <li><a href="course/create.html">Add a Course</a></li>
                <li><a href="assign/create.php">Assign Course</a></li>

            </ul>
        </div><!-- /.navbar-collapse -->
    </div><!-- /.container-fluid -->
</nav>

<div class="container">
    <div class="row">
        <div class=" col-md-offset-3 col-md-6">
            <table class="table table-bordered table-hover">
                <thead>
                    <tr>
                        <th>Sr. No.</th>
                        <th>ID</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>SEIP ID</th>
                        <th>Courses</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                <?php
                    $counter = 1;
                    foreach($students as $student):




                        $query = "SELECT course_id FROM map_students_courses WHERE student_id=".$student['id'];
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
                    <tr>
                        <td><?php echo $counter++;?></td>
                        <td><?php echo $student['id']?></td>
                        <td><?php echo $student['first_name']?></td>
                        <td><?php echo $student['last_name']?></td>
                        <td><?php echo $student['seip']?></td>
                        <td><?php echo $courses;?></td>

                        <td>
                            <a href="student/show.php?id=<?=$student['id']?>">Show</a> |
                            <a href="student/edit.php?id=<?=$student['id']?>">Edit</a> |
                            <a href="student/delete.php?id=<?=$student['id']?>">Delete</a>
                            <a href="assign/edit.php?id=<?=$student['id']?>">Edit Courses</a>
                        </td>
                    </tr>
                <?php
                    endforeach;
                ?>
                </tbody>
            </table>
        </div>
    </div>
</div>






<!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
<!-- Include all compiled plugins (below), or include individual files as needed -->
<script src="assets/js/bootstrap.min.js"></script>
</body>
</html>
