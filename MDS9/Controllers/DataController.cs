using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDS9.Models;
using MDS9.DatabaseContext;

namespace MDS9.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private MDSDbContext db = new MDSDbContext();

        //
        // GET: /Data/

        public ActionResult Index()
        {
            if (User.Identity.Name == "Administrator")
            {
                var o1 = db.o1.Include(o => o.ups).Include(o => o.s5).Include(o => o.m4);
                return View(o1.ToList());
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        public ActionResult BillData()
        {
            if (User.Identity.Name == "Administrator")
            {
                var b1 = db.b1.Include(b => b.ups).Include(b => b.s7);
                return View(b1.ToList());
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        public ActionResult StudentData()
        {
            if (User.Identity.Name == "Administrator")
            {
                var s1 = db.s1.Include(s => s.ups);
                return View(s1.ToList());
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        public ActionResult MealData()
        {
            if (User.Identity.Name == "Administrator")
            {
                return View(db.m1.ToList());
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        public ActionResult ParentData()
        {
            if (User.Identity.Name == "Administrator")
            {
                return View(db.UserProfiles.ToList().Where(x=>x.UserName!="Administrator"));
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        public ActionResult UserRestrict()
        {
            return View();
        }
    }
}