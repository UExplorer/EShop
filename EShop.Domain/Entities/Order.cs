using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Identity;

namespace EShop.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string User { get; set; }
        public Status OrderStatus { get; set; }
        public Shipment Shipment { get; set; }

        public List<CartLine> OrderCart { get; set; }
    }
}
