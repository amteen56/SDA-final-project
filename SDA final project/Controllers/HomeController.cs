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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (WebSecurity.IsAuthenticated)
            {

                ViewBag.dashboard = "active";
                var listofoulets = new List<List<Tuple<int, int, int>>>();
                for (int j = 0; j < 2; j++)
                {
                    var sum = 0; List<Tuple<int, int, int>> list = new List<Tuple<int, int, int>>();
                    int year = 0;
                    var tuple = new Tuple<int, int, int>(2019, 1, 452);
                    int y1 = DateTime.Now.Year, y2 = y1;
                    for (int i = 1; i < 13; i++)
                    {
                        sum = 0; string outletname = "outlet_" + (j + 1);
                        int month = DateTime.Now.AddMonths(-i + 1).Month;
                        int years = DateTime.Now.AddMonths(-i + 1).Year;
                        if (y1 != years) y2 = years;
                        var orderlist = Miscellaneous.HabibDataClass.Habib.CustomerOrders.Where(c => c.outletName == outletname && c.dateOfOrder.Month == month && c.dateOfOrder.Year == years).ToList();
                        foreach (var item in orderlist)
                        {
                            year = item.dateOfOrder.Date.Year;
                            sum += Convert.ToInt32(item.finalAmount);

                        }
                        tuple = new Tuple<int, int, int>(years, month - 1, sum);
                        list.Add(tuple);

                    }
                    string aa = y2.ToString().Substring(1, y2.ToString().Length - 1);
                    string y11 = y1.ToString();
                    y11 = y11.Substring(1, y11.Length - 1);
                    if (y1 != y2)
                        ViewBag.yes = "Monthly Sales, " + aa + "-" + y11;
                    else
                        ViewBag.yes = "Monthly Sales, " + y1;

                    listofoulets.Add(list);
                }
                int countNoraml = 0;
                int countcustom = 0;
                for (int i = 1; i < 8; i++)
                {
                    DateTime Date = DateTime.Now.AddDays(-i + 1).Date;
#pragma warning disable CS0618 // Type or member is obsolete
                    var orderlist = Miscellaneous.HabibDataClass.Habib.CustomerOrders.Where(c => System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(c.dateOfOrder) == Date).ToList();
#pragma warning restore CS0618 // Type or member is obsolete
                    countNoraml += orderlist.Count();
#pragma warning disable CS0618 // Type or member is obsolete
                    var orderlist1 = Miscellaneous.HabibDataClass.Habib.CustomrOrders.Where(c => System.Data.Entity.Core.Objects.EntityFunctions.TruncateTime(c.deliveryDate) == Date).ToList();
#pragma warning restore CS0618 // Type or member is obsolete
                    countcustom += orderlist1.Count();
                }
                Dashboardviewmodel obj = new Dashboardviewmodel();
                obj.salesdata = listofoulets;
                obj.customorders = countcustom;
                obj.normalorders = countNoraml;
                return View(obj);
            }


            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
    }
}

 