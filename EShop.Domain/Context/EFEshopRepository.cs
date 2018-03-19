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
    /// <summary>
    /// Implementation of IEShopRepository
    /// </summary>
    public class EFEshopRepository : IEshopRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Goods> GetGoods()
        {
           return context.Goods.Include(g=>g.Reviews); 
        }

        public IEnumerable<Category> GetCategories()
        {
           return context.Categories;
        }

        public IEnumerable<Review> Reviews
        {
            get { return context.Reviews.Include(c=>c.Goods); }
        }

        public void EditGoods(Goods goods)
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
            context.SaveChanges();
        }

        public void AddGoods(Goods goods)
        {
            if (goods != null)
            {
                context.Goods.Add(goods);
                context.SaveChanges();
            }
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
