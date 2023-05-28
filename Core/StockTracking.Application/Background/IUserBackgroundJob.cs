using StockTracking.Domain.Entities.User;

namespace StockTracking.Application.Background
{
    public interface IUserBackgroundJob:IBackgroundJob<User>
    {
    }
}
