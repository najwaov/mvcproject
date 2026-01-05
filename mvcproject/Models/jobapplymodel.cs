using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mvcproject.Models;

namespace mvcproject.Models
{
    public class jobapplymodel
    {
        //public int cid { get; set; }
        //public string cname { get; set; }
        //public string company_desc { set; get; }
        //public int job_id { get; set; }
        //public string title { get; set; }
        //public string description { get; set; }
        //public int experience { get; set; }
        //public string qualification { get; set; }
        //public string location { get; set; }
        //public string message { get; set; }
       
        public int user_id { get; set; }
        public int job_id { get; set; }
        public  DateTime application_date { get; set; }
        public string resume { get; set; }

        [Required(ErrorMessage = "Please upload resume")]
        public HttpPostedFileBase ResumeFile { get; set; }
        public string msg { get; set; }
    }
}