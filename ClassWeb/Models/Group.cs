using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
<<<<<<< HEAD:ClassWeb/Models/Group.cs
    public class Group:DatabaseObject
    {
        private string _GroupName;
=======
    /// <summary>
    /// Code by Elvis
    /// Groups are a team of student members for a projects in class
    /// </summary>

    public class Group: DatabaseNamedObject 
    {
        #region Private Variables
>>>>>>> Elvis:ClassWeb/Models/Group.cs
        private string _EmailAddress;
        private string _UserName;
        private string _Password;
        private Assignment _AssignmentID;
<<<<<<< HEAD:ClassWeb/Models/Group.cs

        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }
=======
        #endregion
>>>>>>> Elvis:ClassWeb/Models/Group.cs

        #region Public Variables
        [Display(Name ="Group's Email-address",
            Description = "Email-address used to contact the group; which all members will have access.")]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

        [Display(Name = "Group's Login Username",
            Description = "Username to login to group's account profile.")]
        public string Username
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        [Display(Name = "Group's Login Password",
            Description = "Password to login to group's account profile.")]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

<<<<<<< HEAD:ClassWeb/Models/Group.cs
=======
        //Foreign Key
>>>>>>> Elvis:ClassWeb/Models/Group.cs
        public Assignment AssignmentID
        {
            get { return _AssignmentID; }
            set { _AssignmentID = value; }
        }
        #endregion
    }
}
