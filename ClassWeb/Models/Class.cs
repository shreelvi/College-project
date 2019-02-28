using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Class : DatabaseNamedObject
    {
        /// <summary>
        /// 
        /// </summary>

        #region Private Variables
        private bool _Available;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private int _SectionID;
        #endregion

        #region Public Variables
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
        #endregion
    }
}