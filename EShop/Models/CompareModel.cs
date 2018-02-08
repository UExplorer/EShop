using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShop.Domain.Entities;

namespace EShop.Models
{
    public class CompareModel
    {
        public Goods Item1 { get; set; }
        public Goods Item2 { get; set; }

        public string ReturnUrl { get; set; }
    }
}