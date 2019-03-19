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

CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_GetAssignmentsbyUserID` (IN `UserID` INT)  BEGIN
	 SELECT * FROM assignment
	 WHERE assignment.UserID = UserID;
END$$

DELIMITER $$
CREATE PROCEDURE `sproc_CheckUserName`
(IN `UserName` VARCHAR(128))
BEGIN
    SET @User_exists = 0;
    SELECT COUNT(1) INTO @found
    FROM `login_users`
    WHERE `UserName` = UserName;
    IF @found > 0 THEN
        SET @User_exists = 1;
    END IF;
    SELECT @User_exists;
END $$
DELIMITER ;

DELIMITER $$

CREATE PROCEDURE `sproc_GetAllUsers` ()  
BEGIN
	 SELECT * FROM login_users1;
END$$