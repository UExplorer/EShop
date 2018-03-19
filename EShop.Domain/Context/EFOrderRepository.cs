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
    public class EFOrderRepository : IOrderRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Order> GetOrders()
        {
            return context.Orders.Include(o => o.OrderCart).Include(o => o.Shipment);
        }

        public IEnumerable<CartLine> GetCartLines()
        {
            return context.CartLines.Include(c => c.Goods);
        }

        public IEnumerable<Status> GetStatuses()
        {
            return context.Statuses;
        }

        public IEnumerable<Shipment> GetShipments()
        {
            return context.Shipments;
        }

        public void EditOrder(Order order)
        {
            Order dbEntry = context.Orders.Find(order.Id);
            if (dbEntry != null)
            {
                dbEntry.OrderStatus = order.OrderStatus;
                dbEntry.Shipment = new Shipment();
                dbEntry.OrderCart = order.OrderCart;
                dbEntry.User = order.User;
            }
            context.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            if (order != null)
            {
                context.Orders.Add(order);
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
    }
}
