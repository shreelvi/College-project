using Microsoft.AspNetCore.Antiforgery.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Assignment:DatabaseNamedObject
    {
        #region Database String
        internal const string db_ID = "AssignmentID";
        internal const string db_FileName = "FileName";
        internal const string db_DateStarted = "DateStarted";
        internal const string db_DateDue = "DateDue";
        internal const string db_DateSubmited = "DateSubmited";
        internal const string db_Grade = "Grade";
        internal const string db_Feedback = "Feedback";
        internal const string db_FileSize = "FileSize";
        internal const string db_IsEditable = "IsEditable";
        #endregion

        #region Private Variable
        private DateTime _DateStarted;
        private DateTime _DateDue;
        private  DateTime _DateSubmited;
        private int _Grade;
        private string _Feedback;
        private double _FileSize;
        private bool _IsEditable;
        #endregion

        #region Public Properties
        public bool IsEditable
        {
            get
            {
                return _IsEditable;
            }
            set
            {
                _IsEditable = value;
            }
        }
        public double FileSize
        {
            get
            {
                return _FileSize;
            }
            set
            {
                _FileSize = value;
            }
        }
        public DateTime StartDate
        {
            get { return _DateStarted; }
            set { _DateStarted = value; }
        }

        [Display(Name = "Date Due")]
        public DateTime DueDate
        {
            get { return _DateDue; }
            set { _DateDue = value; }
        }
        [Display(Name = "Date Submitted")]
        public DateTime SubmisionDate
        {
            get { return _DateSubmited; }
            set { _DateSubmited = value;}
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
        #endregion
    }
}
