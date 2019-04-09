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
IN Assignment bit(4)
)
BEGIN
     INSERT INTO Roles(Name,IsAdmin,Users,Role,Assignment)
               VALUES(Name,IsAdmin,Users,Role, Assignment);               
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
IN Assignment bit(4)
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
CREATE PROCEDURE sproc_CourseGet(
IN CourseID int
)
BEGIN
     SELECT * FROM courses
     WHERE courses.CourseID = CourseID;
END
$$

-- =============================================
-- Author:		Meshari
-- Create date:	07 April 2019
-- Description:	Get the specified user from the database.
-- =============================================
CREATE PROCEDURE sproc_UserGet(
IN UserID int
)
BEGIN
     SELECT * FROM Users
     WHERE users.UserID = UserID;
END
$$

DELIMITER $$
CREATE PROCEDURE sproc_SectionRemoveByID(
IN SectionID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Sections
          WHERE Sections.SectionID = SectionID;

     -- SELECT -1 if we had an error
END
$$
