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
    public class OrderController : Controller
    {
        private MDSDbContext db = new MDSDbContext();

        //
        // GET: /Order/

        public ActionResult AdminRestrict()
        {
            return View();
        }
        public ActionResult MealList()
        {
            return View(db.m1.ToList());
        }
        public ActionResult Index()
        {
            if (User.Identity.Name != "Administrator")
            {
                var o1 = db.o1.Include(o => o.ups).Include(o => o.s5).Include(o => o.m4);
                return View(o1.ToList());
            }
            else
            {
                return RedirectToAction("AdminRestrict");
            }
        }

        //
        // GET: /Order/Details/5

        public ActionResult Details(int id = 0)
        {
            OrderDetails orderdetails = db.o1.Find(id);
            if (orderdetails == null)
            {
                return HttpNotFound();
            }
            return View(orderdetails);
        }

        //
        // GET: /Order/Create

        public ActionResult Create()
        {
            if (User.Identity.Name != "Administrator")
            {
                ViewBag.UserId = new SelectList(db.UserProfiles.Where(x=>x.UserName==User.Identity.Name), "UserId", "FName");
                ViewBag.StudentId = new SelectList(db.s1.Where(x => x.ups.UserName == User.Identity.Name), "StudentId", "Fname");
                ViewBag.MealId = new SelectList(db.m1, "MealId", "Name");
                ViewBag.TotalAmount = new SelectList(db.m1, "Price", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("AdminRestrict");
            }
        }

        //
        // POST: /Order/Create

        [HttpPost]
        public ActionResult Create(OrderDetails orderdetails)
        {
            if (User.Identity.Name != "Administrator")
            {
                if (ModelState.IsValid)
                {
                    db.o1.Add(orderdetails);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", orderdetails.UserId);
                ViewBag.StudentId = new SelectList(db.s1, "StudentId", "Fname", orderdetails.StudentId);
                ViewBag.MealId = new SelectList(db.m1, "MealId", "Name", orderdetails.MealId);
                return View(orderdetails);
            }
            else
            {
                return RedirectToAction("AdminRestrict");
            }
        }

        //
        // GET: /Order/Edit/5

        public ActionResult Edit(int id = 0)
        {
            OrderDetails orderdetails = db.o1.Find(id);
            if (orderdetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", orderdetails.UserId);
            ViewBag.StudentId = new SelectList(db.s1, "StudentId", "Fname", orderdetails.StudentId);
            ViewBag.MealId = new SelectList(db.m1, "MealId", "Name", orderdetails.MealId);
            return View(orderdetails);
        }

        //
        // POST: /Order/Edit/5

        [HttpPost]
        public ActionResult Edit(OrderDetails orderdetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderdetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", orderdetails.UserId);
            ViewBag.StudentId = new SelectList(db.s1, "StudentId", "Fname", orderdetails.StudentId);
            ViewBag.MealId = new SelectList(db.m1, "MealId", "Name", orderdetails.MealId);
            return View(orderdetails);
        }

        //
        // GET: /Order/Delete/5

        public ActionResult Delete(int id = 0)
        {
            OrderDetails orderdetails = db.o1.Find(id);
            if (orderdetails == null)
            {
                return HttpNotFound();
            }
            return View(orderdetails);
        }

        //
        // POST: /Order/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderDetails orderdetails = db.o1.Find(id);
            db.o1.Remove(orderdetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}