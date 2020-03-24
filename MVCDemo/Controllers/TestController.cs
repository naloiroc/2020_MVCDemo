using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class TestController : Controller
    {
        // GET: Dest
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 回傳Json格式
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnJson()
        {
            Student student = new Student
            {
                Name = "yuzhen",
                Age = 22,
            };
            return Json(student, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 回傳View
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnView()
        {
            // 可指定View名稱，若與Action名稱相同則可省略
            // return View("ReturnView");
            return View("");
        }

        /// <summary>
        /// 回傳PartialView
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnPartialView()
        {
            // PartialView習慣"_"開頭
            return PartialView("_ReturnPartialView");
        }

        /// <summary>
        /// 回傳至 ReturnView 頁面
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnRedirectToAction()
        {
            return RedirectToAction("ReturnView");
        }

        /// <summary>
        /// 三種傳資料方式ViewData、ViewBag、TempData
        /// return View時三種都有值
        /// return RedirectToAction時只有TempData有值
        /// </summary>
        /// <returns></returns>
        public ActionResult PassData1()
        {
            ViewData["Data1"] = "Pass by ViewData";
            ViewBag.Data2 = "Pass by ViewBag";
            TempData["Data3"] = "Pass by TempData";
            return View();
        }

        /// <summary>
        /// 僅有TempData有值
        /// </summary>
        /// <returns></returns>
        public ActionResult PassData2()
        {
            string data1;
            string data2;
            string data3;
            if (ViewData["Data1"] != null)
            {
                data1 = ViewData["Data1"].ToString();
            }
            if (ViewBag.Data2 != null)
            {
                data2 = ViewBag.Data2;
            }
            if (TempData["Data3"] != null)
            {
                data3 = (string)TempData["Data3"];
            }
            return View();
        }

        public ActionResult PassData3()
        {
            ViewData["Data1"] = "Pass by ViewData";
            ViewBag.Data2 = "Pass by ViewBag";
            TempData["Data3"] = "Pass by TempData";
            return RedirectToAction("PassData2");
        }


        /// <summary>
        /// 回傳View Model的形式
        /// </summary>
        /// <returns></returns>
        public ActionResult PassViewModel()
        {
            Student student = new Student
            {
                Name = "Yuzhen",
                Age = 22,
            };

            return View(student);
            // 另一種寫法
            // return View("PassViewModel", student);
        }
    }
}