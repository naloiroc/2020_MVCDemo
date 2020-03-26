using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class SessionController : Controller
    {
        public ActionResult Index()
        {
            Student student = new Student
            {
                Name = "Yuzhen",
                Age = 20,
            };
            Session["Data"] = student;
            ViewBag.Message = "Action from Index";
            return View("Index", student);
        }

        public ActionResult Index2()
        {
            Student student = (Student)Session["Data"];
            ViewBag.Message = "Action from Index2";
            return View("Index", student);
        }

        public ActionResult Add()
        {
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(2);
            Session["LIST"] = list;
            int count1 = list.Count;

            list.Add(3);
            int count2 = ((List<int>)Session["LIST"]).Count;
            Session["LIST"] = list;
            int count3 = ((List<int>)Session["LIST"]).Count;
            return new EmptyResult();
        }
    }
}