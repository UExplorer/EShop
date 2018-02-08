using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int AvailableCount { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }

        public List<Review> Reviews { get; set; }

        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public List<CartLine> CartLines { get; set; }
        public int? CartlinesId { get; set; }

        public string Pictrure { get; set; }
    }
}
