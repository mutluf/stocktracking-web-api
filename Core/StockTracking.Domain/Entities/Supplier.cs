namespace StockTracking.Domain.Entities
{
    public class Supplier:BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public ICollection<Product> Products{ get; set; }
    }
}
