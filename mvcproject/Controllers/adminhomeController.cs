using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;
using System.IO;

namespace mvcproject.Controllers
{
    public class adminhomeController : Controller
    {
        mvcprojectEntities1 dbobj = new mvcprojectEntities1();
        companydb dbclsobj = new companydb();

        public ActionResult admin_home_pageload()
        {          
            return View();
        }

        [HttpPost]
        public ActionResult job_insertclick(jobinsert clsobj)
        {
            if (Session["cid"] == null)
            {
                return RedirectToAction("login_pageload", "ulogin");
            }
            if (ModelState.IsValid)
            {
                int cid = Convert.ToInt32(Session["cid"]);

                dbobj.sp_jobinsert(
                    cid,
                    clsobj.jtitle,
                    clsobj.jdesc,
                    clsobj.jloc,
                    clsobj.jexperience,
                    clsobj.jskill,
                   clsobj.cdate,
                    clsobj.edate,
                    clsobj.passoutyear,
                    clsobj.jqua
                );

                clsobj.msg = "Successfully inserted!";
            }

            

            return View("admin_home_pageload", clsobj);
        }
    }
}