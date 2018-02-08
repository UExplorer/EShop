using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Areas.Administration.Models
{
    public class AdminGoodsModel
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

        public int CategoryId { get; set; }

        public string PictrureName { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}