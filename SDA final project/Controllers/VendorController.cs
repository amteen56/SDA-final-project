using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDA_final_project.Models;
using SDA_final_project.Miscellaneous;
//using SDA_final_project.View_Modals;
using WebMatrix.WebData;
using System.Web.Security;

namespace SDA_final_project.Controllers
{
    public class VendorController : Controller
    {
        // Habib habib = new Habib();

        //use this code for every....
       Habib habib = new Habib();
        // GET: Vendor
        public ActionResult Index()
        {

            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Equals("headOffice"))
                {
                    var Vendnor_List = (from a in habib.Vendors select a).ToList();
                    ViewBag.Vendor = "active";
                    ViewBag.vlist = "active";
                    ViewBag.vlistDisplay = "block";
                    return View(Vendnor_List);
                }
                else
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Outlet", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Create()
        {

            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Equals("headOffice"))
                {
                    ViewBag.vcreate = "active"; ViewBag.Vendor = "active";
                    ViewBag.vlistDisplay = "block";
                    return View();
                }
                else
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Outlet", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Edit(int id)
        {
            Vendor model = (from a in habib.Vendors where a.vendor_Id == id select a).SingleOrDefault();
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendor model)
        {
            if (ModelState.IsValid)
            {
                model.DeleteStatus = 0;
                habib.Vendors.Add(model);
                habib.SaveChanges();
                return RedirectToAction("Index", "Vendor");
            }
            else
                return View();
        }
        public ActionResult Delete(int id)
        {
            var obj = habib.Vendors.Single(c => c.vendor_Id == id);
            obj.DeleteStatus = 1;
            return RedirectToAction("Index", "Vendor");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVendor(Vendor model)
        {
            var obj = habib.Vendors.Single(c => c.vendor_Id == model.vendor_Id);
            obj.vendorName = model.vendorName;
            obj.vendorCompany = model.vendorCompany;
            obj.vendorContactNo = model.vendorContactNo;
            obj.vendorAddress = model.vendorAddress;

            habib.SaveChanges();
            return RedirectToAction("Index", "Vendor");
        }
        public ActionResult VendorOrderList()
        {


            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Equals("headOffice"))
                {
                    List<VendorOrder> vo = habib.VendorOrders.ToList();
                    ViewBag.Orders = "active";
                    ViewBag.orderDisplay = "block";
                    ViewBag.orderList = "active";
                    return View(vo);
                }
                else
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Outlet", "Home");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult VendorOrderDetails(int id)
        {
            List<VendorOrder_ShoeSizeColor> vo = habib.VendorOrder_ShoeSizeColor.Where(c => c.vendorOrder_Id == id).ToList();

            return View(vo);
        }

    }
}