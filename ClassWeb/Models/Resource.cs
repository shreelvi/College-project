
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClassWeb.Models;

namespace ClassWeb.Models
{
<<<<<<< HEAD:ClassWeb/Models/Resources.cs
    public class Resource:DatabaseObject
    {

        private string _Name;
=======
    /// <summary>
    /// Code by Elvis
    /// Resources are files that are uploaded in the system
    /// </summary>
    public class Resource: DatabaseNamedObject
    {

        #region Private Variables
>>>>>>> Elvis:ClassWeb/Models/Resource.cs
        private DateTime _DateModified;
        private DateTime _DateUploaded;
        private int _ResourceSize;
        private int _MaxSize;
        private User _UserID;
        private Assignment _AssignmentID;
<<<<<<< HEAD:ClassWeb/Models/Resources.cs

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        
=======
        #endregion

        #region Public Variables
>>>>>>> Elvis:ClassWeb/Models/Resource.cs
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

        public User UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public Assignment AssignmentID
        {
            get { return _AssignmentID; }
            set { _AssignmentID = value; }
        }
        #endregion
    }
}
