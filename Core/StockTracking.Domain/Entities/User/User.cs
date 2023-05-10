using Microsoft.AspNetCore.Identity;

namespace StockTracking.Domain.Entities.User
{
    public class User:IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
