using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.Areas.Administration.Models
{
    public class AdminUsersModel
    {
        [Display(Name = "Id пользователя")]
        public string Id { get; set; }

        [Display(Name = "Логин")]
        public string UserName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Роли")]
        public IList<string> Roles { get; set; }

        [Display(Name = "Мобильный телефон")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Статус пользователя")]
        public bool IsEnabled { get; set; }
    }
}