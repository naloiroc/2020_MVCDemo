using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class HelperModel
    {
        
        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string Text { get; set; }

        [Display(Name = "Display1")]
        public string Display { get; set; }

        [DisplayName("Display2")]
        public string DisplayName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birthday1")]
        public DateTime Birthday1 { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birthday2")]
        public DateTime Birthday2 { get; set; }

        [DisplayName("Number")]
        [Required]
        public int? Number { get; set; }


        [DisplayName("Hidden")]
        [Required]
        public string Hidden { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }

        [DisplayName("RadioButton")]
        [Required]
        public string Radio { get; set; }

        [DisplayName("CheckBox")]
        public bool Check { get; set; }

        [DisplayName("Label")]
        public string Label { get; set; }

    }
}