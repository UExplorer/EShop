using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EShop.Areas.Administration.Models
{
    public class AdminUsersModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsEnabled { get; set; }
    }
}