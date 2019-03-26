using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ClassWeb.Model;

namespace ClassWeb.Models
{
    /// <summary>
    /// Created By Meshari
    /// A section number tells how many sections of a class is offered in the semester.  
    /// </summary>
    public class Section: DatabaseRecord
    {
        #region Private Variables
        private int _SectionNumber;
        private DateTime _SectionStart;
        private DateTime _SectionEnd;
        //private Class _Class;
        //private int _ClassID;
        private User _User;
        private int _UserID;
        #endregion

        #region Constructors
        public Section()
        {
        }
        internal Section(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            Fill(dr);
        }

        #endregion

        #region Database String
        internal const string db_ID = "RoleID";
        internal const string db_SectionNumber = "SectionNumber";
        internal const string db_SectionStart = "SectionStart";
        internal const string db_SectionEnd = "SectionEnd";
       // internal const string db_Class = "ClassID";
        internal const string db_User = "UserID";
        #endregion

        #region public Properites
        public int SectionNumber
        {
            get { return _SectionNumber; }
            set { _SectionNumber = value; }
        }

        public DateTime SectionStart
        {
            get { return _SectionStart; }
            set { _SectionStart = value; }
        }
        public DateTime SectionEnd
        {
            get { return _SectionEnd; }
            set { _SectionEnd = value; }
        }
        //public Class Class
        //{
        //    get { return _Class; }
        //    set { _Class = value; }
        //}
        //public int ClassID
        //{
        //    get { return _ClassID; }
        //    set { _ClassID = value; }
        //}
        public User User
        {
            get { return _User; }
            set { _User = value; }
        }
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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
            _SectionNumber = dr.GetInt32(db_SectionNumber);
            _SectionStart = dr.GetDateTime(db_SectionStart);
            _SectionEnd = dr.GetDateTime(db_SectionEnd);
            //_ClassID = dr.GetInt32(Class.db_ID);
            _UserID = dr.GetInt32(User.db_ID);
        }
        #endregion
        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
