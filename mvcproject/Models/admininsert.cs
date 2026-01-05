using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    public class admininsert
    {
        [Required(ErrorMessage = "enter the name")]
        public string name { set; get; }
        [Required(ErrorMessage = "enter company description")]
        public string description { set; get; }
        [EmailAddress(ErrorMessage = "enter valid email")]
        public string email { set; get; }

        [Required(ErrorMessage = "enter the address")]
        public string address { set; get; }
        [Required(ErrorMessage = "enter the phone no")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "enter valid phone no")]
        public string phone { set; get; }
        
        public string username { set; get; }
        public string pass { set; get; }
        [Compare("pass", ErrorMessage = "password missmatch")]
        public string cpassword { set; get; }
        public string adminmsg { set; get; }
    }
}