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
    /// Creating  classes model for our project.
    /// Class is like Fall 2018, Spring 2019
    /// Course will have classes and classes will have sections. 
    /// Start date and end date are the first and last day of the semester respectively.
    /// </summary>

        #region Private Variables
        private bool _Available;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private int _SectionID;
        #endregion

         #region public class
        
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
        #endregion
    }
}



    
