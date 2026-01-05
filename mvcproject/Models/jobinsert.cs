using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    public class companyclass
    {
        public int cid { get; set; }
        public string cname { get; set; }
    }
    public class jobinsert
    {
        [Required(ErrorMessage = "select the company")]
        public int cid { set; get; }
       public int job_id { set; get; }

        [Required(ErrorMessage = "enter the job title")]
        public string jtitle { set; get; }
        [Required(ErrorMessage ="enter the job description")]
        public string jdesc { set; get; }
        [Required(ErrorMessage = "enter the job location")]
        public string jloc { set; get; }
        [Required(ErrorMessage = "enter required experience")]
        public string jexperience { set; get; }

        [Required(ErrorMessage = "enter required skills")]
        public string jskill { set; get; }
      
        public DateTime cdate { set; get; }
        [Required(ErrorMessage = "enter the end date")]
        public DateTime edate { set; get; }
        [Required(ErrorMessage ="enter the required pass out year")]
        public int passoutyear { set; get; }
        [Required(ErrorMessage ="enter required qualification")]
        public string jqua { set; get; }
        public string msg { get; set; }
    }
}