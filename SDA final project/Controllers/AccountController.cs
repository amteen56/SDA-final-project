using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace SDA_final_project.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string uName = form["UserName"];
            string pass = form["Password"];

           
                if(WebSecurity.Login(uName, pass))
                {

                return RedirectToAction("Authentication", "Account");
                    
                }
                else
                {
                    ViewBag.error = "Wrong username or password";
                }
            
           

            return View();
        }

        public ActionResult Authentication()
        {

            if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Equals("headOffice"))
            {
                return RedirectToAction("Index", "Home");
            }else if(Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Contains("outlet"))
            {
                return RedirectToAction("Outlet", "Home");
            }


            

            return View();
        }

        public ActionResult Logout()
        {
            WebSecurity.Logout();

            return View("Login");
        }
    }
}