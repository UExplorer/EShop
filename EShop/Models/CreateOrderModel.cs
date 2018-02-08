using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.Domain.Entities;

namespace EShop.Models
{
    public class CreateOrderModel
    {

        [Display(Name = "Список товаров")]
        public string GoodsList { get; set; }

        [Required]
        [Display(Name = "Страна")]
        [RegularExpression(@"[A-Za-zА-Яа-я]{3,15}", ErrorMessage = "Некорректное название страны")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Адрес проживания")]
        [RegularExpression(@"[A-Za-zА-Яа-я0-9._,-]{3,30}", ErrorMessage = "Некорректный адресс")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Город")]
        [RegularExpression(@"[A-Z a-z А-Я а-я]{3,20}", ErrorMessage = "Некорректное название города")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Индекс")]
        [RegularExpression(@"[0-9]{3,5}", ErrorMessage = "Допустимы только цифры")]
        public int ZIP { get; set; }

        [Required]
        [Display(Name = "Имя получателя")]
        [RegularExpression(@"[A-Za-zА-Яа-я]{3,40}", ErrorMessage = "Некорректное имя")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Имя должно входить в рамки 3-40 символов")]
        public string Reciver { get; set; }
    }
}