using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SDA_final_project.Models;
using SDA_final_project.Miscellaneous;
using SDA_final_project.View_Modals;
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

        [HttpGet]
        public ActionResult CreateOrder()
        {

            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Equals("headOffice"))
                {
                    VendorOrderViewModel vendor_order = new VendorOrderViewModel();

                    vendor_order.shoesList = (from a in habib.Shoes
                                              select a).ToList();

                    vendor_order.colorsList = (from a in habib.Colors
                                               select a).ToList();

                    vendor_order.sizesList = (from a in habib.Sizes
                                              select a).ToList();

                    vendor_order.vendorsList = (from a in habib.Vendors
                                                select a).ToList();

                    ViewBag.Orders = "active";
                    ViewBag.orderDisplay = "block";
                    ViewBag.createOrder = "active";

                    return View(vendor_order);
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

        [HttpPost]
        public ActionResult CreateOrder(VendorOrderViewModel model, FormCollection form)
        {
            vendorOrderDataHandler handler = new vendorOrderDataHandler();
            string date = form["dateOfOrder"];

            int vendorId = Convert.ToInt32(form["venID"]);

            int totalAmount = Convert.ToInt32(form["total"]);

            string payMode = form["mode"];

            string paymentMethod = form["payment_method"];

            handler.addNewShoes(model.shoesToAdd, model.shoesCost, model.varieties, Convert.ToDateTime(date));
            handler.addNewSizes(model.sizesToAdd);
            handler.addNewColors(model.colors);
            handler.addShoeSize(model.shoes, model.sizes);
            handler.addShoeSizeColor(model.shoes, model.sizes, model.colors, model.qty);


            if (payMode == "1")
            {

                VendorOrder order = new VendorOrder()
                {
                    vendor_Id = vendorId,
                    dateOfOrder = Convert.ToDateTime(date),
                    paymentMode = paymentMethod,
                    paymentStatus = "Paid",
                    totalAmount = totalAmount,
                    amountRemaining = 0,
                    totalQty = handler.getTotalQuantity(model.qty),
                    paymentType = "Full"

                };
                habib.VendorOrders.Add(order);
                habib.SaveChanges();

                handler.addVendorOrderListDetails(order.vendorOrder_Id, model.shoes, model.sizes, model.colors, model.qty);

                handler.addPayment(order.vendorOrder_Id, totalAmount, Convert.ToDateTime(date));
            }
            else if (payMode == "2")
            {
                VendorOrder order = new VendorOrder()
                {
                    vendor_Id = vendorId,
                    dateOfOrder = Convert.ToDateTime(date),
                    paymentMode = paymentMethod,
                    paymentStatus = "Partial Paid",
                    totalAmount = totalAmount,
                    amountRemaining = totalAmount,
                    totalQty = handler.getTotalQuantity(model.qty),
                    paymentType = "Partial"
                };
                habib.VendorOrders.Add(order);
                habib.SaveChanges();

                handler.addVendorOrderListDetails(order.vendorOrder_Id, model.shoes, model.sizes, model.colors, model.qty);

                handler.addPartialPayments(order.vendorOrder_Id, model.remainings);
            }

            return RedirectToAction("CreateOrder", "Vendor");
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