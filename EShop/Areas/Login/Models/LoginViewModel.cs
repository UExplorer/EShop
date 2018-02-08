using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Areas.Login.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}