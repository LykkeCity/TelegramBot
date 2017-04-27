using System.Threading.Tasks;

namespace Core.Messages
{
    public interface IMessagesService
    {
        Task<string> GetWelcomeMsg(string firstName, string lastName);
        Task<string> GetStartPrivateMsg();
        Task<string> GetGroupMsg();
        Task<string> GetLkkPriceMsg(double lkkUsdAsk, double lkkUsdBid, double btcLkkAsk, double btcLkkBid);
        Task<string> GetAndroidAppMsg(string anroidAppUrl);
        Task<string> GetIosAppMsg(string iosAppUrl);
        Task<string> GetSupportMailMsg(string supportEmail);
    }
}
