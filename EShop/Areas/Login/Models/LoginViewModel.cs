using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EShop.Areas.Login.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть в пределах 3-20 символов")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [StringLength(16, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть в пределах 6-16 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}