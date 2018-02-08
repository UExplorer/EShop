using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EShop.Domain.Entities;

namespace EShop.Models
{
    public class GoodsModel
    {
        public List<Goods> Goods { get; set; }
        public int? Category { get; set; }

        public string Name { get; set; }
        public string Color { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? Length { get; set; }
        public decimal? PriceLow { get; set; }
        public decimal? PriceHight { get; set; }

        public SortModel Sort { get; set; }
    }
}