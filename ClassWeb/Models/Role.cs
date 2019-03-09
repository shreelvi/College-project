using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By: Kishor Simkhada
    /// Role is a designated position for each user.
    /// Each role can be assigned to zero to many users.
    /// Each role user can have one to multiple permissions. 
    /// </summary>
    public class Role : DatabaseRolePermission
    {
        private MySqlDataReader dr;

        public Role()
        {
        }

        public Role(MySqlDataReader dr)
        {
            this.dr = dr;
        }
        #region Constructors

        private void Fill(MySqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Database String
        internal const string db_Title = "Title";
        internal const string db_Description = "Description";
        internal const string db_DateCreated = "DateCreated";
        internal const string db_DateModified = "DateModified";
        internal const string db_DateDeleted = "DateDeleted";

        #endregion

        #region Private Variables
        private string _Title;
        private string _Description;
        private DateTime _DateCreated;
        private DateTime _DateModified;
        private DateTime _DateDeleted;

        public object Title { get; internal set; }


        #endregion

    }
}
