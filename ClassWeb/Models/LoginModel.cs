using System;
using System.ComponentModel.DataAnnotations;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By: Elvis
    /// Seperate Model for Login View.
    /// This model is used to get information only required for login view.
    /// It also model for add user now as I am testing to verify password using hashing. 
    /// </summary>

    public class LoginModel : DatabaseRecord
    {

        #region Constructors
        public LoginModel()
        {
        }
        internal LoginModel(MySql.Data.MySqlClient.MySqlDataReader dr)
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
        private string _UserName;
        private string _Password;
        private string _ConfirmPassword;
        private DateTime _DateCreated;
        private int _RoleID;
        private string _Salt;
        #endregion

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_FirstName = "FirstName";
        internal const string db_MiddleName = "MiddleName";
        internal const string db_LastName = "LastName";
        internal const string db_EmailAddress = "EmailAddress";
        internal const string db_Address = "Address";
        internal const string db_UserName = "UserName";
        internal const string db_Password = "Password";
        //internal const string db_DateCreated = "DateCreated";
        internal const string db_RoleID = "RoleID";
        internal const string db_Salt = "Salt";
        #endregion

        #region public Properites
        [Required]
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
        [Required]
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        [Required]
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

        [Required]
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword
        {
            get { return _ConfirmPassword; }
            set { _ConfirmPassword = value; }
        }

        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }
        /// <summary>
        /// Gets or sets the Salt for this PeerVal.User object
        /// </summary>
        public string Salt
        {
            get { return _Salt; }
            set { _Salt = value; }
        }

        /// <summary>
        /// Gets or sets the RoleID for this PeerVal.User object.
        /// </summary>
        /// <remarks></remarks>
        public int RoleID
        {
            get
            {
                return _RoleID;
            }
            set
            {
                _RoleID = value;
            }
        }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
     

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
            _FirstName = dr.GetString(db_FirstName);
            _MiddleName = dr.GetString(db_MiddleName);
            _LastName = dr.GetString(db_LastName);
            _EmailAddress = dr.GetString(db_EmailAddress);
            //_Address = dr.GetString(db_Address);
            _UserName = dr.GetString(db_UserName);
            _Password = dr.GetString(db_Password);
            //_DateCreated = dr.GetDateTime(db_DateCreated);
            _Salt = dr.GetString(db_Salt);
            //_RoleID = dr.GetInt32(db_RoleID);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
