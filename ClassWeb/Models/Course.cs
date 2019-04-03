using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

//Meshari, 02/14
//Courses => A course is like 4430, 3307, etc.
//Each course can be accessible to one to many users.
//Each course can be taught by multiple professors, hence multiple classes.


namespace ClassWeb.Models
{
    public class Course : DatabaseRecord
    {

        #region Private Variables

        private int _Number;
        private int _Name;
        private int _ClassID;
        #endregion

        #region Public Variables
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        public int Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int ClassID
        {
            get { return _ClassID; }
            private set { _ClassID = value; }
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

        public override void Fill(MySql.Data.MySqlClient.MySqlDataReader dr)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
        internal static Task FirstOrDefaultAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
        #endregion
    }

}
