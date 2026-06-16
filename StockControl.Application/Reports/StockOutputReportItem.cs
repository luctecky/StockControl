using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Application.Reports
{
    public class StockOutputReportItem
    {
        public string ProductName { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }

    }
}
