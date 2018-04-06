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
    public class SessionsController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: Sessions
        public ActionResult Index( )
        {
            int tid = Convert.ToInt32(Session["TeacherId"]);
            if (tid > 0)
            {
                var sessionst = db.Sessions.Where(s => s.CourseId == s.Coursse.Id).Include(s => s.Material);
                return View(sessionst.ToList());
            }
            int stdid = Convert.ToInt32(Session["StudedntId"]);
            var stucourse = db.StudentCourses.Where(sc => sc.StudentId == stdid).FirstOrDefault();
            if (stucourse == null)
            {
                return RedirectToAction("Create", "StudentCourses");
            }
           var sessions = db.Sessions.Where(s => s.CourseId == s.Coursse.Id).Include(s => s.Material);
           return View(sessions.ToList());
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Sessions/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name");
            ViewBag.MaterialsId = new SelectList(db.Materials, "ID", "Notes");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Video,MaterialsId,Date,Time,QuiseId,CourseId")] Session session,int id)
        {
            if (ModelState.IsValid)
            {
                session.CourseId = id;
                session.Coursse_Id = id;
                session.Material_id = session.MaterialsId;
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", session.CourseId);
            ViewBag.MaterialsId = new SelectList(db.Materials, "ID", "LectureSlide", session.MaterialsId);
            return View(session);
        }

        // GET: Sessions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", session.CourseId);
            ViewBag.MaterialsId = new SelectList(db.Materials, "ID", "LectureSlide", session.MaterialsId);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Video,MaterialsId,Date,Time,QuiseId,CourseId")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", session.CourseId);
            ViewBag.MaterialsId = new SelectList(db.Materials, "ID", "LectureSlide", session.MaterialsId);
            return View(session);
        }

        // GET: Sessions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
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
