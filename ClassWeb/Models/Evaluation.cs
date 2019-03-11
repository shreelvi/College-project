//Created By: holmjona (using Code generator)
//Created On: 2/19/2019 11:40:38 PM
using System;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using ClassWeb.Models;

namespace ClassWeb {
    /// <summary>
    /// TODO: Comment this
    /// </summary>
    /// <remarks></remarks>

    public class Evaluation : DatabaseRecord {
        #region Constructors
        public Evaluation() {
        }
        internal Evaluation(MySql.Data.MySqlClient.MySqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "EvaluationID";
        internal const string db_Name = "Name";
        internal const string db_DateStart = "DateStart";
        internal const string db_DateClosed = "DateClosed";

        #endregion

        #region Private Variables
        private string _Name;
        private DateTime _DateStart;
        private DateTime _DateClosed;
        private List<Prompt> _Prompts = null;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Name for this PeerVal.Evaluation object.
        /// </summary>
        /// <remarks></remarks>
        public string Name {
            get {
                return _Name;
            }
            set {
                _Name = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the DateStart for this PeerVal.Evaluation object.
        /// </summary>
        /// <remarks></remarks>
        public DateTime DateStart {
            get {
                return _DateStart;
            }
            set {
                _DateStart = value;
            }
        }

        /// <summary>
        /// Gets or sets the DateClosed for this PeerVal.Evaluation object.
        /// </summary>
        /// <remarks></remarks>
        public DateTime DateClosed {
            get {
                return _DateClosed;
            }
            set {
                _DateClosed = value;
            }
        }


        //public List<Prompt> Prompts {
        //    get {
        //        if (_Prompts == null) _Prompts = DAL.GetPrompts(this);
        //        return _Prompts;
        //    }
        //    set { _Prompts = value; }
        //}






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
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr) {
            _ID = dr.GetInt32(db_ID);
            _Name = dr.GetString(db_Name);
            _DateStart = dr.GetDateTime(db_DateStart);
            _DateClosed = dr.GetDateTime(db_DateClosed);
        }

        #endregion

        public override string ToString() {
            return this.GetType().ToString();
        }

    }
}
