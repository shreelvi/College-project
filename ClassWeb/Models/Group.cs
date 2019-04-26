using ClassWeb.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace ClassWeb.Models
{
    /// <summary>
    /// Code by Elvis
    /// Groups are a team of student members for a project in class
    /// </summary>

    public class Group : DatabaseNamedRecord
    {
        #region Constructors
        /// <summary>
        /// By Sakshi
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public Group()
        { }
        internal Group(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }
        #region Private Variables
        private string _EmailAddress;
        private string _UserName;
        private string _Password;
        private string _Salt;
        private string _DirectoryPath;
        private List<Assignment> _Assignments;
        private int _AssignmentID;
        private List<User> _Users;
        #endregion

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Name = "Name";
        internal const string db_EmailAddress = "EmailAddress";
        internal const string db_UserName = "UserName";
        internal const string db_Password = "Password";
        internal const string db_Salt = "Salt";
        internal const string db_DirectoryPath = "DirectoryPath";
        internal const string db_Assignments = "Assignments";
        internal const string db_AssignmentID = "AssignmentID";

        #endregion
        #region Public Variables
        [Display(Name = "Group's Email-address",
            Description = "Email-address used to contact the group; which all members will have access.")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "Group's Login Username",
            Description = "Username to login to group's account profile.")]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [Display(Name = "Group's Login Password",
            Description = "Password to login to group's account profile.")]
        public string Password
        {
            get
            {
                if (String.IsNullOrEmpty(_Password)) _Password = "";
                return _Password;
            }
            set { _Password = value.Trim(); }
        }

        public string Salt
        {
            get { return _Salt; }
            set { _Salt = value.Trim(); }
        }
        public string DirectoryPath
        {
            get { return _DirectoryPath; }
            set { _DirectoryPath = value; }
        }
        public List<Assignment> Assignments
        {
            get { return _Assignments; }
            set { _Assignments = value; }
        }
        //Foreign Key
        public int AssignmentID
        {
            get { return _AssignmentID; }
            set { _AssignmentID = value; }
        }
        public List<User> Users
        {
            get
            {
                if (_Users == null)
                {
                    _Users = DAL.GetGroupUsers(_ID);
                }
                return _Users;
            }
            set { _Users = value; }
        }

        #endregion

        #region Public Functions

        public override int dbSave()
        {
            if (_ID < 0)
            {
                return dbAdd();

            }
            else
            {
                return dbUpdate();
            }
        }

        protected override int dbAdd()
        {
            _ID = DAL.AddGroup(this);
            return ID;

        }

        protected override int dbUpdate()
        {
            return DAL.UpdateGroup(this);

        }
        public int dbRemoveUserFromGroup()
        {
            return DAL.RemoveUserFromGroup(this);
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
            _Name = dr.GetString(db_Name);
            _EmailAddress = dr.GetString(db_EmailAddress);
            _UserName = dr.GetString(db_UserName);
            _Password = dr.GetString(db_Password);
            _Salt = dr.GetString(db_Salt);
            //  _AssignmentID = dr.GetInt32(db_AssignmentID);

        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }

        internal static Task FirstOrDefaultAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
    #endregion
}