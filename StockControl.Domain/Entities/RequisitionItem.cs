namespace StockControl.Domain.Entities
{
    public class RequisitionItem
    {
        public int Id { get; set; }
        public int RequisitionId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public decimal GetTotalCost()
        {
            return Product.GetCostPrice() * Quantity;
        }
    }
}