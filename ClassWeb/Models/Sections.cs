using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Sections
    {
        private int _ID;
        private int _SectionNumber;
        private int _ClassID;
        private int _UserID;

        [Key]
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

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
