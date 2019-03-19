using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
}
 /// <summary>
 /// Created By: Kishor Simkhada
 /// Role is a designated position for each user.
 /// Each role can be assigned to zero to many users.
 /// Each role user can have one to multiple permissions. 
 /// </summary>

        /*
    public class Role:DatabaseRecord
    {
        #region Constructors
        public Role()
        {
        }
        internal Role(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Private Variables
        private string _Description;
        #endregion

        #region public Properites
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        #endregion

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Name = "Name";
        internal const string db_Description = "Description";
        #endregion

        #region Public Functions

       
        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a MySqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        #endregion
    }
}
*/