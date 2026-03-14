using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Domain.Entities
{
    public class CompositeProduct : Product
    {
        public List<ProductComponent> Components { get; set; } = new List<ProductComponent>();
        public override decimal GetCostPrice()
        {
            return Components.Sum(c => c.SimpleProduct.GetCostPrice() * c.Quantity);
        }
    }
}
