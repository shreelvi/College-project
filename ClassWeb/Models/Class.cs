using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
<<<<<<< HEAD
    public class Class
=======
    public class Class : DatabaseNamedObject
>>>>>>> Elvis
    {
        /// <summary>
        /// 
        /// </summary>
<<<<<<< HEAD
        private string _Title;
=======

        #region Private Variables
>>>>>>> Elvis
        private bool _Available;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private int _SectionID;
<<<<<<< HEAD

        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }
=======
        #endregion

        #region Public Variables
>>>>>>> Elvis
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
<<<<<<< HEAD
=======

>>>>>>> Elvis
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
<<<<<<< HEAD
        public DateTime DateEnd        {
=======

        public DateTime DateEnd
        {
>>>>>>> Elvis
            get
            {
                return _DateEnd;
            }
            set
            {
                _DateEnd = value;
            }
        }
<<<<<<< HEAD
=======

>>>>>>> Elvis
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
<<<<<<< HEAD



    }
}
=======
        #endregion
    }
}



    
>>>>>>> Elvis
