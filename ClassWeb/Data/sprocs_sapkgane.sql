
DELIMITER $$
CREATE PROCEDURE sproc_ClassUpdate(
(IN `ID` INT,
 IN `Title` VARCHAR(30),
 IN `IsAvailable` BOOLEAN,
 IN `DataStart` DATE, 
 IN `DateEnd` DATE,
 IN `SectionID` INT
)
BEGIN
     UPDATE Class
          SET
               Class.ID = ID,
               Class.Title = Title,
               Class.IsAvailable = IsAvailable,
               Class.DateStart = DateStart,
               Class.DateEnd = DateEnd
			   Class.SectionID= SectionID
          WHERE Class.ID = ID;
END
$$

DELIMITER $$
CREATE PROCEDURE `sproc_ClassGetByID` (
IN `ID` INT,
 IN `Title` VARCHAR(30),
 IN `IsAvailable` BOOLEAN,
 IN `DataStart` DATE, 
 IN `DateEnd` DATE,
 IN `SectionID` INT
 ) 
 BEGIN
	 SELECT * FROM class
	 WHERE class.ID =ID;
END
$$
