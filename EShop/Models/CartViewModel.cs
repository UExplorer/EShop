using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.Domain.Entities;

namespace EShop.Models
{
    public class CartViewModel
    {
        [Display(Name = "Корзина")]
        public Cart Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}