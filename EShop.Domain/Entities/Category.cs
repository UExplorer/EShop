using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities
{
    /// <summary>
    /// Class represents Category entity and used for creating db-table "Categories"
    /// </summary>
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Goods> Goods { get; set; }
    }
}
