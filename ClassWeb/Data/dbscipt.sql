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
VALUES ('Admin',1,b'1111',b'1111',b'1111') ,
('Power User',1,b'0111',b'0111',b'0111') ,
('Data Entry',1,b'0110',b'0110',b'0110');

-- -----------------------------------------------------
-- Table dbo.Users
-- -----------------------------------------------------
CREATE TABLE `Users` (
  `UserID` INT NOT NULL AUTO_INCREMENT,
  `FirstName` VARCHAR(45) NULL,
  `MiddleName` VARCHAR(45) NULL,
  `LastName` VARCHAR(45) NULL,
  `EmailAddress` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `UserName` VARCHAR(128) NULL,
  `Password` CHAR(64) NULL,
  `Salt` CHAR(50) NULL,
  `RoleID` INT DEFAULT 2,
  `DirectoryPath` VARCHAR(128) NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP,
  `DateModified` datetime DEFAULT CURRENT_TIMESTAMP,
  `DateDeleted` datetime NULL,
  PRIMARY KEY (`UserID`),
  INDEX `Users_Roles_idx` (`RoleID` ASC),
  CONSTRAINT `Users_Roles`
    FOREIGN KEY (`RoleID`)
    REFERENCES `Roles` (`RoleID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;

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
  
  
--Added column in Roles Table to store CRUD datetime
ALTER TABLE Roles
ADD DateCreated datetime DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE Roles
ADD DateModified datetime DEFAULT CURRENT_TIMESTAMP;
ALTER TABLE Roles
ADD DateDeleted datetime DEFAULT CURRENT_TIMESTAMP;



-- -----------------Section------------------------------
-- ======================================================

-- Author: Meshari
-- Create date:	31 March 2019
-- Description:	Create section table in the database
-- ======================================================
  CREATE TABLE `Sections` (
  `SectionID` int(11) NOT NULL AUTO_INCREMENT,
  `CRN` int(11) NOT NULL,
  `SectionNumber` int(45) DEFAULT NULL,
  `UserID` int(11) NOT NULL,
  `CourseID` int(11) NOT NULL,
   PRIMARY KEY (`SectionID`),
   CONSTRAINT `Section_Users`
    FOREIGN KEY (`UserID`)
    REFERENCES `Users` (`UserID`),
   CONSTRAINT `Section_Courses`
    FOREIGN KEY (`CourseID`)
    REFERENCES `Courses` (`CourseID`)
);

INSERT INTO `sections`(`SectionID`, `CRN`, `SectionNumber`, `UserID`, `CourseID`) VALUES (1, 25545, 02, 13, 1);
INSERT INTO `sections`(`SectionID`, `CRN`, `SectionNumber`, `UserID`, `CourseID`) VALUES (2, 36758, 01, 12, 1);
INSERT INTO `sections`(`SectionID`, `CRN`, `SectionNumber`, `UserID`, `CourseID`) VALUES (4, 36758, 01, 11, 2);



CREATE TABLE `Courses` (
  `CourseID` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `CourseTitle` VARCHAR(45) NOT NULL,
  `CourseName` VARCHAR(45)  NULL,
  `CourseDescription` VARCHAR(128) NULL);