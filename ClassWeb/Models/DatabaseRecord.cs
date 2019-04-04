using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    /// <summary>
    /// Database Record class is used to add, update, and save objects
    /// It also contains ID and Name attribute
    /// Reference: GitHub Prof. Holmes PeerVal Project
    /// </summary>
    public abstract class DatabaseRecord
    {
        public int _ID;
        /// <summary>
        /// The User given Name for the Object.
        /// </summary>
        [Display(Name = "ID")]
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public abstract int dbSave();

        protected abstract int dbAdd();

        protected abstract int dbUpdate();

        public abstract void Fill(MySql.Data.MySqlClient.MySqlDataReader dr);

        public abstract override string ToString();
    }
}