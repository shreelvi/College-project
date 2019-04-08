using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
   // Author: Meshari
   // Create date:	31 March 2019
    public class Section : DatabaseNamedRecord
    {
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
        private int _SectionNumber;
        private int _CRN;
        //private int _ClassID;
        private int _UserID;
        private int _CourseID;
        private Course _Course;
        private User _User;

        #endregion


        #region Public Properties
        public int SectionNumber
        {
            get
            {
                return _SectionNumber;
            }

            set
            {
                _SectionNumber = value;
            }
        }

        public int CRN
        {
            get
            {
                return _CRN;
            }

            set
            {
                _CRN = value;
            }
        }


        //public int ClassID
        //{
        //    get
        //    {
        //        return _ClassID;
        //    }

        //    set
        //    {
        //        _ClassID = value;
        //    }
        //}


        public int UserID
        {
            get
            {
                return _UserID;
            }

            set
            {
                _UserID = value;
            }
        }

        public int CourseID
        {
            get
            {
                return _CourseID;
            }

            set
            {
                _CourseID = value;
            }
        }


        #endregion

        #region Database String
        internal const string db_ID = "SectionID";
        internal const string db_CRN = "CRN";
        internal const string db_Number = "SectionNumber";
        internal const string db_UserID = "UserID";
        internal const string db_CourseID = "CourseID";
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
            _CRN = dr.GetInt32(db_CRN);
            _SectionNumber = dr.GetInt32(db_Number);
            _UserID = dr.GetInt32(db_UserID);
            _CourseID = dr.GetInt32(db_CourseID);
        }
        #endregion


        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
