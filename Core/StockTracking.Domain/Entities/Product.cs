namespace StockTracking.Domain.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public string BarcodeNumber { get; set; }
        public string Brand { get; set; }
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
        public Supplier? Supplier { get; set; }
        public int SupplierId { get; set; }
        public Category? Category { get; set; }
        public int DepotId { get; set; }
        public Depot? Depot { get; set; }

        public int CategoryId { get; set; }
        public double Price { get; set; }   
        public string Description { get; set; }
        
        public ICollection<StockMovement>? StockMovements { get; set; }
    }
}
