using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EShop.Domain.Entities;
using EShop.Domain.Identity;

namespace EShop.Areas.Administration.Models
{
    public class AdminOrderModel
    {
        public int OrderId { get; set; }
        public List<CartLine> OrderCart { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public Shipment Shipment { get; set; }
        public decimal TotalCost { get; set; }
        public static SelectList List { get; set; }
        public int OrderStatus { get; set; }
    }
}