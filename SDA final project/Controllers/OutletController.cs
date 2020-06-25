using SDA_final_project.Models;
using SDA_final_project.View_Modals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace SDA_final_project.Controllers
{
    public class OutletController : Controller
    {
        Models.Habib h = new Habib();


        // GET: Outlet
        public ActionResult Index()
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Equals("headOffice"))
                {
                    ViewBag.outlet = "active";
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

        public ActionResult Shoes(int id)
        {

            List<Outlet_ShoeSizeColor> shoe_Size_Colors = Miscellaneous.HabibDataClass.Habib.Outlet_ShoeSizeColor.Where(c => c.outlet_Id == id).ToList(); ;
            // List<Shoe> shoes = shoe_Size_Colors.Select(c => c.Shoe_Size.Shoe).ToList();




            return View(shoe_Size_Colors);
        }


        public ActionResult Quantity(string id, int outletid)
        {

            int shoeID = h.Shoes.Where(c => c.shoeArticle.Equals(id)).First().shoe_Id;


            List<Shoe_Size> shoeSizes = h.Shoe_Size.Where(c => c.shoe_Id == shoeID).ToList();

            List<Shoe_Size_Color> shoe_Size_Colors = new List<Shoe_Size_Color>();

            foreach (Shoe_Size x in shoeSizes)
            {
                shoe_Size_Colors.AddRange(x.Shoe_Size_Color.ToList());
            }

            List<Outlet_ShoeSizeColor> outlet = new List<Outlet_ShoeSizeColor>();

            foreach (Shoe_Size_Color x in shoe_Size_Colors)
            {
                outlet.AddRange(x.Outlet_ShoeSizeColor.ToList());
            }

            outlet = outlet.Where(c => c.outlet_Id == outletid).ToList();
            return PartialView(outlet);
        }


        public ActionResult warehouse()
        {

            List<Shoe_Size_Color> s = Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(c => c.quantityInWH != 0).ToList();

            return View(s);
        }

        public ActionResult warehouseQuantity(string id)
        {

            List<Shoe_Size_Color> s = Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(c => c.Shoe_Size.Shoe.shoeArticle.Equals(id)).ToList();

            return View(s);
        }

    }

}