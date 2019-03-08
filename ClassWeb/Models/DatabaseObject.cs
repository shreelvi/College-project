using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public abstract class DatabaseObject
    {
        /// <summary>
        /// Created By: Kishor Simkhada
        /// Object for database that can be inherited by other DatabaseNamed object for ID to all classes.
        /// </summary>

        #region Private Variable
        private int _ID;
        #endregion

        #region Public Class
        //Primary Key for database
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        #endregion
    }
}
