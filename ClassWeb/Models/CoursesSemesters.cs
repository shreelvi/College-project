using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ClassWeb.Models
{
    public class CoursesSemesters:DatabaseRecord
    {
        /// <summary>
        /// By: Ganesh Sapkota 
        /// Ref: Professor's code for PeerEval
        /// Creating  CoursesSemester model for our project. 
        /// </summary>

        #region Private Variables
        private int _CourseID;
        private int _SemesterID;
        private int _YearID;
        private int _SectionID;
        private int _UserID;
        #endregion


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
        public int SemesterID
        {
            get
            {
                return _SemesterID;
            }
            set
            {
                _SemesterID = value;
            }
        }
        public int YearID
        {
            get
            {
                return _YearID;
            }
            set
            {
                _YearID = value;
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
