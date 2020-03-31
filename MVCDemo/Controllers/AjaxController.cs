using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;
namespace MVCDemo.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IndexPartial()
        {
            Student s = (Student)Session["model"] ?? new Student { Name = "Yuzhen" };
            return PartialView("_IndexPartial", s);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InsertPartial()
        {
            return PartialView("_InsertPartial");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insert(string name)
        {
            Student s = new Student { Name = name };
            Session["model"] = s;
            var data = new
            {
                IsSuccess = true
            };
            return Json(data);
        }
    }
}