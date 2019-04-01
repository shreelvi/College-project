-- -----------------------------------------------------
-- Table dbo.Roles
-- -----------------------------------------------------
CREATE TABLE `login_roles` (
  `RoleID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`RoleID`)
);

-- -----------------------------------------------------
-- Table dbo.Users
-- -----------------------------------------------------
CREATE TABLE `login_users` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `MiddleName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `LastName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `EmailAddress` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `UserName` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `Password` char(64) DEFAULT NULL,
  `RoleID` int(11) NOT NULL DEFAULT '2',
  `Salt` char(128) DEFAULT NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP,
  `DateModified` datetime DEFAULT CURRENT_TIMESTAMP,
  `DirectoryPath` string NOT NULL,
  PRIMARY KEY (`UserID`),
  KEY `FK1` (`RoleID`),
  CONSTRAINT `FK1` FOREIGN KEY (`RoleID`) REFERENCES `login_roles` (`RoleID`)
);

--
-- Table structure for table `assignment`
--

CREATE TABLE `assignment` (
  `AssignmentID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  `DateDue` datetime DEFAULT NULL,
  `DateSubmission` datetime DEFAULT CURRENT_TIMESTAMP,
  `Grade` char(10) DEFAULT NULL,
  `Feedback` varchar(128) DEFAULT NULL,
  `DateModified` datetime DEFAULT CURRENT_TIMESTAMP,
  `Size` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL
);

-- Indexes for table `assignment`
--
ALTER TABLE `assignment`
  ADD PRIMARY KEY (`AssignmentID`),
  ADD CONSTRAINT FK_AssignmentUser FOREIGN KEY (`UserID`) REFERENCES Login_users(UserID);
  

-- -----------------Section------------------------------
-- ======================================================

-- Author: Meshari
-- Create date:	31 March 2019
-- Description:	Create section table in the database
-- ======================================================
  CREATE TABLE `Sections` (
  `SectionID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) DEFAULT NULL,
  `SectionNumber` int(45) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
   PRIMARY KEY (`SectionID`)
   CONSTRAINT `Section_Users`
    FOREIGN KEY (`UserID`)
    REFERENCES `PeerVal`.`Users` (`UserID`)
);

  
