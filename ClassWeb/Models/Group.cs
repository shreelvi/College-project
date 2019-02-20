using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Group:DatabaseObject
    {
        private string _GroupName;
        private string _EmailAddress;
        private string _UserName;
        private string _Password;
        private Assignment _AssignmentID;

        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        public string Username
        {
            get { return _UserName; }
            set { _UserName = value; }
        }


        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public Assignment AssignmentID
        {
            get { return _AssignmentID; }
            set { _AssignmentID = value; }
        }
    }
}
