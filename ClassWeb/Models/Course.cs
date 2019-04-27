using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

//Meshari, 02/14
//Courses => A course is like 4430, 3307, etc.
//Each course can be accessible to one to many users.
//Each course can be taught by multiple professors, hence multiple classes.


namespace ClassWeb.Models
{
    public class Course : DatabaseNamedRecord
    {
        #region Constructors
        public Course()
        {
        }
        internal Course(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private string _Subject;
        private int _CourseNumber;
        private string _CourseTitle;
        #endregion

        #region Public Variables

        public string Subject
        {
            get { return _Subject; }
           set { _Subject = value; }
        }
        public int CourseNumber
        {
            get { return _CourseNumber; }
            set { _CourseNumber = value; }
        }
        public string CourseTitle
        {
            get { return _CourseTitle; }
            set { _CourseTitle = value; }
        }



        #endregion

        #region Database String
        internal const string db_ID = "CourseID";
        internal const string db_Subject = "Subject";
        internal const string db_CourseNumber = "CourseNumber";
        internal const string db_CourseTitle = "CourseTitle";
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

        public int dbRemove()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Subs
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Subject = dr.GetString(db_Subject);
            _CourseNumber = dr.GetInt32(db_CourseNumber);
            _CourseTitle = dr.GetString(db_CourseTitle);
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
        #endregion
    }
}