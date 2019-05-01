using ClassWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By: Elvis
    /// Copied by Sakshi for Group Login
    /// Seperate Model for Login View.
    /// This model is used to get information only required for login view.
    /// It also model for add user now as I am testing to verify password using hashing. 
    /// </summary>

    public class GroupLoginModel : DatabaseNamedRecord
    {

        #region Constructors
        public GroupLoginModel()
        {
        }
        internal GroupLoginModel(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region private variable
        private string _EmailAddress;
        private string _UserName;
        private string _ClassName;
        private string _Password;
        private string _Salt;
        private string _ConfirmPassword;
        private bool _IsEmailConfirmed = false;
        private string _EmailToken;
        private string _DirectoryPath;
        private List<User> _Users;
        
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

        #region public Properites
        [Required]
        
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
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
        public string EmailToken
        {
            get { return _EmailToken; }
            set { _EmailToken = value; }
        }
        public bool IsEmailConfirmed
        {
            get { return _IsEmailConfirmed; }
            set { _IsEmailConfirmed = value; }
        }
    
        /// <summary>
        /// Gets or sets the Salt for this PeerVal.User object
        /// </summary>
        public string Salt
        {
            get { return _Salt; }
            set { _Salt = value; }
        }
        public string DirectoryPath
        {
            get { return _DirectoryPath; }
            set { _DirectoryPath = value; }
        }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

        public List<User> Users
        {
            get { return _Users; }
            set { _Users = value; }
        }

        [Required]
        [Display(Name = "Class Information")]
        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
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
            _EmailAddress = dr.GetString(db_EmailAddress);
            _UserName = dr.GetString(db_UserName);
            _Password = dr.GetString(db_Password);
            _Salt = dr.GetString(db_Salt);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
