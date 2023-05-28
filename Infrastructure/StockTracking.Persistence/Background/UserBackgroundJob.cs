using Hangfire;
using StockTracking.Application.Background;
using StockTracking.Domain.Entities.User;

namespace StockTracking.Persistence.Background
{
    public class UserBackgroundJob : BackgroundJob<User>, IUserBackgroundJob
    {
        public UserBackgroundJob(IBackgroundJobClient backgroundJobClient) : base(backgroundJobClient)
        {
        }
    }
}
