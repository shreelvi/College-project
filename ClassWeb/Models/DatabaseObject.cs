using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class DatabaseObject
    {
        #region Private Variable
        private int _ID;
        #endregion
        #region Public Class
        [Key]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        #endregion
    }
}
