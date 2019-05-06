using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Semester : DatabaseNamedRecord
    {
        /// <summary>
        /// Modified by Ganesh
        /// semester like Fall, Spring, Summer
        /// </summary>
        #region Constructors
        public Semester()
        {
        }
        internal Semester(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion


        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Name = "SemesterName";
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

        public int dbRemove()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Subs
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Name = dr.GetString(db_Name);
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
        #endregion
    }
}
