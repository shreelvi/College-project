using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Section:DatabaseNamedObject
    {
        private int _SectionNumber;
        private int _ClassID;
        private int _UserID;
        public int SectionNumber
        {
            get
            { return _SectionNumber;
            }

            set
            { _SectionNumber = value;
            }
        }


        public int ClassID
        {
            get
            {
                return _ClassID;
            }

            set
            {
                _ClassID = value;
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






    }
}
