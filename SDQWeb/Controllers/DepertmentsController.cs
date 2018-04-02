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
    public class DepertmentsController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: Depertments
        public ActionResult Index()
        {
            var depertments = db.Depertments.Include(d => d.Address).Include(d => d.Institute);
            return View(depertments.ToList());
        }

        // GET: Depertments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depertment depertment = db.Depertments.Find(id);
            if (depertment == null)
            {
                return HttpNotFound();
            }
            return View(depertment);
        }

        // GET: Depertments/Create
        public ActionResult Create()
        {
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Id");
            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name");
            return View();
        }

        // POST: Depertments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,AddressId,InstituteId")] Depertment depertment)
        {
            if (ModelState.IsValid)
            {
                db.Depertments.Add(depertment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Id", depertment.AddressId);
            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name", depertment.InstituteId);
            return View(depertment);
        }

        // GET: Depertments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depertment depertment = db.Depertments.Find(id);
            if (depertment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Id", depertment.AddressId);
            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name", depertment.InstituteId);
            return View(depertment);
        }

        // POST: Depertments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,AddressId,InstituteId")] Depertment depertment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(depertment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressId = new SelectList(db.Addresses, "Id", "Id", depertment.AddressId);
            ViewBag.InstituteId = new SelectList(db.Institutes, "Id", "Name", depertment.InstituteId);
            return View(depertment);
        }

        // GET: Depertments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depertment depertment = db.Depertments.Find(id);
            if (depertment == null)
            {
                return HttpNotFound();
            }
            return View(depertment);
        }

        // POST: Depertments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Depertment depertment = db.Depertments.Find(id);
            db.Depertments.Remove(depertment);
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
