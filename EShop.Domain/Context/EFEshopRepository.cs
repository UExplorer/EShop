using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Entities;
using EShop.Domain.Interfaces;

namespace EShop.Domain.Context
{
    public class EFEshopRepository : IEshopRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Goods> Goods
        {
            get { return context.Goods.Include(g=>g.Reviews); }
        }

        public IEnumerable<Order> Orders
        {
            get { return context.Orders.Include(o=>o.OrderCart).Include(o=>o.Shipment); }
        }

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IEnumerable<CartLine> CartLines
        {
            get { return context.CartLines.Include(c=>c.Goods); }
        }

        public IEnumerable<Status> Statuses
        {
            get { return context.Statuses; }
        }

        public IEnumerable<Shipment> Shipments
        {
            get { return context.Shipments; }
        }

        public IEnumerable<Review> Reviews
        {
            get { return context.Reviews; }
        }

        public void SaveGoods(Goods goods)
        {
            if (goods.Id == 0)
                context.Goods.Add(goods);
            else
            {
                Goods dbEntry = context.Goods.Find(goods.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = goods.Name;
                    dbEntry.Description = goods.Description;
                    dbEntry.Price = goods.Price;
                    dbEntry.Category = context.Categories.Find(goods.CategoryId) ?? context.Categories.Find(1);
                    dbEntry.CartLines = goods.CartLines;
                    dbEntry.Pictrure = goods.Pictrure;
                    dbEntry.Price = goods.Price;
                }
            }
            context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            if (order.Id == 0)
                context.Orders.Add(order);
            else
            {
                Order dbEntry = context.Orders.Find(order.Id);
                if (dbEntry != null)
                {
                    dbEntry.OrderStatus = order.OrderStatus;
                    dbEntry.Shipment = new Shipment();
                    dbEntry.OrderCart = order.OrderCart;
                    dbEntry.User = order.User;
                }
            }
            context.SaveChanges();
        }

        public void DeleteGoods(int id)
        {
            Goods dbEntry = context.Goods.Find(id);
            if(dbEntry != null)
            {
                context.Goods.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public void DeleteOrder(int id)
        {
            Order dbEntry = context.Orders.Find(id);
            if (dbEntry != null)
            {
                context.Orders.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public void AddReview(Review item)
        {
            if (item != null)
            {
                context.Reviews.Add(item);
                context.SaveChanges();
            }
        }

        public void DeleteReview(int id)
        {
            Review dbEntry = context.Reviews.Find(id);
            if (dbEntry != null)
            {
                context.Reviews.Remove(dbEntry);
                context.SaveChanges();
            }
        }

        public Goods FindGoodsById(int id)
        {
            return context.Goods.Find(id);
        }
    }
}
