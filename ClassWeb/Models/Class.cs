using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class Classes:DatabaseObject
    {
        /// <summary>
        /// 
        /// </summary>
        private int _ID;
        private string _Title;
        private bool _Available;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private int _SectionID;


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
        public DateTime DateEnd        {
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



    }
}
