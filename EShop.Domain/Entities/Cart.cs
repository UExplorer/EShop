using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities
{
    /// <summary>
    /// Class responsible for creating and manipulating Cart for current Session.
    /// </summary>
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        /// <summary>
        /// Constructor, witch created Cart object with existing CartLines
        /// </summary>
        /// <param name="line">List of CartLine objects. Set existing CartLines in the Cart</param>
        public Cart(List<CartLine> line)
        {
            lineCollection = line;
        }


        public Cart()
        {

        }

        /// <summary>
        /// Add new Goods item to the Cart with some Quantity
        /// </summary>
        /// <param name="goods">Goods type item</param>
        /// <param name="quantity">numeric quantity of adding items</param>
        public void AddItem(Goods goods, int quantity)
        {
            CartLine line = lineCollection
                .FirstOrDefault(g=>g.Goods.Id==goods.Id);

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Goods = goods,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        /// <summary>
        /// Removes current Goods items from Cart
        /// </summary>
        /// <param name="goods">Goods item, wich shold be removed</param>
        public void RemoveLine(Goods goods)
        {
            lineCollection.RemoveAll(l => l.Goods.Id == goods.Id);
        }

        /// <summary>
        /// Calculating current total price of all items in Cart
        /// </summary>
        /// <returns>Price of all times in Cart</returns>
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Goods.Price * e.Quantity);

        }

        /// <summary>
        /// Clears Cart from all Goods in it
        /// </summary>
        public void Clear()
        {
            lineCollection.Clear();
        }

        /// <summary>
        /// returns current collection of Goods in Cart
        /// </summary>
        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
}
