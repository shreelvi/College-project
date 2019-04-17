-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 10, 2019 at 06:35 PM
-- Server version: 10.1.36-MariaDB
-- PHP Version: 7.2.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `web_masters`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_User` (IN `u_FirstName` VARCHAR(50), IN `u_MiddleName` VARCHAR(50), IN `u_LastName` VARCHAR(50), IN `u_EmailAddress` VARCHAR(50), IN `u_Address` VARCHAR(50), IN `u_UserName` VARCHAR(30), IN `u_Password` CHAR(70), IN `u_PhoneNumber` BIGINT, IN `u_DateCreated` DATETIME, IN `u_DateModified` DATETIME, IN `u_DateDeleted` DATETIME, IN `u_AccountExpired` TINYINT, IN `u_AccountLocked` TINYINT, IN `u_PasswordExpired` TINYINT, IN `u_Enabled` TINYINT)  BEGIN
     INSERT INTO user(FirstName,MiddleName,LastName,EmailAddress,Address,UserName,Password,PhoneNumber,DateCreated,DateModified, DateDeleted,AccountExpired,AccountLocked, PasswordExpired,Enabled)
     VALUES(u_FirstName,u_MiddleName,u_LastName,u_EmailAddress, u_Address,u_UserName,u_Password,u_PhoneNumber,Now(),NOW(),NOW(),u_AccountExpired,u_AccountLocked, u_PasswordExpired,u_Enabled);
     END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_user` (IN `u_EmailAddress` VARCHAR(50))  BEGIN DELETE FROM user where EmailAddress = u_EmailAddress; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_AllRoles` ()  BEGIN 
SELECT * FROM role; 
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `get_RoleByID` (IN `r_roleId` INT)  BEGIN SELECT * FROM roles WHERE roleID = r_roleId;
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
  `MaxSize` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `ResourceID` int(11) NOT NULL
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
  `Title` varchar(64) DEFAULT NULL,
  `Availability` tinyint(1) DEFAULT NULL,
  `DateStart` datetime DEFAULT NULL,
  `DateEnd` datetime DEFAULT NULL,
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
  `AssignmentID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `permission`
--

CREATE TABLE `permission` (
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
  `DateDeleted` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `role`
--

INSERT INTO `role` (`ID`, `Title`, `Description`, `DateCreated`, `DateModified`, `DateDeleted`) VALUES
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
  `SectionID` int(11) NOT NULL,
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
  `Role` varchar(128) NOT NULL,
  `RoleID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`ID`, `FirstName`, `MiddleName`, `LastName`, `EmailAddress`, `Address`, `UserName`, `Password`, `PhoneNumber`, `DateCreated`, `DateModified`, `DateDeleted`, `AccountExpired`, `AccountLocked`, `PasswordExpired`, `Enabled`, `Role`, `RoleID`) VALUES
(2, 'Sam', 'K.', 'kartik', 'ksam@gmail.com', '5555 s ', 'ksam', NULL, 9999, '2019-03-03 00:00:00', '2019-03-04 00:00:00', NULL, NULL, NULL, NULL, NULL, 'juygfghj', 1),
(4, 'Sam', 'K.', 'kartik', 'ksam33@gmail.com', '5555 s ', 'ksam', NULL, 9999, '2019-03-03 00:00:00', '2019-03-04 00:00:00', NULL, NULL, NULL, NULL, NULL, 'juygfghj', 1);

-- --------------------------------------------------------

--
-- Table structure for table `usergroup`
--

CREATE TABLE `usergroup` (
  `ID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `GroupID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `userrole`
--

CREATE TABLE `userrole` (
  `ID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `RoleID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `usersection`
--

CREATE TABLE `usersection` (
  `ID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `SectionID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assignment`
--
ALTER TABLE `assignment`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkUser` (`UserID`),
  ADD KEY `fkResource` (`ResourceID`);

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
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkSection` (`SectionID`);

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
-- Indexes for table `role`
--
ALTER TABLE `role`
  ADD PRIMARY KEY (`ID`);

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
-- Indexes for table `sectionresources`
--
ALTER TABLE `sectionresources`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkSection` (`SectionID`),
  ADD KEY `fkResource` (`ResourceID`);

--
-- Indexes for table `setionassignment`
--
ALTER TABLE `setionassignment`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkSection` (`SectionID`),
  ADD KEY `fkAssignment` (`AssignmentID`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `EmailAddress` (`EmailAddress`),
  ADD KEY `fkRole` (`RoleID`);

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
-- AUTO_INCREMENT for table `role`
--
ALTER TABLE `role`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

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

--
-- AUTO_INCREMENT for table `sectionresources`
--
ALTER TABLE `sectionresources`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `setionassignment`
--
ALTER TABLE `setionassignment`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `usergroup`
--
ALTER TABLE `usergroup`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `userrole`
--
ALTER TABLE `userrole`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `usersection`
--
ALTER TABLE `usersection`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `assignment`
--
ALTER TABLE `assignment`
  ADD CONSTRAINT `assignment_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `user` (`ID`) ON UPDATE CASCADE,
  ADD CONSTRAINT `assignment_ibfk_2` FOREIGN KEY (`ResourceID`) REFERENCES `resources` (`ID`) ON UPDATE CASCADE;

--
-- Constraints for table `class`
--
ALTER TABLE `class`
  ADD CONSTRAINT `class_ibfk_1` FOREIGN KEY (`SectionID`) REFERENCES `section` (`ID`);

--
-- Constraints for table `groups`
--
ALTER TABLE `groups`
  ADD CONSTRAINT `groups_ibfk_1` FOREIGN KEY (`AssignmentID`) REFERENCES `assignment` (`ID`) ON UPDATE CASCADE;

--
-- Constraints for table `resources`
--
ALTER TABLE `resources`
  ADD CONSTRAINT `resources_ibfk_1` FOREIGN KEY (`AssignmentID`) REFERENCES `assignment` (`ID`) ON UPDATE CASCADE,
  ADD CONSTRAINT `resources_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `user` (`ID`) ON UPDATE CASCADE;

--
-- Constraints for table `section`
--
ALTER TABLE `section`
  ADD CONSTRAINT `section_ibfk_1` FOREIGN KEY (`ClassID`) REFERENCES `class` (`ID`),
  ADD CONSTRAINT `section_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `user` (`ID`);

--
-- Constraints for table `user`
--
ALTER TABLE `user`
  ADD CONSTRAINT `user_ibfk_1` FOREIGN KEY (`RoleID`) REFERENCES `role` (`ID`) ON UPDATE CASCADE;

--
-- Constraints for table `usergroup`
--
ALTER TABLE `usergroup`
  ADD CONSTRAINT `usergroup_ibfk_1` FOREIGN KEY (`GroupID`) REFERENCES `groups` (`ID`) ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
