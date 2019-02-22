using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    //Summary
    // Code by Elvis
    // DatabaseNamed Object includes all of the class name or title


    public abstract class DatabaseNamedObject :DatabaseObject
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}
