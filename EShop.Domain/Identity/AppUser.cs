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
    /// <summary>
    /// Class whose implemented IdentityUser for current application needs 
    /// </summary>
    public class AppUser : IdentityUser
    {
        public bool IsEnabled { get; set; }

    }
}
