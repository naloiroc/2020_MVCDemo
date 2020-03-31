using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Repository;
using MVCDemo.Data;

namespace MVCDemo.Controllers
{
    public class EFController : Controller
    {
        private SchoolRepository repo;
        public EFController()
        {
            repo = new SchoolRepository();
        }

        // GET: EF
        public ActionResult Index()
        {
            Student s1 = new Student
            {
                Name = "A",
                Age = 20,
                ClassID = "11L010",
            };
            Student s2 = new Student
            {
                Name = "B",
                Age = 21,
                ClassID = "11L010",
            };
            Student s3 = new Student
            {
                Name = "C",
                Age = 22,
                ClassID = "11L010",
            };

            // 新增一筆
            repo.InsertStudent(s1);

            // 新增多筆
            List<Student> list = new List<Student>{s2, s3};
            repo.InsertStudent(list);

            // 修改
            var modifyStudent = repo.GetStudentByName("A").First();
            modifyStudent.Age = 100;
            repo.ModifyStudent(modifyStudent);

            // 刪除
            var deleteStudent = repo.GetStudentByName("B").First();
            repo.DeleteStudent(deleteStudent);

            var listStudentInfo = repo.GetStudent("");

            return new EmptyResult();
        }

        public ActionResult Index2()
        {
            Student s1 = new Student
            {
                Name = "X",
                Age = 20,
                ClassID = "11L010",
            };

            // 新增
            repo.AddStudent(s1);

            Student s2 = repo.GetStudentByName("X").First();
            s2.Age = 100;

            // 修改
            repo.EditStudent(s2);

            // 刪除
            repo.RemoveStudent(s2);

            return new EmptyResult();
        }

    }
}