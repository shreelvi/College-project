using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//Meshari, 02/14
//Courses => A course is like 4430, 3307, etc.
//Each course can be accessible to one to many users.
//Each course can be taught by multiple professors, hence multiple classes.


namespace ClassWeb.Models
{
<<<<<<< HEAD
    public class Course:DatabaseObject
    {
        private string _Name;
        private int _Number;
        private int _ClassID;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
=======
    public class Course:DatabaseNamedObject
    {
        #region Private Variables
        private int _Number;
        private int _ClassID;
        #endregion

        #region Public Variables
>>>>>>> Elvis
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        public int ClassID
        {
            get { return _ClassID; }
           private set { _ClassID = value; }
        }
<<<<<<< HEAD
=======
        #endregion
>>>>>>> Elvis
    }
}
