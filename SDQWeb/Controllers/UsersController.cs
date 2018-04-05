using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SDQWeb.Models;

namespace SDQWeb.Controllers
{
    public class UsersController : Controller
    {
        private SDQEntities db = new SDQEntities();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Address).Include(u => u.PresentAddress);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            ViewBag.UserName = user.FirstName + " " + user.LastName;
            ViewBag.Image = user.Profile;
            ViewBag.Id = user.ID;
            if (user == null)
            {
                return HttpNotFound();
            }
            PresentAddress presentAddress = new PresentAddress();
            ProfileViewModel profileView = new ProfileViewModel();
            profileView.Name = user.FirstName + user.LastName;
            profileView.Email = user.Email;
            profileView.DateOfBirth = user.DateOfBirth.ToLongDateString();
            profileView.Gender = (user.Gender == 1) ? "Male" : "Female";
            profileView.Image = user.Profile;
            profileView.Mobile = user.MobileNo.ToString();
            profileView.UserId = user.ID;
            Address address = new Address();

            if (!user.AddressID.HasValue)
            {
                return RedirectToAction("Index", "Addresses");
            }
            if (!user.PresentAddressId.HasValue)
            {
                return RedirectToAction("Index", "PresentAddresses");
            }

//Profile Check And Upload

            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Id");
            ViewBag.PresentAddressId = new SelectList(db.PresentAddresses, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Email,MobileNo,DateOfBirth,Gender,Password,PresentAddressId,AddressID,Profile")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Main");
            }

            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Id", user.AddressID);
            ViewBag.PresentAddressId = new SelectList(db.PresentAddresses, "Id", "Id", user.PresentAddressId);
            return View(user);
        }

        // GET: Users/ViewAdd
        public ActionResult ViewAdd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Id");
            ViewBag.PresentAddressId = new SelectList(db.PresentAddresses, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewAdd([Bind(Include = "ID,AddressID")] User user)
        {
            if (ModelState.IsValid)
            {
               db.Users.Find(user.ID).AddressID = user.AddressID;
                db.SaveChanges();
                return RedirectToAction("Create",new { Controller="PresentAddresses", Action= "Create"});
            }
            return View();
        }
        // GET: Users/ViewPreAdd
        public ActionResult ViewPreAdd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Id");
            ViewBag.PresentAddressId = new SelectList(db.PresentAddresses, "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewPreAdd([Bind(Include = "ID,PresentAddressId")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Find(user.ID).PresentAddressId = user.PresentAddressId;
                db.SaveChanges();

            }
            return RedirectToAction("Index", new { Controller = "Home", Action = "Index", id = Convert.ToInt32(Session["id"]) });
        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Id", user.AddressID);
            ViewBag.PresentAddressId = new SelectList(db.PresentAddresses, "Id", "Id", user.PresentAddressId);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Email,MobileNo,DateOfBirth,Gender,Password,PresentAddressId,AddressID,Profile")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AddressID = new SelectList(db.Addresses, "Id", "Id", user.AddressID);
            ViewBag.PresentAddressId = new SelectList(db.PresentAddresses, "Id", "Id", user.PresentAddressId);
            return View(user);
        }

        //GET Users/UploadProfile/id
        public ActionResult UploadProfile(int id)
        {
            if (id <= 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult UploadProfile([Bind(Include = "ID,FirstName,LastName,Email,MobileNo,DateOfBirth,Gender,Password,PresentAddressId,AddressID,Profile")] User user)
        {
            var path = "";
            HttpPostedFileBase file = Request.Files["Profile"];
            if (file != null)
            {
                if (file.ContentLength > 0)
                {
                    string ext = Path.GetExtension(file.FileName).ToLower();
                    if ( ext.Contains("jpg") || ext.Contains("jpeg") || ext.Contains("gif") || ext.Contains("png"))
                    {
                        if (ModelState.IsValid)
                        {
                            db.Users.Find(user.ID).Profile = Path.GetFileName(file.FileName);
                            db.SaveChanges();
                        }
                        else
                        {
                            ViewBag.img = "{Profile Uploaded Failed";
                            return View(user);
                        }
                        path = Path.Combine(Server.MapPath("~/Image/"),file.FileName);
                        file.SaveAs(path);
                        ViewBag.img = "Profile Uploaded Successfully! will Change Next Login";
                        return View(user);
                    }
                }
            }
            return View();
        }
        // GET: Users/Delete/5
        [NonAction]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
