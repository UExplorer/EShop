using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.Models
{
    public class ReviewModel
    {
        [Required]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Отзыв")]
        public string Text { get; set; }

        [Display(Name = "Дата")]  
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Оценка")]
        public int Stars { get; set; }

        [Display(Name = "Товар")]
        public int GoodsId { get; set; }
    }
}