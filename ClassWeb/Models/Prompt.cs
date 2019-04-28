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

    public class Prompt : DatabaseRecord {
        #region Constructors
        public Prompt() {
        }
        internal Prompt(MySql.Data.MySqlClient.MySqlDataReader dr) {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "PromptID";
        internal const string db_Text = "Text";

        #endregion

        #region Private Variables
        private string _Text;

        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the Text for this PeerVal.Prompt object.
        /// </summary>
        /// <remarks></remarks>
        public string Text {
            get {
                return _Text;
            }
            set {
                _Text = value.Trim();
            }
        }


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
        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr) {
            _ID = dr.GetInt32(db_ID);
            _Text = dr.GetString(db_Text);
        }

        #endregion

    
    }
}
