-- phpMyAdmin SQL Dump
-- version 4.8.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 25, 2019 at 05:29 AM
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

INSERT INTO `courses`(`CourseID`, `CourseTitle`, `CourseName`, `CourseDescription`) VALUES (1, 'INFO 4407', 'Database Design', 'INFO Database Design');
 INSERT INTO `courses`(`CourseID`, `CourseTitle`, `CourseName`, `CourseDescription`) VALUES (2, 'INFO 3307', 'System Design', 'System Design for INFO');
 INSERT INTO `courses`(`CourseID`, `CourseTitle`, `CourseName`, `CourseDescription`) VALUES (3, 'INFO 4482', 'System Development Implementation Method', 'Informatics Course');

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

INSERT INTO `Roles`(
`RoleID`, `Name`, `IsAdmin`,`Users`, `Role`, `Assignment`) 
VALUES ('Admin',1,b'1111',b'1111',b'1111') ,
('Power User',1,b'0111',b'0111',b'0111') ,
('Data Entry',1,b'0110',b'0110',b'0110');
--
-- Table structure for table `sections`
--

CREATE TABLE `sections` (
  `SectionID` int(11) NOT NULL,
  `CRN` int(11) NOT NULL,
  `SectionNumber` int(45) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `CourseID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------
INSERT INTO `sections`(`SectionID`, `CRN`, `SectionNumber`) VALUES (1, 25545, 02);
INSERT INTO `sections`(`SectionID`, `CRN`, `SectionNumber`) VALUES (2, 36758, 01);
INSERT INTO `sections`(`SectionID`, `CRN`, `SectionNumber`) VALUES (4, 36758, 01);

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

ALTER TABLE `assignment`
  ADD PRIMARY KEY (`AssignmentID`),
  ADD CONSTRAINT FK_AssignmentUser FOREIGN KEY (`UserID`) REFERENCES Login_users(UserID);
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
