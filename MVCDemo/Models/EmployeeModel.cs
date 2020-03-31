using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    [Serializable]
    public class EmployeeModel
    {
        [Required]
        [DisplayName("身分證")]
        [StringLength(10, ErrorMessage = "身分證長度必須為10碼")]
        public string EmpID { get; set; }
        [Required]
        [DisplayName("姓名")]
        public string EmpName { get; set; }
        [Required]
        [DisplayName("性別")]
        public string Sex { get; set; }
        [Required]
        [DisplayName("生日")]
        public DateTime Birthday { get; set; }
    }
}