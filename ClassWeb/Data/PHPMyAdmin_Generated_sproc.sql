-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Host: MYSQL7003.site4now.net
-- Generation Time: May 06, 2019 at 10:22 AM
-- Server version: 5.6.26-log
-- PHP Version: 5.6.27

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_a458d6_shreelv`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `add_Group` (IN `g_Name` VARCHAR(50), IN `g_EmailAddress` VARCHAR(50), IN `g_UserName` VARCHAR(50), IN `g_Password` CHAR(128), IN `g_Salt` CHAR(128), OUT `g_ID` INT)  BEGIN 
INSERT INTO groups(Name, EmailAddress, Username, Password, Salt) values (g_Name, g_EmailAddress, g_UserName,g_Password, g_Salt); 
SET g_ID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `CheckGroupUsernameExists` (IN `username` VARCHAR(128))  BEGIN 
	SET @group_exists = 0; 
	SELECT 1 INTO @group_exists 
    FROM groups 
    WHERE groups.UserName = username; 
	SELECT @group_exists;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `CheckUserExistsInGroup` (IN `GroupID` INT, IN `UserID` INT)  BEGIN 
	SET @user_exists = 0; 
	SELECT 1 INTO @user_exists 
    FROM groupsusers 
    WHERE groupsusers.GroupID = GroupID and groupsusers.UserID = UserID; 
	SELECT @user_exists;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `delete_GroupByID` (IN `ID` INT)  BEGIN
delete from groups
where groups.ID=ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `delete_GroupUserByID` (IN `GroupID` INT, IN `UserID` INT)  BEGIN 
delete from groupsusers where groupsusers.GroupID = GroupID AND
groupsusers.UserID = UserID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `get_Group` ()  BEGIN 
SELECT * FROM groups; 
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `get_GroupByID` (IN `GroupID` INT)  BEGIN 
SELECT * from groups g where g.ID = GroupID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `get_GroupByUserName` (IN `username` VARCHAR(128))  BEGIN 
SELECT * from groups g where g.UserName = username; 
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `get_GroupUsersByID` (IN `GroupID` INT)  BEGIN
SELECT  users.ID, users.FirstName, users.LastName, users.EmailAddress 
 from groups 
  inner join groupsusers on groups.ID = groupsusers.GroupID
  inner join users on groupsusers.UserID = users.ID 
where groups.ID = GroupID; 
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sprocs_DeleteCourseByID` (IN `c_ID` INT(50))  BEGIN DELETE FROM course where ID = c_ID; 
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sprocs_EditCourse` (IN `c_CourseTitle` VARCHAR(50), IN `c_CourseName` VARCHAR(50))  BEGIN UPDATE course SET Name = c_CourseTitle, Number = c_CourseName ();
 END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AddGroup` (IN `Name` VARCHAR(45), IN `UserName` VARCHAR(128), IN `Password` CHAR(64), IN `Salt` CHAR(50), OUT `ID` INT)  BEGIN
     INSERT INTO groups(Name,UserName, Password, Salt)
     VALUES(Name,UserName, Password, Salt);
SET ID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AddUserToClass` (OUT `CourseSemesterUserID` INT, IN `CourseSemesterID` INT(11), IN `UserID` INT(11))  BEGIN
	INSERT INTO CourseSemesterUsers(CourseSemesterID, UserID)
    			VALUES (CourseSemesterID, UserID);
    SET CourseSemesterUserID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AddUserToExistingGroup` (IN `groupID` INT, IN `userID` INT, IN `ID` INT)  BEGIN
	INSERT INTO groupsusers(ID, GroupID, UserID)
    			VALUES (ID, groupID, userID);
               
    
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AddUserToGroup` (IN `GroupID` INT(11), IN `UserID` INT(11), OUT `GroupUserID` INT(11))  BEGIN
	INSERT INTO groupsusers(GroupID, UserID)
    			VALUES (GroupID, UserID);
    SET GroupUserID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentAdd` (OUT `ID` INT, IN `FileLocation` VARCHAR(100), IN `FileName` VARCHAR(45), IN `DateSubmited` DATETIME, IN `Feedback` VARCHAR(50), IN `FileSize` INT, IN `Grade` FLOAT, IN `IsEditable` TINYINT, IN `DateModified` DATETIME, IN `UserName` VARCHAR(45))  BEGIN
INSERT INTO `assignment` (`FileLocation`,`FileName`, `DateSubmited`,
 `Feedback`, `FileSize`,`Grade`, `IsEditable`, `DateModified`,`UserName`)
 VALUES (`FileLocation`,`FileName`, now(),
 `Feedback`, `FileSize`,`Grade`, `IsEditable`, now(),`UserName`);
     SET ID = last_insert_id();
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentDeleteByID` (IN `ID` INT)  BEGIN
DELETE FROM assignment
Where Assignment.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetAllByFileName` (IN `Filename` VARCHAR(50))  BEGIN
Select * from assignment
where  assignment.FileName=`FileName`;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetAllByUserName` (IN `UserName` VARCHAR(45))  BEGIN
Select * from assignment
where assignment.UserName=`UserName`;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetAllByUserNameAndLocation` (IN `UserName` VARCHAR(45), IN `FileLocation` VARCHAR(50))  BEGIN
Select * from assignment
where assignment.UserName=`UserName` AND assignment.FileLocation like CONCAT(`FileLocation` , '%');
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetByFileName` (IN `FileName` VARCHAR(50))  BEGIN
Select * from assignment
where assignment.FileName=`FileName`;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetByID` (IN `ID` INT)  BEGIN
Select * from assignment
where assignment.ID=`ID`;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetByLocation` (IN `FileLocation` VARCHAR(100))  BEGIN
Select * from assignment
where FileLocation LIKE assignment.FileLocation;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetByNameAndLocation` (IN `FileName` VARCHAR(50), IN `FileLocation` VARCHAR(50))  BEGIN
Select * from assignment
where assignment.FileLocation=`FileLocation`&&assignment.FileName=`FileName`;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetByNameLocationUserName` (IN `FileName` VARCHAR(50), IN `FileLocation` VARCHAR(50), IN `UserName` VARCHAR(50))  BEGIN
Select * from assignment
where assignment.FileLocation=`FileLocation`&&assignment.FileName=`FileName`&&assignment.UserName=`UserName`;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentGetUserNameAndEmail` (IN `UserName` VARCHAR(50), IN `EmailAddress` VARCHAR(50))  BEGIN
Select * from Users where Users.UserName=`UserName`&&Users.EmailAddress=`EmailAddress`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentResubmit` (IN `ID` INT, IN `Feedback` VARCHAR(50))  BEGIN
update Assignment
set Assignment.Feedback=Feedback
where Assignment.ID=ID;
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CheckUserByEmail` (IN `EmailAddress` VARCHAR(128))  BEGIN
    SET @User_id = 0;
    SELECT Users.ID INTO @User_id
    FROM `Users`
    WHERE users.`EmailAddress` = `EmailAddress`;
    SELECT @User_id;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CheckUserName` (IN `Username` VARCHAR(128))  BEGIN
    SET @User_exists = 0;
    SELECT 1 INTO @User_exists
    FROM `users`
    WHERE users.`UserName` = `Username`;
    SELECT @User_exists;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CheckUserName1` (IN `Username1` VARCHAR(128))  BEGIN
    SET @User_exists = 0;
    SELECT 1 INTO @User_exists
    FROM `Users`
    WHERE Users.`UserName` = `Username1`;
    SELECT @User_exists;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CourseAdd` (OUT `ID` INT, IN `Title` VARCHAR(45), IN `Name` VARCHAR(45), IN `Description` VARCHAR(128))  BEGIN
     INSERT INTO courses(Title, Name, Description)
               VALUES(Title, Name, Description);               
     SET ID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CourseRemove` (IN `ID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM courses
          WHERE courses.ID = ID;

     END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CourseSemesterAdd` (OUT `CourseSemesterID` INT, IN `CRN` INT(11), IN `CourseID` INT(11), IN `SemesterID` INT(11), IN `YearID` INT(11), IN `SectionID` INT(11), IN `DateStart` DATETIME, IN `DateEnd` DATETIME, IN `Name` VARCHAR(128))  BEGIN
     INSERT INTO CourseSemesters(Name, CRN, CourseID, SemesterID, YearID, SectionID, DateStart, DateEnd)
               VALUES(Name, CRN, CourseID, SemesterID, YearID, SectionID,DateStart, DateEnd);               
     SET CourseSemesterID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CourseSemesterEdit` (IN `CourseSemesterID` INT(11), IN `CRN` INT(11), IN `CourseID` INT(11), IN `SemesterID` INT(11), IN `YearID` INT(11), IN `SectionID` INT(11))  BEGIN
     UPDATE CourseSemesters
          SET
			   CourseSemesters.CRN = CRN,
               CourseSemesters.CourseID = CourseID,
			   CourseSemesters.SemesterID = SemesterID,
			   CourseSemesters.YearID = YearID,
			   CourseSemesters.SectionID = SectionID
          WHERE CourseSemesters.CourseSemesterID = CourseSemesterID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CourseSemesterGet` (IN `CourseSemesterID` INT)  BEGIN
     SELECT * FROM coursesemesters
     WHERE coursesemesters.CourseSemesterID = CourseSemesterID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CourseSemesterGetAll` ()  BEGIN
     SELECT * FROM coursesemesters
     ORDER BY coursesemesters.CourseSemesterID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CourseSemesterRemove` (IN `CourseSemesterID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM CourseSemesters
          WHERE CourseSemesters.CourseSemesterID = CourseSemesterID;

     END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_CreateCourse` (OUT `ID` INT, IN `CourseTitle` VARCHAR(50), IN `CourseName` VARCHAR(50), IN `CourseDescription` VARCHAR(256))  BEGIN
     INSERT INTO Courses(CourseTitle,CourseName, CourseDescription)
     VALUES(CourseTitle, CourseName, CourseDescription);
SET ID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetAllCourses` ()  BEGIN
     SELECT * FROM courses;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetAllUsers` ()  BEGIN
	 SELECT * FROM Users;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetClassesForUser` (IN `ID` INT)  BEGIN
    SELECT coursesemesters.CourseSemesterID, coursesemesters.Name, coursesemesters.CRN, coursesemesters.CourseID, coursesemesters.SemesterID, coursesemesters.YearID, coursesemesters.SectionID, coursesemesters.DateStart, coursesemesters.DateEnd from users
    INNER JOIN coursesemesterusers
    ON users.ID = coursesemesterusers.UserID
    INNER JOIN coursesemesters
    ON coursesemesters.CourseSemesterID = coursesemesterusers.CourseSemesterID
    WHERE users.ID = ID AND coursesemesters.DateEnd > NOW();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetCourse` (IN `ID` INT)  BEGIN
     SELECT * FROM courses
     WHERE courses.ID = ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetCourseByID` (IN `ID` INT)  BEGIN
Select * from Courses where Courses.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetUserByUsername` (IN `Username` VARCHAR(128))  BEGIN
	 SELECT * FROM Users
	 WHERE Users.UserName = Username;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_getuserbyusername1` (IN `username` VARCHAR(128))  BEGIN
	 SELECT * FROM Login_Users1
	 WHERE Login_Users1.UserName = username;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetUsersForClass` (IN `CourseSemesterID` INT)  BEGIN
    SELECT users.ID, users.FirstName, users.LastName, users.EmailAddress 
    FROM coursesemesters
    INNER JOIN coursesemesterusers
    ON coursesemesters.CourseSemesterID = coursesemesterusers.CourseSemesterID
    INNER JOIN users
    ON users.ID = coursesemesterusers.UserID
    WHERE coursesemesters.CourseSemesterID = CourseSemesterID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetUsersFromGroup` (IN `GroupID` INT)  BEGIN
    SELECT users.ID, users.FirstName,users.MiddleName, users.LastName, users.EmailAddress, users.Password, users.DateCreated, users.DateModified, users.Salt, users.RoleID
    FROM groups
    INNER JOIN groupsusers
    ON groups.id = groupsusers.GroupID
    INNER JOIN users
    ON groupsusers.UserID = users.ID
    WHERE groups.id = GroupID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetUsersWithRoles` (IN `UserID` INT(11))  BEGIN
    SELECT u.UserName, u.RoleID, r.Name, r.IsAdmin, r.users, r.Role, r.Assignment From Users u
    JOIN Roles r ON u.RoleID = r.RoleID
    WHERE u.ID = UserID;
    END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_RoleAdd` (OUT `ID` INT, IN `Name` VARCHAR(45), IN `IsAdmin` BIT(1), IN `Users` BIT(4), IN `Role` BIT(4), IN `Assignment` BIT(4))  BEGIN
     INSERT INTO Roles(Name,IsAdmin,Users,Role,Assignment, DateCreated)
               VALUES(Name,IsAdmin,Users,Role, Assignment,CURRENT_TIMESTAMP());               
     SET ID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_RoleGet` (IN `RoleID` INT)  BEGIN
     SELECT * FROM Roles
     WHERE Roles.ID = RoleID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_RoleGetByID` (IN `ID` INT)  BEGIN SELECT * FROM roles WHERE roles.ID = `ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_RoleRemove` (IN `ID` INT)  BEGIN
     DELETE FROM Roles
          WHERE Roles.ID = `ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_RolesGetAll` ()  BEGIN
     SELECT * FROM Roles;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_RoleUpdate` (IN `ID` INT, IN `Name` VARCHAR(45), IN `IsAdmin` BIT(1), IN `Users` BIT(4), IN `Role` BIT(4), IN `Assignment` BIT(4))  BEGIN
     UPDATE Roles
          SET
               Roles.Name = Name,
               Roles.IsAdmin = IsAdmin,
               Roles.Users = Users,
               Roles.Role = Role,
               Roles.Assignment = Assignment,
               Roles.DateModified = CURRENT_TIMESTAMP()
          WHERE Roles.ID = ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SectionAdd` (OUT `SectionID` INT, IN `Number` INT(45))  BEGIN
     INSERT INTO Sections(Number)
               VALUES(Number);               
     SET SectionID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SectionGet` (IN `SectionID` INT)  BEGIN
     SELECT * FROM Sections
     WHERE Sections.SectionID = SectionID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SectionRemoveByID` (IN `SectionID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Sections
          WHERE Sections.SectionID = SectionID;

     END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SectionsGetAll` ()  BEGIN
     SELECT * FROM Sections;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SectionUpdate` (IN `SectionID` INT(11), IN `SectionNumber` INT(45))  BEGIN
     UPDATE Sections
          SET
               Sections.SectionNumber = SectionNumber
          WHERE Sections.SectionID = SectionID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SemesterAdd` (OUT `ID` INT, IN `SemesterName` VARCHAR(128))  BEGIN
     INSERT INTO Semesters(SemesterName)
               VALUES(SemesterName);               
     SET ID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SemesterEdit` (IN `ID` INT, IN `SemesterName` VARCHAR(128))  BEGIN
     UPDATE Semesters
          SET
               Semesters.SemesterName = SemesterName
			  
          WHERE Semesters.ID = ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SemesterGet` (IN `ID` INT)  BEGIN
     SELECT * FROM semesters
     WHERE semesters.ID = ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SemesterGetAll` ()  BEGIN
     SELECT * FROM Semesters;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_SemesterRemove` (IN `ID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM semesters
          WHERE semesters.ID = ID;

     END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UpdateCourse` (IN `ID` INT, IN `Title` VARCHAR(50), IN `Name` VARCHAR(50))  BEGIN
     UPDATE courses
          SET
               courses.`Title` = `Title`,
               courses.`Name` = `Name`,
               courses.`Description`=`Description`
               where courses.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserAdd` (OUT `ID` INT, IN `FirstName` VARCHAR(50), IN `MiddleName` VARCHAR(45), IN `LastName` VARCHAR(50), IN `EmailAddress` VARCHAR(50), IN `UserName` VARCHAR(50), IN `Password` VARCHAR(100), IN `Salt` VARCHAR(50), IN `VerificationCode` VARCHAR(50))  BEGIN
INSERT INTO Users (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`,`VerificationCode`)
 VALUES (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`,`VerificationCode`);
     SET ID = last_insert_id();
End$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserArchiveStatusUpdate` (IN `ID` INT, IN `Archived` TINYINT)  BEGIN
UPDATE users
SET users.Archived = `Archived`where users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserByID` (IN `id` INT)  BEGIN
Select * from users
Where users.ID=id;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserDeleteByID` (IN `ID` INT)  BEGIN
delete from Users
where Users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserDisableStatusUpdate` (IN `ID` INT, IN `Enabled` TINYINT)  BEGIN
UPDATE users
SET users.Enabled = `Enabled`where users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserGetAll` ()  BEGIN
Select * from users;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserGetByEmailAddress` (IN `u_EmailAddress` VARCHAR(50))  BEGIN SELECT a.FirstName, a.MiddleName, a.LastName, a.EmailAddress, a.Address, a.UserName, a.PhoneNumber, b.RoleID, b.Title FROM user u
Inner JOIN userrole c on u.id = c.UserID
LEFT OUTER JOIN role b on c.UserID = b.ID
WHERE a.EmailAddress = u_EmailAddress; 
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserGetByID` (IN `ID` INT)  BEGIN
Select * From Users
Where Users.ID=ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserGetByUserName` (IN `UserName` NVARCHAR(128))  BEGIN
SELECT * FROM Users
WHERE Users.UserName = `UserName`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserGetSalt` (IN `Username` VARCHAR(256))  BEGIN
SELECT Salt FROM Users
WHERE Users.UserName = Username;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserPasswordCodeUpdate` (IN `ID` INT, IN `ResetCode` VARCHAR(50))  BEGIN
UPDATE users
SET users.ResetCode = `ResetCode`where users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserPasswordUpdate` (IN `ID` INT, IN `Password` VARCHAR(70))  BEGIN UPDATE users
SET users.Password = `Password`
where users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserRoleUpdate` (IN `ID` INT, IN `RoleID` INT)  BEGIN UPDATE users
SET users.RoleID = `RoleID`
where users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UsersGetALL` ()  BEGIN
SELECT * FROM Users;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserUpdate` (IN `ID` INT, IN `FirstName` VARCHAR(50), IN `LastName` VARCHAR(50), IN `UserName` VARCHAR(50))  BEGIN UPDATE users
SET users.FirstName = `FirstName`,
users.LastName = `LastName`,
users.UserName = `UserName`,
users.DateModified= NOW()
where users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserUpdateVerificationCode` (IN `ID` INT, IN `VerificationCode` VARCHAR(50))  BEGIN
UPDATE users
SET users.VerificationCode = `VerificationCode`where users.ID=`ID`;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_YearAdd` (OUT `ID` INT, IN `YEAR` INT(11))  BEGIN
     INSERT INTO Years(Year, Name)
               VALUES(Year, Year);               
     SET ID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_YearEdit` (IN `ID` INT, IN `Year` INT(11))  BEGIN
     UPDATE Years
          SET
               Years.Year = Year
			  
          WHERE Years.ID = ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_YearGet` (IN `ID` INT)  BEGIN
     SELECT * FROM Years
     WHERE Years.ID = ID;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_YearGetAll` ()  BEGIN
     SELECT * FROM Years;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_YearRemove` (IN `ID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM years
          WHERE years.ID = ID;

     END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `update_Group` (IN `g_Name` VARCHAR(50), IN `g_Username` VARCHAR(50), IN `g_Password` VARCHAR(50), IN `g_Salt` CHAR(50))  BEGIN
UPDATE groups g set g.Name = g_Name, g.UserName = g_Username, g.Password = g_Password, g.Salt = g_Salt; 
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `assignment`
--

CREATE TABLE `assignment` (
  `ID` int(11) NOT NULL,
  `FileLocation` varchar(100) NOT NULL,
  `DateStarted` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DateDue` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DateSubmited` datetime NOT NULL,
  `Grade` float NOT NULL DEFAULT '-1',
  `Feedback` varchar(50) NOT NULL,
  `FileSize` int(11) NOT NULL DEFAULT '-1',
  `IsEditable` tinyint(1) NOT NULL DEFAULT '-1',
  `DateModified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `FileName` varchar(45) NOT NULL,
  `DateDeleted` varchar(45) NOT NULL DEFAULT '9999-12-31 23:59:59.997',
  `UserName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `assignment`
--

INSERT INTO `assignment` (`ID`, `FileLocation`, `DateStarted`, `DateDue`, `DateSubmited`, `Grade`, `Feedback`, `FileSize`, `IsEditable`, `DateModified`, `FileName`, `DateDeleted`, `UserName`) VALUES
(1174, '/sapkgane/sample/Assignments/sample/Assignments/Assignment4.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:02', 0, 'File Submitted', 897, 0, '2019-05-01 11:59:02', 'Assignment4.html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1175, '/sapkgane/sample/Assignments/sample/Assignments/Assignment5.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 18750, 0, '2019-05-01 11:59:04', 'Assignment5.html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1176, '/sapkgane/sample/Assignments/sample/Assignments/Assignment6.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 2180, 0, '2019-05-01 11:59:04', 'Assignment6.html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1177, '/sapkgane/sample/Assignments/sample/Assignments/Assignment7B.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 122, 0, '2019-05-01 11:59:04', 'Assignment7B.html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1178, '/sapkgane/sample/Assignments/sample/Assignments/Assignment8.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 3157, 0, '2019-05-01 11:59:04', 'Assignment8.html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1179, '/sapkgane/sample/Assignments/sample/Assignments/Assignment9.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 1071, 0, '2019-05-01 11:59:04', 'Assignment9.html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1180, '/sapkgane/sample/Assignments/sample/Assignments/assn03.htm', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 5966, 0, '2019-05-01 11:59:04', 'assn03.htm', '9999-12-31 23:59:59.997', 'sapkgane'),
(1181, '/sapkgane/sample/Assignments/sample/Assignments/directory.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 1724, 0, '2019-05-01 11:59:04', 'directory.json', '9999-12-31 23:59:59.997', 'sapkgane'),
(1182, '/sapkgane/sample/Assignments/sample/Assignments/jquery-3.3.1.min.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 86927, 0, '2019-05-01 11:59:04', 'jquery-3.3.1.min.js', '9999-12-31 23:59:59.997', 'sapkgane'),
(1183, '/sapkgane/sample/Assignments/sample/Assignments/people.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 644, 0, '2019-05-01 11:59:04', 'people.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1184, '/sapkgane/sample/Assignments/sample/Assignments/people.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:04', 0, 'File Submitted', 1110, 0, '2019-05-01 11:59:04', 'people.js', '9999-12-31 23:59:59.997', 'sapkgane'),
(1185, '/sapkgane/sample/Assignments/sample/Assignments/Resume (2).Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 8466, 0, '2019-05-01 11:59:05', 'Resume (2).Html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1186, '/sapkgane/sample/Assignments/sample/Assignments/Resume.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 8342, 0, '2019-05-01 11:59:05', 'Resume.Html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1187, '/sapkgane/sample/Assignments/sample/Assignments/Resume2.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 7698, 0, '2019-05-01 11:59:05', 'Resume2.Html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1188, '/sapkgane/sample/Images/sample/Images/bengal.ico', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 9742, 0, '2019-05-01 11:59:05', 'bengal.ico', '9999-12-31 23:59:59.997', 'sapkgane'),
(1189, '/sapkgane/sample/Images/sample/Images/Kishor Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 607013, 0, '2019-05-01 11:59:05', 'Kishor Pic.jpg', '9999-12-31 23:59:59.997', 'sapkgane'),
(1190, '/sapkgane/sample/Images/sample/Images/Kishor_Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 607013, 0, '2019-05-01 11:59:05', 'Kishor_Pic.jpg', '9999-12-31 23:59:59.997', 'sapkgane'),
(1191, '/sapkgane/sample/Images/sample/Images/logoICS.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 30628, 0, '2019-05-01 11:59:05', 'logoICS.jpg', '9999-12-31 23:59:59.997', 'sapkgane'),
(1192, '/sapkgane/sample/Images/sample/Images/sharedCourses.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 39855, 0, '2019-05-01 11:59:05', 'sharedCourses.jpg', '9999-12-31 23:59:59.997', 'sapkgane'),
(1193, '/sapkgane/sample/Script/sample/Script/ArithmeticCalculator.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 3164, 0, '2019-05-01 11:59:05', 'ArithmeticCalculator.js', '9999-12-31 23:59:59.997', 'sapkgane'),
(1194, '/sapkgane/sample/Script/sample/Script/Asign7b.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 6408, 0, '2019-05-01 11:59:05', 'Asign7b.js', '9999-12-31 23:59:59.997', 'sapkgane'),
(1195, '/sapkgane/sample/Script/sample/Script/Calc.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 321, 0, '2019-05-01 11:59:05', 'Calc.js', '9999-12-31 23:59:59.997', 'sapkgane'),
(1196, '/sapkgane/sample/Script/sample/Script/jquery-3.3.1.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:05', 0, 'File Submitted', 271751, 0, '2019-05-01 11:59:05', 'jquery-3.3.1.js', '9999-12-31 23:59:59.997', 'sapkgane'),
(1197, '/sapkgane/sample/Script/sample/Script/JQuery.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 3326, 0, '2019-05-01 11:59:06', 'JQuery.js', '9999-12-31 23:59:59.997', 'sapkgane'),
(1198, '/sapkgane/sample/Styles/sample/Styles/ArithmeticCalculator.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 119, 0, '2019-05-01 11:59:06', 'ArithmeticCalculator.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1199, '/sapkgane/sample/Styles/sample/Styles/Assign7.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 461, 0, '2019-05-01 11:59:06', 'Assign7.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1200, '/sapkgane/sample/Styles/sample/Styles/CalcStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 60, 0, '2019-05-01 11:59:06', 'CalcStyle.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1201, '/sapkgane/sample/Styles/sample/Styles/Informatics.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 755, 0, '2019-05-01 11:59:06', 'Informatics.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1202, '/sapkgane/sample/Styles/sample/Styles/madscreen.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 1540, 0, '2019-05-01 11:59:06', 'madscreen.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1203, '/sapkgane/sample/Styles/sample/Styles/Print.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 192, 0, '2019-05-01 11:59:06', 'Print.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1204, '/sapkgane/sample/Styles/sample/Styles/ResumeStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 162, 0, '2019-05-01 11:59:06', 'ResumeStyle.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1205, '/sapkgane/sample/Styles/sample/Styles/site.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 11:59:06', 0, 'File Submitted', 780, 0, '2019-05-01 11:59:06', 'site.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1206, '/sapkgane/INFO-2220/.vs/config/INFO-2220/.vs/config/applicationhost.config', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 85367, 0, '2019-05-01 12:03:26', 'applicationhost.config', '9999-12-31 23:59:59.997', 'Admin'),
(1207, '/sapkgane/INFO-2220/.vs/INFO-2220/v15/INFO-2220/.vs/INFO-2220/v15/.suo', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 19968, 0, '2019-05-01 12:03:26', '.suo', '9999-12-31 23:59:59.997', 'Admin'),
(1208, '/sapkgane/INFO-2220/.vs/INFO-2220/.vs/ProjectSettings.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 37, 0, '2019-05-01 12:03:26', 'ProjectSettings.json', '9999-12-31 23:59:59.997', 'Admin'),
(1209, '/sapkgane/INFO-2220/.vs/INFO-2220/.vs/slnx.sqlite', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 180224, 0, '2019-05-01 12:03:26', 'slnx.sqlite', '9999-12-31 23:59:59.997', 'Admin'),
(1210, '/sapkgane/INFO-2220/.vs/INFO-2220/.vs/VSWorkspaceState.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 100, 0, '2019-05-01 12:03:26', 'VSWorkspaceState.json', '9999-12-31 23:59:59.997', 'Admin'),
(1211, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Assignment4.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 897, 0, '2019-05-01 12:03:26', 'Assignment4.html', '9999-12-31 23:59:59.997', 'Admin'),
(1212, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Assignment5.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 18750, 0, '2019-05-01 12:03:26', 'Assignment5.html', '9999-12-31 23:59:59.997', 'Admin'),
(1213, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Assignment6.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 2180, 0, '2019-05-01 12:03:26', 'Assignment6.html', '9999-12-31 23:59:59.997', 'Admin'),
(1214, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Assignment7B.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 122, 0, '2019-05-01 12:03:26', 'Assignment7B.html', '9999-12-31 23:59:59.997', 'Admin'),
(1215, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Assignment8.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:26', 0, 'File Submitted', 3157, 0, '2019-05-01 12:03:26', 'Assignment8.html', '9999-12-31 23:59:59.997', 'Admin'),
(1216, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Assignment9.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 1071, 0, '2019-05-01 12:03:27', 'Assignment9.html', '9999-12-31 23:59:59.997', 'Admin'),
(1217, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/assn03.htm', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 5966, 0, '2019-05-01 12:03:27', 'assn03.htm', '9999-12-31 23:59:59.997', 'Admin'),
(1218, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/directory.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 1724, 0, '2019-05-01 12:03:27', 'directory.json', '9999-12-31 23:59:59.997', 'Admin'),
(1219, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/jquery-3.3.1.min.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 86927, 0, '2019-05-01 12:03:27', 'jquery-3.3.1.min.js', '9999-12-31 23:59:59.997', 'Admin'),
(1220, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/people.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 644, 0, '2019-05-01 12:03:27', 'people.css', '9999-12-31 23:59:59.997', 'Admin'),
(1221, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/people.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 1110, 0, '2019-05-01 12:03:27', 'people.js', '9999-12-31 23:59:59.997', 'Admin'),
(1222, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Resume (2).Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 8466, 0, '2019-05-01 12:03:27', 'Resume (2).Html', '9999-12-31 23:59:59.997', 'Admin'),
(1223, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Resume.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 8342, 0, '2019-05-01 12:03:27', 'Resume.Html', '9999-12-31 23:59:59.997', 'Admin'),
(1224, '/sapkgane/INFO-2220/Assignments/INFO-2220/Assignments/Resume2.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 7698, 0, '2019-05-01 12:03:27', 'Resume2.Html', '9999-12-31 23:59:59.997', 'Admin'),
(1225, '/sapkgane/INFO-2220/Data/INFO-2220/Data/informatics2018f.docx', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 16307, 0, '2019-05-01 12:03:27', 'informatics2018f.docx', '9999-12-31 23:59:59.997', 'Admin'),
(1226, '/sapkgane/Informatics.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 755, 0, '2019-05-01 12:03:27', 'Informatics.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1227, '/sapkgane/INFO-2220/Images/INFO-2220/Images/bengal.ico', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:27', 0, 'File Submitted', 9742, 0, '2019-05-01 12:03:27', 'bengal.ico', '9999-12-31 23:59:59.997', 'Admin'),
(1228, '/sapkgane/INFO-2220/Images/INFO-2220/Images/Kishor Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 607013, 0, '2019-05-01 12:03:28', 'Kishor Pic.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1229, '/sapkgane/INFO-2220/Images/INFO-2220/Images/Kishor_Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 607013, 0, '2019-05-01 12:03:28', 'Kishor_Pic.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1230, '/sapkgane/INFO-2220/Images/INFO-2220/Images/logoICS.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 30628, 0, '2019-05-01 12:03:28', 'logoICS.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1231, '/sapkgane/INFO-2220/Images/INFO-2220/Images/sharedCourses.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 39855, 0, '2019-05-01 12:03:28', 'sharedCourses.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1232, '/sapkgane/INFO-2220/INFO-2220/Index.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 936, 0, '2019-05-01 12:03:28', 'Index.Html', '9999-12-31 23:59:59.997', 'Admin'),
(1233, '/sapkgane/INFO-2220/Script/INFO-2220/Script/ArithmeticCalculator.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 3164, 0, '2019-05-01 12:03:28', 'ArithmeticCalculator.js', '9999-12-31 23:59:59.997', 'Admin'),
(1234, '/sapkgane/INFO-2220/Script/INFO-2220/Script/Asign7b.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 6408, 0, '2019-05-01 12:03:28', 'Asign7b.js', '9999-12-31 23:59:59.997', 'Admin'),
(1235, '/sapkgane/INFO-2220/Script/INFO-2220/Script/Calc.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 321, 0, '2019-05-01 12:03:28', 'Calc.js', '9999-12-31 23:59:59.997', 'Admin'),
(1236, '/sapkgane/INFO-2220/Script/INFO-2220/Script/jquery-3.3.1.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 271751, 0, '2019-05-01 12:03:28', 'jquery-3.3.1.js', '9999-12-31 23:59:59.997', 'Admin'),
(1237, '/sapkgane/INFO-2220/Script/INFO-2220/Script/JQuery.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 3326, 0, '2019-05-01 12:03:28', 'JQuery.js', '9999-12-31 23:59:59.997', 'Admin'),
(1238, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/ArithmeticCalculator.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 119, 0, '2019-05-01 12:03:28', 'ArithmeticCalculator.css', '9999-12-31 23:59:59.997', 'Admin'),
(1239, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/Assign7.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 461, 0, '2019-05-01 12:03:28', 'Assign7.css', '9999-12-31 23:59:59.997', 'Admin'),
(1240, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/CalcStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:28', 0, 'File Submitted', 60, 0, '2019-05-01 12:03:28', 'CalcStyle.css', '9999-12-31 23:59:59.997', 'Admin'),
(1241, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/Informatics.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:29', 0, 'File Submitted', 755, 0, '2019-05-01 12:03:29', 'Informatics.css', '9999-12-31 23:59:59.997', 'Admin'),
(1242, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/madscreen.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:29', 0, 'File Submitted', 1540, 0, '2019-05-01 12:03:29', 'madscreen.css', '9999-12-31 23:59:59.997', 'Admin'),
(1243, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/Print.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:29', 0, 'File Submitted', 192, 0, '2019-05-01 12:03:29', 'Print.css', '9999-12-31 23:59:59.997', 'Admin'),
(1244, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/ResumeStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:29', 0, 'File Submitted', 162, 0, '2019-05-01 12:03:29', 'ResumeStyle.css', '9999-12-31 23:59:59.997', 'Admin'),
(1245, '/sapkgane/INFO-2220/Styles/INFO-2220/Styles/site.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:29', 0, 'File Submitted', 780, 0, '2019-05-01 12:03:29', 'site.css', '9999-12-31 23:59:59.997', 'Admin'),
(1246, '/sapkgane/INFO-2220/Informatics.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:03:44', 0, 'File Submitted', 755, 0, '2019-05-01 12:03:44', 'Informatics.css', '9999-12-31 23:59:59.997', 'sapkgane'),
(1247, 'Starting Page.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:06:17', 0, 'File Submitted', 2735, 0, '2019-05-01 12:06:17', 'Starting Page.html', '9999-12-31 23:59:59.997', 'Admin'),
(1248, '/INFO-2220/.vs/config/INFO-2220/.vs/config/applicationhost.config', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:32', 0, 'File Submitted', 85367, 0, '2019-05-01 12:09:32', 'applicationhost.config', '9999-12-31 23:59:59.997', 'Admin'),
(1249, '/INFO-2220/.vs/INFO-2220/v15/INFO-2220/.vs/INFO-2220/v15/.suo', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:33', 0, 'File Submitted', 19968, 0, '2019-05-01 12:09:33', '.suo', '9999-12-31 23:59:59.997', 'Admin'),
(1250, '/INFO-2220/.vs/INFO-2220/.vs/ProjectSettings.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:34', 0, 'File Submitted', 37, 0, '2019-05-01 12:09:34', 'ProjectSettings.json', '9999-12-31 23:59:59.997', 'Admin'),
(1251, '/INFO-2220/.vs/INFO-2220/.vs/slnx.sqlite', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:36', 0, 'File Submitted', 180224, 0, '2019-05-01 12:09:36', 'slnx.sqlite', '9999-12-31 23:59:59.997', 'Admin'),
(1252, '/INFO-2220/.vs/INFO-2220/.vs/VSWorkspaceState.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:37', 0, 'File Submitted', 100, 0, '2019-05-01 12:09:37', 'VSWorkspaceState.json', '9999-12-31 23:59:59.997', 'Admin'),
(1253, '/INFO-2220/Assignments/INFO-2220/Assignments/Assignment4.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:38', 0, 'File Submitted', 897, 0, '2019-05-01 12:09:38', 'Assignment4.html', '9999-12-31 23:59:59.997', 'Admin'),
(1254, '/INFO-2220/Assignments/INFO-2220/Assignments/Assignment5.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:40', 0, 'File Submitted', 18750, 0, '2019-05-01 12:09:40', 'Assignment5.html', '9999-12-31 23:59:59.997', 'Admin'),
(1255, '/INFO-2220/Assignments/INFO-2220/Assignments/Assignment6.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:42', 0, 'File Submitted', 2180, 0, '2019-05-01 12:09:42', 'Assignment6.html', '9999-12-31 23:59:59.997', 'Admin'),
(1256, '/INFO-2220/Assignments/INFO-2220/Assignments/Assignment7B.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:43', 0, 'File Submitted', 122, 0, '2019-05-01 12:09:43', 'Assignment7B.html', '9999-12-31 23:59:59.997', 'Admin'),
(1257, '/INFO-2220/Assignments/INFO-2220/Assignments/Assignment8.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:45', 0, 'File Submitted', 3157, 0, '2019-05-01 12:09:45', 'Assignment8.html', '9999-12-31 23:59:59.997', 'Admin'),
(1258, '/INFO-2220/Assignments/INFO-2220/Assignments/Assignment9.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:47', 0, 'File Submitted', 1071, 0, '2019-05-01 12:09:47', 'Assignment9.html', '9999-12-31 23:59:59.997', 'Admin'),
(1259, '/INFO-2220/Assignments/INFO-2220/Assignments/assn03.htm', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:48', 0, 'File Submitted', 5966, 0, '2019-05-01 12:09:48', 'assn03.htm', '9999-12-31 23:59:59.997', 'Admin'),
(1260, '/INFO-2220/Assignments/INFO-2220/Assignments/directory.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:51', 0, 'File Submitted', 1724, 0, '2019-05-01 12:09:51', 'directory.json', '9999-12-31 23:59:59.997', 'Admin'),
(1261, '/INFO-2220/Assignments/INFO-2220/Assignments/jquery-3.3.1.min.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:52', 0, 'File Submitted', 86927, 0, '2019-05-01 12:09:52', 'jquery-3.3.1.min.js', '9999-12-31 23:59:59.997', 'Admin'),
(1262, '/INFO-2220/Assignments/INFO-2220/Assignments/people.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:54', 0, 'File Submitted', 644, 0, '2019-05-01 12:09:54', 'people.css', '9999-12-31 23:59:59.997', 'Admin'),
(1263, '/INFO-2220/Assignments/INFO-2220/Assignments/people.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:56', 0, 'File Submitted', 1110, 0, '2019-05-01 12:09:56', 'people.js', '9999-12-31 23:59:59.997', 'Admin'),
(1264, '/INFO-2220/Assignments/INFO-2220/Assignments/Resume (2).Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:57', 0, 'File Submitted', 8466, 0, '2019-05-01 12:09:57', 'Resume (2).Html', '9999-12-31 23:59:59.997', 'Admin'),
(1265, '/INFO-2220/Assignments/INFO-2220/Assignments/Resume.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:09:59', 0, 'File Submitted', 8342, 0, '2019-05-01 12:09:59', 'Resume.Html', '9999-12-31 23:59:59.997', 'Admin'),
(1266, '/INFO-2220/Assignments/INFO-2220/Assignments/Resume2.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:00', 0, 'File Submitted', 7698, 0, '2019-05-01 12:10:00', 'Resume2.Html', '9999-12-31 23:59:59.997', 'Admin'),
(1267, '/INFO-2220/Data/INFO-2220/Data/informatics2018f.docx', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:02', 0, 'File Submitted', 16307, 0, '2019-05-01 12:10:02', 'informatics2018f.docx', '9999-12-31 23:59:59.997', 'Admin'),
(1268, '/INFO-2220/Images/INFO-2220/Images/bengal.ico', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:04', 0, 'File Submitted', 9742, 0, '2019-05-01 12:10:04', 'bengal.ico', '9999-12-31 23:59:59.997', 'Admin'),
(1269, '/INFO-2220/Images/INFO-2220/Images/Kishor Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:05', 0, 'File Submitted', 607013, 0, '2019-05-01 12:10:05', 'Kishor Pic.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1270, '/INFO-2220/Images/INFO-2220/Images/Kishor_Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:07', 0, 'File Submitted', 607013, 0, '2019-05-01 12:10:07', 'Kishor_Pic.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1271, '/INFO-2220/Images/INFO-2220/Images/logoICS.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:08', 0, 'File Submitted', 30628, 0, '2019-05-01 12:10:08', 'logoICS.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1272, '/INFO-2220/Images/INFO-2220/Images/sharedCourses.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:10', 0, 'File Submitted', 39855, 0, '2019-05-01 12:10:10', 'sharedCourses.jpg', '9999-12-31 23:59:59.997', 'Admin'),
(1273, '/INFO-2220/INFO-2220/Index.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:11', 0, 'File Submitted', 936, 0, '2019-05-01 12:10:11', 'Index.Html', '9999-12-31 23:59:59.997', 'Admin'),
(1274, '/INFO-2220/Script/INFO-2220/Script/ArithmeticCalculator.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:13', 0, 'File Submitted', 3164, 0, '2019-05-01 12:10:13', 'ArithmeticCalculator.js', '9999-12-31 23:59:59.997', 'Admin'),
(1275, '/INFO-2220/Script/INFO-2220/Script/Asign7b.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:14', 0, 'File Submitted', 6408, 0, '2019-05-01 12:10:14', 'Asign7b.js', '9999-12-31 23:59:59.997', 'Admin'),
(1276, '/INFO-2220/Script/INFO-2220/Script/Calc.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:16', 0, 'File Submitted', 321, 0, '2019-05-01 12:10:16', 'Calc.js', '9999-12-31 23:59:59.997', 'Admin'),
(1277, '/INFO-2220/Script/INFO-2220/Script/jquery-3.3.1.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:17', 0, 'File Submitted', 271751, 0, '2019-05-01 12:10:17', 'jquery-3.3.1.js', '9999-12-31 23:59:59.997', 'Admin'),
(1278, '/INFO-2220/Script/INFO-2220/Script/JQuery.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:18', 0, 'File Submitted', 3326, 0, '2019-05-01 12:10:18', 'JQuery.js', '9999-12-31 23:59:59.997', 'Admin'),
(1279, '/INFO-2220/Styles/INFO-2220/Styles/ArithmeticCalculator.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:19', 0, 'File Submitted', 119, 0, '2019-05-01 12:10:19', 'ArithmeticCalculator.css', '9999-12-31 23:59:59.997', 'Admin'),
(1280, '/INFO-2220/Styles/INFO-2220/Styles/Assign7.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:21', 0, 'File Submitted', 461, 0, '2019-05-01 12:10:21', 'Assign7.css', '9999-12-31 23:59:59.997', 'Admin'),
(1281, '/INFO-2220/Styles/INFO-2220/Styles/CalcStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:22', 0, 'File Submitted', 60, 0, '2019-05-01 12:10:22', 'CalcStyle.css', '9999-12-31 23:59:59.997', 'Admin'),
(1282, '/INFO-2220/Styles/INFO-2220/Styles/Informatics.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:24', 0, 'File Submitted', 755, 0, '2019-05-01 12:10:24', 'Informatics.css', '9999-12-31 23:59:59.997', 'Admin'),
(1283, '/INFO-2220/Styles/INFO-2220/Styles/madscreen.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:25', 0, 'File Submitted', 1540, 0, '2019-05-01 12:10:25', 'madscreen.css', '9999-12-31 23:59:59.997', 'Admin'),
(1284, '/INFO-2220/Styles/INFO-2220/Styles/Print.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:27', 0, 'File Submitted', 192, 0, '2019-05-01 12:10:27', 'Print.css', '9999-12-31 23:59:59.997', 'Admin'),
(1285, '/INFO-2220/Styles/INFO-2220/Styles/ResumeStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:29', 0, 'File Submitted', 162, 0, '2019-05-01 12:10:29', 'ResumeStyle.css', '9999-12-31 23:59:59.997', 'Admin'),
(1286, '/INFO-2220/Styles/INFO-2220/Styles/site.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:10:30', 0, 'File Submitted', 780, 0, '2019-05-01 12:10:30', 'site.css', '9999-12-31 23:59:59.997', 'Admin'),
(1287, '/sapkgane/html.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:14:44', 0, 'File Submitted', 0, 0, '2019-05-01 12:14:44', 'html.html', '9999-12-31 23:59:59.997', 'sapkgane'),
(1288, '/aaa/html.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:13', 0, 'File Submitted', 171, 0, '2019-05-01 12:49:13', 'html.html', '9999-12-31 23:59:59.997', 'aaa'),
(1289, '/aaa/sample/Styles/sample/Styles/ArithmeticCalculator.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:24', 0, 'File Submitted', 119, 0, '2019-05-01 12:49:24', 'ArithmeticCalculator.css', '9999-12-31 23:59:59.997', 'aaa'),
(1290, '/aaa/sample/Styles/sample/Styles/Assign7.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 461, 0, '2019-05-01 12:49:25', 'Assign7.css', '9999-12-31 23:59:59.997', 'aaa'),
(1291, '/aaa/sample/Styles/sample/Styles/CalcStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 60, 0, '2019-05-01 12:49:25', 'CalcStyle.css', '9999-12-31 23:59:59.997', 'aaa'),
(1292, '/aaa/sample/Styles/sample/Styles/Informatics.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 755, 0, '2019-05-01 12:49:25', 'Informatics.css', '9999-12-31 23:59:59.997', 'aaa'),
(1293, '/aaa/sample/Styles/sample/Styles/madscreen.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 1540, 0, '2019-05-01 12:49:25', 'madscreen.css', '9999-12-31 23:59:59.997', 'aaa'),
(1294, '/aaa/sample/Styles/sample/Styles/Print.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 192, 0, '2019-05-01 12:49:25', 'Print.css', '9999-12-31 23:59:59.997', 'aaa'),
(1295, '/aaa/sample/Styles/sample/Styles/ResumeStyle.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 162, 0, '2019-05-01 12:49:25', 'ResumeStyle.css', '9999-12-31 23:59:59.997', 'aaa'),
(1296, '/aaa/sample/Styles/sample/Styles/site.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 780, 0, '2019-05-01 12:49:25', 'site.css', '9999-12-31 23:59:59.997', 'aaa'),
(1297, '/aaa/sample/Script/sample/Script/ArithmeticCalculator.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 3164, 0, '2019-05-01 12:49:25', 'ArithmeticCalculator.js', '9999-12-31 23:59:59.997', 'aaa'),
(1298, '/aaa/sample/Script/sample/Script/Asign7b.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 6408, 0, '2019-05-01 12:49:25', 'Asign7b.js', '9999-12-31 23:59:59.997', 'aaa'),
(1299, '/aaa/sample/Script/sample/Script/Calc.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 321, 0, '2019-05-01 12:49:25', 'Calc.js', '9999-12-31 23:59:59.997', 'aaa'),
(1300, '/aaa/sample/Script/sample/Script/jquery-3.3.1.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:25', 0, 'File Submitted', 271751, 0, '2019-05-01 12:49:25', 'jquery-3.3.1.js', '9999-12-31 23:59:59.997', 'aaa'),
(1301, '/aaa/sample/Script/sample/Script/JQuery.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 3326, 0, '2019-05-01 12:49:26', 'JQuery.js', '9999-12-31 23:59:59.997', 'aaa'),
(1302, '/aaa/sample/Images/sample/Images/bengal.ico', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 9742, 0, '2019-05-01 12:49:26', 'bengal.ico', '9999-12-31 23:59:59.997', 'aaa'),
(1303, '/aaa/sample/Images/sample/Images/Kishor Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 607013, 0, '2019-05-01 12:49:26', 'Kishor Pic.jpg', '9999-12-31 23:59:59.997', 'aaa'),
(1304, '/aaa/sample/Images/sample/Images/Kishor_Pic.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 607013, 0, '2019-05-01 12:49:26', 'Kishor_Pic.jpg', '9999-12-31 23:59:59.997', 'aaa'),
(1305, '/aaa/sample/Images/sample/Images/logoICS.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 30628, 0, '2019-05-01 12:49:26', 'logoICS.jpg', '9999-12-31 23:59:59.997', 'aaa'),
(1306, '/aaa/sample/Images/sample/Images/sharedCourses.jpg', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 39855, 0, '2019-05-01 12:49:26', 'sharedCourses.jpg', '9999-12-31 23:59:59.997', 'aaa'),
(1307, '/aaa/sample/Assignments/sample/Assignments/Assignment4.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 897, 0, '2019-05-01 12:49:26', 'Assignment4.html', '9999-12-31 23:59:59.997', 'aaa'),
(1308, '/aaa/sample/Assignments/sample/Assignments/Assignment5.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 18750, 0, '2019-05-01 12:49:26', 'Assignment5.html', '9999-12-31 23:59:59.997', 'aaa'),
(1309, '/aaa/sample/Assignments/sample/Assignments/Assignment6.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 2180, 0, '2019-05-01 12:49:26', 'Assignment6.html', '9999-12-31 23:59:59.997', 'aaa'),
(1310, '/aaa/sample/Assignments/sample/Assignments/Assignment7B.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 122, 0, '2019-05-01 12:49:26', 'Assignment7B.html', '9999-12-31 23:59:59.997', 'aaa'),
(1311, '/aaa/sample/Assignments/sample/Assignments/Assignment8.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 3157, 0, '2019-05-01 12:49:26', 'Assignment8.html', '9999-12-31 23:59:59.997', 'aaa'),
(1312, '/aaa/sample/Assignments/sample/Assignments/Assignment9.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:26', 0, 'File Submitted', 1071, 0, '2019-05-01 12:49:26', 'Assignment9.html', '9999-12-31 23:59:59.997', 'aaa'),
(1313, '/aaa/sample/Assignments/sample/Assignments/assn03.htm', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 5966, 0, '2019-05-01 12:49:27', 'assn03.htm', '9999-12-31 23:59:59.997', 'aaa'),
(1314, '/aaa/sample/Assignments/sample/Assignments/directory.json', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 1724, 0, '2019-05-01 12:49:27', 'directory.json', '9999-12-31 23:59:59.997', 'aaa'),
(1315, '/aaa/sample/Assignments/sample/Assignments/jquery-3.3.1.min.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 86927, 0, '2019-05-01 12:49:27', 'jquery-3.3.1.min.js', '9999-12-31 23:59:59.997', 'aaa'),
(1316, '/aaa/sample/Assignments/sample/Assignments/people.css', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 644, 0, '2019-05-01 12:49:27', 'people.css', '9999-12-31 23:59:59.997', 'aaa'),
(1317, '/aaa/sample/Assignments/sample/Assignments/people.js', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 1110, 0, '2019-05-01 12:49:27', 'people.js', '9999-12-31 23:59:59.997', 'aaa'),
(1318, '/aaa/sample/Assignments/sample/Assignments/Resume (2).Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 8466, 0, '2019-05-01 12:49:27', 'Resume (2).Html', '9999-12-31 23:59:59.997', 'aaa'),
(1319, '/aaa/sample/Assignments/sample/Assignments/Resume.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 8342, 0, '2019-05-01 12:49:27', 'Resume.Html', '9999-12-31 23:59:59.997', 'aaa'),
(1320, '/aaa/sample/Assignments/sample/Assignments/Resume2.Html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:49:27', 0, 'File Submitted', 7698, 0, '2019-05-01 12:49:27', 'Resume2.Html', '9999-12-31 23:59:59.997', 'aaa'),
(1321, '/aaa/new/html.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-05-01 12:51:26', 0, 'File Submitted', 171, 0, '2019-05-01 12:51:26', 'html.html', '9999-12-31 23:59:59.997', 'aaa');

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `ID` int(11) NOT NULL,
  `Title` varchar(45) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`ID`, `Title`, `Name`, `Description`) VALUES
(16, 'INFO 4007', 'INFO 4007', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `coursesemestergroups`
--

CREATE TABLE `coursesemestergroups` (
  `CourseSemesterGroupID` int(11) NOT NULL,
  `CourseSemesterID` int(11) NOT NULL DEFAULT '1',
  `GroupID` int(11) NOT NULL DEFAULT '1'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `coursesemesters`
--

CREATE TABLE `coursesemesters` (
  `CourseSemesterID` int(11) NOT NULL,
  `Name` varchar(128) DEFAULT NULL,
  `CRN` int(11) DEFAULT NULL,
  `CourseID` int(11) NOT NULL DEFAULT '1',
  `SemesterID` int(11) NOT NULL DEFAULT '1',
  `YearID` int(11) NOT NULL DEFAULT '1',
  `SectionID` int(11) NOT NULL DEFAULT '1',
  `DateStart` datetime DEFAULT NULL,
  `DateEnd` datetime DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `coursesemesters`
--

INSERT INTO `coursesemesters` (`CourseSemesterID`, `Name`, `CRN`, `CourseID`, `SemesterID`, `YearID`, `SectionID`, `DateStart`, `DateEnd`) VALUES
(41, NULL, 124235, 16, 7, 10, 17, '2019-05-31 00:33:00', '2019-06-27 00:03:00');

-- --------------------------------------------------------

--
-- Table structure for table `coursesemesterusers`
--

CREATE TABLE `coursesemesterusers` (
  `CourseSemesterUserID` int(11) NOT NULL,
  `CourseSemesterID` int(11) NOT NULL DEFAULT '1',
  `UserID` int(11) NOT NULL DEFAULT '1'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `coursesemesterusers`
--

INSERT INTO `coursesemesterusers` (`CourseSemesterUserID`, `CourseSemesterID`, `UserID`) VALUES
(30, 40, 119),
(27, 37, 119);

-- --------------------------------------------------------

--
-- Table structure for table `groups`
--

CREATE TABLE `groups` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) NOT NULL,
  `UserName` varchar(128) NOT NULL,
  `Password` varchar(64) DEFAULT NULL,
  `ResetCode` varchar(128) DEFAULT NULL,
  `Salt` char(128) DEFAULT NULL,
  `DirectoryPath` varchar(264) DEFAULT NULL,
  `AssignmentID` int(11) DEFAULT NULL,
  `CourseSemesterID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `groups`
--

INSERT INTO `groups` (`ID`, `Name`, `UserName`, `Password`, `ResetCode`, `Salt`, `DirectoryPath`, `AssignmentID`, `CourseSemesterID`) VALUES
(50, 'r', 'r', '2j31HN5ShhZWLYUjkQEvnmEoWJQk0r4oCEVsGafFEA6PWAui9G/UHXvt3XQcKbG0', NULL, 'WHxXkdRbaRn74vvbCpqT3m0uLv2taZquEc0rXyu/ZLEBqRI7lI', NULL, NULL, NULL),
(51, 'sdfsdf', 'aaa', 'r0NN7kT0lEe+q2S4iyVvRxrghz3HRXhQh9BqIBpwgo9X+pVLoDaLmythy+3jqO8t', NULL, 'S3UYJzgn0AJIQP7G+u7gD1V8/1ACyiRDCu655iFf6Nn9Es0p6t', NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `groupsusers`
--

CREATE TABLE `groupsusers` (
  `GroupUserID` int(11) NOT NULL,
  `GroupID` int(11) NOT NULL DEFAULT '1',
  `UserID` int(11) NOT NULL DEFAULT '1'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Table structure for table `login_roles`
--

CREATE TABLE `login_roles` (
  `ID` int(11) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `login_roles`
--

INSERT INTO `login_roles` (`ID`, `Name`, `Description`) VALUES
(1, 'Admin', 'ClassWeb Admin'),
(2, 'Professor', 'Classweb Professor'),
(3, 'Student', 'ClassWeb Students');

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `ID` int(11) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `IsAdmin` bit(1) NOT NULL DEFAULT b'0',
  `Users` bit(4) NOT NULL DEFAULT b'0',
  `Role` bit(4) NOT NULL DEFAULT b'0',
  `Assignment` bit(4) NOT NULL DEFAULT b'0',
  `DateCreated` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateModified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`ID`, `Name`, `IsAdmin`, `Users`, `Role`, `Assignment`, `DateCreated`, `DateModified`) VALUES
(1, 'Admin', b'1', b'1111', b'1111', b'1111', '2019-04-06 15:35:38', '2019-04-30 21:12:26'),
(4, 'Student', b'0', b'0000', b'0000', b'1111', '2019-05-01 03:02:28', '2019-05-01 03:02:28'),
(41, 'Professor', b'1', b'1111', b'0010', b'1111', '2019-05-01 12:36:45', '2019-05-01 12:36:45');

-- --------------------------------------------------------

--
-- Table structure for table `sections`
--

CREATE TABLE `sections` (
  `SectionID` int(11) NOT NULL,
  `Number` int(45) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `sections`
--

INSERT INTO `sections` (`SectionID`, `Number`) VALUES
(17, 1);

-- --------------------------------------------------------

--
-- Table structure for table `semesters`
--

CREATE TABLE `semesters` (
  `ID` int(11) NOT NULL,
  `SemesterName` varchar(128) NOT NULL DEFAULT 'FALL'
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `semesters`
--

INSERT INTO `semesters` (`ID`, `SemesterName`) VALUES
(7, 'Summer');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `FirstName` varchar(50) NOT NULL DEFAULT 'null',
  `MiddleName` varchar(50) DEFAULT 'null',
  `LastName` varchar(50) NOT NULL DEFAULT 'null',
  `EmailAddress` varchar(50) NOT NULL,
  `VerificationCode` varchar(50) NOT NULL,
  `UserName` varchar(30) NOT NULL DEFAULT 'null',
  `Password` char(70) NOT NULL,
  `PhoneNumber` bigint(20) NOT NULL DEFAULT '-1',
  `DateCreated` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateModified` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `AccountExpired` tinyint(1) NOT NULL DEFAULT '0',
  `Archived` tinyint(1) NOT NULL DEFAULT '0',
  `PasswordExpired` tinyint(1) DEFAULT '0',
  `Enabled` tinyint(1) NOT NULL DEFAULT '1',
  `Salt` varchar(50) NOT NULL,
  `RoleID` int(11) NOT NULL DEFAULT '4',
  `ResetCode` varchar(45) NOT NULL DEFAULT 'null',
  `Storage` int(11) NOT NULL DEFAULT '1250'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`ID`, `FirstName`, `MiddleName`, `LastName`, `EmailAddress`, `VerificationCode`, `UserName`, `Password`, `PhoneNumber`, `DateCreated`, `DateModified`, `DateDeleted`, `AccountExpired`, `Archived`, `PasswordExpired`, `Enabled`, `Salt`, `RoleID`, `ResetCode`, `Storage`) VALUES
(1, 'Test', 'Z', 'test', 'simkkish@isu.edu', '', 'Admin', 'T0xEPRzigon35dU1RFJtsZ0Cvw8BC9WljGcXxdNmEpR2fUzd+3GseM72/kQNkQMT', 0, '0000-00-00 00:00:00', '2019-04-28 12:06:38', '2001-00-00 00:00:00', 0, 0, 0, 1, 'ZtPlALT8qigjo7oOo8wa7fqBgXgoFlayN5M/MHH+zQ8J0p5Ech', 1, '0c67a130-5804-4c24-98e4-587c64241f7d', 1250),
(108, 'Tester', 'tester', 'hacker', 'tester@test.com', '', 'Tester', 'EDWXD/Ty/ySKSd3WLp9BeOKpu8t+LyhdwC24MXyl4QQB/TuxHYumFbbDxLTx53dm', -1, '2019-04-30 16:24:26', '2019-05-01 02:02:30', '2019-04-30 16:24:26', 0, 0, 0, 1, 'w/JzKsdg427N/4qI7clsnGq4y4WleyZa4v8vaooltkKzAvicn8', 40, '', 1250),
(119, 'jon', NULL, 'holmes', 'holmjon2@isu.edu', ' ', 'holmjon2', '3HVC6JoGbks5swgLiCGnSkuW/Xk6TcSwQWzGRoCLWneaGXr/rXYOsKzp1MOf3q0p', -1, '2019-05-01 12:35:11', '2019-05-01 12:35:11', '2019-05-01 12:35:11', 0, 0, 0, 1, 'HxDrML79GAs8qFICgvozv90VNWWkl+9J87BT2RdGi8WIuJ4kN2', 41, 'null', 1250),
(120, 'aaa', NULL, 'aaa', 'aaa', ' ', 'aaa', 'hr1NE3s0fVcUhFyBkicGNa4xcQtBJEVYZVIShgnboMjnv3BzWugIrDhvRqeRAHrP', -1, '2019-05-01 12:46:04', '2019-05-01 12:46:04', '2019-05-01 12:46:04', 0, 0, 0, 1, 'zUvcA+pd1LYFKaA2m43e9dIyW+lns1kceRU9zRLHzH0qXRWJnt', 4, '1d9b861b-1d08-4053-91f8-dce0b7b9fe9f', 1250);

-- --------------------------------------------------------

--
-- Table structure for table `years`
--

CREATE TABLE `years` (
  `ID` int(11) NOT NULL,
  `Year` int(11) NOT NULL DEFAULT '2019',
  `Name` varchar(45) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

--
-- Dumping data for table `years`
--

INSERT INTO `years` (`ID`, `Year`, `Name`) VALUES
(10, 2019, '2019');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assignment`
--
ALTER TABLE `assignment`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `FileName_UNIQUE` (`FileLocation`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `ID_UNIQUE` (`ID`);

--
-- Indexes for table `coursesemestergroups`
--
ALTER TABLE `coursesemestergroups`
  ADD PRIMARY KEY (`CourseSemesterGroupID`),
  ADD KEY `CourseSemesters` (`CourseSemesterID`),
  ADD KEY `groups` (`GroupID`);

--
-- Indexes for table `coursesemesters`
--
ALTER TABLE `coursesemesters`
  ADD PRIMARY KEY (`CourseSemesterID`),
  ADD KEY `Courses` (`CourseID`),
  ADD KEY `Semesters` (`SemesterID`),
  ADD KEY `Years` (`YearID`),
  ADD KEY `Sections` (`SectionID`);

--
-- Indexes for table `coursesemesterusers`
--
ALTER TABLE `coursesemesterusers`
  ADD PRIMARY KEY (`CourseSemesterUserID`),
  ADD KEY `CourseSemesters` (`CourseSemesterID`),
  ADD KEY `Users` (`UserID`);

--
-- Indexes for table `groups`
--
ALTER TABLE `groups`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `CourseSemesterID` (`CourseSemesterID`),
  ADD KEY `AssignmentID` (`AssignmentID`);

--
-- Indexes for table `groupsusers`
--
ALTER TABLE `groupsusers`
  ADD PRIMARY KEY (`GroupUserID`),
  ADD KEY `Groups` (`GroupID`),
  ADD KEY `Users` (`UserID`);

--
-- Indexes for table `login_roles`
--
ALTER TABLE `login_roles`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `sections`
--
ALTER TABLE `sections`
  ADD PRIMARY KEY (`SectionID`);

--
-- Indexes for table `semesters`
--
ALTER TABLE `semesters`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `EmailAddress_UNIQUE` (`EmailAddress`);

--
-- Indexes for table `years`
--
ALTER TABLE `years`
  ADD PRIMARY KEY (`ID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `assignment`
--
ALTER TABLE `assignment`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=1322;

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `coursesemestergroups`
--
ALTER TABLE `coursesemestergroups`
  MODIFY `CourseSemesterGroupID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `coursesemesters`
--
ALTER TABLE `coursesemesters`
  MODIFY `CourseSemesterID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- AUTO_INCREMENT for table `coursesemesterusers`
--
ALTER TABLE `coursesemesterusers`
  MODIFY `CourseSemesterUserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT for table `groups`
--
ALTER TABLE `groups`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=52;

--
-- AUTO_INCREMENT for table `groupsusers`
--
ALTER TABLE `groupsusers`
  MODIFY `GroupUserID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=70;

--
-- AUTO_INCREMENT for table `login_roles`
--
ALTER TABLE `login_roles`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- AUTO_INCREMENT for table `sections`
--
ALTER TABLE `sections`
  MODIFY `SectionID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `semesters`
--
ALTER TABLE `semesters`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=121;

--
-- AUTO_INCREMENT for table `years`
--
ALTER TABLE `years`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `groups`
--
ALTER TABLE `groups`
  ADD CONSTRAINT `AssignmentID` FOREIGN KEY (`AssignmentID`) REFERENCES `assignment` (`ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
