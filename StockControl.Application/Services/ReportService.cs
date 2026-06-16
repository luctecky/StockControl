using StockControl.Application.Reports;
using StockControl.Domain.Entities;
using StockControl.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Application.Services
{
    public class ReportService
    {
        private readonly IRequisitionRepository _requisitionRepository;

        public ReportService(IRequisitionRepository requisitionRepository)
        {
            _requisitionRepository = requisitionRepository;
        }

        //--------------------------------------------------------------------------
        //REPORT 1: Requisition report
        //Show all products (Simple + compisite) requisitioned
        //Group by product and show total quantity requisitioned for each product
        //--------------------------------------------------------------------------

        public IEnumerable<RequisitionReportItem> GetRequisitionsReport(
            DateTime startDate,
            DateTime endDate)
        {
            ValidatePeriod(startDate, endDate);

            var requisitions = _requisitionRepository.GetByPeriod(startDate, endDate);
            var result       = new Dictionary<string, RequisitionReportItem>();

            foreach (var requisition in requisitions)
            {
                foreach (var item in requisition.Items)
                {
                    var key = item.Product.Name;

                    if (!result.ContainsKey(key))
                    {
                        result[key] = new RequisitionReportItem
                        {
                            ProductName = item.Product.Name,
                            TotalQuantity   = 0,
                            TotalCost       = 0,
                            TotalSalePrice  = 0
                        };
                    }

                    result[key].TotalQuantity   += item.Quantity;
                    result[key].TotalCost       += item.Quantity * item.Product.GetCostPrice();
                    result[key].TotalSalePrice  += item.Quantity * item.Product.SalePrice;
                }
            }

            return result.Values.OrderBy(r => r.ProductName);
        }

        // ─────────────────────────────────────────────────
        // REPORT 2: Stock Output Report
        // Shows ONLY simple products withdrawn from stock
        // When a composite product is requisitioned,
        // expands its components as individual simple products
        // ─────────────────────────────────────────────────

        public IEnumerable<StockOutputReportItem> GetStockOutputReport(
            DateTime startDate,
            DateTime endDate)
        {
            ValidatePeriod(startDate, endDate);

            var requisitions = _requisitionRepository.GetByPeriod(startDate, endDate);
            var result = new Dictionary<string, StockOutputReportItem>();

            foreach (var requisition in requisitions)
            {
                foreach (var item in requisition.Items)
                {
                    // Simple product → add directly
                    if (item.Product is SimpleProduct simple)
                    {
                        AddStockOutputItem(result, simple.Name, item.Quantity, simple.CostPrice);
                    }

                    // Composite product → expand into its simple components
                    else if (item.Product is CompositeProduct composite)
                    {
                        foreach (var component in composite.Components)
                        {
                            var totalQty = component.Quantity * item.Quantity;
                            AddStockOutputItem(
                                result,
                                component.SimpleProduct.Name,
                                totalQty,
                                component.SimpleProduct.CostPrice);
                        }
                    }
                }
            }

            return result.Values.OrderBy(r => r.ProductName);
        }

        private void AddStockOutputItem(
            Dictionary<string, StockOutputReportItem> result,
            string productName,
            int quantity,
            decimal costPrice)
        {
            if (!result.ContainsKey(productName))
            {
                result[productName] = new StockOutputReportItem
                {
                    ProductName = productName,
                    TotalQuantity = 0,
                    TotalCost = 0
                };
            }

            result[productName].TotalQuantity += quantity;
            result[productName].TotalCost += costPrice * quantity;
        }

        private void ValidatePeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new Exception("Start date cannot be greater than end date.");

            if (startDate > DateTime.Today)
                throw new Exception("Start date cannot be in the future.");
        }
    }
}
