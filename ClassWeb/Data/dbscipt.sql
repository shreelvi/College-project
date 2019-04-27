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
-- Modified date: 26 April
-- Deleted CRN column from the table
-- Description:	Create section table in the database
-- ======================================================
  CREATE TABLE `Sections` (
  `SectionID` int(11) NOT NULL AUTO_INCREMENT,
  `SectionNumber` int(45) DEFAULT NULL,
   PRIMARY KEY (`SectionID`)
);





CREATE TABLE `Courses` (
  `CourseID` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `CourseTitle` VARCHAR(45) NOT NULL,
  `CourseName` VARCHAR(45)  NULL,
  `CourseDescription` VARCHAR(128) NULL);

INSERT INTO `courses`(`CourseID`, `CourseTitle`, `CourseName`, `CourseDescription`) VALUES (1, 'INFO 4407', 'Database Design', 'INFO Database Design');
 INSERT INTO `courses`(`CourseID`, `CourseTitle`, `CourseName`, `CourseDescription`) VALUES (2, 'INFO 3307', 'System Design', 'System Design for INFO')
 INSERT INTO `courses`(`CourseID`, `CourseTitle`, `CourseName`, `CourseDescription`) VALUES (3, 'INFO 4482', 'System Development Implementation Method', 'Informatics Course')
-- -----------------CourseSemesters------------------------------
-- ======================================================

-- Author: Elvis
-- Create date:	09 April 2019
-- Modified date: 26 April 2019
-- Description:	Create CourseSemesters table in the database
-- ======================================================
  CREATE TABLE `CourseSemesters` (
  `CourseSemesterID` int(11) NOT NULL AUTO_INCREMENT,
  `CourseID` int(11) NOT NULL DEFAULT 1,
  `SemesterID` int(11) NOT NULL DEFAULT 1,
  `YearID` int(11) NOT NULL DEFAULT 1,
  `SectionID` int(11) NOT NULL DEFAULT 1,
  `UserID` int(11) NOT NULL,
   PRIMARY KEY (`CourseSemesterID`),
   CONSTRAINT `Courses`
    FOREIGN KEY (`CourseID`)
    REFERENCES `Courses` (`CourseID`),
   CONSTRAINT `Semesters`
    FOREIGN KEY (`SemesterID`)
    REFERENCES `Semesters` (`SemesterID`),
   CONSTRAINT `Years`
    FOREIGN KEY (`YearID`)
    REFERENCES `Years` (`YearID`),
   CONSTRAINT `Sections`
    FOREIGN KEY (`SectionID`)
    REFERENCES `Sections` (`SectionID`),
   CONSTRAINT `Users`
    FOREIGN KEY (`UserID`)
    REFERENCES `Users` (`UserID`)
);

ALTER TABLE `coursesemesters` ADD `CRN` INT(11) NULL AFTER `CourseSemesterID`;
ALTER TABLE `coursesemesters` ADD `DateStart` DATE NULL AFTER `UserID`, ADD `DateEnd` DATE NULL AFTER `DateStart`;

-- -----------------Years------------------------------
-- ======================================================

CREATE TABLE `Years` (
  `YearID` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `Year` int(11) NOT NULL DEFAULT 2019);

INSERT INTO `years`(`YearID`, `Year`) VALUES (1,2019);

-- -----------------Semesters------------------------------
-- ======================================================

CREATE TABLE `Semsters` (
  `SemesterID` int(11) NOT NULL PRIMARY KEY AUTO_INCREMENT,
  `SemesterName` NVARCHAR(128) NOT NULL DEFAULT 'FALL');

  INSERT INTO `semesters`(`SemesterID`, `SemesterName`) VALUES (1,'Fall')
INSERT INTO `semesters`(`SemesterID`, `SemesterName`) VALUES (2,'Spring')

-- -----------------Group------------------------------
-- Description:	Create Groups table in the database
-- Copied from Sakshi branch --- 
-- ======================================================
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

-- Author: Elvis
-- Create date:	20 April 2019
-- Description:	Create GroupsUsers association table in the database
-- ======================================================
CREATE TABLE `GroupsUsers` (
  `GroupUserID` int(11) NOT NULL AUTO_INCREMENT,
  `GroupID` int(11) NOT NULL DEFAULT 1,
  `UserID` int(11) NOT NULL DEFAULT 1,
   PRIMARY KEY (`GroupUserID`),
   CONSTRAINT `Groups`
    FOREIGN KEY (`GroupID`)
    REFERENCES `Groups` (`id`),
   CONSTRAINT `Users`
    FOREIGN KEY (`UserID`)
    REFERENCES `Users` (`UserID`)
 );

 -- -----------------CourseSemesterUsers------------------------------
-- Description:	Association Table for CourseSemesters and Users table.
-- As these two have many to many relationship.
-- ======================================================

-- Author: Elvis
-- Create date: 26 April 2019
-- Description:	Create CourseSemesterUsers table in the database
-- ======================================================
  CREATE TABLE `CourseSemesterUsers` ( 
  `CourseSemesterUserID` int(11) NOT NULL AUTO_INCREMENT, 
  `CourseSemesterID` int(11) NOT NULL DEFAULT 1, 
  `UserID` int(11) NOT NULL DEFAULT 1, 
  PRIMARY KEY (`CourseSemesterUserID`), 
  CONSTRAINT `CourseSemesters` 
   FOREIGN KEY (`CourseSemesterID`) 
   REFERENCES `CourseSemesters` (`CourseSemesterID`), 
  CONSTRAINT `Users` 
   FOREIGN KEY (`UserID`) 
   REFERENCES `Users` (`UserID`)
   );