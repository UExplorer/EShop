using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.Domain.Entities;

namespace EShop.Models
{
    public class GoodsModel
    {
        [Display(Name = "Список товаров")]
        public List<Goods> Goods { get; set; }

        [Display(Name = "Категория")]
        public int? Category { get; set; }

        [Display(Name = "Название товара")]
        public string Name { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }

        [Display(Name = "Высота")]
        public int? Height { get; set; }

        [Display(Name = "Ширина")]
        public int? Width { get; set; }

        [Display(Name = "Длина")]
        public int? Length { get; set; }

        [Display(Name = "Мин. цена")]
        public decimal? PriceLow { get; set; }

        [Display(Name = "Макс. цена")]
        public decimal? PriceHight { get; set; }

        [Display(Name = "Фильтры сортировки")]
        public SortModel Sort { get; set; }
    }
}