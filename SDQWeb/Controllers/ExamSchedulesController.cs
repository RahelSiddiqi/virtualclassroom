﻿using System;
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
    public class ExamSchedulesController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: ExamSchedules
        public ActionResult Index()
        {
            var examSchedules = db.ExamSchedules.Include(e => e.Coursse);
            return View(examSchedules.ToList());
        }

        // GET: ExamSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            if (examSchedule == null)
            {
                return HttpNotFound();
            }
            return View(examSchedule);
        }

        // GET: ExamSchedules/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name");
            return View();
        }

        // POST: ExamSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Date,CourseId")] ExamSchedule examSchedule)
        {
            if (ModelState.IsValid)
            {
                db.ExamSchedules.Add(examSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", examSchedule.CourseId);
            return View(examSchedule);
        }

        // GET: ExamSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            if (examSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", examSchedule.CourseId);
            return View(examSchedule);
        }

        // POST: ExamSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Date,CourseId")] ExamSchedule examSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(examSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Coursses, "Id", "Name", examSchedule.CourseId);
            return View(examSchedule);
        }

        // GET: ExamSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            if (examSchedule == null)
            {
                return HttpNotFound();
            }
            return View(examSchedule);
        }

        // POST: ExamSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ExamSchedule examSchedule = db.ExamSchedules.Find(id);
            db.ExamSchedules.Remove(examSchedule);
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
