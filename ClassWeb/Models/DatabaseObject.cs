using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public abstract class DatabaseObject
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

    }
}
