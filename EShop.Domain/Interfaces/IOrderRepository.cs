using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// returns all Orders from Orders table
        /// </summary>
        IEnumerable<Order> GetOrders();

        /// <summary>
        /// returns all CartLines from CartLines table
        /// </summary>
        IEnumerable<CartLine> GetCartLines();

        /// <summary>
        /// returns all Statuses from Statuses table
        /// </summary>
        IEnumerable<Status> GetStatuses();

        /// <summary>
        /// returns all Shipments from Shipment table
        /// </summary>
        IEnumerable<Shipment> GetShipments();

        /// <summary>
        /// Saving modified Order item into database
        /// </summary>
        /// /// <param name="order">Order item for Order table</param>
        void EditOrder(Order order);

        /// <summary>
        /// Saving new Order item into database
        /// </summary>
        /// /// <param name="order">Order item for Order table</param>
        void AddOrder(Order order);

        /// <summary>
        /// Deleting Order item by set id
        /// </summary>
        /// <param name="id">Id number in Order table</param>
        void DeleteOrder(int id);
    }
}
