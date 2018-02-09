using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.Areas.Administration.Models
{
    public class AdminGoodsModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "В наличии (шт)")]
        public int AvailableCount { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }

        [Display(Name = "Высота")]
        public int Height { get; set; }

        [Display(Name = "Ширина")]
        public int Width { get; set; }

        [Display(Name = "Длина")]
        public int Length { get; set; }

        [Required]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }

        [Display(Name = "Картинка")]
        public string PictrureName { get; set; }
        public HttpPostedFileBase Picture { get; set; }
    }
}