using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By: Kishor Simkhada
    /// Object for database of Role and Permission that can be inherited by other object.
    /// </summary>
    public abstract class DatabaseRolePermission
    {
        #region Private Variable
        private int _ID;
        private string _Title;
        private string _Description;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;
        #endregion
        #region Public properties
        /// <summary>
        /// Public properties for the Role Permission Object
        /// </summary>
        //Primary Key for database
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
        #endregion

    }
}
