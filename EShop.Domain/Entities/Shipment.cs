using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Identity;

namespace EShop.Domain.Entities
{
    public class Shipment
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public int ZIP { get; set; }
        public string Reciver { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}
        