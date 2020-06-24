using System;
using System.Collections.Generic;
using System.Linq;
using SDA_final_project.View_Modals;
using System.Web;
using SDA_final_project.Models;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Web.Security;

namespace SDA_final_project.Controllers
{
    public class PointOfSaleController : Controller
    {
        // GET: PointOfSale
        public ActionResult Index()
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Contains("outlet"))
                {
                    ViewBag.PointOfSale = "active";

                    posindex posindex = new posindex();
                    posindex.Shoeslist =  Miscellaneous.HabibDataClass.Habib.Shoes.ToList();
                    posindex.Employeeids = Miscellaneous.HabibDataClass.Habib.Employees.ToList();
                    return View(posindex);
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

        public string getSizes(string id)
        {
            string result = "";

            List<Models.Shoe_Size> sizes = Miscellaneous.HabibDataClass.Habib.Shoe_Size.Where(c => c.Shoe.shoeArticle == id).ToList();

            foreach (Models.Shoe_Size x in sizes)
            {
                result += "<option>" + x.Size.sizeNo + "</option>";
            }

            return result;
        }

        public string getColors(string id, int sizeno)
        {
            string result = "";

            List<Models.Shoe_Size_Color> sizes = Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(c => c.Shoe_Size.Shoe.shoeArticle.Equals(id) && c.Shoe_Size.Size.sizeNo == sizeno).ToList();

            foreach (Models.Shoe_Size_Color x in sizes)
            {
                result += "<option>" + x.Color.colorName + "</option>";
            }

            return result;
        }

        public int getPrice(string id)
        {
            int a = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.Shoes.Where(c => c.shoeArticle.Equals(id)).First().shoePrice);
            return a;
        }


        public int getstausamount(string a, int s, string c)
        {
            int Temp = Convert.ToInt32(s);
            string Temp1 = Convert.ToString(WebSecurity.CurrentUserName);
            int color = Miscellaneous.HabibDataClass.Habib.Colors.Where(cc => cc.colorName.Equals(c)).FirstOrDefault().color_Id;
            int shoesid = Miscellaneous.HabibDataClass.Habib.Shoes.Where(cc => cc.shoeArticle.Equals(a)).FirstOrDefault().shoe_Id;
            int sizeid = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.Sizes.Where(cc => cc.sizeNo == (Temp)).FirstOrDefault().size_Id);
            int shoesizeid = Miscellaneous.HabibDataClass.Habib.Shoe_Size.Where(cc => cc.shoe_Id == shoesid && cc.size_Id == sizeid).FirstOrDefault().shoeSize_Id;
            int shoesizecolorid = Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(cc => cc.shoeSize_Id == shoesizeid && cc.color_Id == color).FirstOrDefault().shoeSizeColor_Id;
            int outletid = Miscellaneous.HabibDataClass.Habib.Outlets.Where(cc => cc.outletName.Equals(Temp1)).FirstOrDefault().outlet_Id;

            int qty = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.Outlet_ShoeSizeColor.Where(cc => cc.shoeSizeColor_Id == shoesizecolorid && cc.outlet_Id == outletid).First().quantity);
            return qty;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomOrder(CustomrOrder model, FormCollection form)
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Contains("outlet"))
                {
                    if (ModelState.IsValid)
                    {
                        model.deliveryDate = Convert.ToDateTime(form["dateofdelivery"]).Date;
                        model.orderStatus = "InProcess";
                        model.outletName = WebSecurity.CurrentUserName;
                        Miscellaneous.HabibDataClass.Habib.CustomrOrders.Add(model);
                        Miscellaneous.HabibDataClass.Habib.SaveChanges();
                        return RedirectToAction("Index", "PointOfSale");
                    }
                    else
                        return View();
                }
                else
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        [HttpGet]
        public ActionResult checkout(string[] articles, string[] sizes, string[] colors, string[] quantity, string total, int employeeid, int discount, string cname)
        {
            Models.CustomerOrder customerOrder = new Models.CustomerOrder()
            {
                customerName = cname,
                dateOfOrder = DateTime.Now,
                discount = discount,
                employee_Id = employeeid,
                finalAmount = int.Parse(total) - discount,
                totalAmount = int.Parse(total),
                outletName = WebSecurity.CurrentUserName,
                deleteRemarks = "a"
            };

            Miscellaneous.HabibDataClass.Habib.CustomerOrders.Add(customerOrder);
            Miscellaneous.HabibDataClass.Habib.SaveChanges();

            int id = Miscellaneous.HabibDataClass.Habib.CustomerOrders.ToList().Last().customerOrder_Id;
            List<Models.ShoeSizeColor_CustomerOrder> l = new List<Models.ShoeSizeColor_CustomerOrder>();

            for (int i = 0; i < articles.Length; i++)
            {
                var temp1 = articles[i];
                var temp2 = int.Parse(sizes[i]);

                var temp3 = colors[i];
                var temp4 = quantity[i];
                var shoessizeid = Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(c => c.Shoe_Size.Shoe.shoeArticle.Equals(temp1) && c.Shoe_Size.Size.sizeNo == temp2 && c.Color.colorName.Equals(temp3)).First().shoeSizeColor_Id;

                l.Add(new Models.ShoeSizeColor_CustomerOrder()
                {
                    batch_Id = 1,
                    customerOrder_Id = id,
                    price = Miscellaneous.HabibDataClass.Habib.Shoes.Where(c => c.shoeArticle.Equals(temp1)).First().shoePrice,
                    quantity = int.Parse(temp4),
                    shoeSizeColor_Id = shoessizeid
                });
                var idd = Miscellaneous.HabibDataClass.Habib.Outlets.Where(c => c.outletName.Equals(WebSecurity.CurrentUserName)).First().outlet_Id;
                var deductquantity = Miscellaneous.HabibDataClass.Habib.Outlet_ShoeSizeColor.Where(c => c.shoeSizeColor_Id == shoessizeid && c.outlet_Id == idd).SingleOrDefault();
                deductquantity.quantity -= int.Parse(temp4);
                Miscellaneous.HabibDataClass.Habib.SaveChanges();

            }

            Miscellaneous.HabibDataClass.Habib.ShoeSizeColor_CustomerOrder.AddRange(l);
            Miscellaneous.HabibDataClass.Habib.SaveChanges();

            return Json(new { newURL = Url.Action("Receipt", "PointOfSale", new { id = id }) }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CustomOrder()
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Contains("outlet"))
                {
                    ViewBag.customorder = "active";
                    return View();
                }
                else
                {
                    WebSecurity.Logout();
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public ActionResult Receipt(int id)
        {
            Models.CustomerOrder customerOrder = Miscellaneous.HabibDataClass.Habib.CustomerOrders.Find(id);

            List<Models.ShoeSizeColor_CustomerOrder> l = customerOrder.ShoeSizeColor_CustomerOrder.ToList();

            View_Modals.posReciptModel posModel = new View_Modals.posReciptModel(customerOrder, l);
            posModel.EmpName = Miscellaneous.HabibDataClass.Habib.Employees.Find(posModel.customerOrder.employee_Id).employeeName;
            return View(posModel);
        }
    }
}