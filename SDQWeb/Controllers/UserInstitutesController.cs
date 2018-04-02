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
    public class UserInstitutesController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: UserInstitutes
        public ActionResult Index()
        {
            var userInstitutes = db.UserInstitutes.Include(u => u.Institute).Include(u => u.User);
            return View(userInstitutes.ToList());
        }

        // GET: UserInstitutes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInstitute userInstitute = db.UserInstitutes.Find(id);
            if (userInstitute == null)
            {
                return HttpNotFound();
            }
            return View(userInstitute);
        }

        // GET: UserInstitutes/Create
        public ActionResult Create()
        {
            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: UserInstitutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInstitute userInstitute)
        {
            userInstitute.UserId = Convert.ToInt32(Session["id"]);
            if (ModelState.IsValid)
            {
                db.UserInstitutes.Add(userInstitute);
                if (db.UserInstitutes.Where(ui => ui.UserId == userInstitute.UserId).FirstOrDefault() == null)
                    return RedirectToAction("Details", new { Controller = "Users", Action = "Details", userInstitute.Id });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name", userInstitute.InstituteId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName", userInstitute.UserId);
            return View(userInstitute);
        }

        // GET: UserInstitutes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInstitute userInstitute = db.UserInstitutes.Find(id);
            if (userInstitute == null)
            {
                return HttpNotFound();
            }
            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name", userInstitute.InstituteId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName", userInstitute.UserId);
            return View(userInstitute);
        }

        // POST: UserInstitutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,InstituteId")] UserInstitute userInstitute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userInstitute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name", userInstitute.InstituteId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName", userInstitute.UserId);
            return View(userInstitute);
        }

        // GET: UserInstitutes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserInstitute userInstitute = db.UserInstitutes.Find(id);
            if (userInstitute == null)
            {
                return HttpNotFound();
            }
            return View(userInstitute);
        }

        // POST: UserInstitutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserInstitute userInstitute = db.UserInstitutes.Find(id);
            db.UserInstitutes.Remove(userInstitute);
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
