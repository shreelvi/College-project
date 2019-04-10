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
    public abstract class DatabaseRolePermission:DatabaseNamedObject
    {
        #region Private Variable
        private string _Description;
       
        #endregion

        #region Public properties
        /// <summary>
        /// Public properties for the Role Permission Object
        /// </summary>
        //Primary Key for database
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
       

       
        #endregion

    }
}
