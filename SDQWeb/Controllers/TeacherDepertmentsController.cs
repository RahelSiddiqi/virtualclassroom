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
    public class TeacherDepertmentsController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: TeacherDepertments
        public ActionResult Index()
        {
            var teacherDepertments = db.TeacherDepertments.Include(t => t.Depertment).Include(t => t.Teacher);
            return View(teacherDepertments.ToList());
        }

        // GET: TeacherDepertments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherDepertment teacherDepertment = db.TeacherDepertments.Find(id);
            if (teacherDepertment == null)
            {
                return HttpNotFound();
            }
            return View(teacherDepertment);
        }

        // GET: TeacherDepertments/Create
        public ActionResult Create()
        {
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id");
            return View();
        }

        // POST: TeacherDepertments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TeacherId,DepertmentId")] TeacherDepertment teacherDepertment)
        {
            if (ModelState.IsValid)
            {
                int id = Convert.ToInt32(Session["id"]);
                var stdnt = db.Teachers.Where(st => st.UserId == id).FirstOrDefault();
                if (stdnt == null)
                {
                    return RedirectToAction("Create", new { Controller = "Teachers", Action = "Create" });
                }
                db.TeacherDepertments.Add(teacherDepertment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", teacherDepertment.DepertmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id", teacherDepertment.TeacherId);
            return View(teacherDepertment);
        }

        // GET: TeacherDepertments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherDepertment teacherDepertment = db.TeacherDepertments.Find(id);
            if (teacherDepertment == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", teacherDepertment.DepertmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id", teacherDepertment.TeacherId);
            return View(teacherDepertment);
        }

        // POST: TeacherDepertments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TeacherId,DepertmentId")] TeacherDepertment teacherDepertment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherDepertment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepertmentId = new SelectList(db.Depertments, "Id", "Name", teacherDepertment.DepertmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id", teacherDepertment.TeacherId);
            return View(teacherDepertment);
        }

        // GET: TeacherDepertments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherDepertment teacherDepertment = db.TeacherDepertments.Find(id);
            if (teacherDepertment == null)
            {
                return HttpNotFound();
            }
            return View(teacherDepertment);
        }

        // POST: TeacherDepertments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherDepertment teacherDepertment = db.TeacherDepertments.Find(id);
            db.TeacherDepertments.Remove(teacherDepertment);
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
