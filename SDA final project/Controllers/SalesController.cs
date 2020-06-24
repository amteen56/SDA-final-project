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

        public ActionResult Details(int id)
        {
            SalesDetailsViewModel obj = new SalesDetailsViewModel(); ;
            var shoesize = Miscellaneous.HabibDataClass.Habib.ShoeSizeColor_CustomerOrder.Where(c => c.customerOrder_Id == id).ToList();
            var shoesizeid = new List<int>();
            var colorid = new List<int>();
            foreach (var item in shoesize)
            {
                int termp = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(c => c.shoeSizeColor_Id == item.shoeSizeColor_Id).FirstOrDefault().shoeSize_Id);
                shoesizeid.Add(termp);
                int termp1 = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(c => c.shoeSizeColor_Id == item.shoeSizeColor_Id).FirstOrDefault().color_Id);
                colorid.Add(termp1);
                int termp2 = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.Shoe_Size.Where(c => c.shoeSize_Id == termp).FirstOrDefault().shoe_Id);
                int termp3 = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.Shoe_Size.Where(c => c.shoeSize_Id == termp).FirstOrDefault().size_Id);
                string shoesname = Miscellaneous.HabibDataClass.Habib.Shoes.Where(c => c.shoe_Id == termp2).FirstOrDefault().shoeArticle;
                string sizeno = Miscellaneous.HabibDataClass.Habib.Sizes.Where(c => c.size_Id == termp3).FirstOrDefault().sizeNo.ToString();
                string Colorname = Miscellaneous.HabibDataClass.Habib.Colors.Where(c => c.color_Id == termp1).FirstOrDefault().colorName.ToString();
                string shoeprice = Miscellaneous.HabibDataClass.Habib.Shoes.Where(c => c.shoe_Id == termp2).FirstOrDefault().shoePrice.ToString();
                int qty = Convert.ToInt32(Miscellaneous.HabibDataClass.Habib.ShoeSizeColor_CustomerOrder.Where(c => c.customerOrder_Id == id).FirstOrDefault().quantity);
                var tuple = new Tuple<string, string, string, string, string, int>(termp2.ToString(), shoesname, sizeno, Colorname, shoeprice, qty);
                obj.listdetails.Add(tuple);
            }
            return View(obj);
        }
    }
}