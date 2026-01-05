using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcproject.Models
{
    public class Jobsearch
    {

        public Jobsearch()
        {
            selectjob = new List<jobList>();
            insertse= new jobList();
        }

          public jobList insertse { set; get; }

          public List<jobList> selectjob { set; get; }
    }
         public class jobList
    {

         public int Job_id { get; set; }

        public int Company_id { get; set; }
        

           public string Job_Title { get; set; }

           public string Job_description { get; set; }

             public string Job_Experience { get; set; }

           public string Job_Skills { get; set; }
 
   
           public System.DateTime Job_enddate { get; set; }

             public string Job_Location { get; set; }

              public string Job_Status { get; set; }

              public int passoutyear { get; set; }

              public string job_qualification { get; set; }
    }
}