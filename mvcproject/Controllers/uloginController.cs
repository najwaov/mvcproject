using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class uloginController : Controller
    {
        mvcprojectEntities1 dbobj = new mvcprojectEntities1();
        // GET: ulogin
        public ActionResult login_pageload()
        {
            return View();
        }
        //public ActionResult user_home()
        //{
        //    return View();
        //}
        //public ActionResult admin_home()
        //{
        //    return View();
        //}
        public ActionResult login_click(logincls objcls)
        {
            if (ModelState.IsValid)
            {
                var val = dbobj.sp_logincount_id(objcls.username, objcls.password).First();
                if (val == 1)
                {
                    var uid = dbobj.sp_logingetid(objcls.username, objcls.password).FirstOrDefault();
                    
                    var lt = dbobj.sp_logintype(objcls.username, objcls.password).FirstOrDefault();
                    if (lt == "user")
                    {
                        Session["uid"] = uid;
                        return RedirectToAction("user_home_pageload", "userhome");
                    }
                    else if (lt == "admin")
                    {
                        Session["cid"] = uid;
                        return RedirectToAction("admin_home_pageload", "adminhome");
                    }
                }
                else
                {
                    ModelState.Clear();
                    objcls.msg = "invalid username or password..";
                    return View("login_pageload", objcls);
                }
            }
            else
            {
                ModelState.Clear();
                objcls.msg = "invalid login";
                return View("login_pageload", objcls);

            }

            return View("login_pageload", objcls);
        }
    }
}