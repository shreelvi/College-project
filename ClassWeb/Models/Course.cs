using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClassWeb.Models;

namespace ClassWeb.Models
{
    /// <summary>
    ///Code by Meshari
    ///Courses => A course is like 4430, 3307, etc.
    ///Each course can be accessible to one to many users.
    ///Each course can be taught by multiple professors, hence multiple classes.
    /// </summary>
    public class Course: DatabaseRecord
    {
        #region Private Variables
        private string _CourseName;
        private int _CourseNumber;
        private Class _Class;
        private int _ClassID;
        #endregion

        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public Course()
        {
        }
        internal Course(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "CourseID";
        internal const string db_CourseName = "CourseName";
        internal const string db_CourseNumber = "CourseNumber";
        internal const string db_Class = "ClassID";
        #endregion

        #region Public Properites
        public string CourseName
        {
            get { return _CourseName; }
            set { _CourseName = value; }
        }

        public int CourseNumber
        {
            get { return _CourseNumber; }
            set { _CourseNumber = value; }
        }
        public Class Class
        {
            get { return _Class; }
            set { _Class = value; }
        }
        public int ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }
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
        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a MySqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _CourseName = dr.GetString(db_CourseName);
            _CourseNumber = dr.GetInt32(db_CourseNumber);
            _ClassID = dr.GetInt32(Class.db_ID);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
