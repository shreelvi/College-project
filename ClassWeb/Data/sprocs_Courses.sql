-- -----------------Course---------------------------------
-- Description: sproc CRUD for Courses-------------------
-- Reference: PeerVal Project, Github------------------
-- ======================================================

-- Author: Mohan
-- sproc_CreateCourse
-- Description:	Add a new Course to the Database.
-- =============================================

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_CreateCourse`(OUT CourseID int, IN `Subject` VARCHAR(50), IN `CourseNumber` int, IN `CourseTitle` VARCHAR(50))
BEGIN
     INSERT INTO course(Subject, CourseNumber, CourseTitle)
		VALUES(Subject, CourseNumber, CourseTitle);
	SET CourseID = last_insert_id();
END$$

-- Author: Mohan
-- sproc_GetCourse
-- Description:	get Course from the Database.
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetCourse`(IN CourseID int)
BEGIN
     SELECT * FROM course
     WHERE course.CourseID = CourseID;
END$$


-- Author: Mohan
-- sproc_GetAllCourses
-- Description:	Gets all courses from the database. 
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_GetAllCourses`()
BEGIN
     SELECT * FROM course;
END$$


-- Author: Mohan
-- sproc_UpdateCourse
-- Description:	Edit course. 
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_UpdateCourse`(IN CourseID int, IN Subject int, IN CourseName VARCHAR(50), IN CourseTitle VARCHAR(50))
BEGIN
     UPDATE course
          SET
               course.Subject = Subject,
               course.CourseTitle = CourseTitle,
               course.CourseName = CourseName
          WHERE course.CourseID = CourseID;
END$$


-- Author: Mohan
-- sproc_DeleteCourseByID
-- Description:	Delete a course. 
-- =============================================
CREATE DEFINER=`root`@`localhost` PROCEDURE `sproc_DeleteCourseByID`(IN `ID` int)
BEGIN 
	DELETE FROM course 
		WHERE Course.ID = `ID`; 
END$$