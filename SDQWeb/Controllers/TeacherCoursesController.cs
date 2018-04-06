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
    public class TeacherCoursesController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: TeacherCourses
        public ActionResult Index()
        {
            var teacherCourses = db.TeacherCourses.Include(t => t.Coursse).Include(t => t.Teacher);
            return View(teacherCourses.ToList());
        }

        // GET: TeacherCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherCourse teacherCourse = db.TeacherCourses.Find(id);
            if (teacherCourse == null)
            {
                return HttpNotFound();
            }
            return View(teacherCourse);
        }

        // GET: TeacherCourses/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name");
            if (ViewBag.CourseId == null)
            {
                return RedirectToAction("Create", "Coursses");
            }
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id");
            return View();
        }

        // POST: TeacherCourses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseId,TeacherId")] TeacherCourse teacherCourse)
        {
            if (ModelState.IsValid)
            {
                if (teacherCourse.CourseId >0)
                {
                    db.TeacherCourses.Add(teacherCourse);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(teacherCourse);
            }

            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", teacherCourse.CourseId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id", teacherCourse.TeacherId);
            return View(teacherCourse);
        }

        // GET: TeacherCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherCourse teacherCourse = db.TeacherCourses.Find(id);
            if (teacherCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", teacherCourse.CourseId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id", teacherCourse.TeacherId);
            return View(teacherCourse);
        }

        // POST: TeacherCourses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseId,TeacherId")] TeacherCourse teacherCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(teacherCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", teacherCourse.CourseId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "Id", "Id", teacherCourse.TeacherId);
            return View(teacherCourse);
        }

        // GET: TeacherCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TeacherCourse teacherCourse = db.TeacherCourses.Find(id);
            if (teacherCourse == null)
            {
                return HttpNotFound();
            }
            return View(teacherCourse);
        }

        // POST: TeacherCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TeacherCourse teacherCourse = db.TeacherCourses.Find(id);
            db.TeacherCourses.Remove(teacherCourse);
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
