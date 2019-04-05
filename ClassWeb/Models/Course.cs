using MySql.Data.MySqlClient;
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
        public Course()
        {

        }
        //database strings
        #region Database  String
        internal const string db_ID = "ID";
        internal const string db_Name = "Name";
        internal const string db_Number = "Number";
        internal const string db_ClassID = "ClassID";
        #endregion

      //Fills the data from the database while making objects
        public override void Fill(MySqlDataReader dr)
        {
            _ID = dr.GetInt32(db_ID);
            _Name = dr.GetString(db_Name);
            _Number = dr.GetInt32(db_Number);
            _ClassID = dr.GetInt32(db_ClassID);
            
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


        public override string ToString()
        {
            return this.GetType().ToString();
        }
        internal static Task FirstOrDefaultAsync(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }


        #region Private Variables

        private int _Number;
        private string _Name;
        private int _ClassID;

        public Course(MySqlDataReader dr)
        {
        }
        #endregion

        #region Public Variables
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int ClassID
        {
            get { return _ClassID; }
            private set { _ClassID = value; }
        }
       
        #endregion
    }

}
