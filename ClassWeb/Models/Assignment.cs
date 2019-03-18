using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Assignment:DatabaseNamedRecord
    {
        #region Private Variables
        private string _Description;
        private DateTime _DateDue;
        private DateTime _DateSubmission;
        private int _Grade;
        private string _Feedback;
        private DateTime _DateModified;
        private int _Size;
        private int _UserID;
        private User _User;
        #endregion

        #region Database string
        internal const string db_ID = "AssignmentID";
        internal const string db_Name = "Name";
        internal const string db_Description = "Description";
        internal const string db_DateDue = "DateDue";
        internal const string db_DateSubmission = "DateSubmission";
        internal const string db_Grade = "Grade";
        internal const string db_Feedback = "Feedback";
        internal const string db_DateModified = "DateModified";
        internal const string db_Size = "Size";
        internal const string db_UserID= "UserID";
        #endregion

        #region Public Properties
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [Display(Name = "Date Due")]
        public DateTime DateDue
        {
            get { return _DateDue; }
            set { _DateDue = value; }
        }
        [Display(Name = "Date Submitted")]
        public DateTime DateSubmission
        {
            get { return _DateSubmission; }
            set { _DateSubmission = value; }
        }

        public int Grade
        {
            get { return _Grade; }
            set
            {
                if (value > 100)
                {
                    _Grade = 100;
                }
                if (value < 0)
                {
                    _Grade = 0;
                }
                _Grade = value;
            }
        }

        public string Feedback
        {
            get { return _Feedback; }
            set { _Feedback = value; }
        }

        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
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
        public int Size
        {
            get { return _Size; }
            set { _Size = value; }
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
            _Name = dr.GetString(db_Name);
            _Description = dr.GetString(db_Description);
            _DateDue = dr.GetDateTime(db_DateDue);
            _DateSubmission = dr.GetDateTime(db_DateSubmission);
            _Grade = dr.GetChar(db_Grade);
            _Feedback = dr.GetString(db_Feedback);
            _DateModified = dr.GetDateTime(db_DateModified);
            _Size = dr.GetInt32(db_Size);
            _UserID = dr.GetInt32(db_UserID);        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
