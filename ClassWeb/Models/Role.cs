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
        #endregion

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Name = "Name";
        internal const string db_Description = "Description";
        #endregion

        #region Public Functions
        public override int dbSave()
        {
            if (_ID < 0)
            {
                return dbAdd();
            }
            else
            {
                return dbUpdate();
            }
        }
        /// <summary>
        /// Calls DAL function to add Role to the database.
        /// </summary>
        /// <remarks></remarks>
        protected override int dbAdd()
        {
            _ID = DAL.AddRole(this);
            return ID;
        }

        /// <summary>
        /// Calls DAL function to update Role to the database.
        /// </summary>
        /// <remarks></remarks>
        protected override int dbUpdate()
        {
            return DAL.UpdateRole(this);
        }

        /// <summary>
        /// Calls DAL function to remove Role from the database.
        /// </summary>
        /// <remarks></remarks>
        public int dbRemove()
        {
            return DAL.RemoveRole(this);
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

        public override string ToString()
        {
            return this.GetType().ToString();
        }

    }
}

