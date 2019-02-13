using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Assignment
    {
        private int _ID;
        private string _Title;
        private string _Description;
        private DateTime _StartDate;
        private DateTime _DueDate;
        private DateTime _SubmissionDate;
        private int _Grade;
        private string _Feedback; 

        [Key]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

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

        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }

        public DateTime SubmisionDate
        {
            get { return _SubmissionDate; }
            set { _SubmissionDate = value; }
        }

        public int Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }

        public string Feedback
        {
            get { return _Feedback; }
            set { _Feedback = value; }
        }
    }
}
