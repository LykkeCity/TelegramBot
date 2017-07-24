using System.Threading.Tasks;

namespace Core.Messages
{
    public interface IMessagesService
    {
        Task<string> GetWelcomeMsg(string firstName, string lastName);
        Task<string> GetStartPrivateMsg();
        Task<string> GetGroupMsg();
        Task<string> GetLkkPriceMsg(double? lkkUsdAsk, double? lkkUsdBid, double? lkkBtcAsk, double? lkkBtcBid);
        Task<string> GetAndroidAppMsg(string anroidAppUrl);
        Task<string> GetIosAppMsg(string iosAppUrl);
        Task<string> GetSupportMailMsg(string supportEmail);
        Task<string> GetFaqMsg(string faqUrl);
        Task<string> GetAppMsg();
        Task<string> GetRatesMsg(string pair, double? bid, double? ask);
        Task<string> GetPairsMsg();
    }
}
