using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    // <Summary>
    // Code by Elvis
    // DatabaseNamed Object inherits DatabaseObject includes the name or title of a class
    // Other Classes can inherit this class to use primary ID and Name property
    // </Summary>



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
