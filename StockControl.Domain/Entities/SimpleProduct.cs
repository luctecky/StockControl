using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Domain.Entities
{
    public class SimpleProduct : Product
    {
        public decimal CostPrice { get; set; }
        public override decimal GetCostPrice()
        {
            return CostPrice;
        }
    }
}
