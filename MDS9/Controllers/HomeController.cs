using MDS9.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MDS9.Models;

namespace MDS9.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private MDSDbContext db = new MDSDbContext();
        public ActionResult Index()
        {
            if (User.Identity.Name == "Administrator")
            {
                return RedirectToAction("AdminIndex");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult About()
        {
            if (User.Identity.Name == "Administrator")
            {
                return RedirectToAction("AdminAbout");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AdminAbout()
        {
            return View();
        }

        public ActionResult Contact()
        {
            if (User.Identity.Name == "Administrator")
            {
                return RedirectToAction("AdminContact");
            }
            else
            {
                return View();
            }
        }

        public ActionResult AdminContact()
        {
            return View();
        }
    }
}
