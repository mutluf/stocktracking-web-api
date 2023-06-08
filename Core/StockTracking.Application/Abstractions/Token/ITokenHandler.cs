namespace StockTracking.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        DTOS.Token CreateAccess(int minute, string userId);
    }
}
