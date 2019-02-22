using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*Mohan S Madai
 * INFO 4482
 * Project: Classweb
 * Purpose: To create model for section
 */

namespace ClassWeb.Models
{
     /// <summary>
    ///  A section number tells how many sections of a class is offered in the semester. For example,  01 in 4430-01 is the section number. 
    ///Each section can have one class. 
   ///Each user in a section can be assigned/upload one to many assignments.
  /// </summary>
    public class Sections:DatabaseObject
    {
        //creating variable region

        #region private variable
        private int _SectionNumber;
        private int _ClassID;
        private int _UserID;
        #endregion

        //creating public properties region
        #region public properties 
        public int SectionNumber
        {
            get { return _SectionNumber;}
            set { _SectionNumber = value;}
        }


        public int ClassID
        {
            get {return _ClassID;}
            set {_ClassID = value;}
        }


        public int UserID
        {
            get {return _UserID;}
            set { _UserID = value;}
        }

        #endregion




    }
}
