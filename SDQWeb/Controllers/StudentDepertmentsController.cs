using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SDQWeb.Models;

namespace SDQWeb.Controllers
{
    public class StudentDepertmentsController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: StudentDepertments
        public ActionResult Index()
        {
            var studentDepertments = db.StudentDepertments.Include(s => s.Depertment).Include(s => s.Student);
            return View(studentDepertments.ToList());
        }

        // GET: StudentDepertments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDepertment studentDepertment = db.StudentDepertments.Find(id);
            if (studentDepertment == null)
            {
                return HttpNotFound();
            }
            return View(studentDepertment);
        }

        // GET: StudentDepertments/Create
        public ActionResult Create()
        {
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name");
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id");
            return View();
        }

        // POST: StudentDepertments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DepertmentId,StudentId")] StudentDepertment studentDepertment)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["id"]);
                var stdnt = db.Students.Where(st => st.UserId == id).FirstOrDefault();
                if (stdnt == null)
                {
                    return RedirectToAction("Create", new { Controller = "Students", Action = "Create" });
                }
                studentDepertment.StudentId = stdnt.Id;
                db.StudentDepertments.Add(studentDepertment);
                db.SaveChanges();
                return RedirectToAction("Index","Users");
            }

            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", studentDepertment.DepertmentId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id", studentDepertment.StudentId);
            return View(studentDepertment);
        }

        // GET: StudentDepertments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDepertment studentDepertment = db.StudentDepertments.Find(id);
            if (studentDepertment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", studentDepertment.DepertmentId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id", studentDepertment.StudentId);
            return View(studentDepertment);
        }

        // POST: StudentDepertments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DepertmentId,StudentId")] StudentDepertment studentDepertment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentDepertment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", studentDepertment.DepertmentId);
            ViewBag.StudentId = new SelectList(db.Students, "Id", "Id", studentDepertment.StudentId);
            return View(studentDepertment);
        }

        // GET: StudentDepertments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDepertment studentDepertment = db.StudentDepertments.Find(id);
            if (studentDepertment == null)
            {
                return HttpNotFound();
            }
            return View(studentDepertment);
        }

        // POST: StudentDepertments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentDepertment studentDepertment = db.StudentDepertments.Find(id);
            db.StudentDepertments.Remove(studentDepertment);
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
