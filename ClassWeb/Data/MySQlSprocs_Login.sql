-- -----------------User-----------------
-- =============================================
-- Author:		Elvis
-- Create date:	10 March 2019
-- Description:	Add a new  User to the database.
-- =============================================
CREATE PROCEDURE sproc_UserAdd(
OUT UserID int,
IN FirstName nvarchar(45),
IN MiddleName nvarchar(45),
IN LastName nvarchar(45),
IN EmailAddress nvarchar(128),
IN UserName nvarchar(128),
IN Password char(50),
IN Salt char(50)
)
BEGIN
     INSERT INTO login_Users(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt)
     VALUES(FirstName,MiddleName,LastName,EmailAddress,UserName, Password, Salt);
SET UserID = LAST_INSERT_ID();
END
$$

-- =============================================
-- Author:		Elvis
-- Create date:	10 March 2019
-- Description:	Retrieve specific User from the database.
-- =============================================
CREATE PROCEDURE sproc_getuserbyusername(
IN username NVARCHAR(128))
)
BEGIN
	 SELECT * FROM Login_Users
	 WHERE Login_Users.UserName = username;
END
$$

