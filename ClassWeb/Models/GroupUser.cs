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
    public class GroupUser: DatabaseRecord
    {
        #region Constructors
        public GroupUser()
        {
        }
        internal GroupUser(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private int _GroupID;
        private int _UserID;
        private Group _Group;
        private User _User;


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
                    //_Group = DAL.GetGroup(_GroupID);
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
        internal const string db_ID = "GroupUserID";
        internal const string db_GroupID = "GroupID";
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
            _GroupID = dr.GetInt32(db_GroupID);
            _UserID = dr.GetInt32(db_UserID);
            
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
