using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By: Kishor Simkhada
    /// Everyone is a user when they sign up except Admin.
    /// Special permission will be provided based on the roles assigned to them on the system.
    /// Every user can login to the system unless deleted.
    /// </summary>
    public class User:DatabaseObject
    {

        #region Database String
        internal const string db_FirstName = "FirstName";
        internal const string db_MiddleName = "MiddleName";
        internal const string db_LastName = "LastName";
        internal const string db_EmailAddress = "EmailAddress";
        internal const string db_Address = "Address";
        internal const string db_UserName = "UserName";
        internal const string db_Password = "Password";
        internal const string db_PhoneNumber = "PhoneNumber";
        internal const string db_DateCreated = "DateCreated";
        internal const string db_DateModified = "DateModified";
        internal const string db_DateDeleted = "DateDeleted";
        internal const string db_AccountExpired = "AccountExpired";
        internal const string db_AccountLocked = "AccountLocked";
        internal const string db_Role = "RoleID";

        #endregion
        #region private variable
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _EmailAddress;
        private string _Address;
        private string _Password;
        private string _UserName;
        private long _PhoneNumber;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;
        private bool _AccountExpired;
        private bool _Enabled;
        private bool _PasswordExpired;
        private bool _AccountLocked;
        private Role _Role;
        private int _RoleID;
        private MySqlDataReader dr;

        public User(MySqlDataReader dr)
        {
            this.dr = dr;
        }
        #endregion
        #region public Properites

        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        public Role Roles
        {
            get { return _Role; }
            set { _Role = value; }
        }

        

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
            
        }
        public string MiddleName
        {
            get { return _MiddleName; }
            set { _MiddleName = value; }
        }


        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public long PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }


        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }

        public DateTime DateDeleted
        {
            get { return _DateDeleted; }
            set { _DateDeleted = value; }
        }

        public bool AccountExpired
        {
            get { return _AccountExpired; }
            set { _AccountExpired = value; }
        }

        public bool AccountLocked
        {
            get { return _AccountLocked; }
            set { _AccountLocked = value; }
        }

        public bool PasswordExpired
        {
            get { return _PasswordExpired; }
            set { _PasswordExpired = value; }
        }

        public bool Enabled
        {
            get { return _Enabled; }
            set { _Enabled = value; }
        }
        #endregion

    }
}
