using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities
{
    /// <summary>
    /// Class represents Review entity and used for creating db-table "Reviews"
    /// </summary>
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public string Author { get; set; }
        public int Stars { get; set; }

        public Goods Goods { get; set; }
    }
}
