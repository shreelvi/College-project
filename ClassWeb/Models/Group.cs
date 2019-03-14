using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClassWeb.Models;

namespace ClassWeb.Models
{
    /// <summary>
    /// Code by Elvis and Meshari
    /// Groups are a team of student members for a projects in class
    /// </summary>

    public class Group: DatabaseRecord
    {
        #region Private Variables
        private string _GroupName;
        private string _EmailAddress;
        private string _UserName;
        private string _Password;
        private Assignment _Assignment;
        private int _AssignmentID;
        #endregion

        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public Group()
        {
        }
        internal Group(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "GroupID";
        internal const string db_GroupName = "GroupName";
        internal const string db_EmailAddress = "EmailAddress";
        internal const string db_UserName = "UserName";
        internal const string db_Password = "Password";
        internal const string db_Assignment = "AssignmentID";
        #endregion

        #region Public Properites
        [Display(Name ="Group's Email-address",
            Description = "Email-address used to contact the group; which all members will have access.")]
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "Group's Login Username",
            Description = "Username to login to group's account profile.")]
        public string Username
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [Display(Name = "Group's Login Password",
            Description = "Password to login to group's account profile.")]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
        //Foreign Key
        public Assignment Assignment
        {
            get { return _Assignment; }
            set { _Assignment = value; }
        }
        public int AssignmentID
        {
            get { return _AssignmentID; }
            set { _AssignmentID = value; }
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
            _GroupName = dr.GetString(db_GroupName);
            _EmailAddress = dr.GetString(db_EmailAddress);
            _UserName = dr.GetString(db_UserName);
            _Password = dr.GetString(db_Password);
            _AssignmentID = dr.GetInt32(Assignment.db_ID);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
