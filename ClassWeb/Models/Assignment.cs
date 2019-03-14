using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClassWeb.Models;

namespace ClassWeb.Models
{
    /// <summary>
    /// Code by Meshari
    /// It is a task assigned by a professor to the student and graded by the grader. 
    /// </summary>
    public class Assignment: DatabaseRecord
    {
        #region Private Variables
        private string _Title;
        private string _Description;
        private DateTime _StartDate;
        private DateTime _DueDate;
        private  DateTime _SubmissionDate;
        private int _Grade;
        private string _Feedback;
        #endregion

        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public Assignment()
        {
        }
        internal Assignment(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "ClassID";
        internal const string db_Title = "Title";
        internal const string db_Description = "Description";
        internal const string db_StartDate = "DateStarted";
        internal const string db_DueDate = "DateEnded";
        internal const string db_SubmissionDate = "SubmissionDate";
        internal const string db_Grade = "Grade";
        internal const string db_Feedback = "Feedback";
        #endregion

        #region Public Properites
        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public DateTime StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        [Display(Name = "Date Due")]
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
        [Display(Name = "Date Submitted")]
        public DateTime SubmisionDate
        {
            get { return _SubmissionDate; }
            set { _SubmissionDate = value;}
        }

        public int Grade
        {
            get { return _Grade; }
            set {
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

        public string Name { get; internal set; }
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
            _Title = dr.GetString(db_Title);
            _Description = dr.GetString(db_Description);
            _StartDate = dr.GetDateTime(db_StartDate);
            _DueDate = dr.GetDateTime(db_DueDate);
            _SubmissionDate = dr.GetDateTime(db_SubmissionDate);
            _Grade = dr.GetInt32(db_Grade);
            _Feedback = dr.GetString(db_Feedback);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
