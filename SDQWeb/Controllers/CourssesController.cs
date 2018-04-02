using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SDQWeb.Models;

namespace SDQWeb.Controllers
{
    public class CourssesController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: Coursses
        public ActionResult Index(int? id)
        {
            using (SDQEntities sdq = new SDQEntities())
            {
                int userid = Convert.ToInt32(Session["id"]);
                var tcr = db.Teachers.Where(t => t.UserId == userid).FirstOrDefault();
                if (tcr != null)
                {
                    Session["TeacherId"] = tcr.Id;
                    Session["TeacherUserId"] = tcr.UserId;
                    Session["TeacherDept"] = tcr.TeacherDepertments;
                    Session["TeacherCourses"] = tcr.TeacherCourses;
                    Session["TeacherUser"] = tcr.User;
                    return RedirectToAction("Index", new { controller = "Home", action = "Index" , id = tcr.UserId});
                }
                var std = db.Students.Where(s => s.UserId == userid).FirstOrDefault();
                if (std != null)
                {
                    Session["StudedntId"] = std.Id;
                    Session["StudentuserId"] = std.UserId;
                    Session["StudentDept"] = std.StudentDepertments;
                    Session["StudentCourses"] = std.StudentCourses;
                    Session["StudentUser"] = std.User;
                    return RedirectToAction("Index", "Sessions");
                }
                var stu = sdq.Students.Where(a => a.UserId == id).FirstOrDefault();
            }
            
            var coursses = db.Coursses.Include(c => c.Depertment);
            return View(coursses.ToList());
        }

        // GET: Coursses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coursse coursse = db.Coursses.Find(id);
            if (coursse == null)
            {
                return HttpNotFound();
            }
            return View(coursse);
        }

        // GET: Coursses/Create
        public ActionResult Create()
        {
            int userid = Convert.ToInt32(Session["id"]);
            var tcr = db.Teachers.Where(t => t.UserId == userid).FirstOrDefault();
            if(tcr != null)
            {
                 if (tcr.TeacherDepertments.Count() == 0)
                 {
                    return RedirectToAction("Create", new { controller = "TeacherDepertments", action = "Create", id = tcr.UserId });
                 }
            }
            
            var std = db.Students.Where(t => t.UserId == userid).FirstOrDefault();
            if (std.StudentDepertments.Count() == 0)
            {
                return RedirectToAction("Create", new { controller = "StudentDepertments", action = "Create", id = std.UserId });
            }
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name");
            return View();
        }

        // POST: Coursses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Cradit,Description,DepertmentId")] Coursse coursse)
        {
            if (ModelState.IsValid)
            {
                db.Coursses.Add(coursse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", coursse.DepertmentId);
            return View(coursse);
        }

        // GET: Coursses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coursse coursse = db.Coursses.Find(id);
            if (coursse == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", coursse.DepertmentId);
            return View(coursse);
        }

        // POST: Coursses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Cradit,Description,DepertmentId")] Coursse coursse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", coursse.DepertmentId);
            return View(coursse);
        }

        // GET: Coursses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coursse coursse = db.Coursses.Find(id);
            if (coursse == null)
            {
                return HttpNotFound();
            }
            return View(coursse);
        }

        // POST: Coursses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coursse coursse = db.Coursses.Find(id);
            db.Coursses.Remove(coursse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
