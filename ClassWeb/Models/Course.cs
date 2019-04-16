using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;


namespace ClassWeb.Models
{

    /// <summary>
    /// Created By: Mohan
    /// Courses => A course is like 4430, 3307, etc.
    /// Each course can be accessible to one to many users.
    /// Each course can be taught by multiple professors, hence multiple classes.
    /// A course has a course name and a number.
    /// </summary>
    ///
    public class Course : DatabaseRecord
    {
        public Course()
        {

        }

        internal Course(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #region Private Variables

        private string _CourseTitle;
        private string _CourseName;
        private int _ClassID;
        private List<Course> _Courses;
        #endregion

        //database strings
        #region Database  String

        internal const string db_ID = "ID";
        internal const string db_CourseTitle = "CourseTitle";
        internal const string db_CourseName = "CourseName";
        internal const string db_ClassID = "ClassID";
        #endregion


        #region Public Functions
        public override int dbSave()
        {
            throw new NotImplementedException();
        }

        protected override int dbAdd()
        {
            throw new NotImplementedException();
        }

        protected override int dbUpdate()
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            return this.GetType().ToString();
        }
        internal static Task FirstOrDefaultAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }

        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _CourseTitle = dr.GetString(db_CourseTitle);
            _CourseName = dr.GetString(db_CourseName);
            _ClassID = dr.GetInt32(db_ClassID);

        }

        #endregion


        #region Public Variables
        public string CourseTitle
        {
            get { return _CourseTitle; }
            set { _CourseTitle = value; }
        }

        public string CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }
        public int ClassID
        {
            get { return _ClassID; }
            private set { _ClassID = value; }
        }

        public List<Course> Courses
        {
            get { return _Courses; }
            set { _Courses = value; }
        }
        #endregion
    } 
}
