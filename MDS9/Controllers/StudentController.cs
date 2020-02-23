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
    public class StudentController : Controller
    {
        private MDSDbContext db = new MDSDbContext();

        //
        // GET: /Student/

        public ActionResult UserRestrict()
        {
            return View();
        }

        public ActionResult Index()
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

        //
        // GET: /Student/Details/5

        public ActionResult Details(int id = 0)
        {
            Student student = db.s1.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            if (User.Identity.Name == "Administrator")
            {
                ViewBag.UserId = new SelectList(db.UserProfiles.Where(x=>x.UserName!="Administrator"), "UserId", "FName");
                return View();
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (User.Identity.Name == "Administrator")
            {
                if (ModelState.IsValid)
                {
                    db.s1.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UserId = new SelectList(db.UserProfiles.Where(x => x.UserName != "Administrator"), "UserId", "FName", student.UserId);
                return View(student);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // GET: /Student/Edit/5

        public ActionResult Edit(int id = 0)
        {
            if (User.Identity.Name == "Administrator")
            {
                Student student = db.s1.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", student.UserId);
                return View(student);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (User.Identity.Name == "Administrator")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(student).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", student.UserId);
                return View(student);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // GET: /Student/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (User.Identity.Name == "Administrator")
            {
                Student student = db.s1.Find(id);
                if (student == null)
                {
                    return HttpNotFound();
                }
                return View(student);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.Name == "Administrator")
            {
                Student student = db.s1.Find(id);
                db.s1.Remove(student);
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