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
        IEnumerable<Goods> GetGoods();

        /// <summary>
        /// returns all Categories from Categories table
        /// </summary>
        IEnumerable<Category> GetCategories();

        /// <summary>
        /// returns all Reviews from Reviews table
        /// </summary>
        IEnumerable<Review> Reviews { get; }

        /// <summary>
        /// Saving new Goods item into database
        /// </summary>
        /// /// /// <param name="goods">Goods item for Goods table</param>
        void AddGoods(Goods goods);

        /// <summary>
        /// Saving modified Goods item into database
        /// </summary>
        /// /// /// <param name="goods">Goods item for Goods table</param>
        void EditGoods(Goods goods);

        /// <summary>
        /// Deleting Goods item by set id
        /// </summary>
        /// <param name="id">Id number in Goods table</param>
        void DeleteGoods(int id);

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