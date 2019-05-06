-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 27, 2019 at 09:47 PM
-- Server version: 10.1.37-MariaDB
-- PHP Version: 7.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `sapkgane`
--

DELIMITER $$
--
-- Procedures
--
CREATE  PROCEDURE `AddGroup` (IN `g_Name` VARCHAR(50), IN `g_EmailAddress` VARCHAR(50), IN `g_UserName` VARCHAR(50), IN `g_Password` CHAR(128), IN `g_Salt` CHAR(128), OUT `g_ID` INT)  BEGIN 
INSERT INTO groups(Name, EmailAddress, Username, Password, Salt) values (g_Name, g_EmailAddress, g_UserName,g_Password, g_Salt); 
SET g_ID = LAST_INSERT_ID();
END$$

CREATE  PROCEDURE `get_GroupByUserName` (IN `username` VARCHAR(128))  BEGIN 
SELECT * from groups g where g.Username = username; 
END$$

CREATE  PROCEDURE `sproc_AddUser` (OUT `UserID` INT, IN `FirstName` VARCHAR(45), IN `MiddleName` VARCHAR(45), IN `LastName` VARCHAR(45), IN `EmailAddress` VARCHAR(128), IN `UserName` VARCHAR(128), IN `Password` CHAR(64), IN `Salt` CHAR(50), IN `RoleID` INT, IN `DirectoryPath` VARCHAR(256))  BEGIN
     INSERT INTO Users(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, RoleID, DirectoryPath)
     VALUES(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, RoleID, DirectoryPath);
SET UserID = LAST_INSERT_ID();
END$$

CREATE  PROCEDURE `sproc_AddUserToGroup` (OUT `GroupUserID` INT, IN `GroupID` INT(11), IN `UserID` INT(11))  BEGIN
	INSERT INTO GroupsUsers(GroupID, UserID)
    			VALUES (GroupID, UserID);
    SET GroupUserID = LAST_INSERT_ID();
END$$

CREATE  PROCEDURE `sproc_AssignmentAdd` (OUT `AssignmentID` INT, IN `Name` VARCHAR(45), IN `Feedback` VARCHAR(128), IN `UserID` INT)  BEGIN
     INSERT INTO assignment(Name, Feedback, UserID)
     VALUES(Name, Feedback, UserID);
SET AssignmentID = LAST_INSERT_ID();
END$$

CREATE  PROCEDURE `sproc_AssignmentDeleteByID` (IN `ID` INT)  BEGIN
DELETE FROM assignment
Where Assignment.ID=`ID`;
END$$

CREATE  PROCEDURE `sproc_AssignmentGetAllByFileName` (IN `FileName` VARCHAR(45))  BEGIN
Select * from assignment
where  assignment.FileName=`FileName`;
End$$

CREATE  PROCEDURE `sproc_AssignmentGetAllByUserName` (IN `UserName` VARCHAR(45))  BEGIN
Select * from assignment
where assignment.UserName=`UserName`;
End$$

CREATE  PROCEDURE `sproc_AssignmentGetAllByUserNameAndLocation` (IN `UserName` VARCHAR(45), IN `FileLocation` VARCHAR(50))  BEGIN
Select * from assignment
where assignment.UserName=`UserName` AND assignment.FileLocation like CONCAT(`FileLocation` , '%');
End$$

CREATE  PROCEDURE `sproc_AssignmentGetByID` (IN `ID` INT)  BEGIN
Select * from assignment
where assignment.ID=`ID`;
End$$

CREATE PROCEDURE `sproc_AssignmentGetByLocation` (IN `FileLocation` VARCHAR(100))  BEGIN
Select * from assignment
where FileLocation LIKE assignment.FileLocation;
End$$

CREATE PROCEDURE `sproc_AssignmentGetByNameLocationUserName` (IN `FileName` VARCHAR(50), IN `FileLocation` VARCHAR(45), IN `UserName` VARCHAR(50))  BEGIN
Select * from assignment
where assignment.FileLocation=`FileLocation`&&assignment.FileName=`FileName`&&assignment.UserName=`UserName`;
End$$

CREATE  PROCEDURE `sproc_AssignmentResubmit` (IN `ID` INT, IN `Feedback` VARCHAR(45))  BEGIN
update Assignment
set Assignment.Feedback=Feedback
where Assignment.ID=ID;
End$$

CREATE PROCEDURE `sproc_CheckUserByEmail` (IN `EmailAddress` VARCHAR(128))  BEGIN
    SET @User_id = 0;
    SELECT Users.UserID INTO @User_id
    FROM `Users`
    WHERE Users.`EmailAddress` = `EmailAddress`;
    SELECT @User_id;
END$$

CREATE PROCEDURE `sproc_CheckUserName1` (IN `Username1` VARCHAR(128))  BEGIN
    SET @User_exists = 0;
    SELECT 1 INTO @User_exists
    FROM `Users`
    WHERE Users.`UserName` = `Username1`;
    SELECT @User_exists;
END$$

CREATE PROCEDURE `sproc_ClassesGetAll` ()  BEGIN
select * From Classes;
END$$

CREATE PROCEDURE `sproc_CourseAdd` (OUT `CourseID` INT, IN `CourseTitle` VARCHAR(45), IN `CourseName` VARCHAR(45), IN `CourseDescription` VARCHAR(128))  BEGIN
     INSERT INTO courses(CourseTitle, CourseName, CourseDescription)
               VALUES(CourseTitle, CourseName, CourseDescription);               
     SET CourseID = LAST_INSERT_ID();
END$$

CREATE PROCEDURE `sproc_CourseSemesterEdit` (IN `CourseID` INT(11), IN `SemesterID` INT(11), IN `YearID` INT(11), IN `SectionID` INT(11), IN `UserID` INT(11))  BEGIN
     UPDATE Sections
          SET
               CourseSemesters.CourseID = CourseID,
			   CourseSemesters.SemesterID = SemesterID,
			   CourseSemesters.YearID = YearID,
			   CourseSemesters.SectionID = SectionID,
			   CourseSemesters.CourseID = CourseID,
               CourseSemesters.UserID = UserID
          WHERE CourseSemesters.CourseSemesterID = CourseSemesterID;
END$$

CREATE PROCEDURE `sproc_CourseSemesterGet` (IN `CourseSemesterID` INT)  BEGIN
     SELECT * FROM coursesemesters
     WHERE coursesemesters.CourseSemesterID = CourseSemesterID;
END$$

CREATE PROCEDURE `sproc_CourseSemesterRemove` (IN `CourseSemesterID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM coursesemesters
          WHERE coursesemesters.CourseSemesterID = CourseSemesterID;

     -- SELECT -1 if we had an error
END$$

CREATE PROCEDURE `sproc_CourseSemestersGetAll` ()  BEGIN
     SELECT * FROM CourseSemesters;
END$$

CREATE  PROCEDURE `sproc_CreateCourse` (OUT `CourseID` INT, IN `Subject` VARCHAR(50), IN `CourseNumber` INT, IN `CourseTitle` VARCHAR(50))  BEGIN
     INSERT INTO course(Subject, CourseNumber, CourseTitle)
		VALUES(Subject, CourseNumber, CourseTitle);
	SET CourseID = last_insert_id();
END$$

CREATE PROCEDURE `sproc_DeleteCourseByID` (IN `ID` INT)  BEGIN 
	DELETE FROM course 
		WHERE Course.ID = `ID`; 
END$$

CREATE PROCEDURE `sproc_GetAllAssignment` ()  BEGIN
Select * From assignment;
END$$

CREATE PROCEDURE `sproc_GetAllCourses` ()  BEGIN
     SELECT * FROM course;
END$$

CREATE PROCEDURE `sproc_GetAllUsers` ()  BEGIN
	 SELECT * FROM Users;
END$$

CREATE PROCEDURE `sproc_GetAssignmentsbyUserID` (IN `UserID` INT)  BEGIN 
	SELECT * FROM assignment 
	WHERE assignment.UserID = UserID 
	ORDER BY assignment.AssignmentID DESC; 
END$$

CREATE PROCEDURE `sproc_GetCourse` (IN `CourseID` INT)  BEGIN
     SELECT * FROM courses
     WHERE courses.CourseID = CourseID;
END$$

CREATE PROCEDURE `sproc_GetSaltForUser` (IN `Username` VARCHAR(256))  BEGIN
SELECT Salt FROM Users
WHERE Users.UserName = Username;
END$$

CREATE  PROCEDURE `sproc_GetUsersFromGroup` (IN `GroupID` INT)  BEGIN
    SELECT groups.id, users.UserID, users.FirstName, users.EmailAddress
    FROM groups
    INNER JOIN groupsusers
    ON groups.id = groupsusers.GroupID
    INNER JOIN users
    ON groupsusers.UserID = users.UserID
    WHERE groups.id = GroupID;
END$$

CREATE PROCEDURE `sproc_GetUsersWithRoles` (IN `UserID` INT(11))  BEGIN
    SELECT u.UserName, u.RoleID, r.Name, r.IsAdmin, r.users, r.Role, r.Assignment 
	From Users u
    JOIN Roles r ON u.RoleID = r.RoleID
    WHERE u.UserID = UserID;
    END$$

CREATE PROCEDURE `sproc_ResubmitAssignment` (IN `ID` INT, IN `Feedback` VARCHAR(45))  BEGIN
update Assignment
set Assignment.Feedback=Feedback
where Assignment.ID=ID;
End$$

CREATE PROCEDURE `sproc_RoleAdd` (OUT `RoleID` INT, IN `Name` NVARCHAR(45), IN `IsAdmin` BIT(1), IN `Users` BIT(4), IN `Role` BIT(4), IN `Assignment` BIT(4), IN `Course` BIT(4), IN `Semester` BIT(4), IN `Year` BIT(4), IN `Section` BIT(4), IN `CourseSemester` BIT(4))  BEGIN
     INSERT INTO Roles(Name,IsAdmin,Users,Role,Assignment, Course, Semester, Year, Section, CourseSemester)
               VALUES(Name,IsAdmin,Users,Role, Assignment, Course, Semester, Year, Section, CourseSemester);               
     SET RoleID = LAST_INSERT_ID;
END$$

CREATE PROCEDURE `sproc_RoleDeleteByID` (IN `ID` INT)  BEGIN
delete from Users
where Users.ID=`ID`;
END$$

CREATE PROCEDURE `sproc_RoleGet` (IN `RoleID` INT)  BEGIN
     SELECT * FROM Roles
     WHERE Roles.RoleID = RoleID;
END$$

CREATE PROCEDURE `sproc_RoleRemove` (IN `RoleID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Roles
          WHERE Roles.RoleID = RoleID;

     -- SELECT -1 if we had an error
END$$

CREATE PROCEDURE `sproc_RolesGetAll` ()  BEGIN
     SELECT * FROM Roles;
END$$

CREATE PROCEDURE `sproc_RoleUpdate` (IN `ID` INT, IN `Name` VARCHAR(45), IN `IsAdmin` BIT(1), IN `Users` BIT(4), IN `Role` BIT(4), IN `Assignment` BIT(4))  BEGIN
     UPDATE Roles
          SET
               Roles.`Name` = `Name`,
               Roles.`IsAdmin` = `IsAdmin`,
               Roles.`Users` = `Users`,
               Roles.`Role` = `Role`,
               Roles.`Assignment` = `Assignment`
          WHERE Roles.`ID` = `ID`;
END$$

CREATE PROCEDURE `sproc_SectionAdd` (OUT `SectionID` INT, IN `CRN` INT(11), IN `Number` INT(45), IN `UserID` INT(11), IN `CourseID` INT(11))  BEGIN
     INSERT INTO Sections(CRN,Number, UserID, CourseID)
               VALUES(CRN,Number, UserID, CourseID);               
     SET SectionID = LAST_INSERT_ID();
END$$

CREATE PROCEDURE `sproc_SectionGet` (IN `SectionID` INT)  BEGIN
     SELECT * FROM Sections
     WHERE Sections.SectionID = SectionID;
END$$

CREATE PROCEDURE `sproc_SectionRemove` (IN `RoleID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Sections
          WHERE Sections.SectionID = SectionID;

     -- SELECT -1 if we had an error
END$$

CREATE PROCEDURE `sproc_SectionsGetAll` ()  BEGIN
     SELECT * FROM Sections;
END$$

CREATE PROCEDURE `sproc_SectionUpdate` (IN `SectionID` INT(11), IN `CRN` INT(11), IN `Number` INT(45), IN `UserID` INT(11), IN `CourseID` INT(11))  BEGIN
     UPDATE Sections
          SET
               Sections.CRN = CRN,
               Sections.Number = Number,
               Sections.UserID = UserID,
			   Sections.CourseID = CourseID
          WHERE Sections.SectionID = SectionID;
END$$

CREATE PROCEDURE `sproc_SemesterAdd` (OUT `SemesterID` INT, IN `SemesterName` VARCHAR(128))  BEGIN
     INSERT INTO Semesters(SemesterName)
               VALUES(SemesterName);               
     SET SemesterID = LAST_INSERT_ID();
END$$

CREATE PROCEDURE `sproc_SemesterEdit` (IN `SemesterID` INT, IN `SemesterName` VARCHAR(128))  BEGIN
     UPDATE Semesters
          SET
               Semesters.SemesterName = SemesterName
			  
          WHERE Semesters.SemesterID = SemesterID;
END$$

CREATE PROCEDURE `sproc_SemesterGet` (IN `SemesterID` INT)  BEGIN
     SELECT * FROM semesters
     WHERE semesters.SemesterID = SemesterID;
END$$

CREATE PROCEDURE `sproc_SemesterGetAll` ()  BEGIN
     SELECT * FROM Semesters;
END$$

CREATE PROCEDURE `sproc_SemesterRemove` (IN `SemesterID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM semesters
          WHERE semesters.SemesterID = SemesterID;

     -- SELECT -1 if we had an error
END$$

CREATE PROCEDURE `sproc_SetSaltForUser` (IN `UserID` INT, IN `Salt` CHAR)  BEGIN
UPDATE Users
SET Users.Salt = Salt
WHERE Users.UserID = UserID;
END$$

CREATE PROCEDURE `sproc_UpdateCourse` (IN `CourseID` INT, IN `Subject` INT, IN `CourseName` VARCHAR(50), IN `CourseTitle` VARCHAR(50))  BEGIN
     UPDATE course
          SET
               course.Subject = Subject,
               course.CourseTitle = CourseTitle,
               course.CourseName = CourseName
          WHERE course.CourseID = CourseID;
END$$

CREATE PROCEDURE `sproc_UserAdd` (OUT `ID` INT, IN `FirstName` VARCHAR(50), IN `MiddleName` VARCHAR(45), IN `LastName` VARCHAR(50), IN `EmailAddress` VARCHAR(50), IN `UserName` VARCHAR(50), IN `Password` VARCHAR(100), IN `Salt` VARCHAR(50))  BEGIN
INSERT INTO Users (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`)
 VALUES (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`);
     SET ID = last_insert_id();
End$$

CREATE PROCEDURE `sproc_UserByID` (IN `id` INT)  BEGIN
Select * from users
Where users.ID=id;
END$$

CREATE PROCEDURE `sproc_UserDeleteByID` (IN `ID` INT)  BEGIN
delete from Users
where Users.ID=`ID`;
END$$

CREATE PROCEDURE `sproc_UserGetAll` ()  BEGIN
Select * from users;
END$$

CREATE PROCEDURE `sproc_UserGetByEmailAddress` (IN `u_EmailAddress` VARCHAR(50))  BEGIN 
	SELECT a.FirstName, a.MiddleName, a.LastName, a.EmailAddress, a.Address, a.UserName, a.PhoneNumber, b.RoleID, b.Title FROM user u
	Inner JOIN userrole c on u.id = c.UserID
	LEFT OUTER JOIN role b on c.UserID = b.ID
	WHERE a.EmailAddress = u_EmailAddress; 
END$$

CREATE PROCEDURE `sproc_UserGetByID` (IN `ID` INT)  BEGIN
Select * From Users
Where Users.ID=ID;
END$$

CREATE PROCEDURE `sproc_UserGetByUsername` (IN `Username` VARCHAR(128))  BEGIN
	 SELECT * FROM Users
	 WHERE Users.UserName = Username;
END$$

CREATE PROCEDURE `sproc_UserPasswordUpdate` (IN `ID` INT, IN `Password` VARCHAR(70))  BEGIN UPDATE users
SET users.Password = `Password`
WHERE users.ID=`ID`;
END$$

CREATE PROCEDURE `sproc_UserRoleUpdate` (IN `RoleID` INT, IN `Name` NVARCHAR(45), IN `IsAdmin` BIT(1), IN `Users` BIT(4), IN `Role` BIT(4), IN `Assignment` BIT(4), IN `Course` BIT(4), IN `Semester` BIT(4), IN `Year` BIT(4), IN `Section` BIT(4), IN `CourseSemester` BIT(4))  BEGIN
     UPDATE Roles
          SET
               Roles.Name = Name,
               Roles.IsAdmin = IsAdmin,
               Roles.Users = Users,
               Roles.Role = Role,
               Roles.Assignment = Assignment
          WHERE Roles.RoleID = RoleID;
END$$

CREATE PROCEDURE `sproc_UserUpdate` (IN `ID` INT, IN `FirstName` VARCHAR(50), IN `LastName` VARCHAR(50), IN `UserName` VARCHAR(50), IN `ResetCode` VARCHAR(50))  BEGIN 
UPDATE users
SET users.FirstName = `FirstName`,
users.LastName = `LastName`,users.UserName = `UserName`,users.DateModified= NOW(),
users.ResetCode=`ResetCode`
WHERE users.ID=`ID`;
END$$

CREATE PROCEDURE `sproc_YearAdd` (OUT `YearID` INT, IN `YEAR` INT(11))  BEGIN
     INSERT INTO Years(Year)
               VALUES(Year);               
     SET YearID = LAST_INSERT_ID();
END$$

CREATE PROCEDURE `sproc_YearEdit` (IN `YearID` INT, IN `Year` INT(11))  BEGIN
     UPDATE Years
          SET
               Years.Year = Year
			  
          WHERE Years.YearID = YearID;
END$$

CREATE PROCEDURE `sproc_YearGet` (IN `YearID` INT)  BEGIN
     SELECT * FROM Years
     WHERE Years.YearID = YearID;
END$$

CREATE PROCEDURE `sproc_YearGetAll` ()  BEGIN
     SELECT * FROM Years;
END$$

CREATE PROCEDURE `sproc_YearRemove` (IN `YearID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM years
          WHERE years.YearID = YearID;

     -- SELECT -1 if we had an error
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `assignment`
--

CREATE TABLE `assignment` (
  `AssignmentID` int(11) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  `DateDue` datetime DEFAULT NULL,
  `DateSubmission` datetime DEFAULT CURRENT_TIMESTAMP,
  `Grade` char(10) DEFAULT NULL,
  `Feedback` varchar(128) DEFAULT NULL,
  `DateModified` datetime DEFAULT CURRENT_TIMESTAMP,
  `Size` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `classes`
--

CREATE TABLE `classes` (
  `ID` int(11) NOT NULL,
  `Title` varchar(64) DEFAULT NULL,
  `Availability` tinyint(1) DEFAULT NULL,
  `DateStart` datetime DEFAULT NULL,
  `DateEnd` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `CourseID` int(11) NOT NULL,
  `CourseTitle` varchar(45) NOT NULL,
  `CourseName` varchar(45) DEFAULT NULL,
  `CourseDescription` varchar(128) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`CourseID`, `CourseTitle`, `CourseName`, `CourseDescription`) VALUES
(1, 'INFO 4407', 'Database Design', 'INFO Database Design');

-- --------------------------------------------------------

--
-- Table structure for table `coursesemesters`
--

CREATE TABLE `coursesemesters` (
  `CourseSemesterID` int(11) NOT NULL,
  `CourseID` int(11) NOT NULL DEFAULT '1',
  `SemesterID` int(11) NOT NULL DEFAULT '1',
  `YearID` int(11) NOT NULL DEFAULT '1',
  `SectionID` int(11) NOT NULL DEFAULT '1',
  `UserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `groups`
--

CREATE TABLE `groups` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) DEFAULT NULL,
  `EmailAddress` varchar(64) DEFAULT NULL,
  `Username` varchar(128) DEFAULT NULL,
  `Password` varchar(64) DEFAULT NULL,
  `ResetCode` varchar(128) DEFAULT NULL,
  `Salt` char(128) DEFAULT NULL,
  `DirectoryPath` varchar(264) DEFAULT NULL,
  `AssignmentID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `groupsusers`
--

CREATE TABLE `groupsusers` (
  `GroupUserID` int(11) NOT NULL,
  `GroupID` int(11) NOT NULL DEFAULT '1',
  `UserID` int(11) NOT NULL DEFAULT '1'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `permissions`
--

CREATE TABLE `permissions` (
  `ID` int(11) NOT NULL,
  `Title` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `DateDeleted` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `resources`
--

CREATE TABLE `resources` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `DateUploaded` datetime DEFAULT NULL,
  `ResourceSize` int(11) DEFAULT NULL,
  `MaxSize` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `RoleID` int(11) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `IsAdmin` bit(1) DEFAULT NULL,
  `Users` bit(4) DEFAULT NULL,
  `Role` bit(4) DEFAULT NULL,
  `
Assignment` bit(4) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `sections`
--

CREATE TABLE `sections` (
  `SectionID` int(11) NOT NULL,
  `CRN` int(11) NOT NULL,
  `Number` int(45) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `CourseID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `semsters`
--

CREATE TABLE `semsters` (
  `SemesterID` int(11) NOT NULL,
  `SemesterName` varchar(128) CHARACTER SET utf8 NOT NULL DEFAULT 'FALL'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserID` int(11) NOT NULL,
  `FirstName` varchar(45) DEFAULT NULL,
  `MiddleName` varchar(45) DEFAULT NULL,
  `LastName` varchar(45) DEFAULT NULL,
  `EmailAddress` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `UserName` varchar(128) DEFAULT NULL,
  `Password` char(64) DEFAULT NULL,
  `Salt` char(50) DEFAULT NULL,
  `RoleID` int(11) DEFAULT '2',
  `DirectoryPath` varchar(128) DEFAULT NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP,
  `DateModified` datetime DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `years`
--

CREATE TABLE `years` (
  `YearID` int(11) NOT NULL,
  `Year` int(11) NOT NULL DEFAULT '2019'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assignment`
--
ALTER TABLE `assignment`
  ADD PRIMARY KEY (`AssignmentID`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`CourseID`);

--
-- Indexes for table `coursesemesters`
--
ALTER TABLE `coursesemesters`
  ADD PRIMARY KEY (`CourseSemesterID`),
  ADD KEY `CourseSemesters_idx` (`CourseID`),
  ADD KEY `Semesters` (`SemesterID`),
  ADD KEY `Years` (`YearID`),
  ADD KEY `Sections` (`SectionID`),
  ADD KEY `Users` (`UserID`);

--
-- Indexes for table `groupsusers`
--
ALTER TABLE `groupsusers`
  ADD PRIMARY KEY (`GroupUserID`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`RoleID`);

--
-- Indexes for table `sections`
--
ALTER TABLE `sections`
  ADD PRIMARY KEY (`SectionID`);

--
-- Indexes for table `semsters`
--
ALTER TABLE `semsters`
  ADD PRIMARY KEY (`SemesterID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserID`),
  ADD KEY `Users_Roles_idx` (`RoleID`);

--
-- Indexes for table `years`
--
ALTER TABLE `years`
  ADD PRIMARY KEY (`YearID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `assignment`
--
ALTER TABLE `assignment`
  MODIFY `AssignmentID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `CourseID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `coursesemesters`
--
ALTER TABLE `coursesemesters`
  MODIFY `CourseSemesterID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `groupsusers`
--
ALTER TABLE `groupsusers`
  MODIFY `GroupUserID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `RoleID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `sections`
--
ALTER TABLE `sections`
  MODIFY `SectionID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `semsters`
--
ALTER TABLE `semsters`
  MODIFY `SemesterID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `UserID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `years`
--
ALTER TABLE `years`
  MODIFY `YearID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `coursesemesters`
--
ALTER TABLE `coursesemesters`
  ADD CONSTRAINT `Courses` FOREIGN KEY (`CourseID`) REFERENCES `courses` (`CourseID`),
  ADD CONSTRAINT `Sections` FOREIGN KEY (`SectionID`) REFERENCES `sections` (`SectionID`),
  ADD CONSTRAINT `Semesters` FOREIGN KEY (`SemesterID`) REFERENCES `semesters` (`SemesterID`),
  ADD CONSTRAINT `Users` FOREIGN KEY (`UserID`) REFERENCES `users` (`UserID`),
  ADD CONSTRAINT `Years` FOREIGN KEY (`YearID`) REFERENCES `years` (`YearID`);

--
-- Constraints for table `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `Users_Roles` FOREIGN KEY (`RoleID`) REFERENCES `roles` (`RoleID`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
