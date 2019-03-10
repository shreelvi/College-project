using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
<<<<<<< HEAD
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

=======
 /// <summary>
 /// Created By: Kishor Simkhada
 /// Role is a designated position for each user.
 /// Each role can be assigned to zero to many users.
 /// Each role user can have one to multiple permissions. 
 /// </summary>

    public class Role:DatabaseNamedRecord
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

        public override string ToString()
        {
            return this.GetType().ToString();
        }

        public override int dbSave()
        {
            throw new NotImplementedException();
        }

        protected override int dbAdd()
        {
            throw new NotImplementedException();
        }

        protected override int dbUpdate()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Public Subs
        /// <summary>
        /// Fills object from a MySqlClient Data Reader
        /// </summary>
        /// <remarks></remarks>
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Name = dr.GetString(db_Name);
            _Description = dr.GetString(db_Description);
        }
        #endregion
>>>>>>> Elvis
    }
}

