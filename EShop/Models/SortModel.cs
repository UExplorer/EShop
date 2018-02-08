using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public class SortModel
    {
        [Display(Name = "Сортировка по новизне")]
        public string RevId { get; set; }

        [Display(Name = "Toogle для сортировки")]
        public bool RevboolId { get; set; }

        [Display(Name = "Сортировка по имени")]
        public string RevName { get; set; }

        [Display(Name = "Toogle для сортировки")]
        public bool RevboolName { get; set; }

        [Display(Name = "Сортировка по цене")]
        public string RevPrice { get; set; }

        [Display(Name = "Toogle для сортировки")]
        public bool RevboolPrice { get; set; }
    }
}