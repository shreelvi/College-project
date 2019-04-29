using ClassWeb.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created on: 26 April 2019
    /// Created by: shreelvi
    /// Description: Association table for CourseSemester and User models
    /// </summary>
    public class CourseSemesterUser : DatabaseRecord
    {

        #region Constructors
        public CourseSemesterUser()
        {
        }
        internal CourseSemesterUser
(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private int _CourseSemesterID;
        private int _UserID;
        private CourseSemester _CourseSemester;
        private User _User;

        #endregion

        #region Public Properties

        public int CourseSemesterID
        {
            get
            {
                return _CourseSemesterID;
            }

            set
            {
                _CourseSemesterID = value;
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
        /// Gets or sets the Semester for this object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public CourseSemester CourseSemesters
        {
            get
            {
                if (_CourseSemester == null)
                {
                    _CourseSemester = DAL.GetCourseSemester(_CourseSemesterID);
                }
                return _CourseSemester;
            }
            set
            {
                _CourseSemester = value;
                if (value == null)
                {
                    _CourseSemesterID = -1;
                }
                else
                {
                    _CourseSemesterID = value.ID;
                }
            }
        }


        /// <summary>
        /// Gets or sets the User for this object.
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
                    _User = DAL.UserGetByID(_UserID);
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
        internal const string db_ID = "ID";
        internal const string db_CourseSemesterID = "CourseSemesterID";
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
            _CourseSemesterID = dr.GetInt32(db_CourseSemesterID);
            _UserID = dr.GetInt32(db_UserID);
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}