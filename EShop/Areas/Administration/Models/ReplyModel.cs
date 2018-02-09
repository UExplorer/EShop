using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EShop.Domain.Identity;

namespace EShop.Areas.Administration.Models
{
    public class ReplyModel
    {
        [Required]
        [Display(Name = "От кого (Ваш email)")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный Email адресс")]
        public string From { get; set; }

        public string User { get; set; }

        [Required]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "Сообщение")]
        public string TextBody { get; set; }
    }
}