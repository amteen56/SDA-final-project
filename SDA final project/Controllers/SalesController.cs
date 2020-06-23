using SDA_final_project.View_Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SDA_final_project.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Sales()
        {

            if (WebSecurity.IsAuthenticated)
            {

                    ViewBag.sales = "active";
                    return View(Miscellaneous.HabibDataClass.Habib.CustomerOrders.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}