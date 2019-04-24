-- -----------------Course------------------------------
-- ======================================================

-- Author: Mohan
-- Description:	Create course table in the database
-- ======================================================

CREATE TABLE `course` (
  `CourseID` int(11) PRIMARY KEY AUTO_INCREMENT,
  `Subject` VARCHAR(50) NOT NULL,
  `CourseNumber` INT(11)  NOT NULL,
  `CourseTitle` VARCHAR(50) NOT NULL);
  
INSERT INTO course(Subject,  CourseNumber,CourseTitle) 
	VALUES ( 'INFO', '4482', 'System Development Implementation Method');
