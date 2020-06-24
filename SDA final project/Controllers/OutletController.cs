using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using SDA_final_project.Models;
using WebMatrix.WebData;

namespace SDA_final_project.Controllers
{
    public class OutletController : Controller
    {
        Models.Habib h = new Habib();


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
            return View(shoe_Size_Colors);
        }
        public ActionResult warehouseQuantity(string id)
        {
            List<Shoe_Size_Color> s = Miscellaneous.HabibDataClass.Habib.Shoe_Size_Color.Where(c => c.Shoe_Size.Shoe.shoeArticle.Equals(id)).ToList();
            return View(s);
        }
    }
}