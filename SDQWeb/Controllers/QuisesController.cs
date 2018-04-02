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
    public class QuisesController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: Quises
        public ActionResult Index()
        {
            return View(db.Quises.ToList());
        }

        // GET: Quises/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quise quise = db.Quises.Find(id);
            if (quise == null)
            {
                return HttpNotFound();
            }
            return View(quise);
        }

        // GET: Quises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Question,Answer")] Quise quise)
        {
            if (ModelState.IsValid)
            {
                db.Quises.Add(quise);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quise);
        }

        // GET: Quises/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quise quise = db.Quises.Find(id);
            if (quise == null)
            {
                return HttpNotFound();
            }
            return View(quise);
        }

        // POST: Quises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Question,Answer")] Quise quise)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quise).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quise);
        }

        // GET: Quises/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quise quise = db.Quises.Find(id);
            if (quise == null)
            {
                return HttpNotFound();
            }
            return View(quise);
        }

        // POST: Quises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quise quise = db.Quises.Find(id);
            db.Quises.Remove(quise);
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
