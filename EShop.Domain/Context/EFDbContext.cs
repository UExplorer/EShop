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
    /// <summary>
    /// Context class wich creating basic Entities into database and fills it with set default data 
    /// </summary>
    public class EFDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        /// <summary>
        /// Set initializer for specified connection
        /// </summary>
        public EFDbContext(): base("DefaultConnection") 
        {
            Database.SetInitializer<EFDbContext>(new EshopDBInitializer()); 
        }
    }

    /// <summary>
    /// Initializer with defaul data for database
    /// </summary>
    public class EshopDBInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            // Statuses
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

            // Categories
            context.Categories.Add(new Category()
            {
                Name = "Смартфоны"
            });
            context.Categories.Add(new Category()
            {
                Name = "Кнопочные"
            });
            context.Categories.Add(new Category()
            {
                Name = "Раскладушки"
            });
            context.Categories.Add(new Category()
            {
                Name = "Слайдеры"
            });
            context.SaveChanges();

            // Goods
            context.Goods.Add(new Goods()
            {
                Name = "Meizu M2 Note",
                CategoryId = context.Categories.Where(c=>c.Id == 1).Select(c=>c.Id).SingleOrDefault(),
                Description = "Нет предела совершенству. Компания Meizu представляет M2 Note всего через 6 месяцев после запуска " +
                              "его предшественника, которого новая модель превосходит во многих аспектах. Улучшенная эргономика корпуса, " +
                              "оптимизированная камера и абсолютно новая кнопка mBack — вот лишь некоторые из усовершенствований. Все это и " +
                              "многое другое поразительно улучшает качество использования вами нового смартфона.",
                AvailableCount = 0,
                Price = 4000,
                Pictrure = "MeizuM2Note.jpg"
            });
            context.Goods.Add(new Goods()
            {
                Name = "Samsung Galaxy S8 64GB",
                CategoryId = 1,
                Description = "Samsung Galaxy S8 переворачивают представление о классическом дизайне смартфона.Безграничный " +
                              "* изогнутый с двух сторон экран подчеркивает гармонию стиля и инноваций.",
                AvailableCount = 10,
                Price = 10000,
                Pictrure = "SamsungS8.png"
            });
            context.Goods.Add(new Goods()
            {
                Name = "Nomi i242 X-treme Black-Green",
                CategoryId = 2,
                Description = "Оставайтесь на связи в любом уголке планеты и не переживайте из-за сложных условий – Nomi i242" +
                              " X-Treme выдержит самые серьезные испытания. Яркий и выразительный дизайн, высококачественные " +
                              "и прочные материалы, продуманная эргономика – этот защищенный телефон полностью соответствует вашему " +
                              "образу жизни. Защита от пыли и воды по стандарту IP68, повышенная стойкость перед ударами и падениями – " +
                              "используйте телефон там, где другим устройствам это не под силу.",
                AvailableCount = 5,
                Price = 1300,
                Pictrure = "nomi_i242.jpg"
            });
            context.Goods.Add(new Goods()
            {
                Name = "LG G360 Titan/Титан",
                CategoryId = 2,
                Description = "Мобильный телефон LG G360 выполнен из качественных комплектующих с соблюдением строгого контроля и всех " +
                              "необходимых стандартов на каждом этапе сборки. Стильный и элегантный дизайн модели идеально сочетается с " +
                              "невысокой ценой, долговечностью, надёжностью и практичностью. Оригинальный телефон станет ярким дополнением к " +
                              "образу своего обладателя, придавая уверенность и подчёркивая стиль и вкус.",
                AvailableCount = 3,
                Price = 1950,
                Pictrure = "lg_g360_titan.jpg"
            });
            context.Goods.Add(new Goods()
            {
                Name = "Prestigio Grace B1 1242 Duo Red",
                CategoryId = 3,
                Description = "Наслаждайтесь комфортом и элегантным дизайном Grace B1!Он тонкий, легкий и просто управляется одной рукой.Телефон " +
                              "доступен в современных цветовых вариантах - красный + черный, синий + черный и черный. Широкий дисплей По сравнению со " +
                              "старыми телефонами - раскладушками этот очень удобен в использовании.Он имеет яркий 2, 4 - дюймовый дисплей, на котором " +
                              "удобно просматривать контент и читать сообщения.",
                AvailableCount = 5,
                Price = 699,
                Pictrure = "Presigio.png"
            });
            context.Goods.Add(new Goods()
            {
                Name = "Prestigio Grace B1 1242 Duo Red",
                CategoryId = 3,
                Description = "Разработка бюджетного класса выполнена в форм-факторе слайдер. Из особенностей аппарата стоит отметить FM radio с " +
                              "возможностью записи, поддержку карт памяти стандарта microSDHC до 8 Gb. Спецификации: GSM 850/900/1800/1900, EDGE, " +
                              "GPRS class 10, 2.0 TFT дисплей(128x160 pix, 65536 оттенков)",
                AvailableCount = 7,
                Price = 906,
                Pictrure = "samsung-c3110.jpg"
            });
            context.SaveChanges();

            //Roles
            context.Roles.Add(new AppRole() {Name = "Admin"});
            context.Roles.Add(new AppRole() { Name = "Moderator" });
            context.Roles.Add(new AppRole() {Name = "User"});

            // Shipment
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

            // Admin
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var user = new AppUser()
            {
                UserName = "Admin", Email = "test@gmail.com", IsEnabled = true
                   
            };
            userManager.Create(user,"1234567");
            userManager.AddToRole(user.Id, "Admin");
            context.SaveChanges();

            // Test User
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
