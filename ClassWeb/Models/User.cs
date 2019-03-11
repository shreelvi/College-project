using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Model;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By: Kishor Simkhada
    /// Everyone is a user when they sign up except Admin.
    /// Special permission will be provided based on the roles assigned to them on the system.
    /// Every user can login to the system unless deleted.
    /// </summary>
    public class User : DatabaseObject
    {
        #region Constructors
        public User()
        {
        }
        internal User(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion


        #region private variable
        private string _FirstName;
        private string _MiddleName;
        private string _LastName;
        private string _EmailAddress;
        private string _Address;
        private string _Password;
        private string _ConfirmPassword;
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
        private string _Salt;
        #endregion

        #region Database String
        internal const string db_ID = "UserID";
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
        internal const string db_DateArchived = "DateDeleted";
        internal const string db_AccountExpired = "IsExpired";
        internal const string db_Enabled = "IsEnabled";
        internal const string db_PasswordExpired = "PasswordExpired";
        internal const string db_AccountLocked = "AccountLocked";
        internal const string db_Role = "RoleID";
        internal const string db_Salt = "Salt";
        #endregion

        #region public Properites

        [Required(ErrorMessage = "Please provide First Name", AllowEmptyStrings = false)]
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

        [Required(ErrorMessage = "Please provide Last Name", AllowEmptyStrings = false)]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Required(ErrorMessage = "Please provide valid email address", AllowEmptyStrings = false)]
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

        [Required(ErrorMessage = "Please provide password", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be 8 character long.")]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        /// <summary>
        /// Gets or sets the Salt for user object
        /// </summary>
        public string Salt
        {
            get { return _Salt; }
            set { _Salt = value; }
        }

        [Compare("Password", ErrorMessage = "Confirm Password does not match")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }

        [Required(ErrorMessage = "Please provide username", AllowEmptyStrings = false)]
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

        /// <summary>
        /// Gets or sets the RoleID for user.
        /// </summary>
        public int RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }
        /// <summary>
        /// Gets or sets the role for this user
        /// </summary>
        [Required(ErrorMessage = "Please define role of user", AllowEmptyStrings = false)]
        public Role Roles
        {
            get { return _Role; }
            set { _Role = value; }

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


        //    #region#region Public Functions
        //    public override int dbSave()
        //    {
        //        if (_ID < 0)
        //        {
        //            return dbAdd();
        //        }
        //        else
        //        {
        //            return dbUpdate();
        //        }
        //    }
        //    /// <summary>
        //    /// Calls DAL function to add User to the database.
        //    /// Reference Professor's PeerVal
        //    /// </summary>
        //    /// <remarks></remarks>
        //    protected override int dbAdd()
        //    {
        //        _ID = DAL.AddUser(this);
        //        return ID;
        //    }

        //    /// <summary>
        //    /// Calls DAL function to update User to the database.
        //    /// </summary>
        //    /// <remarks></remarks>
        //    protected override int dbUpdate()
        //    {
        //        return DAL.UpdateUser(this);
        //    }

        //    /// <summary>
        //    /// Calls DAL function to remove User from the database.
        //    /// </summary>
        //    /// <remarks></remarks>
        //    public int dbRemove()
        //    {
        //        return DAL.RemoveUser(this);
        //    }

        //    #endregion

        //    #region
        //    #region Public Subs
        //    /// <summary>
        //    /// Fills object from a MySqlClient Data Reader
        //    /// </summary>
        //    /// <remarks></remarks>
        //    public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        //    {
        //        _ID = dr.GetInt32(db_ID);
        //        _FirstName = dr.GetString(db_FirstName);
        //        _MiddleName = dr.GetString(db_MiddleName);
        //        _LastName = dr.GetString(db_LastName);
        //        _UserName = dr.GetString(db_UserName);
        //        _Password = dr.GetString(db_Password);
        //        _Salt = dr.GetString(db_Salt);
        //        _RoleID = dr.GetInt32(Role.db_ID);
        //    }

        //    #endregion

        //    public override string ToString()
        //    {
        //        return this.GetType().ToString();
        //    }
        //}
    }
}
