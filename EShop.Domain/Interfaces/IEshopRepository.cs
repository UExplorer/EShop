using System.Collections.Generic;
using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces
{
    /// <summary>
    /// Intarface for manipulating data from database. Defines all basic Entities used in application 
    /// </summary>
    public interface IEshopRepository
    {
        /// <summary>
        /// returns all Goods from Goods table
        /// </summary>
        IEnumerable<Goods> Goods { get; }
        
        /// <summary>
        /// returns all Categories from Categories table
        /// </summary>
        IEnumerable<Category> Categories { get; }

        /// <summary>
        /// returns all Orders from Orders table
        /// </summary>
        IEnumerable<Order> Orders { get; }

        /// <summary>
        /// returns all CartLines from CartLines table
        /// </summary>
        IEnumerable<CartLine> CartLines { get; }

        /// <summary>
        /// returns all Statuses from Statuses table
        /// </summary>
        IEnumerable<Status> Statuses { get; }

        /// <summary>
        /// returns all Shipments from Shipment table
        /// </summary>
        IEnumerable<Shipment> Shipments { get; }

        /// <summary>
        /// returns all Reviews from Reviews table
        /// </summary>
        IEnumerable<Review> Reviews { get; }

        /// <summary>
        /// Saving new or modified Goods item into database
        /// </summary>
        /// /// /// <param name="goods">Goods item for Goods table</param>
        void SaveGoods(Goods goods);

        /// <summary>
        /// Saving new or modified Order item into database
        /// </summary>
        /// /// <param name="order">Order item for Order table</param>
        void SaveOrder(Order order);

        /// <summary>
        /// Deleting Goods item by set id
        /// </summary>
        /// <param name="id">Id number in Goods table</param>
        void DeleteGoods(int id);

        /// <summary>
        /// Deleting Order item by set id
        /// </summary>
        /// <param name="id">Id number in Order table</param>
        void DeleteOrder(int id);

        /// <summary>
        /// Adding new Review item to Reviews database
        /// </summary>
        /// <param name="item">Item of Review type witch shold be saved</param>
        void AddReview(Review item);

        /// <summary>
        /// Deleting Review item by set id
        /// </summary>
        /// <param name="id">Id number in Reviews table</param>
        void DeleteReview(int id);

        /// <summary>
        /// Returns Goods item by set id
        /// </summary>
        /// <param name="id">Id number in Goods table</param>
        /// <returns></returns>
        Goods FindGoodsById(int id);
    }
}