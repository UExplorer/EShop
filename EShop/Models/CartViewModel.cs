using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShop.Domain.Entities;

namespace EShop.Models
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}