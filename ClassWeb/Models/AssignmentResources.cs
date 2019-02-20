using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Models; 

namespace ClassWeb.Models
{
    public abstract class AssignmentResources:DatabaseObject
    {
        ///summary
        ///created by Sakshi Khetan
        ///This is for an inheritance between resource and assignment class
        
        
        #region Private Variable
        private string _Name;
        private DateTime _DateModified;
        private DateTime _DateUploaded;
        private int _ResourceSize;
        private int _MaxSize;
        private User _UserID;
        private Assignment _AssignmentID;
        #endregion

        #region Public class
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

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

