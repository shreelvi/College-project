using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Courses => A course is like 4430, 3307, etc.
//Each course can be accessible to one to many users.
//Each course can be taught by multiple professors, hence multiple classes.


namespace ClassWeb.Models
{
    public class Cource
    {
        private int _CourseID;
        private string _Name;
        private int _Number;
        private int _ClassID;

        public int CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        public int ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }
    }
}
