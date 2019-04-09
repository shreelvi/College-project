using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    public class Semester:DatabaseRecord
    {
        /// <summary>
        /// By: Ganesh Sapkota 
        /// Ref: Professor's code for PeerEval
        /// Creating  semester model for our project.
        /// Class is like Fall , Spring 
        /// </summary>

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Name = "Name";
        #endregion

        #region Private Variables
        private string _Name;
        #endregion

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        public override int dbSave()
        {
            throw new NotImplementedException();
        }

        public override void Fill(MySqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
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
    }

}
