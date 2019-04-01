-- -----------------User-----------------
-- =============================================
$$
CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_UserAdd` (OUT `UserID` INT, IN `FirstName` VARCHAR(45), IN `MiddleName` VARCHAR(45), IN `LastName` VARCHAR(45), IN `EmailAddress` VARCHAR(128), IN `UserName` VARCHAR(128), IN `Password` CHAR(50), IN `Salt` CHAR(50), IN `DirectoryPath` VARCHAR(256))  BEGIN
     INSERT INTO login_Users(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, DirectoryPath)
     VALUES(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt, DirectoryPath);
SET UserID = LAST_INSERT_ID();
END$$

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_getuserbyusername` (IN `username` VARCHAR(128))  BEGIN
	 SELECT * FROM Login_Users
	 WHERE Login_Users1.UserName = username;
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
DELIMITER :

DELIMITER $$
CREATE PROCEDURE `sproc_CheckUserName`
(IN `Username1` VARCHAR(128))
BEGIN
    SET @User_exists = 0;
    SELECT 1 INTO @User_exists
    FROM `login_users`
    WHERE login_Users.`UserName` = `Username1`;
    SELECT @User_exists;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `sproc_GetAllUsers` ()  
BEGIN
	 SELECT * FROM login_users1;
END$$

-- -----------------Sections---------------------------------


-- Author: Meshari
-- Create date:	01 April 2019
-- Description:	Add a new  section to the database.
-- =============================================
DELIMITER $$

CREATE PROCEDURE sproc_SectionAdd(
OUT SectionID int,
IN Name nvarchar(45),
IN SectionNumber INT(45),
IN UserID INT(11)
)
BEGIN
     INSERT INTO Roles(Name,SectionNumber, UserID)
               VALUES(Name,SectionNumber, UserID);               
     SET SectionID = LAST_INSERT_ID;
END
$$

-- ================================================
-- Author: Meshari
-- Create date:	31 March 2019
-- Description:	Update the  section in the database.
-- ================================================

CREATE PROCEDURE sproc_SectionUpdate(
IN SectionID int,
IN Name nvarchar(45),
IN SectionNumber int(45),
IN UserID INT(11)
)
BEGIN
     UPDATE Sections
          SET
               Sections.Name = Name,
               Sections.SectionNumber = SectionNumber,
               Sections.UserID = UserID
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