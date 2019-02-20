using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; 
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Models;

namespace ClassWeb.Models
{
    public class Assignment:AssignmentResources
        ///summary 
        ///Created by: Sakshi Khetan
        ///Every assignment is a type of resource
        ///Every user can upload one to many assignments
        ///this class inherits everything from AssignmentResource class

    {
        private string _Title;
        private string _Description;
        private DateTime _StartDate;
        private DateTime _DueDate;
        private DateTime _SubmissionDate;
        private int _Grade;
        private string _Feedback; 

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
