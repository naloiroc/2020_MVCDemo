using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class Student
    {
        [Display(Name = "姓名")]
        [Required(ErrorMessage = "請輸入姓名")]
        public string Name { get; set; }

        [Display(Name = "年齡")]
        [Required(ErrorMessage = "請輸入年齡")]
        [Range(20, 100, ErrorMessage = "年齡區間為20 ~ 100")]
        public int? Age { get; set; }
    }

    public class MinValueAttribute : ValidationAttribute
    {
        // Fields
        private int _minValue;

        // Constructors
        public MinValueAttribute(int minValue, string ErrorMessage = "")
        {
            this._minValue = minValue;
        }

        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int? data = (int?)value;

            if (data >= _minValue)
            {
                return ValidationResult.Success;
            }
            else
            {
                // invalid
                string errorMessage = null;
                if (string.IsNullOrEmpty(ErrorMessage))
                {
                    errorMessage = $"值 {data} 必須大於 {_minValue} ";
                }
                else
                {
                    errorMessage = ErrorMessage;                
                }
                return new ValidationResult(errorMessage);
            }
        }
    }
}