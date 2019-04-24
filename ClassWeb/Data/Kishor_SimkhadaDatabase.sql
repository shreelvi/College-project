
DELIMITER $$
CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentAdd` (OUT `ID` INT, IN `FileLocation` VARCHAR(100), IN `FileName` VARCHAR(45), IN `DateSubmited` DATETIME, IN `Feedback` VARCHAR(50), IN `FileSize` INT, IN `Grade` FLOAT, IN `IsEditable` BOOL, IN `DateModified` DATETIME, IN `UserName` VARCHAR(45))  BEGIN
INSERT INTO `db_a45fe7_classwe`.`assignment` (`FileLocation`,`FileName`, `DateSubmited`,
 `Feedback`, `FileSize`,`Grade`, `IsEditable`, `DateModified`,`UserName`)
 VALUES (`FileLocation`,`FileName`, `DateSubmited`,
 `Feedback`, `FileSize`,`Grade`, `IsEditable`, `DateModified`,`UserName`);
     SET ID = last_insert_id();
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentDeleteByID` (IN `ID` INT)  BEGIN
DELETE FROM assignment
Where Assignment.ID=`ID`;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentGetAllByFileName` (IN `VARCHAR` (, ``)  )
BEGIN
Select * from assignment
where  assignment.FileName=`FileName`;
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentGetAllByUserName` (IN `UserName` VARCHAR(45))  BEGIN
Select * from assignment
where assignment.UserName=`UserName`;
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentGetAllByUserNameAndLocation` (IN `UserName` VARCHAR(45), IN `FileLocation` VARCHAR(50))  BEGIN
Select * from assignment
where assignment.UserName=`UserName` AND assignment.FileLocation like CONCAT(`FileLocation` , '%');
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentGetByFileName` (IN `VARCHAR` (, ``)  )
BEGIN
Select * from assignment
where assignment.FileName=`FileName`;
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentGetByID` (IN `ID` INT)  BEGIN
Select * from assignment
where assignment.ID=`ID`;
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentGetByLocation` (IN `FileLocation` VARCHAR(100))  BEGIN
Select * from assignment
where FileLocation LIKE assignment.FileLocation;
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentGetByNameLocationUserName` (IN `FileName` VARCHAR(50), IN `VARCHAR` (, ``)  ,IN `UserName` varchar(50))
BEGIN
Select * from assignment
where assignment.FileLocation=`FileLocation`&&assignment.FileName=`FileName`&&assignment.UserName=`UserName`;
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_AssignmentResubmit` (IN `ID` INT, IN `VARCHAR` (, ``)  )
BEGIN
update Assignment
set Assignment.Feedback=Feedback
where Assignment.ID=ID;
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_ClassesGetAll` ()  BEGIN
select * From Classes;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_CreateCourse` (OUT `ID` INT, IN `Name` VARCHAR(50), IN `Number` INT, IN `ClassID` INT)  Begin
insert Into courses(`Name`,`Number`,`ClassID`)values(`Name`,`Number`,`ClassID`);
 SET ID = last_insert_id();
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_GetAllAssignment` ()  BEGIN
Select * From assignment;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_RoleAdd` (OUT `ID` INT, IN `Name` NVARCHAR(45), IN `IsAdmin` BIT(1), IN `Users` BIT(4), IN `Role` BIT(4), IN `Assignment` BIT(4))  BEGIN
     INSERT INTO Roles(Name,IsAdmin,Users,Role,Assignment)
               VALUES(Name,IsAdmin,Users,Role, Assignment);               
     SET ID = last_insert_id();
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_RoleDeleteByID` (IN `ID` INT)  BEGIN
delete from Users
where Users.ID=`ID`;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_RoleGet` (IN `RoleID` INT)  BEGIN
     SELECT * FROM Roles
     WHERE Roles.RoleID = RoleID;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_RoleGetByID` (IN `ID` INT)  BEGIN SELECT * FROM roles WHERE roles.ID = `ID`;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_RoleRemove` (IN `RoleID` INT)  BEGIN
     DECLARE EXIT HANDLER FOR SQLEXCEPTION SELECT -1;
     DELETE FROM Roles
          WHERE Roles.RoleID = RoleID;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_RolesGetAll` ()  BEGIN 
SELECT * FROM roles; 
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_RoleUpdate` (IN `ID` INT, IN `Name` VARCHAR(45), IN `IsAdmin` BIT(1), IN `Users` BIT(4), IN `Role` BIT(4), IN `Assignment` BIT(4))  BEGIN
     UPDATE Roles
          SET
               Roles.`Name` = `Name`,
               Roles.`IsAdmin` = `IsAdmin`,
               Roles.`Users` = `Users`,
               Roles.`Role` = `Role`,
               Roles.`Assignment` = `Assignment`
          WHERE Roles.`ID` = `ID`;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_SetSaltForUser` (IN `UserID` INT, IN `Salt` CHAR)  BEGIN
UPDATE Users
SET Users.Salt = Salt
WHERE Users.UserID = UserID;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserAdd` (OUT `ID` INT, IN `FirstName` VARCHAR(50), IN `MiddleName` VARCHAR(45), IN `LastName` VARCHAR(50), IN `EmailAddress` VARCHAR(50), IN `UserName` VARCHAR(50), IN `Password` VARCHAR(100), IN `Salt` VARCHAR(50))  BEGIN
INSERT INTO Users (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`)
 VALUES (`FirstName`,`MiddleName`, `LastName`,
 `EmailAddress`, `UserName`,`Password`, `Salt`);
     SET ID = last_insert_id();
End$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserByID` (IN `id` INT)  BEGIN
Select * from users
Where users.ID=id;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserDeleteByID` (IN `ID` INT)  BEGIN
delete from Users
where Users.ID=`ID`;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserGetAll` ()  BEGIN
Select * from users;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserGetByEmailAddress` (IN `u_EmailAddress` VARCHAR(50))  BEGIN SELECT a.FirstName, a.MiddleName, a.LastName, a.EmailAddress, a.Address, a.UserName, a.PhoneNumber, b.RoleID, b.Title FROM user u
Inner JOIN userrole c on u.id = c.UserID
LEFT OUTER JOIN role b on c.UserID = b.ID
WHERE a.EmailAddress = u_EmailAddress; 
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserGetByID` (IN `ID` INT)  BEGIN
Select * From Users
Where Users.ID=ID;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserGetByUserName` (IN `UserName` NVARCHAR(128))  BEGIN
SELECT * FROM Users
WHERE Users.UserName = UserName;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserGetSalt` (IN `Username` VARCHAR(256))  BEGIN
SELECT Salt FROM Users
WHERE Users.UserName = Username;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserPasswordUpdate` (IN `ID` INT, IN `Password` VARCHAR(70))  BEGIN UPDATE users
SET users.Password = `Password`
where users.ID=`ID`;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserRoleUpdate` (IN `ID` INT, IN `RoleID` INT)  BEGIN UPDATE users
SET users.RoleID = `RoleID`
where users.ID=`ID`;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UsersGetALL` ()  BEGIN
SELECT * FROM Users;
END$$

CREATE DEFINER=`a45fe7_classwe`@`%` PROCEDURE `sproc_UserUpdate` (IN `ID` INT, IN `FirstName` VARCHAR(50), IN `LastName` VARCHAR(50), IN `UserName` VARCHAR(50), IN `ResetCode` VARCHAR(50))  BEGIN UPDATE users
SET users.FirstName = `FirstName`,
users.LastName = `LastName`,users.UserName = `UserName`,users.DateModified= NOW(),
users.ResetCode=`ResetCode`
where users.ID=`ID`;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `assignment`
--

CREATE TABLE `assignment` (
  `ID` int(11) NOT NULL,
  `FileLocation` varchar(100) NOT NULL,
  `DateStarted` datetime NOT NULL,
  `DateDue` datetime NOT NULL,
  `DateSubmited` datetime NOT NULL,
  `Grade` float NOT NULL DEFAULT '-1',
  `Feedback` varchar(50) NOT NULL,
  `FileSize` int(11) NOT NULL DEFAULT '-1',
  `IsEditable` tinyint(1) NOT NULL DEFAULT '-1',
  `DateModified` datetime NOT NULL,
  `FileName` varchar(45) NOT NULL,
  `DateDeleted` varchar(45) NOT NULL,
  `UserName` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `assignment`
--

INSERT INTO `assignment` (`ID`, `FileLocation`, `DateStarted`, `DateDue`, `DateSubmited`, `Grade`, `Feedback`, `FileSize`, `IsEditable`, `DateModified`, `FileName`, `DateDeleted`, `UserName`) VALUES
(926, '/robots.txt', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-04-20 22:46:48', 0, 'File Submitted', 0, 0, '2019-04-20 22:46:48', 'robots.txt', '', 'Admin'),
(957, '/Starting Page.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-04-20 23:11:04', 0, 'File Submitted', 2735, 0, '2019-04-20 23:11:04', 'Starting Page.html', '', 'Admin'),
(997, '/ViewStorage.html', '0000-00-00 00:00:00', '0000-00-00 00:00:00', '2019-04-21 02:53:01', 0, 'File Submitted', 421, 0, '2019-04-21 02:53:01', 'ViewStorage.html', '', 'Admin');

-- --------------------------------------------------------

--
-- Table structure for table `classes`
--

CREATE TABLE `classes` (
  `ID` int(11) NOT NULL,
  `Title` varchar(64) DEFAULT NULL,
  `Availability` tinyint(1) DEFAULT NULL,
  `DateStart` datetime DEFAULT NULL,
  `DateEnd` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `classes`
--

INSERT INTO `classes` (`ID`, `Title`, `Availability`, `DateStart`, `DateEnd`) VALUES
(8, 'Info-4430', 1, NULL, NULL),
(9, 'Info-2220', 0, NULL, NULL),
(10, 'Info-4482', 1, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `courses`
--

CREATE TABLE `courses` (
  `ID` int(11) NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Number` varchar(10) DEFAULT NULL,
  `ClassID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `courses`
--

INSERT INTO `courses` (`ID`, `Name`, `Number`, `ClassID`) VALUES
(1, 'Info-4430', NULL, 0),
(2, 'Info-2220', NULL, 0),
(3, 'Info-4482', NULL, 0),
(4, 'Info-1181', NULL, 0);

-- --------------------------------------------------------

--
-- Table structure for table `groups`
--

CREATE TABLE `groups` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) DEFAULT NULL,
  `EmailAddress` varchar(64) DEFAULT NULL,
  `Username` varchar(30) DEFAULT NULL,
  `Password` varchar(64) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `permissions`
--

CREATE TABLE `permissions` (
  `ID` int(11) NOT NULL,
  `Title` varchar(45) DEFAULT NULL,
  `Description` varchar(128) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `DateDeleted` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `resources`
--

CREATE TABLE `resources` (
  `ID` int(11) NOT NULL,
  `Name` varchar(30) DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  `DateUploaded` datetime DEFAULT NULL,
  `ResourceSize` int(11) DEFAULT NULL,
  `MaxSize` int(11) DEFAULT NULL,
  `UserID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `roles`
--

CREATE TABLE `roles` (
  `ID` int(11) NOT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `IsAdmin` bit(1) NOT NULL DEFAULT b'0',
  `Users` int(11) DEFAULT NULL,
  `Role` int(11) DEFAULT NULL,
  `Assignment` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `roles`
--

INSERT INTO `roles` (`ID`, `Name`, `IsAdmin`, `Users`, `Role`, `Assignment`) VALUES
(1, 'Admin', b'1', 15, 15, 15),
(2, 'PowerUser', b'1', 9, 9, 9),
(3, 'Data Entry', b'0', 9, 9, 9),
(4, 'Student', b'0', 5, 5, 15),
(5, 'test', b'0', 15, 15, 15),
(6, 'Test', b'0', 15, 15, 15);

-- --------------------------------------------------------

--
-- Table structure for table `sections`
--

CREATE TABLE `sections` (
  `ID` int(11) NOT NULL,
  `Number` int(11) NOT NULL,
  `DateStart` datetime NOT NULL,
  `DateEnd` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `ID` int(11) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `EmailAddress` varchar(50) NOT NULL,
  `Address` varchar(50) NOT NULL,
  `UserName` varchar(30) NOT NULL,
  `Password` char(70) NOT NULL,
  `PhoneNumber` bigint(20) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateModified` datetime NOT NULL,
  `DateDeleted` datetime NOT NULL,
  `AccountExpired` tinyint(1) NOT NULL,
  `AccountLocked` tinyint(1) NOT NULL,
  `PasswordExpired` tinyint(1) DEFAULT NULL,
  `Enabled` tinyint(1) NOT NULL,
  `Salt` varchar(50) NOT NULL,
  `RoleID` int(11) NOT NULL DEFAULT '4',
  `ResetCode` varchar(45) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`ID`, `FirstName`, `MiddleName`, `LastName`, `EmailAddress`, `Address`, `UserName`, `Password`, `PhoneNumber`, `DateCreated`, `DateModified`, `DateDeleted`, `AccountExpired`, `AccountLocked`, `PasswordExpired`, `Enabled`, `Salt`, `RoleID`, `ResetCode`) VALUES
(1, 'Test', 'Z', 'test', 'simkkish@isu.edu', '', 'Admin', 'aDFLUcGbK0tDgVa+UHAEtz8VcmuUH8q3J23LLhE2Gv8GV4l/P/MEtPEDJ8D/c0zW', 0, '0000-00-00 00:00:00', '2019-04-10 12:48:31', '2001-00-00 00:00:00', 0, 0, NULL, 0, 'ZtPlALT8qigjo7oOo8wa7fqBgXgoFlayN5M/MHH+zQ8J0p5Ech', 1, 'e9aa8eed-a7d1-465a-85b6-c9ac20f95fd9'),
(82, 'ttt', 'tttt', 'tttt', 'tttt', '', 'TestUsers', 'yNA4B+Q018Cygy4bvVR7PgwFKE4THU1YNnWZK3CzkHp0IkCGALb3XpPX6sTbwqos', 0, '0000-00-00 00:00:00', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 0, 0, NULL, 0, 'kXVWXAFR2d/iVNer5+7HA8OpWlsmdILjVzscyGPB5jgDQ6HYLj', 4, ''),
(84, 'r', 'r', 'r', 'r', '', 'asd', 'aVXq0/WcwwTwUqtfSexM2ZD1ICMQBZiu7khD/k5Wb3dlEGFg7ojUlzPcFB5+yrlD', 0, '0000-00-00 00:00:00', '2019-04-10 11:51:15', '0000-00-00 00:00:00', 0, 0, NULL, 0, 'JG9KXP107+z0PUuoHUQWwXNkE7UZ6PxFxwTmAdlLdljSH8Prwg', 4, ''),
(85, 'zzzz', 'zzzz', 'zzzz', 'zzzz', '', 'zzzz', 'qcMfGT+cOM6kmmYREzVj3ALyPL73J++55mVHM6xGUOMY3lwdJJhbpd4fr8lR0Nee', 0, '0000-00-00 00:00:00', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 0, 0, NULL, 0, 'Sc1rsUCWYm2zS342laMH01C0eX7nBVDZn39NKpAWClxN7QAj3q', 4, ''),
(86, 'class', 'c', 'class', 'class', '', 'class', 'ZD0N9HMkxvA6whlSkP8u7Rp5j0wDaTotPJ4RFqyVB3g0P1LMQ44A5qvwP/ZcjbmB', 0, '0000-00-00 00:00:00', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 0, 0, NULL, 0, 'DqD1MQKa8mW7ymctRvu/ZONMb8NRRSQi5/32BTA/H79BapwQpD', 4, ''),
(87, 'Mohan', 'S', 'Madai', 'madamoha@isu.edu', '', 'mufc07', 'h4pXmgy9qTkcYWB6I39upxDV9K7DtlbSKHl+KdD+Izw5a1Y/agyQp0sox/UueFl0', 0, '0000-00-00 00:00:00', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 0, 0, NULL, 0, 'k6yP0JDstWYBox9Z91sKu4Z3PBAWwTBjriATbtVJSaG9yZFvTs', 4, ''),
(90, 'aa', 'aaa', 'a', 'aa', '', 'aa', 'Rk/WLdPi3OeG6DE1rt/PU6r/+vbYIFySF9zqkxK/u4gnRM+9+LInRy4g6gJq61cZ', 0, '0000-00-00 00:00:00', '0000-00-00 00:00:00', '0000-00-00 00:00:00', 0, 0, NULL, 0, 'JbOUgxRItatkfoBT07iSD0YzXATHnghwG33icoAUpRIZeFfpnU', 4, '');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `assignment`
--
ALTER TABLE `assignment`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `FileName_UNIQUE` (`FileLocation`);

--
-- Indexes for table `classes`
--
ALTER TABLE `classes`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `courses`
--
ALTER TABLE `courses`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkClass` (`ClassID`);

--
-- Indexes for table `groups`
--
ALTER TABLE `groups`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `permissions`
--
ALTER TABLE `permissions`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `resources`
--
ALTER TABLE `resources`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `fkUser` (`UserID`);

--
-- Indexes for table `roles`
--
ALTER TABLE `roles`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `sections`
--
ALTER TABLE `sections`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `EmailAddress_UNIQUE` (`EmailAddress`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `assignment`
--
ALTER TABLE `assignment`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=998;

--
-- AUTO_INCREMENT for table `classes`
--
ALTER TABLE `classes`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT for table `courses`
--
ALTER TABLE `courses`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT for table `groups`
--
ALTER TABLE `groups`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `permissions`
--
ALTER TABLE `permissions`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `resources`
--
ALTER TABLE `resources`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT for table `roles`
--
ALTER TABLE `roles`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

ALTER TABLE `sections`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

ALTER TABLE `users`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=91;

ALTER TABLE `resources`
  ADD CONSTRAINT `resources_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON UPDATE CASCADE;
COMMIT;
