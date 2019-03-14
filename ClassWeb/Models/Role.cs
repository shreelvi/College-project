using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Model;

namespace ClassWeb.Models
{
 /// <summary>
 /// Created By Meshari
 /// Role is a designated position for each user.
 /// Each role can be assigned to zero to many users.
 /// Each role user can have one to multiple permissions. 
 /// </summary>
    public class Role: DatabaseRecord
    {

        #region Private Variables
        private string _Title;
        private string _Description;
        private DateTime _DateCreated;
        private DateTime _DateModifed;
        private DateTime _DateDeleted;
        #endregion

        #region Constructors
        public Role()
        {
        }
        internal Role(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "RoleID";
        internal const string db_Title = "Title";
        internal const string db_Description = "Description";
        internal const string db_DateCreated = "DateCreated";
        internal const string db_DateModifed = "DateModifed";
        internal const string db_DateDeleted = "DateDeleted";
        #endregion

        #region public Properites
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
        public DateTime DateModifed
        {
            get { return _DateModifed; }
            set { _DateModifed = value; }
        }
        public DateTime DateDeleted
        {
            get { return _DateDeleted; }
            set { _DateDeleted = value; }
        }
        #endregion

        #region Public Functions
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
            _Title = dr.GetString(db_Title);
            _Description = dr.GetString(db_Description);
            _DateCreated = dr.GetDateTime(db_DateCreated);
            _DateModifed = dr.GetDateTime(db_DateModifed);
            _DateDeleted = dr.GetDateTime(db_DateDeleted);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }

    }
}
