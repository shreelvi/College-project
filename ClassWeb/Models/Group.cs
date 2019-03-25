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
    /// Groups are a team of student members for a projects in class
    /// </summary>

    public class Group: DatabaseNamedRecord
    {
        #region Private Variables
        private string _EmailAddress;
        private string _UserName;
        private string _Password;
        private string _Salt;
        private Assignment _AssignmentID;
        #endregion

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Name = "Name";
        internal const string db_EmailAddress = "EmailAddress";
        internal const string db_UserName = "UserName";
        internal const string db_Password = "Password";
        internal const string db_Salt = "Salt";
        internal const string db_AssignmentID = "AssignmentID";
        
        #endregion
        #region Public Variables
        [Display(Name ="Group's Email-address",
            Description = "Email-address used to contact the group; which all members will have access.")]
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

        //Foreign Key
        public Assignment AssignmentID
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
            _ID = DAL.AddGroup(this);
            return ID;
            ///throw new NotImplementedException();
        }

        protected override int dbUpdate()
        {
            return DAL.UpdateGroup(this);
            ///throw new NotImplementedException();
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
            _EmailAddress = dr.GetString(db_EmailAddress);
            _UserName = dr.GetString(db_UserName);
            _Password = dr.GetString(db_Password);
          

        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
