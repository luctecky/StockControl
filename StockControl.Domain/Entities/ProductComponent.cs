using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Domain.Entities
{
    public class ProductComponent
    {
        public int Id { get; set; }
        public int CompositeProductId { get; set; }
        public SimpleProduct SimpleProduct { get; set; }
        public int Quantity { get; set; }
    }
}
