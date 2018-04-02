using SDQWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace SDQWeb.Controllers
{
    
    public class MainController : Controller
    {
        private SDQEntities db = new SDQEntities();
        // GET: Main
        
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {   string message = "";
            using(SDQEntities sdq = new SDQEntities())
            {
                
                var v = sdq.Users.Where(a => a.Email == login.Email).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(v.Password , login.Password)==0)
                    {
                        Session["fname"] = v.FirstName;
                        Session["lname"] = v.LastName;
                        Session["id"] = v.ID;
                        Session["image"] = v.Profile;
                        int Timeout = login.Remember ? 525600 : 1;
                        var Ticket = new FormsAuthenticationTicket(login.Email, login.Remember, Timeout);
                        string Encrypted = FormsAuthentication.Encrypt(Ticket);
                        var Cookie = new HttpCookie(FormsAuthentication.FormsCookieName, Encrypted);
                        Cookie.Expires = DateTime.Now.AddMinutes(Timeout);
                        Cookie.HttpOnly = true;
                        return RedirectToAction("Index",new RouteValueDictionary(new { Controller = "Home", Action="Index", Id = v.ID}));
                    }
                }
                else
                {
                    message = "Invalid Informations";
                }
            }
            ViewBag.Message = message;
            return View();
        }
    }
}