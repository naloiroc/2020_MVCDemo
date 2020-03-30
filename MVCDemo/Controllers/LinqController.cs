using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class LinqController : Controller
    {
        #region -- Anonymous Function --
        /// <summary>
        /// 一般 function 寫法
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IsPositive1(int number)
        {
            return number > 0;
        }

        /// <summary>
        /// Lambda 運算子
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool IsPOsitive2(int number) => number > 0;

        // GET: Linq
        public ActionResult AnonymousFunction()
        {
            // 呼叫 function
            bool res1 = IsPositive1(0);
            bool res2 = IsPOsitive2(0);

            // 將 function 變成變數
            // Func<第一個參數型別, 回傳型別>
            // Func<第一個參數型別, 第二個參數型別, 回傳型別>
            Func<int, bool> isPositive3 = IsPositive1;
            bool res3 = isPositive3(0);

            Func<int, bool> isPositive4 = delegate (int number)
            {
                return number > 0;
            };

            Func<int, bool> isPositive5 = (number) =>
            {
                return number > 0;
            };

            Func<int, bool> isPositive6 = number => number > 0;

            // Action<回傳型別>
            Predicate<int> isPositive7 = n => n > 0;

            return new EmptyResult();
        }
        #endregion

        public ActionResult Linq()
        {
            // Language-Integrated Query
            // linq to Object
            // linq to DataSet
            // linq to Entity
            // linq to XML

            // LINQ Query Syntax
            // LINQ Method Syntax


            List<Student> list = GetData();

            // Select 
            var listSelect1 = from s in list
                              select s;
            var listSelect2 = list.Select(m => m);

            var listSelect3 = (from s in list
                               select s.Name).ToList();
            var listSelect4 = list.Select(m => m.Age).ToList();

            var listSelect5 = from s in list
                              select new Teacher
                              {
                                  FullName = s.Name,
                                  Age = s.Age,
                              };
            var listSelect6 = list.Select(m => new Teacher
            {
                FullName = m.Name,
                Age = m.Age,
            });

            var anonymousType = new
            {
                X = "AA",
                Y = 1,
                Z = true,
            };
            bool res = anonymousType.Z;

            var listSelect7 = from s in list
                              select new
                              {
                                  X = s.Name,
                                  Y = s.Age,
                              };
            var listSelect8 = list.Select(m => new { X = m.Name, Y = m.Age });


            // Where
            var listWhere1 = from s in list
                             where s.Age > 20
                             select s;
            // list.Where(Func<Student, bool> predicate)
            var listWhere2 = list.Where(s => s.Age > 20);

            // Orderby
            var listOrderby1 = from s in list
                               orderby s.Age descending
                               select s;
            var listOrderby2 = list.OrderByDescending(m => m.Age);

            // ThenBy
            var listThenBy1 = from s in list
                              orderby s.Age descending, s.Name descending
                              select s;
            var listThenBy2 = list.OrderByDescending(m => m.Age).ThenBy(m => m.Name);

            // Group 
            var listGroupBy = from s in list
                              group s by s.Age into g
                              select new
                              {
                                  g.Key,
                                  Name = g.FirstOrDefault()?.Name ?? ""
                              };

            // First
            var s1 = list.First();
            var s2 = list.FirstOrDefault();
            var s3 = list.FirstOrDefault(m => m.Age < 40);

            // All, Any
            bool resAny = list.Any(m => m.Age < 50);
            bool resAll = list.All(m => m.Age < 50);

            // Count
            int count1 = list.Count();
            int count2 = list.Where(m => m.Age > 20).Count();
            int count3 = list.Count(m => m.Age > 20);


            return new EmptyResult();
        }

        public List<Student> GetData()
        {
            return new List<Student>
            {
                new Student
                {
                    Name = "A",
                    Age = 10,
                },
                new Student
                {
                    Name = "B",
                    Age = 15,
                },
                new Student
                {
                    Name = "C",
                    Age = 20,
                },
                new Student
                {
                    Name = "D",
                    Age = 25,
                },
                new Student
                {
                    Name = "E",
                    Age = 30,
                },
                new Student
                {
                    Name = "F",
                    Age = 10,
                },
            };
        }

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

            // 移除一筆
            var tmpStudent = listStudent.Where(m => m.Name == "A").FirstOrDefault();
            if (tmpStudent != null)
            {
                listStudent.Remove(tmpStudent);
            }

            // 移除多筆
            listStudent.RemoveAll(m => m.Name == "A");

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

            // 更新一筆
            var tmpStudent = listStudent.Where(m => m.Name == "A").FirstOrDefault();
            if (tmpStudent != null)
            {
                tmpStudent.Name = "X";
            }

            return new EmptyResult();

        }
    }
}