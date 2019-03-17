using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
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

        public static string db_Title { get; internal set; }
        public static string db_DateCreated { get; internal set; }
        public object Title { get; internal set; }
        public object DateCreated { get; internal set; }
        public object DateModified { get; internal set; }
        public object DateDeleted { get; internal set; }
        public static string db_DateModified { get; internal set; }
        public static string db_DateDeleted { get; internal set; }
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
    }
}

