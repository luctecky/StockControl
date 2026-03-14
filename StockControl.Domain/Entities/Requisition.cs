using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Domain.Entities
{
    public class Requisition
    {
        public int Id { get; set; }
        public DateTime WithdrawalDate { get; set; }
        public string ResponsibleEmployee { get; set; }
        public List<RequisitionItem> Items { get; set; } = new List<RequisitionItem>();

        public decimal GetTotalCost()
        {
            decimal total = 0;
            foreach (var item in Items)
                total += item.Product.GetCostPrice() * item.Quantity;
            return total;
        }
    }
}
