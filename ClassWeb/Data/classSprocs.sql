DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_AddUser`(
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
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassAdd`(IN `ID` INT, IN `Title` VARCHAR(30), IN `IsAvailable` BOOLEAN, IN `DataStart` DATE, IN `DateEnd` DATE, IN `SectionID` INT)
BEGIN
     INSERT INTO class(ID,Title,IsAvailable,DateStart,DateEnd,SectionID)
               VALUES(ID,Title,IsAvailable,DateStart,DateEnd,SectionID);               
     SET ID = LAST_INSERT_ID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentAdd`(OUT `AssignmentID` INT, IN `Name` VARCHAR(45), IN `Feedback` VARCHAR(128), IN `UserID` INT)
BEGIN
     INSERT INTO assignment(Name, Feedback, UserID)
     VALUES(Name, Feedback, UserID);
SET AssignmentID = LAST_INSERT_ID();
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassDeleteByID`(
IN ID int
)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Class
          WHERE Class.ID = ID;

     -- SELECT -1 if we had an error
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassRemove`(IN `ID` INT(30), IN `Title` VARCHAR(30), IN `IsAvailable` BOOLEAN, IN `DateStart` DATE, IN `DateEnd` DATE, IN `SectionID` INT)
BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM class
          WHERE class.ID = ID;

     -- SELECT -1 if we had an error
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_ClassUpdate`(IN `ID` INT(11), IN `Title` VARCHAR(30), IN `IsAvailable` BOOLEAN, IN `DateStart` DATE, IN `DateEnd` DATE, IN `SectionID` INT(11))
BEGIN
     UPDATE Class
          SET
               Class.ID = ID,
               Class.Title = Title,
               Class.IsAvailable = IsAvailable,
               Class.DateStart = DateStart,
               Class.DateEnd = DateEnd,
			   Class.SectionID= SectionID
          WHERE Class.ID = ID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetUserByUsername`(IN `Username` VARCHAR(128))
BEGIN
	 SELECT * FROM Users
	 WHERE Users.UserName = Username;
END$$
DELIMITER ;
