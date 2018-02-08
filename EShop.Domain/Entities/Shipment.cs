using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Identity;

namespace EShop.Domain.Entities
{
    /// <summary>
    /// Class represents Shipment entity and used for creating db-table "Shipments"
    /// </summary>
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
        