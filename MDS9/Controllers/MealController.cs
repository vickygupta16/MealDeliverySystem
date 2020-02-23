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
    public class MealController : Controller
    {
        private MDSDbContext db = new MDSDbContext();

        //
        // GET: /Meal/
        public ActionResult UserRestrict()
        {
            return View();
        }

        public ActionResult Index()
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

        //
        // GET: /Meal/Details/5

        public ActionResult Details(int id = 0)
        {
            Meal meal = db.m1.Find(id);
            if (meal == null)
            {
                return HttpNotFound();
            }
            return View(meal);
        }

        //
        // GET: /Meal/Create

        public ActionResult Create()
        {
            if (User.Identity.Name == "Administrator")
            {
                return View();
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Meal/Create

        [HttpPost]
        public ActionResult Create(Meal meal)
        {
            if (User.Identity.Name == "Administrator")
            {
                if (ModelState.IsValid)
                {
                    db.m1.Add(meal);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(meal);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // GET: /Meal/Edit/5

        public ActionResult Edit(int id = 0)
        {
            if (User.Identity.Name == "Administrator")
            {
                Meal meal = db.m1.Find(id);
                if (meal == null)
                {
                    return HttpNotFound();
                }
                return View(meal);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Meal/Edit/5

        [HttpPost]
        public ActionResult Edit(Meal meal)
        {
            if (User.Identity.Name == "Administrator")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(meal).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(meal);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // GET: /Meal/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (User.Identity.Name == "Administrator")
            {
                Meal meal = db.m1.Find(id);
                if (meal == null)
                {
                    return HttpNotFound();
                }
                return View(meal);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Meal/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.Name == "Administrator")
            {
                Meal meal = db.m1.Find(id);
                db.m1.Remove(meal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}