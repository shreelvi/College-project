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
    /// Resources are files that are uploaded in the system
    /// </summary>
    public class Resource: DatabaseRecord
    {

        #region Private Variables
        private string _FileName;
        private DateTime _DateModified;
        private DateTime _DateUploaded;
        private int _ResourceSize;
        private int _MaxSize;
        private User _User;
        private int _UserID;
        private Assignment _Assignment;
        private int _AssignmentID;
        #endregion

        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public Resource()
        {
        }
        internal Resource(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "FileID";
        internal const string db_FileName = "FileName";
        internal const string db_DateModified = "DateModified";
        internal const string db_DateUploaded = "DateUploaded";
        internal const string db_ResourceSize = "ResourceSize";
        internal const string db_MaxSize = "MaxSize";
        internal const string db_User = "UserID";
        internal const string db_Assignment = "AssignmentID";
        #endregion

        #region Public Properites
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }
        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }

 
        public DateTime DateUploaded
        {
            get { return _DateUploaded; }
            set { _DateUploaded = value; }
        }

        public int ResourceSize
        {
            get { return _ResourceSize; }
            set { _ResourceSize = value; }
        }

        public int MaxSize
        {
            get { return _MaxSize; }
            set { _MaxSize = value; }
        }

        public User User
        {
            get { return _User; }
            set { _User = value; }
        }

        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

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
            _FileName = dr.GetString(db_FileName);
            _DateModified = dr.GetDateTime(db_DateModified);
            _DateUploaded = dr.GetDateTime(db_DateUploaded);
            _ResourceSize = dr.GetInt32(db_ResourceSize);
            _MaxSize = dr.GetInt32(db_MaxSize);
            _UserID = dr.GetInt32(User.db_ID);
            _AssignmentID = dr.GetInt32(Assignment.db_ID);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
