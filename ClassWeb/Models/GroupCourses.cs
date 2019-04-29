using ClassWeb.Controllers;
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
    /// Created By: Elvis
    /// Seperate Model for Login View.
    /// This model is used to get information only required for login view.
    /// It also model for add user to the group now as I am testing to verify password using hashing. 
    /// </summary>
    public class GroupCourses : DatabaseRecord
    {
        #region Constructors
        public GroupCourses()
        {
        }
        internal GroupCourses(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private int _GroupID;
        private int _CourseID;
        private Group _Group;
        private Course _Course;


        #endregion

        #region Public Properties
        public int GroupID
        {
            get
            {
                return _GroupID;
            }

            set
            {
                _GroupID = value;
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
        /// Gets or sets the Group for this object.
        /// Reference: Taken code from prof. Holmes Peerval Project
        /// </summary>
        /// <remarks></remarks>
        [XmlIgnore]
        public Group Group
        {
            get
            {
                if (_Group == null)
                {
                    _Group = DAL.GroupGetByID(_GroupID);
                }
                return _Group;
            }
            set
            {
                _Group = value;
                if (value == null)
                {
                    _GroupID = -1;
                }
                else
                {
                    _GroupID = value.ID;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Course for this object.
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
                    _Course = DAL.GetCourseByID(_CourseID);
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



        #endregion

        #region Database String
        internal const string db_ID = "GroupCourseID";
        internal const string db_GroupID = "GroupID";
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
            _GroupID = dr.GetInt32(db_GroupID);
            _CourseID = dr.GetInt32(db_CourseID);

        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
