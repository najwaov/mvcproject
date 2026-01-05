using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchJob_Click(Jobsearch clsobj)
        {
            string qry = "";

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Experience))
            {
                qry += " AND Job_Experience LIKE '%" + clsobj.insertse.Job_Experience + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Skills))
            {
                qry += " AND Job_Skills LIKE '%" + clsobj.insertse.Job_Skills + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Location))
            {
                qry += " AND Job_Location LIKE '%" + clsobj.insertse.Job_Location + "%'";
            }

            return View("jobview_pageload", GetData(clsobj, qry));
        }

        private Jobsearch GetData(Jobsearch clsobj, string qry)
        {
            using (var con = new SqlConnection(
                ConfigurationManager.ConnectionStrings["importdataconnection"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SP_Jobsearches", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@qry", qry);

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                var joblist = new Jobsearch();
                joblist.selectjob = new List<jobList>();

                while (dr.Read())
                {
                    var jobcls = new jobList();

                    jobcls.Job_id = Convert.ToInt32(dr["job_id"]);
                    jobcls.Company_id = Convert.ToInt32(dr["cpy_id"]);
                    jobcls.Job_Title = dr["job_title"].ToString();
                    jobcls.Job_description = dr["job_description"].ToString();
                    jobcls.job_qualification = dr["job_qualification"].ToString();
                    jobcls.Job_Experience = dr["job_experience"].ToString();
                    jobcls.Job_Skills = dr["job_skills"].ToString();
                    jobcls.passoutyear = Convert.ToInt32(dr["pass_out_year"]);
                    jobcls.Job_enddate = Convert.ToDateTime(dr["end_date"]);
                    jobcls.Job_Location = dr["location"].ToString();

                    joblist.selectjob.Add(jobcls);
                }

                con.Close();
                return joblist;
            }
        }
    }
}
