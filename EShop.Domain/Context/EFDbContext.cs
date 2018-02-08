using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Entities;
using EShop.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace EShop.Domain.Context
{
    public class EFDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public EFDbContext(): base("DefaultConnection") 
        {
            Database.SetInitializer<EFDbContext>(new EshopDBInitializer()); 
        }
    }

    public class EshopDBInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            context.Statuses.Add(new Status()
            {
                Name = "Registred"
            });
            context.Statuses.Add(new Status()
            {
                Name = "Paid"
            });
            context.Statuses.Add(new Status()
            {
                Name = "Canceled"
            });
            context.Statuses.Add(new Status()
            {
                Name = "Complited"
            });
            context.Statuses.Add(new Status()
            {
                Name = "In Processing"
            });

            context.Categories.Add(new Category()
            {
                Name = "Mobile Phones"
            });
            context.Categories.Add(new Category()
            {
                Name = "Computers"
            });
            context.Categories.Add(new Category()
            {
                Name = "Other"
            });
            context.SaveChanges();

            context.Goods.Add(new Goods()
            {
                Name = "Meizu M2 Note",
                CategoryId = context.Categories.Where(c=>c.Id == 1).Select(c=>c.Id).SingleOrDefault(),
                Description = "Mobile Phone from Meizu",
                AvailableCount = 5,
                Price = 3000
            });
            context.Goods.Add(new Goods()
            {
                Name = "Hp DV6-6158er",
                CategoryId = 2,
                Description = "Awesome notebook, developed by HP",
                AvailableCount = 0,
                Price = 10000
            });
            context.Goods.Add(new Goods()
            {
                Name = "Cornflackes",
                CategoryId = 3,
                Description = "Just a breakfast meal",
                AvailableCount = 0,
                Price = 30
            });
            context.Goods.Add(new Goods()
            {
                Name = "Meed",
                CategoryId = 3,
                Description = "Smoke Meed Evry Day",
                AvailableCount = 0,
                Price = 250
            });
            context.SaveChanges();

            context.Roles.Add(new AppRole() {Name = "Admin"});
            context.Roles.Add(new AppRole() { Name = "Moderator" });
            context.Roles.Add(new AppRole() {Name = "User"});

            context.SaveChanges();
            var userManager = new AppUserManager(new UserStore<AppUser>(context));
            var shipment = new Shipment()
            {
                Address = "Washington str",
                City = "NY",
                Country = "USA",
                Reciver = "BB Billy",
                ZIP = 1234
            };
            context.Shipments.Add(shipment);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var user = new AppUser()
            {
                UserName = "Admin", Email = "test@gmail.com", IsEnabled = true
                   
            };
            userManager.Create(user,"1234567");
            userManager.AddToRole(user.Id, "Admin");
            context.SaveChanges();

            user = new AppUser() { UserName = "Test", Email = "test@test.com", IsEnabled = true};
            userManager.Create(user, "1234567");
            userManager.AddToRole(user.Id, "User");
            context.SaveChanges();

            context.Orders.Add(new Order()
            {
                Date = DateTime.Now,
                OrderCart = null,
                OrderStatus = context.Statuses.Find(3),
                User = user.UserName,
                Shipment = context.Shipments.First()
            });
            context.Orders.Add(new Order()
            {
                Date = DateTime.Now,
                OrderCart = null,
                OrderStatus = context.Statuses.Find(1),
                User = user.UserName,
                Shipment = context.Shipments.First()
            });
            context.SaveChanges();
        }
    }
}
