using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class adminregController : Controller
    {
        mvcprojectEntities1 dbobj = new mvcprojectEntities1();

        // GET: adminreg
        public ActionResult insertadmin_page_load()
        {
            return View();
        }
        public ActionResult insertadmin_click(admininsert clsobj)
        {
            if (ModelState.IsValid)
            {
                var getmaxid = dbobj.max_reg_id().FirstOrDefault();
                int mid = Convert.ToInt32(getmaxid);
                int regid = 0;
                if (mid == 0)
                {
                    regid = 1;
                }
                else
                {
                    regid = mid + 1;

                }
                dbobj.sp_comp_reg(regid, clsobj.name, clsobj.description, clsobj.email, clsobj.address,clsobj.phone);
                dbobj.sp_logininsert(regid, clsobj.username, clsobj.pass, "admin");
                clsobj.adminmsg = "sucessfully registered";
                return View("insertadmin_page_load", clsobj);

            }
            return View("insertadmin_page_load", clsobj);
        }
    }
}