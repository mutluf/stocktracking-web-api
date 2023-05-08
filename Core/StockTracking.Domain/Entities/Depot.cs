namespace StockTracking.Domain.Entities
{
    public class Depot:BaseEntity
    {
        public string DepotName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Product> Products { get; set; } 
        public ICollection<StockMovement> StockMovements { get; set;}
    }
}
