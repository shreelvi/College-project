using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    public class Class : DatabaseRecord
    {
        /// <summary>
        /// By: Ganesh Sapkota 
        /// Ref: Professor's code for PeerEval
        /// Creating  classes model for our project.
        /// Class is like Fall 2018, Spring 2019
        /// Course will have classes and classes will have sections. 
        /// Start date and end date are the first and last day of the semester respectively.
        /// </summary>

        #region Database String
        internal const string db_ID = "ID";
        internal const string db_Title = "Title";
        internal const string db_IsAvailable = "Availability";
        internal const string db_DateStart = "DateStart";
        internal const string db_DateEnd = "DateEnd";
        internal const string db_SectionID = "SectionID";
        #endregion
        internal Class(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Title = dr.GetString(db_Title);
            _IsAvailable = dr.GetBoolean(db_IsAvailable);
            //_DateStart = dr.GetDateTime(db_DateStart);
            //_DateEnd = dr.GetDateTime(db_DateEnd);
        }

        #region Private Variables
        private string _Title;
        private bool _IsAvailable;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private int _SectionID;

        #endregion

        #region public class

        public bool IsAvailable
        {
            get
            {
                return _IsAvailable;
            }
            set
            {
                _IsAvailable = value;
            }
        }

        public DateTime DateStart
        {
            get
            {
                return _DateStart;
            }
            set
            {
                _DateStart = value;
            }
        }

        public DateTime DateEnd
        {
            get
            {
                return _DateEnd;
            }
            set
            {
                _DateEnd = value;
            }
        }

        public int SectionID
        {
            get
            {
                return _SectionID;
            }
            set
            {
                _SectionID = value;
            }
        }

        public string Title {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        public override int dbSave()
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

        internal static Class Get(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
