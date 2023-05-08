

namespace StockTracking.Domain.Entities
{
    public class StockMovement:BaseEntity
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public Depot Depot { get; set; }
        public int DepotId { get; set; }
        public StockMovementType StockMovementType { get; set; }
        public int StockMovementTypeId { get; set; }
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
    }
}
