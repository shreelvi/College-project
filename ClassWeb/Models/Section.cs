using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ClassWeb.Model;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    public class Section : DatabaseRecord
    {
        /// <summary>
        /// Modified by Ganesh
        /// sections are like 01,02. For example, on INFO 4482-01, 01 is section number
        /// </summary>
        #region Constructors
        public Section()
        {
        }
        internal Section(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }
        #endregion

        #region Private Variables
        private int _Number;
        //private int _CRN;
        //private int _ClassID;


        #endregion


        #region Public Properties

        public int Number
        {
            get
            {
                return _Number;
            }

            set
            {
                _Number = value;
            }
        }




        #endregion

        #region Database String
        internal const string db_ID = "SectionID";
        internal const string db_Number = "Number";
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
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Number = dr.GetInt32(db_Number);
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }


    }
}