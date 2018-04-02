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
    public class PresentAddressesController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: PresentAddresses
        public ActionResult Index()
        {
            var presentAddresses = db.PresentAddresses.Include(p => p.Country).Include(p => p.Zip);
            return View(presentAddresses.ToList());
        }

        // GET: PresentAddresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentAddress presentAddress = db.PresentAddresses.Find(id);
            if (presentAddress == null)
            {
                return HttpNotFound();
            }
            return View(presentAddress);
        }

        // GET: PresentAddresses/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name");
            ViewBag.ZipCode = new SelectList(db.Zips, "ZipCode", "City");
            return View();
        }

        // POST: PresentAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryID,ZipCode")] PresentAddress presentAddress)
        {
            if (ModelState.IsValid)
            {
                db.PresentAddresses.Add(presentAddress);
                db.SaveChanges();
                Session["PresentAddId"] = presentAddress.Id;
                int id = Convert.ToInt16(Session["id"]);
                return RedirectToAction("ViewPreAdd", new { Action = "ViewPreAdd", Controller = "Users", Id = id });
            }

            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name", presentAddress.CountryID);
            ViewBag.ZipCode = new SelectList(db.Zips, "ZipCode", "City", presentAddress.ZipCode);
            return View(presentAddress);
        }

        // GET: PresentAddresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentAddress presentAddress = db.PresentAddresses.Find(id);
            if (presentAddress == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name", presentAddress.CountryID);
            ViewBag.ZipCode = new SelectList(db.Zips, "ZipCode", "City", presentAddress.ZipCode);
            return View(presentAddress);
        }

        // POST: PresentAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryID,ZipCode")] PresentAddress presentAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(presentAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Countries, "Id", "Name", presentAddress.CountryID);
            ViewBag.ZipCode = new SelectList(db.Zips, "ZipCode", "City", presentAddress.ZipCode);
            return View(presentAddress);
        }

        // GET: PresentAddresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PresentAddress presentAddress = db.PresentAddresses.Find(id);
            if (presentAddress == null)
            {
                return HttpNotFound();
            }
            return View(presentAddress);
        }

        // POST: PresentAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PresentAddress presentAddress = db.PresentAddresses.Find(id);
            db.PresentAddresses.Remove(presentAddress);
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
