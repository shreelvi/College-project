
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace classWeb.Models
{
    public class Actor
    {
        private int _ResourceID;
        private string _ResourceName;
        private DateTime _DateModified;
        private DateTime _DateUploaded;
        private int _ResourceSize;
        private int _MaxSize;
        private Users _UserID;
        private Assignments _AssignmentID;

        
        [Key]
        public int ResourceID
        {
            get { return _ResourceID; }
            set { _ResourceID = value; }
        }


        public string ResourceName
        {
            get { return _ResourceName; }
            set { _ResourceName = value; }
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

        public Users UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public Assignments AssignmentID
        {
            get { return _AssignmentID; }
            set { _AssignmentID = value; }
        }
    }
}
