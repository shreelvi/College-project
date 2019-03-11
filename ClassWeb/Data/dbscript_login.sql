-- -----------------------------------------------------
-- Table dbo.Roles
-- -----------------------------------------------------
CREATE TABLE `login_roles` (
  `RoleID` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `Description` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  PRIMARY KEY (`RoleID`)
);

-- -----------------------------------------------------
-- Table dbo.Users
-- -----------------------------------------------------
CREATE TABLE `login_users` (
  `UserID` int(11) NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `MiddleName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `LastName` varchar(45) CHARACTER SET utf8 DEFAULT NULL,
  `EmailAddress` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `UserName` varchar(128) CHARACTER SET utf8 DEFAULT NULL,
  `Password` char(64) DEFAULT NULL,
  `RoleID` int(11) NOT NULL DEFAULT '2',
  `Salt` char(128) DEFAULT NULL,
  `DateCreated` datetime DEFAULT CURRENT_TIMESTAMP,
  `DateModified` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`UserID`),
  KEY `FK1` (`RoleID`),
  CONSTRAINT `FK1` FOREIGN KEY (`RoleID`) REFERENCES `login_roles` (`RoleID`)
);