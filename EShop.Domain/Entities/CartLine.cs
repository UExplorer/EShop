using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities
{
    public class CartLine
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public Goods Goods { get; set; }
        public List<Order> Order { get; set; }
    }
}
