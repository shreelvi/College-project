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
ALTER TABLE `coursesemesters` DROP `UserID`;
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

 -- -----------------Course------------------------------
-- ======================================================

-- Author: Mohan
-- Description:	Create course table in the database
-- ======================================================

CREATE TABLE `course` (
  `CourseID` int(11) PRIMARY KEY AUTO_INCREMENT,
  `Subject` VARCHAR(50) NOT NULL,
  `CourseNumber` INT(11)  NOT NULL,
  `CourseTitle` VARCHAR(50) NOT NULL);
  
INSERT INTO course(Subject,  CourseNumber,CourseTitle) 
	VALUES ( 'INFO', '4482', 'System Development Implementation Method');

	-- -----------------User-----------------
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_AddUser` (
OUT `UserID` INT, 
IN `FirstName` VARCHAR(45), 
IN `MiddleName` VARCHAR(45), 
IN `LastName` VARCHAR(45), 
IN `EmailAddress` VARCHAR(128), 
IN `UserName` VARCHAR(128), 
IN `Password` CHAR(64), 
IN `Salt` CHAR(50), 
IN `RoleID` int,
IN `DirectoryPath` VARCHAR(256))  
BEGIN
     INSERT INTO Users(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, RoleID, DirectoryPath)
     VALUES(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, RoleID, DirectoryPath);
SET UserID = LAST_INSERT_ID();
END$$

CREATE PROCEDURE `sproc_GetUserByUsername` (IN `Username` VARCHAR(128))  BEGIN
	 SELECT * FROM Users
	 WHERE Users.UserName = Username;
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentAdd` (OUT `AssignmentID` INT, IN `Name` VARCHAR(45), IN `Feedback` VARCHAR(128), IN `UserID` INT)  BEGIN
     INSERT INTO assignment(Name, Feedback, UserID)
     VALUES(Name, Feedback, UserID);
SET AssignmentID = LAST_INSERT_ID();
END$$

DELIMITER $$

//Sorts the assignment 
CREATE PROCEDURE `sproc_GetAssignmentsbyUserID1` (IN `UserID` INT) 
BEGIN 
	SELECT * FROM assignment 
	WHERE assignment.UserID = UserID 
	ORDER BY assignment.AssignmentID DESC; 
	END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE `sproc_CheckUserName1`
(IN `Username1` VARCHAR(128))
BEGIN
    SET @User_exists = 0;
    SELECT 1 INTO @User_exists
    FROM `Users`
    WHERE Users.`UserName` = `Username1`;
    SELECT @User_exists;
END $$
DELIMITER ;

DELIMITER $$

-- Author: Elvis
-- Description:	Get all the users from the database.
-- =============================================
CREATE PROCEDURE `sproc_GetAllUsers` ()  
BEGIN
	 SELECT * FROM Users;
END$$


-- -----------------Role---------------------------------
-----Description: sproc CRUD for ROLES-------------------
-----Reference: PeerVal Project, Github------------------
-----Taken and modified code to use in Classweb project--
-- ======================================================

-- Author: Elvis
-- Create date:	01 April 2019
-- Description:	Add a new  Role to the database.
-- =============================================
CREATE PROCEDURE sproc_RoleAdd(
OUT RoleID int,
IN Name nvarchar(45),
IN IsAdmin bit(1),
IN Users bit(4),
IN Role bit(4),
IN Assignment bit(4),
IN Course bit(4),
IN Semester bit(4),
IN Year bit(4),
IN Section bit(4),
IN CourseSemester bit(4)
)
BEGIN
     INSERT INTO Roles(Name,IsAdmin,Users,Role,Assignment, Course, Semester, Year, Section, CourseSemester)
               VALUES(Name,IsAdmin,Users,Role, Assignment, Course, Semester, Year, Section, CourseSemester);               
     SET RoleID = LAST_INSERT_ID;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	01 April 2019
-- Description:	Update Role in the database.
-- =============================================
CREATE PROCEDURE sproc_RoleUpdate(
IN RoleID int,
IN Name nvarchar(45),
IN IsAdmin bit(1),
IN Users bit(4),
IN Role bit(4),
IN Assignment bit(4),
IN Course bit(4),
IN Semester bit(4),
IN Year bit(4),
IN Section bit(4),
IN CourseSemester bit(4)
)
BEGIN
     UPDATE Roles
          SET
               Roles.Name = Name,
               Roles.IsAdmin = IsAdmin,
               Roles.Users = Users,
               Roles.Role = Role,
               Roles.Assignment = Assignment
          WHERE Roles.RoleID = RoleID;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	01 April 2019
-- Description:	Get specific Role from the database.
-- =============================================
CREATE PROCEDURE sproc_RoleGet(
IN RoleID int
)
BEGIN
     SELECT * FROM Roles
     WHERE Roles.RoleID = RoleID;
END
$$


-- =============================================
-- Author:		Elvis
-- Create date:	01 April 2019
-- Description:	Get all Roles from the database.
-- =============================================
CREATE PROCEDURE sproc_RolesGetAll()
BEGIN
     SELECT * FROM Roles;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	01 April 2019
-- Description:	Remove specific Role from the database.
-- =============================================
CREATE PROCEDURE sproc_RoleRemove(
IN RoleID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Roles
          WHERE Roles.RoleID = RoleID;

     -- SELECT -1 if we had an error
END
$$
-- ===============================================

-- =============================================
-- Author:		Elvis
-- Create date:	06 April 2019
-- Description:	Returns users with their roles information.
-- =============================================
CREATE PROCEDURE sproc_GetUsersWithRoles( 
IN UserID INT(11)
) 
BEGIN
    SELECT u.UserName, u.RoleID, r.Name, r.IsAdmin, r.users, r.Role, r.Assignment 
	From Users u
    JOIN Roles r ON u.RoleID = r.RoleID
    WHERE u.UserID = UserID;
    END
$$
-- ===============================================


 -----------------Sections---------------------------------


-- Author: Meshari
-- Create date:	01 April 2019
-- Description:	Add a new  section to the database.
-- =============================================
DELIMITER $$

CREATE PROCEDURE sproc_SectionAdd(
OUT SectionID int,
IN CRN int(11),
IN SectionNumber INT(45),
IN UserID INT(11),
IN CourseID INT(11)
)
BEGIN
     INSERT INTO Sections(CRN,SectionNumber, UserID, CourseID)
               VALUES(CRN,SectionNumber, UserID, CourseID);               
     SET SectionID = LAST_INSERT_ID();
END
$$

-- ================================================
-- Author: Meshari
-- Create date:	31 March 2019
-- Description:	Update the  section in the database.
-- ================================================

CREATE PROCEDURE sproc_SectionUpdate(
IN SectionID int(11),
IN CRN int(11),
IN SectionNumber int(45),
IN UserID INT(11),
IN CourseID int(11)
)
BEGIN
     UPDATE Sections
          SET
               Sections.CRN = CRN,
               Sections.SectionNumber = SectionNumber,
               Sections.UserID = UserID,
			   Sections.CourseID = CourseID
          WHERE Sections.SectionID = SectionID;
END
$$

-- =============================================
-- Author:		Meshari
-- Create date:	31 March 2019
-- Description:	Get specific Section from the database.
-- =============================================
CREATE PROCEDURE sproc_SectionGet(
IN SectionID int
)
BEGIN
     SELECT * FROM Sections
     WHERE Sections.SectionID = SectionID;
END
$$


-- =============================================
-- Author:		Meshari
-- Create date:	31 March 2019
-- Description:	Get all sections from the database.
-- =============================================
CREATE PROCEDURE sproc_SectionsGetAll()
BEGIN
     SELECT * FROM Sections;
END
$$

-- =============================================
-- Author:		Meshari
-- Create date:	31 March 2019
-- Description:	Remove specific section from the database.
-- =============================================
CREATE PROCEDURE sproc_SectionRemove(
IN RoleID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Sections
          WHERE Sections.SectionID = SectionID;

     -- SELECT -1 if we had an error
END
$$
-- ===============================================

-- =============================================
-- Author:		Meshari
-- Create date:	07 April 2019
-- Description:	Get the specified course from the database.
-- =============================================
CREATE PROCEDURE sproc_GetCourse(
IN CourseID int
)
BEGIN
     SELECT * FROM courses
     WHERE courses.CourseID = CourseID;
END
$$

 -----------------Semesters---------------------------------


-- Author: Elvis
-- Create date:	09 April 2019
-- Description:	Add a new  Semester object to the database.
-- =============================================
DELIMITER $$

CREATE PROCEDURE sproc_SemesterAdd(
OUT SemesterID int,
IN SemesterName varchar(128)
)
BEGIN
     INSERT INTO Semesters(SemesterName)
               VALUES(SemesterName);               
     SET SemesterID = LAST_INSERT_ID();
END
$$

-- ================================================
-- Author: Elvis
-- Create date:	09 April 2019
-- Description:	Update the  Semester object in the database.
-- ================================================

CREATE PROCEDURE sproc_SemesterEdit(
IN SemesterID int,
IN SemesterName varchar(128)
)
BEGIN
     UPDATE Semesters
          SET
               Semesters.SemesterName = SemesterName
			  
          WHERE Semesters.SemesterID = SemesterID;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Get specific Semester object from the database.
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_SemesterGet(
IN SemesterID int
)
BEGIN
     SELECT * FROM semesters
     WHERE semesters.SemesterID = SemesterID;
END
$$


-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Get all Semester from the database.
-- =============================================
CREATE PROCEDURE sproc_SemesterGetAll()
BEGIN
     SELECT * FROM Semesters;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Remove specific Semester from the database.
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_SemesterRemove(
IN SemesterID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM semesters
          WHERE semesters.SemesterID = SemesterID;

     -- SELECT -1 if we had an error
END
$$

 -----------------CourseSemesters---------------------------------
 
-- ================================================
-- Author: Elvis
-- Create date:	09 April 2019
-- Description:	Update the  CourseSemester object in the database.
-- ================================================

CREATE PROCEDURE sproc_CourseSemesterEdit(
IN SectionID int(11),
IN CourseID int(11),
IN SemesterID INT(11),
IN YearID INT(11),
IN SectionID INT(11),
IN UserID INT(11)
)
BEGIN
     UPDATE Sections
          SET
               CourseSemesters.CourseID = CourseID,
			   CourseSemesters.SemesterID = SemesterID,
			   CourseSemesters.YearID = YearID,
			   CourseSemesters.SectionID = SectionID,
			   CourseSemesters.CourseID = CourseID,
               CourseSemesters.UserID = UserID
          WHERE CourseSemesters.CourseSemesterID = CourseSemesterID;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Get specific CourseSemester object from the database.
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_CourseSemesterGet(
IN CourseSemesterID int
)
BEGIN
     SELECT * FROM coursesemesters
     WHERE coursesemesters.CourseSemesterID = CourseSemesterID;
END
$$


-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Get all CourseSemester from the database.
-- =============================================
CREATE PROCEDURE sproc_CourseSemestersGetAll()
BEGIN
     SELECT * FROM CourseSemesters;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Remove specific CourseSemester from the database.
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_CourseSemesterRemove(
IN CourseSemesterID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM coursesemesters
          WHERE coursesemesters.CourseSemesterID = CourseSemesterID;

     -- SELECT -1 if we had an error
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Remove specific CourseSemester from the database.
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_CourseSemesterRemove(
IN CourseSemesterID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM CourseSemesters
          WHERE CourseSemesters.CourseSemesterID = CourseSemesterID;

     -- SELECT -1 if we had an error
END
$$

 -----------------Years---------------------------------


-- Author: Elvis
-- Create date:	09 April 2019
-- Description:	Add a new  year object to the database.
-- =============================================
DELIMITER $$

CREATE PROCEDURE sproc_YearAdd(
OUT YearID int,
IN YEAR int(11)
)
BEGIN
     INSERT INTO Years(Year)
               VALUES(Year);               
     SET YearID = LAST_INSERT_ID();
END
$$

-- ================================================
-- Author: Elvis
-- Create date:	09 April 2019
-- Description:	Update the  Year object in the database.
-- ================================================

CREATE PROCEDURE sproc_YearEdit(
IN YearID int,
IN Year int(11)
)
BEGIN
     UPDATE Years
          SET
               Years.Year = Year
			  
          WHERE Years.YearID = YearID;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Get specific Year object from the database.
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_YearGet(
IN YearID int
)
BEGIN
     SELECT * FROM Years
     WHERE Years.YearID = YearID;
END
$$


-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Get all the Year object from the database.
-- =============================================
CREATE PROCEDURE sproc_YearGetAll()
BEGIN
     SELECT * FROM Years;
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	09 April 2019
-- Description:	Remove specific year from the database.
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_YearRemove(
IN YearID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM years
          WHERE years.YearID = YearID;

     -- SELECT -1 if we had an error
END
$$


DELIMITER $$
CREATE PROCEDURE sproc_CourseAdd(
OUT CourseID int,
IN CourseTitle varchar(45)
IN CourseName varchar(45)
IN CourseDescription varchar(128)

)
BEGIN
     INSERT INTO courses(CourseTitle, CourseName, CourseDescription)
               VALUES(CourseTitle, CourseName, CourseDescription);               
     SET CourseID = LAST_INSERT_ID();
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	20 April 2019
-- Description:	Checks if user exist and return their ID 
-- =============================================
DELIMITER $$
CREATE PROCEDURE `sproc_CheckUserByEmail`
(IN `EmailAddress` VARCHAR(128))
BEGIN
    SET @User_id = 0;
    SELECT Users.UserID INTO @User_id
    FROM `Users`
    WHERE Users.`EmailAddress` = `EmailAddress`;
    SELECT @User_id;
END $$

 -----------------Group---------------------------------
-- Create date:	20 April 2019
-- Copied code from Sakshi branch to work on adding users to group
-------------------------------------------------------------

CREATE PROCEDURE `add_Group` 
(IN `g_Name` VARCHAR(50), 
IN `g_EmailAddress` VARCHAR(50), 
IN `g_UserName` VARCHAR(50), 
IN `g_Password` CHAR(128), 
IN `g_Salt` CHAR(128), 
OUT `g_ID` INT)  
BEGIN 
INSERT INTO groups(Name, EmailAddress, Username, Password, Salt) values (g_Name, g_EmailAddress, g_UserName,g_Password, g_Salt); 
SET g_ID = LAST_INSERT_ID();
END$$

DELIMITER $$
CREATE PROCEDURE `get_GroupByUserName` (IN `username` VARCHAR(128)) 
BEGIN 
SELECT * from groups g where g.Username = username; 
END$$

-- =============================================
-- Author:		Elvis
-- Create date:	20 April 2019
-- Description:	Add users to group by adding data to groupuser association
-- =============================================
DELIMITER $$
CREATE PROCEDURE sproc_AddUserToGroup(
OUT GroupUserID int,
IN GroupID int(11),
IN UserID INT(11)
)
BEGIN
	INSERT INTO GroupsUsers(GroupID, UserID)
    			VALUES (GroupID, UserID);
    SET GroupUserID = LAST_INSERT_ID();
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	21 April 2019
-- Description:	Gets the list of users in group corresponding to the groupID
-- =============================================
CREATE PROCEDURE sproc_GetUsersFromGroup(
IN GroupID int
)
BEGIN
    SELECT groups.id, users.UserID, users.FirstName, users.EmailAddress
    FROM groups
    INNER JOIN groupsusers
    ON groups.id = groupsusers.GroupID
    INNER JOIN users
    ON groupsusers.UserID = users.UserID
    WHERE groups.id = GroupID;
END
$$


