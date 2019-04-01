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
  'DirectoryPath` string NOT NULL,
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

--
-- Table structure for table `roles`
--
CREATE TABLE IF NOT EXISTS `Roles` ( 
`RoleID` INT NOT NULL AUTO_INCREMENT, 
`Name` VARCHAR(45) NULL, 
`IsAdmin` BIT(1) NULL, 
`Users` BIT(4) NULL, 
`Role` BIT(4) NULL, `
Assignment` BIT(4) NULL, 
PRIMARY KEY (`RoleID`)) ENGINE = InnoDB;

INSERT INTO `Roles`(
`RoleID`, `Name`, `IsAdmin`,`Users`, `Role`, `Assignment`) 
VALUES (null,'Admin',1,b'1111',b'1111',b'1111') ,
(null,'Power User',1,b'0111',b'0111',b'0111') ,
(null,'Data Entry',1,b'0110',b'0110',b'0110');

