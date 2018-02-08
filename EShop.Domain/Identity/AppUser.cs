using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EShop.Domain.Identity
{
    public class AppUser : IdentityUser
    {
        public bool IsEnabled { get; set; }

    }
}
