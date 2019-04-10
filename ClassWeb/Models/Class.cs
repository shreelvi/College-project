using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ClassWeb.Models;

namespace ClassWeb.Models
{
    /// <summary>
    ///Code by Meshari
    /// Class specified based on professor and year.
    /// </summary>
    public class Class : DatabaseRecord
    {
        #region Private Variables
        private string _Title;
        private bool _Available;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private Section _Section;
        private int _SectionID;
        #endregion

        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public Class()
        {
        }
        internal Class(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "ClassID";
        internal const string db_Title = "Title";
        internal const string db_Available = "Available";
        internal const string db_DateStart = "DateStart";
        internal const string db_DateEnd = "DateEnd";
        internal const string db_Section = "SectionID";
        #endregion

        #region Public Properites
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
        public bool Available
        {
            get
            {
                return _Available;
            }
            set
            {
                _Available = value;
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

        public int SectionId
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
        public Section Section
        {
            get
            {
                return _Section;
            }
            set
            {
                _Section = value;
            }
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
            _Available = dr.GetBoolean(db_Available);
            _DateStart = dr.GetDateTime(db_DateStart);
            _DateEnd = dr.GetDateTime(db_DateEnd);
            _SectionID = dr.GetInt32(Section.db_ID);
        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}



    