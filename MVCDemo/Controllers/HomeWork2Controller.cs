using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;
using MVCDemo.Service;

namespace MVCDemo.Controllers
{
    public class HomeWork2Controller : Controller
    {
        // GET: Demo
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _DisplayEmpList()
        {
            if (Session["EmpList"] == null)
            {
                return PartialView(new List<EmployeeModel>());
            }
            else
            {
                return PartialView(Session["EmpList"] as List<EmployeeModel>);
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeModel employee)
        {
            List<EmployeeModel> EmpList = new List<EmployeeModel>();
            if (Session["EmpList"] != null)
            {
                EmpList = Session["EmpList"] as List<EmployeeModel>;
            }
            if (employee.EmpID == null || GlobalMethods.CheckID(employee.EmpID) != "")
            {
                ModelState.AddModelError("EmpID", "身分證格式錯誤");
            }
            if (EmpList.Where(x => x.EmpID == employee.EmpID).Count() > 0)
            {
                ModelState.AddModelError("EmpID", "此身份證字號已重複");
            }

            if (employee.Birthday == null || employee.Birthday.Date > DateTime.Today)
            {
                ModelState.AddModelError("Birthday", "生日不可大於今天");
            }
            List<string> SexCheck = new List<string>();
            SexCheck.Add("1");
            SexCheck.Add("8");
            if (employee.Sex == "男" && !SexCheck.Contains(employee.EmpID.Substring(1, 1)))
            {
                ModelState.AddModelError("Sex", "性別選擇錯誤");
            }
            SexCheck.Clear();
            SexCheck.Add("2");
            SexCheck.Add("9");
            if (employee.Sex == "女" && !SexCheck.Contains(employee.EmpID.Substring(1, 1)))
            {
                ModelState.AddModelError("Sex", "性別選擇錯誤");
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            EmpList.Add(employee);
            Session["EmpList"] = EmpList;
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            Session["SelectID"] = null;
            if (id == null || Session["EmpList"] == null)
            {
                return RedirectToAction("Index");
            }

            List<EmployeeModel> EmpList = Session["EmpList"] as List<EmployeeModel>;
            if (EmpList.Where(x => x.EmpID == id).Count() == 0)
            {
                return RedirectToAction("Index");
            }
            Session["SelectID"] = EmpList.Where(x => x.EmpID == id).Select(x => x.EmpID).FirstOrDefault();
            return View(EmpList.Where(x => x.EmpID == id).FirstOrDefault());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeModel employee)
        {
            if (Session["SelectID"] == null || Session["EmpList"] == null)
            {
                ModelState.AddModelError("EmpID", "非法的修改，請由列表進入");
                return View(employee);
            }

            List<EmployeeModel> EmpList = Session["EmpList"] as List<EmployeeModel>;
            string SelectID = Session["SelectID"] as string;
            if (SelectID != employee.EmpID)
            {
                ModelState.AddModelError("EmpID", "非法的修改，請由列表進入");
                return View(employee);
            }

            if (employee.Birthday == null || employee.Birthday.Date > DateTime.Today)
            {
                ModelState.AddModelError("Birthday", "生日不可大於今天");
            }
            List<string> SexCheck = new List<string>();
            SexCheck.Add("1");
            SexCheck.Add("8");
            if (employee.Sex == "男" && !SexCheck.Contains(employee.EmpID.Substring(1, 1)))
            {
                ModelState.AddModelError("Sex", "性別選擇錯誤");
            }
            SexCheck.Clear();
            SexCheck.Add("2");
            SexCheck.Add("9");
            if (employee.Sex == "女" && !SexCheck.Contains(employee.EmpID.Substring(1, 1)))
            {
                ModelState.AddModelError("Sex", "性別選擇錯誤");
            }
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            var Modify = EmpList.Where(x => x.EmpID == SelectID).FirstOrDefault();
            Modify.Birthday = employee.Birthday;
            Modify.EmpName = employee.EmpName;
            Modify.Sex = employee.Sex;

            Session["EmpList"] = EmpList;
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            if (id == null || Session["EmpList"] == null)
            {
                return RedirectToAction("Index");
            }
            List<EmployeeModel> EmpList = Session["EmpList"] as List<EmployeeModel>;
            if (EmpList.Where(x => x.EmpID == id).Count() == 0)
            {
                return RedirectToAction("Index");
            }
            EmpList.Remove(EmpList.Where(x => x.EmpID == id).FirstOrDefault());
            Session["EmpList"] = EmpList;
            return RedirectToAction("Index");
        }
    }
}