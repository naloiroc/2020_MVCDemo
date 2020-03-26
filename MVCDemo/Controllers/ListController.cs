using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult Add()
        {
            // 新增單筆
            List<Student> listStudent = new List<Student>();
            Student student = new Student
            {
                Name = "A",
                Age = 65,
            };
            listStudent.Add(student);
            listStudent.Add(new Student
            {
                Name = "B",
                Age = 66,
            });

            // 新增多筆
            List<Student> newListStudent = new List<Student>();
            newListStudent.AddRange(listStudent);

            return new EmptyResult();
        }

        public ActionResult Remove()
        {
            List<Student> listStudent = GetData();

            // 刪除第0筆資料
            listStudent.RemoveAt(0);

            // 刪除 A
            Student student = null;
            foreach (Student s in listStudent)
            {
                if (s.Name == "B")
                {
                    student = s;
                }
            }
            if (student != null)
            { 
                listStudent.Remove(student);            
            }

            // 進階寫法
            //var tmpStudent = listStudent.Where(m => m.Name == "A").FirstOrDefault();
            //if (tmpStudent != null)
            //{ 
            //    listStudent.Remove(tmpStudent);            
            //}

            return new EmptyResult();
        }

        /// <summary>
        /// 更新資料
        /// </summary>
        /// <returns></returns>
        public ActionResult Update()
        {
            // 取得資料
            List<Student> listStudent = GetData();

            // 修改第0筆資料
            Student tmpStudent = listStudent[0];
            tmpStudent.Name = "X";

            // 修改 B
            foreach (Student s in listStudent)
            {
                if (s.Name == "B")
                {
                    s.Name = "Y";
                    break;

                }
            }

            return new EmptyResult();
        }

        private List<Student> GetData()
        {
            List<Student> listStudent = new List<Student>();
            Student student = new Student
            {
                Name = "A",
                Age = 65,
            };
            listStudent.Add(student);
            listStudent.Add(new Student
            {
                Name = "B",
                Age = 66,
            });
            return listStudent;
        }
    }
}