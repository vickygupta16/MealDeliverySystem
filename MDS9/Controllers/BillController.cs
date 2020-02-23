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
    public class BillController : Controller
    {
        private MDSDbContext db = new MDSDbContext();

        //
        // GET: /Bill/
        public ActionResult BillList()
        {
            /*var billlist = from stuobj in db.s1.ToList()
                           join ordobj in db.o1.ToList() on stuobj.StudentId equals ordobj.StudentId
                           into table1
                           from ordobj in table1.ToList()
                           select new ViewModel
                           {
                               stuobjm=stuobj,
                               ordobjm=ordobj,
                           };*/
            var billlist = from ordobj in db.o1.ToList()
                           group ordobj by ordobj.StudentId
                               into btable
                               select new
                               {
                                   SID = btable.Key,
                                   UBill = btable.Sum(c => c.TotalAmount)
                               };
            ViewBag.MyBill = billlist;
            if (User.Identity.Name == "Administrator")
            {
                return View();
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

        public ActionResult Index()
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

        //
        // GET: /Bill/Details/5

        public ActionResult Details(int id = 0)
        {
            Bill bill = db.b1.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View(bill);
        }

        //
        // GET: /Bill/Create

        public ActionResult Create()
        {
            if (User.Identity.Name == "Administrator")
            {
                ViewBag.UserId = new SelectList(db.UserProfiles.Where(x=>x.UserName!="Administrator"), "UserId", "Username");
                ViewBag.StudentId = new SelectList(db.s1, "StudentId", "StudentId");
                return View();
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Bill/Create

        [HttpPost]
        public ActionResult Create(Bill bill)
        {
            if (User.Identity.Name == "Administrator")
            {
                if (ModelState.IsValid)
                {
                    db.b1.Add(bill);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.UserId = new SelectList(db.UserProfiles.Where(x => x.UserName != "Administrator"), "UserId", "UserName", bill.UserId);
                ViewBag.StudentId = new SelectList(db.s1, "StudentId", "Fname", bill.StudentId);
                return View(bill);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // GET: /Bill/Edit/5

        public ActionResult Edit(int id = 0)
        {
            if (User.Identity.Name == "Administrator")
            {
                Bill bill = db.b1.Find(id);
                if (bill == null)
                {
                    return HttpNotFound();
                }
                ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bill.UserId);
                ViewBag.StudentId = new SelectList(db.s1, "StudentId", "Fname", bill.StudentId);
                return View(bill);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Bill/Edit/5

        [HttpPost]
        public ActionResult Edit(Bill bill)
        {
            if (User.Identity.Name == "Administrator")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(bill).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.UserId = new SelectList(db.UserProfiles, "UserId", "UserName", bill.UserId);
                ViewBag.StudentId = new SelectList(db.s1, "StudentId", "Fname", bill.StudentId);
                return View(bill);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // GET: /Bill/Delete/5

        public ActionResult Delete(int id = 0)
        {
            if (User.Identity.Name == "Administrator")
            {
                Bill bill = db.b1.Find(id);
                if (bill == null)
                {
                    return HttpNotFound();
                }
                return View(bill);
            }
            else
            {
                return RedirectToAction("UserRestrict");
            }
        }

        //
        // POST: /Bill/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (User.Identity.Name == "Administrator")
            {
                Bill bill = db.b1.Find(id);
                db.b1.Remove(bill);
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