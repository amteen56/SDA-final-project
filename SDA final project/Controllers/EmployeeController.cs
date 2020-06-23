using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SDA_final_project.Miscellaneous;
using SDA_final_project.Models;
using WebMatrix.WebData;

namespace SDA_final_project.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        Habib habib = HabibDataClass.Habib;
        // GET: Vendor
        public ActionResult Index()
        {
            if (WebSecurity.IsAuthenticated)
            {

                if (Roles.GetRolesForUser(WebSecurity.CurrentUserName)[0].Equals("headOffice"))
                {
                    var Employee_List = (from a in habib.Employees select a).ToList();
                    ViewBag.Employee = "active";
                    ViewBag.elist = "active";
                    ViewBag.elistDisplay = "block";
                    return View(Employee_List);
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
                    ViewBag.Employee = "active"; ViewBag.ecreate = "active";
                    ViewBag.elistDisplay = "block";
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
            Employee model = (from a in habib.Employees where a.employee_Id == id select a).SingleOrDefault();
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee model, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                model.DeleteStatus = 0;
                model.typeOfEmployee = form["typeofemp"];
                try
                {
                    model.dateOfHiring = form["dateobj"].ToString(); ;
                }
                catch
                {
                    model.dateOfHiring = null;
                }
                habib.Employees.Add(model);
                habib.SaveChanges();
                return RedirectToAction("Index", "Employee");
            }
            else
                return View();
        }
      
        public ActionResult Delete(int id)
        {
            var obj = habib.Employees.Single(c => c.employee_Id == id);
            obj.DeleteStatus = 1;
            return RedirectToAction("Index", "Employee");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(Employee model, FormCollection form)
        {
            var obj = habib.Employees.Single(c => c.employee_Id == model.employee_Id);
            obj.employeeAddress = model.employeeAddress;
            obj.employeeName = model.employeeName;
            obj.typeOfEmployee = form["typeofemp"];
            obj.employeeContactNo = model.employeeContactNo;
            try
            {
                obj.dateOfHiring = form["dateobj"].ToString(); ;
            }
            catch
            {
                obj.dateOfHiring = null;
            }
            obj.salary = model.salary;

            habib.SaveChanges();
            return RedirectToAction("Index", "Employee");
        }
        public ActionResult Order()
        {
            return View();
        }
    }
}