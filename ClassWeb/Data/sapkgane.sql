-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 10, 2019 at 05:55 PM
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
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_Group` (IN `g_Name` VARCHAR(50), IN `g_EmailAddress` VARCHAR(50), IN `g_UserName` VARCHAR(50), IN `g_Password` CHAR(128), IN `g_Salt` CHAR(128))  BEGIN 
INSERT INTO groups(Name, EmailAddress, Username, Password) values (g_Name, g_EmailAddress, g_UserName,g_Password, g_Salt); 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `add_User` (IN `u_FirstName` VARCHAR(50), IN `u_MiddleName` VARCHAR(50), IN `u_LastName` VARCHAR(50), IN `u_EmailAddress` VARCHAR(50), IN `u_Address` VARCHAR(50), IN `u_UserName` VARCHAR(30), IN `u_Password` CHAR(70), IN `u_PhoneNumber` BIGINT, IN `u_DateCreated` DATETIME, IN `u_DateModified` DATETIME, IN `u_DateDeleted` DATETIME, IN `u_AccountExpired` TINYINT, IN `u_AccountLocked` TINYINT, IN `u_PasswordExpired` TINYINT, IN `u_Enabled` TINYINT)  BEGIN
     INSERT INTO user(FirstName,MiddleName,LastName,EmailAddress,Address,UserName,Password,PhoneNumber,DateCreated,DateModified, DateDeleted,AccountExpired,AccountLocked, PasswordExpired,Enabled)
     VALUES(u_FirstName,u_MiddleName,u_LastName,u_EmailAddress, u_Address,u_UserName,u_Password,u_PhoneNumber,Now(),NOW(),NOW(),u_AccountExpired,u_AccountLocked, u_PasswordExpired,u_Enabled);
     END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `add_UserToGroup` (IN `g_EmailAddress` VARCHAR(50))  BEGIN
INSERT INTO groups(EmailAddress) values(g_EmailAddress);

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `CheckUserName_Group` (IN `username` VARCHAR(128))  BEGIN 
SET @group_exists = 0; 
SELECT 1 into @group_exists from groups where groups.Username = username; 
SELECT @group_exists; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_Group` (IN `g_UserName` VARCHAR(50))  BEGIN 
DELETE from groups where UserName = g_Username;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_GroupByID` (IN `g_ID` INT)  BEGIN 
DELETE from groups where ID = g_ID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_user` (IN `u_EmailAddress` VARCHAR(50))  BEGIN DELETE FROM user where EmailAddress = u_EmailAddress; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_AllRoles` ()  BEGIN 
SELECT * FROM role; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_AssignmentbyGroupID` (IN `groupID` INT)  BEGIN 
SELECT * from assignment
where assignment.GroupID = groupID
ORDER BY assignment.ID DESC; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_Group` ()  BEGIN 
SELECT * FROM groups; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_GroupByID` (IN `ID` INT)  BEGIN 
SELECT ID from groups g where g.ID = ID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_GroupByUserName` (IN `username` VARCHAR(50))  BEGIN 
SELECT Username from groups g where g.Username = username; END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_GroupUserByUserName` (IN `username` VARCHAR(50))  BEGIN
SELECT g.EmailAddress, u.FirstName, u.MiddleName, u.LastName  from groups g
Inner JOIN usergroup ug on g.ID = ug.GroupID
INNER JOIN user u on ug.UserID = u.ID
where g.Username = username; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_RoleByID` (IN `r_roleId` INT)  BEGIN SELECT * FROM roles WHERE roleID = r_roleId;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_SaltForGroup` (IN `username` VARCHAR(128))  BEGIN 
SELECT Salt FROM groups
where groups.Username = username; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_User` (IN `uid` INT)  SELECT * from user$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_UserByEmailAddress` (IN `u_EmailAddress` VARCHAR(50))  BEGIN SELECT a.FirstName, a.MiddleName, a.LastName, a.EmailAddress, a.Address, a.UserName, a.PhoneNumber, b.RoleID, b.Title FROM user u
Inner JOIN userrole c on u.id = c.UserID
LEFT OUTER JOIN role b on c.UserID = b.ID

WHERE a.EmailAddress = u_EmailAddress; 

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `list_roles` (IN `r_roleId` INT)  BEGIN SELECT ID, Title , Description FROM roles; 

END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `remove_Role` (IN `r_RoleID` INT)  BEGIN 
DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM role
          WHERE role.ID = r_RoleID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `remove_UserFromGroup` (IN `userID` INT)  BEGIN 
  DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
    DELETE from usergroup WHERE usergroup.UserID = user.ID;
   END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `set_SaltForGroup` (IN `GroupID` INT, IN `Salt` CHAR)  BEGIN
UPDATE groups
SET groups.Salt = Salt
WHERE groups.ID = GroupID; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_AddUser` (OUT `UserID` INT, IN `FirstName` VARCHAR(45), IN `MiddleName` VARCHAR(45), IN `LastName` VARCHAR(45), IN `EmailAddress` VARCHAR(128), IN `UserName` VARCHAR(128), IN `Password` CHAR(64), IN `Salt` CHAR(50), IN `RoleID` INT, IN `DirectoryPath` VARCHAR(256))  BEGIN
     INSERT INTO Users(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, RoleID, DirectoryPath)
     VALUES(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, RoleID, DirectoryPath);
SET UserID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentAdd` (OUT `AssignmentID` INT, IN `Name` VARCHAR(45), IN `Feedback` VARCHAR(128), IN `UserID` INT)  BEGIN
     INSERT INTO assignment(Name, Feedback, UserID)
     VALUES(Name, Feedback, UserID);
SET AssignmentID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassAdd` (IN `ID` INT, IN `Title` VARCHAR(30), IN `IsAvailable` BOOLEAN, IN `DataStart` DATE, IN `DateEnd` DATE, IN `SectionID` INT)  BEGIN
     INSERT INTO class(ID,Title,IsAvailable,DateStart,DateEnd,SectionID)
               VALUES(ID,Title,IsAvailable,DateStart,DateEnd,SectionID);               
     SET ID = LAST_INSERT_ID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassDeleteByID` (IN `ID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Class
          WHERE Class.ID = ID;

     -- SELECT -1 if we had an error
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassRemove` (IN `ID` INT(30), IN `Title` VARCHAR(30), IN `IsAvailable` BOOLEAN, IN `DateStart` DATE, IN `DateEnd` DATE, IN `SectionID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM class
          WHERE class.ID = ID;

     -- SELECT -1 if we had an error
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassUpdate` (IN `ID` INT(11), IN `Title` VARCHAR(30), IN `IsAvailable` BOOLEAN, IN `DateStart` DATE, IN `DateEnd` DATE, IN `SectionID` INT(11))  BEGIN
     UPDATE Class
          SET
               Class.ID = ID,
               Class.Title = Title,
               Class.IsAvailable = IsAvailable,
               Class.DateStart = DateStart,
               Class.DateEnd = DateEnd,
			   Class.SectionID= SectionID
          WHERE Class.ID = ID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetUserByUsername` (IN `Username` VARCHAR(128))  BEGIN
	 SELECT * FROM Users
	 WHERE Users.UserName = Username;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `update_Group` (IN `g_Name` VARCHAR(50), IN `g_EmailAddress` VARCHAR(50), IN `g_Username` VARCHAR(50), IN `g_Password` VARCHAR(50), IN `g_Salt` CHAR(50))  BEGIN
UPDATE groups g set g.Name = g_Name, g.EmailAddress = g_EmailAddress, g.Username = g_Username, g.Password = g_Password, g.Salt = g_Salt; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `update_Role` (IN `r_ID` INT, IN `r_Title` VARCHAR(45), IN `r_Description` VARCHAR(124), IN `r_DateModified` DATETIME)  BEGIN
     UPDATE Roles
          SET
              Title = r_Title, 
              Description = r_Description, 
              DateModified = NOW()
            
          WHERE ID = r_ID;
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `update_User` (IN `u_FirstName` VARCHAR(50), IN `u_MiddleName` VARCHAR(50), IN `u_LastName` VARCHAR(50), IN `u_EmailAddress` VARCHAR(50), IN `u_Address` VARCHAR(50), IN `u_UserName` VARCHAR(30), IN `u_Password` CHAR(70), IN `u_PhoneNumber` BIGINT, IN `u_DateCreated` DATETIME, IN `u_DateModified` DATETIME, IN `u_DateDeleted` DATETIME)  BEGIN UPDATE user SET FirstName = u_FirstName,MiddleName = u_MiddleName,LastName = u_LastName,EmailAddress = u_EmailAddress,Address = u_Address,UserName = u_UserName,Password = u_Password,PhoneNumber = u_PhoneNumber,DateModified = NOW(), DateDeleted = NOW(); END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `assignment`
--

CREATE TABLE `assignment` (
  `ID` int(11) NOT NULL,
  `Title` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  `StartDate` datetime DEFAULT NULL,
  `DueDate` datetime DEFAULT NULL,
  `SubmissionDate` datetime DEFAULT NULL,
  `Grade` char(10) DEFAULT NULL,
  `Feedback` varchar(128) DEFAULT NULL,
  `Name` varchar(64) DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `DateUploaded` datetime DEFAULT NULL,
  `ResourceSize` int(11) DEFAULT NULL,
  `MaxSize` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `assignmentresource`
--

CREATE TABLE `assignmentresource` (
  `ID` int(11) NOT NULL,
  `AssignmentID` int(11) DEFAULT NULL,
  `ResourcecID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `class`
--

CREATE TABLE `class` (
  `ID` int(11) NOT NULL,
  `Title` varchar(30) NOT NULL,
  `IsAvailable` tinyint(1) NOT NULL,
  `DateStart` date NOT NULL,
  `DateEnd` date NOT NULL,
  `SectionID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `course`
--

CREATE TABLE `course` (
  `ID` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Number` varchar(10) DEFAULT NULL,
  `ClassID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `courseresource`
--

CREATE TABLE `courseresource` (
  `ID` int(11) NOT NULL,
  `CourseID` int(11) NOT NULL,
  `ResourceID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `databaseobject`
--

CREATE TABLE `databaseobject` (
  `ID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `databaseobject`
--

INSERT INTO `databaseobject` (`ID`) VALUES
(1);

-- --------------------------------------------------------

--
-- Table structure for table `groups`
--

CREATE TABLE `groups` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) DEFAULT NULL,
  `EmailAddress` varchar(64) DEFAULT NULL,
  `Username` varchar(30) DEFAULT NULL,
  `Password` varchar(64) DEFAULT NULL,
  `Salt` char(128) DEFAULT NULL,
  `AssignmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `permission`
--

CREATE TABLE `permission` (
  `ID` int(11) NOT NULL,
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
  `UserID` int(11) NOT NULL,
  `AssignmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `role`
--

CREATE TABLE `role` (
  `ID` int(11) NOT NULL,
  `Title` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `DateDeleted` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`ID`, `Title`, `Description`, `DateCreated`, `DateModified`, `DateDeleted`) VALUES
(1, 'Admin', 'hfdfghjvcvbn', '2019-03-04 00:00:00', '2019-03-04 00:00:00', '0000-00-00 00:00:00'),
(5, 'Grader', 'Grades Assignment', '2019-03-13 00:00:00', NULL, NULL),
(1, 'Admin', 'hfdfghjvcvbn', '2019-03-04 00:00:00', '2019-03-04 00:00:00', '0000-00-00 00:00:00'),
(5, 'Grader', 'Grades Assignment', '2019-03-13 00:00:00', NULL, NULL),
(1, 'Admin', 'hfdfghjvcvbn', '2019-03-04 00:00:00', '2019-03-04 00:00:00', '0000-00-00 00:00:00'),
(2, 'Admin', 'hfdfghjvcvbn', '2019-03-04 00:00:00', '2019-03-04 00:00:00', '0000-00-00 00:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `rolepermission`
--

CREATE TABLE `rolepermission` (
  `ID` int(11) NOT NULL,
  `RoleID` int(11) NOT NULL,
  `PermissionID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `section`
--

CREATE TABLE `section` (
  `ID` int(11) NOT NULL,
  `Number` int(11) NOT NULL,
  `DateStart` datetime NOT NULL,
  `DateEnd` datetime NOT NULL,
  `UserID` int(11) NOT NULL,
  `ClassID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `sectionresources`
--

CREATE TABLE `sectionresources` (
  `ID` int(11) NOT NULL,
  `SectionID` int(11) DEFAULT NULL,
  `ResourceID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `setionassignment`
--

CREATE TABLE `setionassignment` (
  `ID` int(11) NOT NULL,
  `SectionID` int(11) DEFAULT NULL,
  `AssignmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `ID` int(11) NOT NULL,
  `FirstName` varchar(50) DEFAULT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `LastName` varchar(50) DEFAULT NULL,
  `EmailAddress` varchar(50) NOT NULL,
  `Address` varchar(50) DEFAULT NULL,
  `UserName` varchar(30) DEFAULT NULL,
  `Password` char(70) DEFAULT NULL,
  `PhoneNumber` bigint(20) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `DateDeleted` datetime DEFAULT NULL,
  `AccountExpired` tinyint(1) DEFAULT NULL,
  `AccountLocked` tinyint(1) DEFAULT NULL,
  `PasswordExpired` tinyint(1) DEFAULT NULL,
  `Enabled` tinyint(1) DEFAULT NULL,
  `Salt` char(128) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`ID`, `FirstName`, `MiddleName`, `LastName`, `EmailAddress`, `Address`, `UserName`, `Password`, `PhoneNumber`, `DateCreated`, `DateModified`, `DateDeleted`, `AccountExpired`, `AccountLocked`, `PasswordExpired`, `Enabled`, `Salt`) VALUES
(2, 'Sam', 'K.', 'kartik', 'ksam@gmail.com', '5555 s ', 'ksam', NULL, 9999, '2019-03-03 00:00:00', '2019-03-04 00:00:00', NULL, NULL, NULL, NULL, NULL, NULL),
(4, 'Sam', 'K.', 'kartik', 'ksam33@gmail.com', '5555 s ', 'ksam', NULL, 9999, '2019-03-03 00:00:00', '2019-03-04 00:00:00', NULL, NULL, NULL, NULL, NULL, NULL),
(2, 'Sam', 'K.', 'kartik', 'ksam@gmail.com', '5555 s ', 'ksam', NULL, 9999, '2019-03-03 00:00:00', '2019-03-04 00:00:00', NULL, NULL, NULL, NULL, NULL, NULL),
(4, 'Sam', 'K.', 'kartik', 'ksam33@gmail.com', '5555 s ', 'ksam', NULL, 9999, '2019-03-03 00:00:00', '2019-03-04 00:00:00', NULL, NULL, NULL, NULL, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `usergroup`
--

CREATE TABLE `usergroup` (
  `ID` int(11) NOT NULL,
  `UserID` int(11) DEFAULT NULL,
  `GroupID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `userrole`
--

CREATE TABLE `userrole` (
  `ID` int(11) NOT NULL,
  `UserID` int(11) DEFAULT NULL,
  `RoleID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `usersection`
--

CREATE TABLE `usersection` (
  `ID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `SectionID` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assignment`
--
ALTER TABLE `assignment`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `assignmentresource`
--
ALTER TABLE `assignmentresource`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkSection` (`AssignmentID`),
  ADD KEY `fkResource` (`ResourcecID`);

--
-- Indexes for table `class`
--
ALTER TABLE `class`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkClass` (`ClassID`);

--
-- Indexes for table `courseresource`
--
ALTER TABLE `courseresource`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkCourse` (`CourseID`),
  ADD KEY `fkResource` (`ResourceID`);

--
-- Indexes for table `databaseobject`
--
ALTER TABLE `databaseobject`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `groups`
--
ALTER TABLE `groups`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkAssignment` (`AssignmentID`);

--
-- Indexes for table `permission`
--
ALTER TABLE `permission`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `resources`
--
ALTER TABLE `resources`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkUser` (`UserID`),
  ADD KEY `fkAssignment` (`AssignmentID`);

--
-- Indexes for table `rolepermission`
--
ALTER TABLE `rolepermission`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkRole` (`RoleID`),
  ADD KEY `fkPermission` (`PermissionID`);

--
-- Indexes for table `section`
--
ALTER TABLE `section`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkUser` (`UserID`),
  ADD KEY `fkClass` (`ClassID`);

--
-- Indexes for table `usergroup`
--
ALTER TABLE `usergroup`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkUser` (`UserID`),
  ADD KEY `GroupID` (`GroupID`);

--
-- Indexes for table `userrole`
--
ALTER TABLE `userrole`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkUser` (`UserID`),
  ADD KEY `fkRole` (`RoleID`);

--
-- Indexes for table `usersection`
--
ALTER TABLE `usersection`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkUser` (`UserID`),
  ADD KEY `fkSection` (`SectionID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `assignment`
--
ALTER TABLE `assignment`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `class`
--
ALTER TABLE `class`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `course`
--
ALTER TABLE `course`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `courseresource`
--
ALTER TABLE `courseresource`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `databaseobject`
--
ALTER TABLE `databaseobject`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `groups`
--
ALTER TABLE `groups`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `permission`
--
ALTER TABLE `permission`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `resources`
--
ALTER TABLE `resources`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `rolepermission`
--
ALTER TABLE `rolepermission`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `section`
--
ALTER TABLE `section`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
