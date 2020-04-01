using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using MVCDemo.Data;


namespace MVCDemo.Repository
{
    public class SchoolRepository
    {
        public void InsertStudent(Student student)
        {
            // 新增一筆
            using (SchoolEntities context = new SchoolEntities())
            {                
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Student.Add(student);
                int res = context.SaveChanges();
            }
        }

        public void InsertStudent(IEnumerable<Student> listStudent)
        {
            // 新增多筆
            using (SchoolEntities context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                context.Student.AddRange(listStudent);
                int res = context.SaveChanges();
            }
        }

        public void ModifyStudent(Student student)
        {
            using (SchoolEntities context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);


                // 修改一筆
                //Student modifyStudent = context.Student.Find(student.ID);
                var modifyStudent = context.Student.Where(m => m.ID == student.ID).First();
                modifyStudent.Name = student.Name;
                modifyStudent.Age = student.Age;
                modifyStudent.ClassID = student.ClassID;
                int res = context.SaveChanges();

                //// 修改多筆
                //var listModifyStudent = context.Student.Where(m => m.ID == student.ID);
                //foreach (var s in listModifyStudent)
                //{ 
                //    s.Name = student.Name;
                //    s.Age = student.Age;
                //    s.ClassID = student.ClassID;                
                //}
                //int res = context.SaveChanges();

            }
        }

        public void DeleteStudent(Student student)
        {
            using (SchoolEntities context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                // 刪除一筆
                var deleteStudent = context.Student.Where(m => m.ID == student.ID).First();
                context.Student.Remove(deleteStudent);
                int res = context.SaveChanges();

                //// 刪除多筆
                //var listDeleteStudent = context.Student.Where(m => m.ID == student.ID);
                //context.Student.RemoveRange(listDeleteStudent);
                //res = context.SaveChanges();
            }
        }

        public List<Student> GetStudentByName(string name)
        {
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var listStudent = context.Student.Where(m => m.Name == name);

                //var listStudent = from s in context.Student
                //                  where s.Name == name
                //                  select s;

                return listStudent.ToList();
            }
        }

        public List<MVCDemo.Models.StudentInfo> GetStudent(string name)
        {
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                var listStudentInfo = from s in context.Student
                                      join c in context.Class
                                      on s.ClassID equals c.ClassID
                                      let IsName = string.IsNullOrEmpty(name) ? false : true
                                      where IsName ? s.Name == name : true
                                      select new MVCDemo.Models.StudentInfo
                                      {
                                          ID = s.ID,
                                          Name = s.Name,
                                          Age = s.Age,
                                          ClassID = s.ClassID,
                                          ClassName = c.ClassName,
                                      };
                //var l = context.Student.Join(
                //                            context.Class,
                //                            s => s.ClassID,
                //                            c => c.ClassID,
                //                            (s, c) => new StudentInfo 
                //                            {
                //                                ID = s.ID,
                //                                Name = s.Name,
                //                                Age = s.Age,
                //                                ClassID = s.ClassID,
                //                                ClassName = c.ClassName,
                //                            });

                return listStudentInfo.ToList();
            }
        }

        public List<Student> TableSqlQuery(string name)
        {
            // 查詢單一 Table 資料
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                string sql;
                sql  = "SELECT ID, Name, Age, ClassID ";
                sql += "FROM Student ";
                sql += "WHERE ";
                if (!string.IsNullOrWhiteSpace(name))
                {
                    sql += "Name = @Name AND ";
                }
                sql += "1 = 1 ";

                List<SqlParameter> listPara = new List<SqlParameter>();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    listPara.Add(new SqlParameter("@Name", name));
                }

                var listStudent = context.Student
                                         .SqlQuery(sql, listPara)
                                         .ToList<Student>();
                return listStudent;
            }
        }

        public List<MVCDemo.Models.StudentInfo> DatabaseSqlQuery(string name)
        {
            // 查詢整個 DB 資料
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                string sql;
                sql = "SELECT s.ID, s.Name, s.Age, s.ClassID, c.ClassName ";
                sql += "FROM Student AS s ";
                sql += "INNER JOIN Class AS c ";
                sql += "on s.ClassID = c.ClassID ";
                sql += "WHERE ";
                if (!string.IsNullOrWhiteSpace(name))
                {
                    sql += "Name = @Name AND ";
                }
                sql += "1 = 1 ";

                List<SqlParameter> listPara = new List<SqlParameter>();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    listPara.Add(new SqlParameter("@Name", name));
                }

                var listStudent = context.Database
                                         .SqlQuery<MVCDemo.Models.StudentInfo>(sql, listPara)
                                         .ToList();
                return listStudent;
            }
        }
        public void DatabaseExecuteSqlCommand(string name)
        {
            // 異動整個 DB 資料
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                string sql;
                sql  = "UPDATE Student";
                sql  = "SET Name = Name + 'X' ";
                sql += "WHERE ";
                if (!string.IsNullOrWhiteSpace(name))
                {
                    sql += "Name = @Name AND ";
                }
                sql += "1 = 1 ";
                List<SqlParameter> listPara = new List<SqlParameter>();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    listPara.Add(new SqlParameter("@Name", name));
                }

                int res = context.Database.ExecuteSqlCommand(sql, listPara);
            }
        }

        public void AddStudent(Student student)
        {
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                context.Entry(student).State = EntityState.Added;
                int res = context.SaveChanges();
            }
        }

        public void EditStudent(Student student)
        {
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                context.Entry(student).State = EntityState.Modified;
                int res = context.SaveChanges();
            }
        }

        public void RemoveStudent(Student student)
        {
            using (var context = new SchoolEntities())
            {
                context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                context.Entry(student).State = EntityState.Deleted;
                int res = context.SaveChanges();
            }
        }
    }

    
}