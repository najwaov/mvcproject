using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{

    public class checkboxlisthelper
    {
        public string value { get; set; }
        public string text { get; set; }
        public bool ischecked { get; set; }
    }
    public class userinsert
    {
        public List<checkboxlisthelper> mskill { get; set; }
        public string[] sskill { get; set; }

        [Required(ErrorMessage = "enter the name")]
        public string name { set; get; }
        [Range(18, 50, ErrorMessage = "enter the valid age")]
        public int age { set; get; }
        [Required(ErrorMessage = "enter the address")]
        public string address { set; get; }
        [Required(ErrorMessage = "enter the phone no")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "enter valid phone no")]
        public string uphone { set; get; }
        
        
        public string skill { get; set; }
        [Required(ErrorMessage ="enter years of experience")]
        public string yoe { set; get; }
        [Required(ErrorMessage = "enter your qualification")]
        public string qua { set; get; }
        [EmailAddress(ErrorMessage = "enter valid email")]
        public string email { set; get; }
        [Required(ErrorMessage ="choose your gender")]
        public string gender { set; get; }
        public string username { set; get; }
        public string pass { set; get; }
        [Compare("pass", ErrorMessage = "password missmatch")]
        public string cpassword { set; get; }
        public string usermsg { set; get; }

    }
}