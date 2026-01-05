using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    public class logincls
    {
        public string id { set; get; }

        [Required(ErrorMessage = "enter the username")]
        public string username { set; get; }

        [Required(ErrorMessage = "enter the password")]
        public string password { set; get; }
        public string msg { set; get; }
        public string ltype { set; get; }
    }
}