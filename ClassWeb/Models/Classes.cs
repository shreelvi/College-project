using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    /// <summary>
    /// By: Ganesh Sapkota 
    /// Creating  classes model for our project.
    /// Class is like Fall 2018, Spring 2019
    /// Course will have classes and classes will have sections. 
    /// Start date and end date are the first and last day of the semester respectively.
    /// </summary>
    public abstract class Classes:DatabaseObject
    {
        #region private variable
        private int _ID;
        private string  _Title;
        private bool _Available;
        private DateTime _DateStart;
        private DateTime _DateEnd;
        private int _SectionID;
        #endregion

        #region public class
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
        #endregion
    }
}