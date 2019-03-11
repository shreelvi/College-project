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
        private string _Description;
        private DateTime _StartDate;
        private DateTime _DueDate;
        private  DateTime _SubmissionDate;
        private int _Grade;
        private string _Feedback;
      
     
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
    }
}
