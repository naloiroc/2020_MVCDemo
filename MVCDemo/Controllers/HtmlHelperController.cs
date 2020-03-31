using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class HtmlHelperController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            HelperModel model = new HelperModel
            {
                Birthday1 = DateTime.Now,
                Birthday2 = DateTime.Now,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HelperModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ActionResult Link(string id, string name)
        {
            var data = new
            {
                id,
                name,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }



}