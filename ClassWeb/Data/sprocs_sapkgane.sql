DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_Group`(IN `g_Name` VARCHAR(50), IN `g_EmailAddress` VARCHAR(50), IN `g_UserName` VARCHAR(50), IN `g_Password` CHAR(128), IN `g_Salt` CHAR(128))
BEGIN 
INSERT INTO groups(Name, EmailAddress, Username, Password) values (g_Name, g_EmailAddress, g_UserName,g_Password, g_Salt); 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_User`(IN `u_FirstName` VARCHAR(50), IN `u_MiddleName` VARCHAR(50), IN `u_LastName` VARCHAR(50), IN `u_EmailAddress` VARCHAR(50), IN `u_Address` VARCHAR(50), IN `u_UserName` VARCHAR(30), IN `u_Password` CHAR(70), IN `u_PhoneNumber` BIGINT, IN `u_DateCreated` DATETIME, IN `u_DateModified` DATETIME, IN `u_DateDeleted` DATETIME, IN `u_AccountExpired` TINYINT, IN `u_AccountLocked` TINYINT, IN `u_PasswordExpired` TINYINT, IN `u_Enabled` TINYINT)
BEGIN
     INSERT INTO user(FirstName,MiddleName,LastName,EmailAddress,Address,UserName,Password,PhoneNumber,DateCreated,DateModified, DateDeleted,AccountExpired,AccountLocked, PasswordExpired,Enabled)
     VALUES(u_FirstName,u_MiddleName,u_LastName,u_EmailAddress, u_Address,u_UserName,u_Password,u_PhoneNumber,Now(),NOW(),NOW(),u_AccountExpired,u_AccountLocked, u_PasswordExpired,u_Enabled);
     END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `CheckUserName_Group`(IN `username` VARCHAR(128))
BEGIN 
SET @group_exists = 0; 
SELECT 1 into @group_exists from groups where groups.Username = username; 
SELECT @group_exists; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `add_UserToGroup`(IN `g_EmailAddress` VARCHAR(50))
BEGIN
INSERT INTO groups(EmailAddress) values(g_EmailAddress);

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_Group`(IN `g_UserName` VARCHAR(50))
BEGIN 
DELETE from groups where UserName = g_Username;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_GroupByID`(IN `g_ID` INT)
BEGIN 
DELETE from groups where ID = g_ID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `delete_user`(IN `u_EmailAddress` VARCHAR(50))
BEGIN DELETE FROM user where EmailAddress = u_EmailAddress; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_AllRoles`()
BEGIN 
SELECT * FROM role; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_AssignmentbyGroupID`(IN `groupID` INT)
BEGIN 
SELECT * from assignment
where assignment.GroupID = groupID
ORDER BY assignment.ID DESC; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_Group`()
BEGIN 
SELECT * FROM groups; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_GroupByID`(IN `ID` INT)
BEGIN 
SELECT ID from groups g where g.ID = ID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_GroupByUserName`(IN `username` VARCHAR(50))
BEGIN 
SELECT Username from groups g where g.Username = username; END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_GroupUserByUserName`(IN `username` VARCHAR(50))
BEGIN
SELECT g.EmailAddress, u.FirstName, u.MiddleName, u.LastName  from groups g
Inner JOIN usergroup ug on g.ID = ug.GroupID
INNER JOIN user u on ug.UserID = u.ID
where g.Username = username; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_RoleByID`(IN `r_roleId` INT)
BEGIN SELECT * FROM roles WHERE roleID = r_roleId;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_SaltForGroup`(IN `username` VARCHAR(128))
BEGIN 
SELECT Salt FROM groups
where groups.Username = username; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_User`(IN `uid` INT)
SELECT * from user$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `get_UserByEmailAddress`(IN `u_EmailAddress` VARCHAR(50))
BEGIN SELECT a.FirstName, a.MiddleName, a.LastName, a.EmailAddress, a.Address, a.UserName, a.PhoneNumber, b.RoleID, b.Title FROM user u
Inner JOIN userrole c on u.id = c.UserID
LEFT OUTER JOIN role b on c.UserID = b.ID

WHERE a.EmailAddress = u_EmailAddress; 

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `list_roles`(IN `r_roleId` INT)
BEGIN SELECT ID, Title , Description FROM roles; 

END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `remove_Role`(IN `r_RoleID` INT)
BEGIN 
DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM role
          WHERE role.ID = r_RoleID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `remove_UserFromGroup`(IN `userID` INT)
BEGIN 
  DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
    DELETE from usergroup WHERE usergroup.UserID = user.ID;
   END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `set_SaltForGroup`(IN `GroupID` INT, IN `Salt` CHAR)
BEGIN
UPDATE groups
SET groups.Salt = Salt
WHERE groups.ID = GroupID; 
END$$
DELIMITER ;

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
CREATE DEFINER=`a458d6_shreelv`@`%` PROCEDURE `sproc_AssignmentAdd`(OUT `AssignmentID` INT, IN `Name` VARCHAR(45), IN `Feedback` VARCHAR(128), IN `UserID` INT)
BEGIN
     INSERT INTO assignment(Name, Feedback, UserID)
     VALUES(Name, Feedback, UserID);
SET AssignmentID = LAST_INSERT_ID();
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

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_Group`(IN `g_Name` VARCHAR(50), IN `g_EmailAddress` VARCHAR(50), IN `g_Username` VARCHAR(50), IN `g_Password` VARCHAR(50), IN `g_Salt` CHAR(50))
BEGIN
UPDATE groups g set g.Name = g_Name, g.EmailAddress = g_EmailAddress, g.Username = g_Username, g.Password = g_Password, g.Salt = g_Salt; 
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_Role`(IN `r_ID` INT, IN `r_Title` VARCHAR(45), IN `r_Description` VARCHAR(124), IN `r_DateModified` DATETIME)
BEGIN
     UPDATE Roles
          SET
              Title = r_Title, 
              Description = r_Description, 
              DateModified = NOW()
            
          WHERE ID = r_ID;
END$$
DELIMITER ;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `update_User`(IN `u_FirstName` VARCHAR(50), IN `u_MiddleName` VARCHAR(50), IN `u_LastName` VARCHAR(50), IN `u_EmailAddress` VARCHAR(50), IN `u_Address` VARCHAR(50), IN `u_UserName` VARCHAR(30), IN `u_Password` CHAR(70), IN `u_PhoneNumber` BIGINT, IN `u_DateCreated` DATETIME, IN `u_DateModified` DATETIME, IN `u_DateDeleted` DATETIME)
BEGIN UPDATE user SET FirstName = u_FirstName,MiddleName = u_MiddleName,LastName = u_LastName,EmailAddress = u_EmailAddress,Address = u_Address,UserName = u_UserName,Password = u_Password,PhoneNumber = u_PhoneNumber,DateModified = NOW(), DateDeleted = NOW(); END$$
DELIMITER ;
