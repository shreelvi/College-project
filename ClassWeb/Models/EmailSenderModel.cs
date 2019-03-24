using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassWeb.Models
{
    public class EmailSenderModel
    {
        private string _EmailAddress;
        private string  _Subject;
        private string _Message;

        [Required]
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }


        public string  Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }

        [Required]
        public string EmailAddress
        {
            get { return _EmailAddress; }
            set { _EmailAddress = value; }
        }

    }
}
