using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public Cart(List<CartLine> line)
        {
            lineCollection = line;
        }

        public Cart()
        {

        }

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

        public void RemoveLine(Goods goods)
        {
            lineCollection.RemoveAll(l => l.Goods.Id == goods.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Goods.Price * e.Quantity);

        }
        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }
}
