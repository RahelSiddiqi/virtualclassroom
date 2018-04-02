using SDQWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDQWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int id)
        {
            int tid = Convert.ToInt32(Session["TeacherId"]);
            if (tid > 0)
            {
                return RedirectToAction("Index", new { controller = "TeacherCourses", action = "Index", id });
            }
            var stdnt = new SDQWeb.Models.SDQEntities().Students.Where(a => a.UserId == id).FirstOrDefault();
            if (stdnt == null)
            {
                return RedirectToAction("Create", new { controller = "Students", action = "Create",id });
            }
            Home home = new Home();
            
            using(SDQEntities sdq = new SDQEntities())
            {
                var v = sdq.Users.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    ViewBag.UserName = v.FirstName + " "+v.LastName;
                    ViewBag.Image = v.Profile;
                    ViewBag.Id = v.ID;
                }
            }

         
            return View(home);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Create()
        {
            return RedirectToAction("Create", new { controller = "UserInstitutes", action = "Index" });
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}