-- MySQL dump 10.16  Distrib 10.1.37-MariaDB, for Win32 (AMD64)
--
-- Host: localhost    Database: web_masters
-- ------------------------------------------------------
-- Server version	10.1.37-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `Login_roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Login_roles` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(256) NOT NULL,
  `Description` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Login_roles`
--

LOCK TABLES `Login_roles` WRITE;
/*!40000 ALTER TABLE `Login_roles` DISABLE KEYS */;
INSERT INTO `Login_roles` VALUES (1,'Admin','Admin of the website'),(2,'Student','ClassWeb Students'),(3,'Professor','ClassWeb Professors'),(4,'Grader','ClassWeb Graders');
/*!40000 ALTER TABLE `Login_roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Login_users`
--

DROP TABLE IF EXISTS `Login_users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Login_users` (
  `UserID` int(11) NOT NULL,
  `FirstName` varchar(256) NOT NULL,
  `MiddleName` varchar(256) DEFAULT NULL,
  `LastName` varchar(256) NOT NULL,
  `EmailAddress` varchar(256) NOT NULL,
  `Address` varchar(256) DEFAULT NULL,
  `UserName` varchar(256) NOT NULL,
  `Password` char(50) DEFAULT NULL,
  `PhoneNumber` varchar(256) DEFAULT NULL,
  `DateCreated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `DateModified` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `DateArchived` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `RoleID` int(10) unsigned NOT NULL,
  `Salt` char(50) DEFAULT NULL,
  PRIMARY KEY (`UserID`),
  KEY `fk_user_role` (`RoleID`),
  CONSTRAINT `fk_user_role` FOREIGN KEY (`RoleID`) REFERENCES `Login_roles` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Login_users`
--

LOCK TABLES `Login_users` WRITE;
/*!40000 ALTER TABLE `Login_users` DISABLE KEYS */;
INSERT INTO `Login_users` VALUES (0,'John',NULL,'Doe','doejon@isu.edu',NULL,'doejohn','john123',NULL,'2019-03-09 19:57:59','0000-00-00 00:00:00','0000-00-00 00:00:00',1,NULL),(1,'Elvis',NULL,'Shrestha','shreelvi@isu.edu',NULL,'shreelvi','x129y190',NULL,'2019-03-08 14:48:49','0000-00-00 00:00:00','0000-00-00 00:00:00',1,NULL);
/*!40000 ALTER TABLE `Login_users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'web_masters'
--
/*!50003 DROP PROCEDURE IF EXISTS `sprocUsersGetALL` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = cp850 */ ;
/*!50003 SET character_set_results = cp850 */ ;
/*!50003 SET collation_connection  = cp850_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sprocUsersGetALL`()
BEGIN
SELECT * FROM Login_Users;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sproc_GetSaltForUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = cp850 */ ;
/*!50003 SET character_set_results = cp850 */ ;
/*!50003 SET collation_connection  = cp850_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetSaltForUser`(
IN Username VARCHAR(256)
)
BEGIN
SELECT Salt FROM Login_Users
WHERE Login_Users.UserName = Username;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sproc_GetUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = cp850 */ ;
/*!50003 SET character_set_results = cp850 */ ;
/*!50003 SET collation_connection  = cp850_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetUser`(
IN Username VARCHAR(256)
)
BEGIN
SELECT * FROM Login_Users
WHERE Login_Users.UserName = Username;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `sproc_SetSaltForUser` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = cp850 */ ;
/*!50003 SET character_set_results = cp850 */ ;
/*!50003 SET collation_connection  = cp850_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_SetSaltForUser`(
IN UserID INT,
IN Salt CHAR 
)
BEGIN
UPDATE Login_Users
SET Login_Users.Salt = Salt
WHERE Login_Users.UserID = UserID;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-03-09 12:58:19
