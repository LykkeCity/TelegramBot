using System.Threading.Tasks;

namespace Core.Messages
{
    public interface IMessagesTemplatesRepository
    {
        Task<string> GetWelcomeMsgTemplate();
        Task<string> GetStartPrivateMsgTemplate();
        Task<string> GetStartGroupMsgTemplate();
        Task<string> GetLkkPriceMsgTemplate();
        Task<string> GetAndroidAppMsgTemplate();
        Task<string> GetIosAppMsgTemplate();
        Task<string> GetSupportMailMsgTemplate();
    }
}
