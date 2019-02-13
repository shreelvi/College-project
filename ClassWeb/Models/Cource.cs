using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Cource
    {
        private int _CourseID;
        private string _Name;
        private int _Number;
        private DateTime _Year;
        private DateTime _DateStart;
        private DateTime _DateEnd;

        public int CourseID
        {
            get { return _CourseID; }
            set { _CourseID = value; }
        }
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        public int Number
        {
            get { return _Number; }
            set { _Number = value; }
        }
        public DateTime Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        public DateTime DateStart
        {
            get { return _DateStart; }
            set { _DateStart = value; }
        }
        public DateTime DateEnd
        {
            get { return _DateEnd; }
            set { _DateEnd = value; }
        }
    }
}
