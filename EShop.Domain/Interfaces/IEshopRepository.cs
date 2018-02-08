using System.Collections.Generic;
using EShop.Domain.Entities;

namespace EShop.Domain.Interfaces
{
    public interface IEshopRepository
    {
        IEnumerable<Goods> Goods { get; }
        IEnumerable<Category> Categories { get; }
        IEnumerable<Order> Orders { get; }
        IEnumerable<CartLine> CartLines { get; }
        IEnumerable<Status> Statuses { get; }
        IEnumerable<Shipment> Shipments { get; }
        IEnumerable<Review> Reviews { get; }
        void SaveGoods(Goods goods);
        void SaveOrder(Order order);
        void DeleteGoods(int id);
        void DeleteOrder(int id);
        void AddReview(Review item);
        void DeleteReview(int id);
        Goods FindGoodsById(int id);
    }
}