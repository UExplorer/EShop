using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.Domain.Entities;

namespace EShop.Models
{
    public class CompareModel
    {
        [Display(Name = "Первый товар для сравнения")]
        public Goods Item1 { get; set; }

        [Display(Name = "Второй товар для сравнения")]
        public Goods Item2 { get; set; }

        public string ReturnUrl { get; set; }
    }
}