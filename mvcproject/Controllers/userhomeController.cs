using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace mvcproject.Controllers
{
    public class userhomeController : Controller
    {
        // GET: userhome
        mvcprojectEntities1 dbobj = new mvcprojectEntities1();
        
        public ActionResult user_home_pageload(logincls objcls )
        {

            //var data = dbobj.sp_jobview().ToList();
            //ViewBag.jobdetails = data;
            //return View();
            return View(getjoblist());
        }
        private Jobsearch getjoblist()
        {
            var joblists = new Jobsearch();
            List<string> lst = new List<string>();
            var job = dbobj.jobs.ToList();
            foreach (var j in job)
            {
                var jobobj = new jobList();

                jobobj.Job_id = j.job_id;
                jobobj.Company_id = j.cpy_id;
                jobobj.Job_Title = j.job_title;
                jobobj.Job_description = j.job_desc;
                jobobj.Job_Experience = j.job_experience;

                jobobj.job_qualification = j.job_qualification;
                jobobj.Job_Skills = j.job_skill;
                jobobj.job_qualification = j.job_qualification;
                jobobj.Job_enddate = j.end_date;
                jobobj.Job_Location = j.location;
                jobobj.passoutyear = j.pass_out_year;

                joblists.selectjob.Add(jobobj);
            }
            return joblists;
        }
        //public ActionResult Application_pageload(int? job_id)
        //{
        //    if (job_id == null)
        //    {
        //        return Content("JOB ID IS NULL");
        //    }

        //    return Content("JOB ID = " + job_id);
        //}
        public ActionResult Application_pageload(int job_id)
        {
            var job = dbobj.sp_job_singleview(job_id).FirstOrDefault();
            if (job == null)
            {
                return Content("Job not found");
            }

            ViewBag.job = job;

            jobapplymodel model = new jobapplymodel
            {
                job_id = job_id,
                application_date = DateTime.Now
            };

            return View(model);
        }



        [HttpPost]

        public ActionResult Application_submit(jobapplymodel model)
        {
            try
            {
                if (Session["uid"] == null) return RedirectToAction("login_pageload", "ulogin");

                if (!ModelState.IsValid)
                {
                    ViewBag.job = dbobj.sp_job_singleview(model.job_id).FirstOrDefault();
                    return View("Application_pageload", model);
                }


                string resPath = Server.MapPath("~/Resumes/");
                if (!Directory.Exists(resPath)) Directory.CreateDirectory(resPath);

                string ext = Path.GetExtension(model.ResumeFile.FileName).ToLower();
                string filename = Guid.NewGuid() + ext;
                string path = Path.Combine(resPath, filename);

                model.ResumeFile.SaveAs(path);

                int uid = Convert.ToInt32(Session["uid"]);
                var existing = dbobj.applications.FirstOrDefault(x => x.user_id == uid && x.job_id == model.job_id);
                if (existing != null)
                {
                    TempData["msg"] = "You have already applied for this job!";
                    return RedirectToAction("user_home_pageload");
                }

                dbobj.sp_application_insert(uid, model.job_id, DateTime.Now, filename);


                TempData["msg"] = "Application submitted successfully";

        
                return RedirectToAction("user_home_pageload");
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }
        public ActionResult SearchJob_Click(Jobsearch clsobj)
        {
            string qry = "";

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Experience))
            {
                qry += " AND job_experience LIKE '%" + clsobj.insertse.Job_Experience + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Skills))
            {
                qry += " AND job_skill LIKE '%" + clsobj.insertse.Job_Skills + "%'";
            }

            if (!string.IsNullOrWhiteSpace(clsobj.insertse.Job_Location))
            {
                qry += " AND location LIKE '%" + clsobj.insertse.Job_Location + "%'";
            }
            if (!string.IsNullOrWhiteSpace(clsobj.insertse.job_qualification))
            {
                qry += " AND job_qualification LIKE '%" + clsobj.insertse.job_qualification + "%'";
            }

            return View("user_home_pageload", GetData(clsobj, qry));
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
                    jobcls.Job_description = dr["job_desc"].ToString();
                    jobcls.job_qualification = dr["job_qualification"].ToString();
                    jobcls.Job_Experience = dr["job_experience"].ToString();
                    jobcls.Job_Skills = dr["job_skill"].ToString();
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
