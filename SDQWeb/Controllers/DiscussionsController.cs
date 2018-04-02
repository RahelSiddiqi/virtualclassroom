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
    public class DiscussionsController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: Discussions
        public ActionResult Index()
        {
            var discussions = db.Discussions.Include(d => d.Post).Include(d => d.User);
            return View(discussions.ToList());
        }

        // GET: Discussions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = db.Discussions.Find(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            return View(discussion);
        }

        // GET: Discussions/Create
        public ActionResult Create()
        {
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title");
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comments,UserId,PostId")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                db.Discussions.Add(discussion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", discussion.PostId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName", discussion.UserId);
            return View(discussion);
        }

        // GET: Discussions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = db.Discussions.Find(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", discussion.PostId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName", discussion.UserId);
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Comments,UserId,PostId")] Discussion discussion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discussion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostId = new SelectList(db.Posts, "Id", "Title", discussion.PostId);
            ViewBag.UserId = new SelectList(db.Users, "ID", "FirstName", discussion.UserId);
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Discussion discussion = db.Discussions.Find(id);
            if (discussion == null)
            {
                return HttpNotFound();
            }
            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Discussion discussion = db.Discussions.Find(id);
            db.Discussions.Remove(discussion);
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
