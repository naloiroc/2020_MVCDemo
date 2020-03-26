using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class HomeWork1Controller : Controller
    {
        public ActionResult Index()
        {
            Employee employee = new Employee()
            {
                EmpID = "A123456789",
                EmpName = "這是View導入"
            };
            return View(employee);
        }

        public PartialViewResult _Index()
        {
            Employee employee = new Employee()
            {
                EmpID = "B123456789",
                EmpName = "這是PartialView導入"
            };
            return PartialView(employee);

        }

    }
}