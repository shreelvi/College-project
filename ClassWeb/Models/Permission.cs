using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Permission
    {
        private int _ID;
        private string _Title;
        private string _Description;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;

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
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set { _DateCreated = value; }
        }


        public DateTime DateModified
        {
            get { return _DateModified; }
            set { _DateModified = value; }
        }

        public DateTime DateDeleted
        {
            get { return _DateDeleted; }
            set { _DateDeleted = value; }
        }
    }
}
