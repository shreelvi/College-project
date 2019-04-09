using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassWeb.Model;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    public class CourseSemester : DatabaseRecord
    {
        #region Constructors
        public CourseSemester()
        {
        }
        internal CourseSemester(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion
        #region Private Variables
        private int _CourseID;
        private int _SemesterID;
        private int _YearID;
        private int _SectionID;
        private int _UserID;
        private Course _Course;
        private User _User;

        #endregion

        #region Public Properties
        public int CourseID
        {
            get
            {
                return _CourseID;
            }

            set
            {
                _CourseID = value;
            }
        }

        public int SemesterID
        {
            get
            {
                return _SemesterID;
            }

            set
            {
                _SemesterID = value;
            }
        }

        public int YearID
        {
            get
            {
                return _YearID;
            }

            set
            {
                _YearID = value;
            }
        }

        public int SectionID
        {
            get
            {
                return _SectionID;
            }

            set
            {
                _SectionID = value;
            }
        }

        public int UserID
        {
            get
            {
                return _UserID;
            }

            set
            {
                _UserID = value;
            }
        }


        /// <summary>
        /// Gets or sets the Course for this Section object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Course Course
        {
            get
            {
                if (_Course == null)
                {
                    _Course = DAL.GetCourse(_CourseID);
                }
                return _Course;
            }
            set
            {
                _Course = value;
                if (value == null)
                {
                    _CourseID = -1;
                }
                else
                {
                    _CourseID = value.ID;
                }
            }
        }

        /// <summary>
        /// Gets or sets the User for this Section object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public User User
        {
            get
            {
                if (_User == null)
                {
                    _User = DAL.GetUser(_UserID);
                }
                return _User;
            }
            set
            {
                _User = value;
                if (value == null)
                {
                    _UserID = -1;
                }
                else
                {
                    _UserID = value.ID;
                }
            }
        }



        #endregion

        #region Database String
        internal const string db_ID = "CourseSemesterID";
        internal const string db_CourseID = "CourseID";
        internal const string db_SemesterID = "SemesterID";
        internal const string db_YearID = "YearID";
        internal const string db_SectionID = "SectionID";
        internal const string db_UserID = "UserID";

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
            _CourseID = dr.GetInt32(db_CourseID);
            _SemesterID = dr.GetInt32(db_SemesterID);
            _YearID = dr.GetInt32(db_YearID);
            _SectionID = dr.GetInt32(db_SectionID);
            _UserID = dr.GetInt32(db_UserID);
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
