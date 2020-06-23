using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDA_final_project.Models;

namespace SDA_final_project.Controllers
{
    public class PaymentController : Controller
    {
        Habib habib = new Habib();
        // GET: Payment
        public ActionResult ViewPayment()
        {
            List<VendorPayment> payments = habib.VendorPayments.ToList();
            return View(payments);
        }
    }
}