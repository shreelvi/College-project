using ClassWeb.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClassWeb.Models
{
    public class ViewGroupUser : DatabaseRecord
    {
        /// <summary>
        /// Created By: Kishor Simkhada
        /// Everyone is a user when they sign up except Admin.
        /// Special permission will be provided based on the roles assigned to them on the system.
        /// Every user can login to the system unless deleted.
        /// </summary>



        #region Constructors
        /// <summary>
        /// Code By Elvis
        /// Constructor to map results of sql query to the class
        /// Reference: GitHub PeerVal Project
        /// </summary>
        public ViewGroupUser()
        {
        }
        internal ViewGroupUser(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region private variable
        private string _FirstName;
        // private string _MiddleName;
        private string _LastName;
        private string _EmailAddress;

        #endregion

        #region Database String
        internal const string db_UserID = "UserID";
        internal const string db_FirstName = "FirstName";
        //internal const string db_MiddleName = "MiddleName";
        internal const string db_LastName = "LastName";
        internal const string db_EmailAddress = "EmailAddress";




        #endregion

        #region public Properites

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }


        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
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
            _ID = dr.GetInt32("ID");
            _FirstName = dr.GetString(db_FirstName);

            _LastName = dr.GetString(db_LastName);
            _EmailAddress = dr.GetString(db_EmailAddress);


        }
        #endregion

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}