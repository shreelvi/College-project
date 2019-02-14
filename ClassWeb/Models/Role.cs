using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Role
    {
        private int _ID;
        private int _Title;
        private int _Description;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;
        [Key]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public int Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public int Description
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
