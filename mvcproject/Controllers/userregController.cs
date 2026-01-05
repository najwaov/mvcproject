using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcproject.Models;

namespace mvcproject.Controllers
{
    public class userregController : Controller
    {
        mvcprojectEntities1 dbobj = new mvcprojectEntities1();
        // GET: userreg
        public ActionResult insertuser_page_load()
        {
            //checkboxlist
            userinsert user = new userinsert();
            user.mskill = getskills();
            return View(user);

        }
        public List<checkboxlisthelper> getskills()
        {
            List<checkboxlisthelper> sts = new List<checkboxlisthelper>()
    {
        new checkboxlisthelper { value = "C#", text = "C#", ischecked = true },
        new checkboxlisthelper { value = "C", text = "C", ischecked = false },
        new checkboxlisthelper { value = "JAVA", text = "JAVA", ischecked = false },
        new checkboxlisthelper { value = "ASP.NET", text = "ASP.NET", ischecked = false },
        new checkboxlisthelper { value = "ASP.NET CORE", text = "ASP.NET CORE", ischecked = false }

    };

            return sts;
        }
        public ActionResult insertuser_click(userinsert clsobj)
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
                var skid = string.Join(",", clsobj.sskill);
                clsobj.skill = skid;
                clsobj.mskill = getskills();
                dbobj.sp_user_reg(regid, clsobj.name, clsobj.age, clsobj.address, clsobj.uphone, clsobj.skill, clsobj.yoe, clsobj.qua, clsobj.email, clsobj.gender);
                dbobj.sp_logininsert(regid, clsobj.username, clsobj.pass, "user");
                clsobj.usermsg = "sucessfully registered";
                return View("insertuser_page_load", clsobj);

            }
            else
            {
              
                clsobj.mskill = getskills();
                return View("insertuser_page_load", clsobj);
            }
        }
    }
}