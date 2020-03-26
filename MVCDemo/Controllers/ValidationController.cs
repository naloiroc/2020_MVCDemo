using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;
namespace MVCDemo.Controllers
{
    public class ValidationController : Controller
    {
        [HttpGet]
        // GET: Validation
        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else if (student.Name.Length < 3)
            {
                ModelState.AddModelError("Name", "姓名長度必須超過3碼");
                return View();
            }

            return RedirectToAction(nameof(Success));
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}