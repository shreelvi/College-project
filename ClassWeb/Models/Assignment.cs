using Microsoft.AspNetCore.Antiforgery.Internal;
using System;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System.ComponentModel;
>>>>>>> Elvis
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
<<<<<<< HEAD
    public class Assignment:DatabaseObject
    {
        private string _Title;
        private string _Description;
        private DateTime _StartDate;
        private DateTime _DueDate;
        private DateTime _SubmissionDate;
        private int _Grade;
        private string _Feedback;
        private Stream _File;

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public Stream File
        {
            get { return _File; }
            set { _File = value; }
        }


=======
    public class Assignment:DatabaseNamedObject
    {
        private string _Description;
        private DateTime _StartDate;
        private DateTime _DueDate;
        private  DateTime _SubmissionDate;
        private int _Grade;
        private string _Feedback;
      
     
>>>>>>> Elvis
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

<<<<<<< HEAD
=======
        [Display(Name = "Date Due")]
>>>>>>> Elvis
        public DateTime DueDate
        {
            get { return _DueDate; }
            set { _DueDate = value; }
        }
<<<<<<< HEAD

        public DateTime SubmisionDate
        {
            get { return _SubmissionDate; }
            set { _SubmissionDate = value; }
=======
        [Display(Name = "Date Submitted")]
        public DateTime SubmisionDate
        {
            get { return _SubmissionDate; }
            set { _SubmissionDate = value;}
>>>>>>> Elvis
        }

        public int Grade
        {
            get { return _Grade; }
<<<<<<< HEAD
            set { _Grade = value; }
=======
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
>>>>>>> Elvis
        }

        public string Feedback
        {
            get { return _Feedback; }
            set { _Feedback = value; }
        }
    }
}
