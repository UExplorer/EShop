using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace EShop.Domain.Identity
{
    /// <summary>
    /// Class whose implemented UserManager for current application needs 
    /// </summary>
    public class AppUserManager : UserManager<AppUser>
    {
        public AppUserManager(IUserStore<AppUser> store)
            : base(store)
        {
        }

        /// <summary>
        /// Creating new AppUserManager for application using Owin
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context">OwinContext of application</param>
        /// <returns></returns>
        public static AppUserManager Create(
            IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(
                new UserStore<AppUser>(context.Get<EFDbContext>()));

            return manager;
        }
    }
}
