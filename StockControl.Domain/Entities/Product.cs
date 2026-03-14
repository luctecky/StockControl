namespace StockControl.Domain.Entities
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal SalePrice { get; set; }

        public abstract decimal GetCostPrice();
    }
}
