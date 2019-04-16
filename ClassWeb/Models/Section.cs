using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassWeb.Model;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace ClassWeb.Models
{
    public class Section:DatabaseRecord
    {
        #region Constructors
        public Section()
        {
        }
        internal Section(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private int _SectionNumber;
        private int _CRN;
        //private int _ClassID;
        private int _UserID;
        private int _CourseID;
        private Course _Course;
        private User _User;

        #endregion


        #region Public Properties

        //public int ID
        //{
        //    get
        //    {
        //        return _ID;
        //    }

        //    set
        //    {
        //        _ID = value;
        //    }
        //}
        public int SectionNumber
        {
            get
            { return _SectionNumber;
            }

            set
            { _SectionNumber = value;
            }
        }

        public int CRN
        {
            get
            {
                return _CRN;
            }

            set
            {
                _CRN = value;
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
        internal const string db_ID = "SectionID";
        internal const string db_CRN = "CRN";
        internal const string db_Number = "SectionNumber";
        internal const string db_UserID = "UserID";
        internal const string db_CourseID = "CourseID";
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
            _CRN = dr.GetInt32(db_CRN);
            _SectionNumber = dr.GetInt32(db_Number);
            _UserID = dr.GetInt32(db_UserID);
            _CourseID = dr.GetInt32(db_CourseID);
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }


    }
}
