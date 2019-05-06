-- Description:	add users to the database
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

-- Description:	Get all the users from the database by username.
-- =============================================
DELIMITER $$
CREATE PROCEDURE `sproc_UserGetByUsername` (
IN `Username` VARCHAR(128)
)  
BEGIN
	 SELECT * FROM Users
	 WHERE Users.UserName = Username;
END$$
-- Description:	add assignments to the databas
-- =============================================
DELIMITER $$
CREATE PROCEDURE `sproc_AssignmentAdd` (
OUT `AssignmentID` INT, 
IN `Name` VARCHAR(45), 
IN `Feedback` VARCHAR(128), 
IN `UserID` INT)  
BEGIN
     INSERT INTO assignment(Name, Feedback, UserID)
     VALUES(Name, Feedback, UserID);
SET AssignmentID = LAST_INSERT_ID();
END$$


 
 -- Description:	Get all the assignments from the database by userID.
-- =============================================
DELIMITER $$
CREATE PROCEDURE `sproc_GetAssignmentsbyUserID` (
IN `UserID` INT
) 
   BEGIN 
	SELECT * FROM assignment 
	WHERE assignment.UserID = UserID 
	ORDER BY assignment.AssignmentID DESC; 
END $$

-- Description:	Check user in the database.
-- =============================================
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



-- Author: Elvis
-- Description:	Get all the users from the database.
-- =============================================
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
CREATE PROCEDURE sproc_UserRoleUpdate(
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
IN Number INT(45),
IN UserID INT(11),
IN CourseID INT(11)
)
BEGIN
     INSERT INTO Sections(CRN,Number, UserID, CourseID)
               VALUES(CRN,Number, UserID, CourseID);               
     SET SectionID = LAST_INSERT_ID();
END
$$

-- ================================================
-- Author: Meshari
-- Create date:	31 March 2019
-- Description:	Update the  section in the database.
-- ================================================
DELIMITER $$
CREATE PROCEDURE sproc_SectionUpdate(
IN SectionID int(11),
IN CRN int(11),
IN Number int(45),
IN UserID INT(11),
IN CourseID int(11)
)
BEGIN
     UPDATE Sections
          SET
               Sections.CRN = CRN,
               Sections.Number = Number,
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
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
DELIMITER $$
CREATE PROCEDURE `AddGroup` 
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
DELIMITER $$
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

-- -----------------Course---------------------------------
-- Description: sproc CRUD for Courses-------------------
-- Reference: PeerVal Project, Github------------------
-- ======================================================

-- Author: Mohan
-- sproc_CreateCourse
-- Description:	Add a new Course to the Database.
-- =============================================

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_CreateCourse`(OUT `ID` int, IN `Subject` VARCHAR(50), IN `CourseNumber` int, IN `CourseTitle` VARCHAR(50))
BEGIN
     INSERT INTO course(`Subject`, `CourseNumber`, `CourseTitle`)
		VALUES(`Subject`, `CourseNumber`, `CourseTitle`);
	SET ID = last_insert_id();
END

-- Author: Mohan
-- sproc_GetCourse
-- Description:	get Course from the Database.
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetCourse`(IN CourseID int)
BEGIN
     SELECT * FROM course
     WHERE course.CourseID = CourseID;
END$$


-- Author: Mohan
-- sproc_GetAllCourses
-- Description:	Gets all courses from the database. 
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetAllCourses`()
BEGIN
     SELECT * FROM course;
END$$

-- Author: Mohan
-- sproc_DeleteCourseByID
-- Description:	Delete a course. 
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetCourseByID`(IN `ID` INT)
BEGIN
Select * From course
Where course.ID=ID;
END


-- Author: Mohan
-- sproc_UpdateCourse
-- Description:	Edit course. 
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_UpdateCourse`(IN `ID` int, IN `Subject` int, IN `CourseNumber` VARCHAR(50), IN `CourseTitle` VARCHAR(50))
BEGIN
     UPDATE course
          SET
               course.`Subject` = `Subject`,
               course.`CourseNumber` = `CourseNumber`,
               course.`CourseTitle` = `CourseTitle`
          WHERE course.`ID` = `ID`;
END


-- Author: Mohan
-- sproc_DeleteCourseByID
-- Description:	Delete a course. 
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_DeleteCourseByID`(IN `ID` int)
BEGIN 
	DELETE FROM course 
		WHERE Course.ID = `ID`; 
END$$



-- Description:	get all assignment from database by filename
-- =============================================
DELIMITER $$

CREATE  PROCEDURE `sproc_AssignmentGetAllByFileName` (IN `VARCHAR` (, ``)  )
BEGIN
Select * from assignment
where  assignment.FileName=`FileName`;
End$$

-- Description:	get all assignment from database by UserName
-- =============================================

CREATE  PROCEDURE `sproc_AssignmentGetAllByUserName` (IN `UserName` VARCHAR(45))  BEGIN
Select * from assignment
where assignment.UserName=`UserName`;
End$$

-- Description:	get all assignment from database by UserNameAndLocation
-- =============================================
DELIMITER $$

CREATE  PROCEDURE `sproc_AssignmentGetAllByUserNameAndLocation` (
IN `UserName` VARCHAR(45), 
IN `FileLocation` VARCHAR(50)
)  
BEGIN
Select * from assignment
where assignment.UserName=`UserName` AND assignment.FileLocation like CONCAT(`FileLocation` , '%');
End$$

-- Description:	get all assignment from database by ID
-- =============================================
DELIMITER $$
CREATE PROCEDURE `sproc_AssignmentGetByID` (IN `ID` INT)  
BEGIN
Select * from assignment
where assignment.ID=`ID`;
End$$

-- Description:	get all assignment from database by Location
-- =============================================
DELIMITER $$

CREATE  PROCEDURE `sproc_AssignmentGetByLocation` (IN `FileLocation` VARCHAR(100))  
BEGIN
Select * from assignment
where FileLocation LIKE assignment.FileLocation;
End$$

-- Description:	get all assignment from database by NameLocationUserName
-- =============================================
DELIMITER $$

CREATE  PROCEDURE `sproc_AssignmentGetByNameLocationUserName`
(
IN `FileName` VARCHAR(50), 
IN `VARCHAR` (, ``)  ,
IN `UserName` varchar(50)
)
BEGIN
Select * from assignment
where assignment.FileLocation=`FileLocation`&&assignment.FileName=`FileName`&&assignment.UserName=`UserName`;
End$$

-- Description:	to resumbit the assignment
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_AssignmentResubmit` (IN `ID` INT, IN `VARCHAR` (, ``)  )
BEGIN
update Assignment
set Assignment.Feedback=Feedback
where Assignment.ID=ID;
End$$

-- Description:	to resubmit the assignment
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_ResubmitAssignment` (IN `ID` INT, IN `VARCHAR` (, ``)  )
BEGIN
update Assignment
set Assignment.Feedback=Feedback
where Assignment.ID=ID;
End$$

-- Description:	get all classes from database 
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_ClassesGetAll` ()  
BEGIN
select * From Classes;
END$$

-- Description:	get all assignment from database 
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_GetAllAssignment` ()  
BEGIN
Select * From assignment;
END$$

-- Description:	add role to the database
-- =============================================
DELIMITER $$
CREATE  PROCEDURE `sproc_RoleAdd` (
OUT `ID` INT, 
IN `Name` NVARCHAR(45), 
IN `IsAdmin` BIT(1), 
IN `Users` BIT(4), 
IN `Role` BIT(4), 
IN `Assignment` BIT(4)
)  
BEGIN
     INSERT INTO Roles(Name,IsAdmin,Users,Role,Assignment)
               VALUES(Name,IsAdmin,Users,Role, Assignment);               
     SET ID = last_insert_id();
END$$

-- Description:	delete role by ID
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_RoleDeleteByID` (IN `ID` INT) 
BEGIN
delete from Users
where Users.ID=`ID`;
END$$

-- Description:	get role  from database 
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_RoleGet` (IN `RoleID` INT)  BEGIN
     SELECT * FROM Roles
     WHERE Roles.RoleID = RoleID;
END$$


-- Description:	get role from database by ID
-- =============================================
DELIMITER $$
CREATE PROCEDURE `sproc_RoleGetByID` (
IN `ID` INT
)  
BEGIN 
SELECT * FROM roles WHERE roles.ID = `ID`;
END$$

-- Description:	remove role from database 
-- =============================================

CREATE PROCEDURE `sproc_RoleRemove` (
IN `RoleID` INT
)  
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Roles
          WHERE Roles.RoleID = RoleID;
END$$

-- Description:	update all role to the database
-- =============================================

CREATE PROCEDURE `sproc_RoleUpdate` (
IN `ID` INT, 
IN `Name` VARCHAR(45), 
IN `IsAdmin` BIT(1), 
IN `Users` BIT(4), 
IN `Role` BIT(4), 
IN `Assignment` BIT(4)
)  
BEGIN
     UPDATE Roles
          SET
               Roles.`Name` = `Name`,
               Roles.`IsAdmin` = `IsAdmin`,
               Roles.`Users` = `Users`,
               Roles.`Role` = `Role`,
               Roles.`Assignment` = `Assignment`
          WHERE Roles.`ID` = `ID`;
END$$

--
---- =============================
DELIMITER $$
CREATE PROCEDURE `sproc_SetSaltForUser` (IN `UserID` INT, IN `Salt` CHAR)  BEGIN
UPDATE Users
SET Users.Salt = Salt
WHERE Users.UserID = UserID;
END$$

-- Description:	add user
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_UserAdd` (
OUT `ID` INT, 
IN `FirstName` VARCHAR(50), 
IN `MiddleName` VARCHAR(45), 
IN `LastName` VARCHAR(50), 
IN `EmailAddress` VARCHAR(50), 
IN `UserName` VARCHAR(50), 
IN `Password` VARCHAR(100), 
IN `Salt` VARCHAR(50)
)  
BEGIN
INSERT INTO Users (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`)
 VALUES (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`);
     SET ID = last_insert_id();
End$$


-- Description:	add user by ID
-- =============================================
DELIMITER $$
CREATE  PROCEDURE `sproc_UserByID` (IN `id` INT)  BEGIN
Select * from users
Where users.ID=id;
END$$

-- Description:	delete  user by ID
-- =============================================
DELIMITER $$
CREATE PROCEDURE `sproc_UserDeleteByID` (IN `ID` INT)  BEGIN
delete from Users
where Users.ID=`ID`;
END$$


-- Description:	get all users from database
-- =============================================
DELIMITER $$
CREATE  PROCEDURE `sproc_UserGetAll` ()  BEGIN
Select * from users;
END$$


-- Description:	get user from database by EmailAddress
-- =============================================
DELIMITER $$
CREATE  PROCEDURE `sproc_UserGetByEmailAddress` (IN `u_EmailAddress` VARCHAR(50))  
BEGIN 
	SELECT a.FirstName, a.MiddleName, a.LastName, a.EmailAddress, a.Address, a.UserName, a.PhoneNumber, b.RoleID, b.Title FROM user u
	Inner JOIN userrole c on u.id = c.UserID
	LEFT OUTER JOIN role b on c.UserID = b.ID
	WHERE a.EmailAddress = u_EmailAddress; 
END$$

-- Description:	get user by ID
-- =============================================
DELIMITER $$

CREATE  PROCEDURE `sproc_UserGetByID` (IN `ID` INT) 
BEGIN
Select * From Users
Where Users.ID=ID;
END$$

--
-- ==================================
DELIMITER $$

CREATE PROCEDURE `sproc_GetSaltForUser` (IN `Username` VARCHAR(256))  
BEGIN
SELECT Salt FROM Users
WHERE Users.UserName = Username;
END$$

-- Description:	update password for user
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_UserPasswordUpdate` (IN `ID` INT, 
IN `Password` VARCHAR(70)
)  
BEGIN UPDATE users
SET users.Password = `Password`
WHERE users.ID=`ID`;
END$$


-- Description:	update user to the database
-- =============================================
DELIMITER $$

CREATE PROCEDURE `sproc_UserUpdate` (
IN `ID` INT, 
IN `FirstName` VARCHAR(50), 
IN `LastName` VARCHAR(50), 
IN `UserName` VARCHAR(50), 
IN `ResetCode` VARCHAR(50)
)  
BEGIN 
UPDATE users
SET users.FirstName = `FirstName`,
users.LastName = `LastName`,users.UserName = `UserName`,users.DateModified= NOW(),
users.ResetCode=`ResetCode`
WHERE users.ID=`ID`;
END$$

DELIMITER $$
CREATE PROCEDURE `add_Group` (IN `g_Name` VARCHAR(50), IN `g_EmailAddress` VARCHAR(50), IN `g_UserName` VARCHAR(50), IN `g_Password` CHAR(128), IN `g_Salt` CHAR(128), OUT `g_ID` INT)  BEGIN 
INSERT INTO groups(Name, EmailAddress, Username, Password, Salt) values (g_Name, g_EmailAddress, g_UserName,g_Password, g_Salt); 
SET g_ID = LAST_INSERT_ID();
END$$

DELIMITER $$

CREATE PROCEDURE `CheckGroupUsernameExists` (IN `username` VARCHAR(128))  BEGIN 
	SET @group_exists = 0; 
	SELECT 1 INTO @group_exists 
    FROM groups 
    WHERE groups.UserName = username; 
	SELECT @group_exists;
END$$

DELIMITER $$
CREATE PROCEDURE `CheckUserExistsInGroup` (IN `GroupID` INT, IN `UserID` INT)  BEGIN 
	SET @user_exists = 0; 
	SELECT 1 INTO @user_exists 
    FROM groupsusers 
    WHERE groupsusers.GroupID = GroupID and groupsusers.UserID = UserID; 
	SELECT @user_exists;
END$$

DELIMITER $$
CREATE  PROCEDURE `delete_GroupByID` (IN `ID` INT)  BEGIN
delete from groups
where groups.ID=ID;
END$$

DELIMITER $$
CREATE PROCEDURE `delete_GroupUserByID` (IN `GroupID` INT, IN `UserID` INT)  BEGIN 
delete from groupsusers where groupsusers.GroupID = GroupID AND
groupsusers.UserID = UserID;
END$$

DELIMITER $$
CREATE PROCEDURE `get_Group` ()  BEGIN 
SELECT * FROM groups; 
END$$

CREATE PROCEDURE `get_GroupByID` (IN `GroupID` INT)  BEGIN 
SELECT * from groups g where g.ID = GroupID;
END$$

DELIMITER $$
CREATE PROCEDURE `get_GroupByUserName` (IN `username` VARCHAR(128))  BEGIN 
SELECT * from groups g where g.UserName = username; 
END$$

DELIMITER $$
CREATE PROCEDURE `get_GroupUsersByID` (IN `GroupID` INT)  BEGIN
SELECT  users.ID, users.FirstName, users.LastName, users.EmailAddress 
 from groups 
  inner join groupsusers on groups.ID = groupsusers.GroupID
  inner join users on groupsusers.UserID = users.ID 
where groups.ID = GroupID; 
END$$

DELIMITER $$
CREATE PROCEDURE `sprocs_DeleteCourseByID` (IN `c_ID` INT(50))  BEGIN DELETE FROM course where ID = c_ID; 
END$$

DELIMITER $$
CREATE PROCEDURE `sprocs_EditCourse` (IN `c_CourseTitle` VARCHAR(50), IN `c_CourseName` VARCHAR(50))  BEGIN UPDATE course SET Name = c_CourseTitle, Number = c_CourseName ();
 END$$

 DELIMITER $$
CREATE PROCEDURE `sproc_AddGroup` (IN `Name` VARCHAR(45), IN `UserName` VARCHAR(128), IN `Password` CHAR(64), IN `Salt` CHAR(50), OUT `ID` INT)  BEGIN
     INSERT INTO groups(Name,UserName, Password, Salt)
     VALUES(Name,UserName, Password, Salt);
SET ID = LAST_INSERT_ID();
END$$

DELIMITER $$
CREATE PROCEDURE `sproc_AddUserToClass` (OUT `CourseSemesterUserID` INT, IN `CourseSemesterID` INT(11), IN `UserID` INT(11))  BEGIN
	INSERT INTO CourseSemesterUsers(CourseSemesterID, UserID)
    			VALUES (CourseSemesterID, UserID);
    SET CourseSemesterUserID = LAST_INSERT_ID();
END$$

DELIMITER $$
CREATE PROCEDURE `sproc_GetClassesForUser` (IN `ID` INT)  BEGIN
    SELECT coursesemesters.CourseSemesterID, coursesemesters.Name, coursesemesters.CRN, coursesemesters.CourseID, coursesemesters.SemesterID, coursesemesters.YearID, coursesemesters.SectionID, coursesemesters.DateStart, coursesemesters.DateEnd from users
    INNER JOIN coursesemesterusers
    ON users.ID = coursesemesterusers.UserID
    INNER JOIN coursesemesters
    ON coursesemesters.CourseSemesterID = coursesemesterusers.CourseSemesterID
    WHERE users.ID = ID AND coursesemesters.DateEnd > NOW();
END$$

DELIMITER $$
CREATE PROCEDURE `sproc_GetUsersForClass` (IN `CourseSemesterID` INT)  BEGIN
    SELECT users.ID, users.FirstName, users.LastName, users.EmailAddress 
    FROM coursesemesters
    INNER JOIN coursesemesterusers
    ON coursesemesters.CourseSemesterID = coursesemesterusers.CourseSemesterID
    INNER JOIN users
    ON users.ID = coursesemesterusers.UserID
    WHERE coursesemesters.CourseSemesterID = CourseSemesterID;
END$$

