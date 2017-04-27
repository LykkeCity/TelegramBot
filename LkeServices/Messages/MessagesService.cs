using System.Globalization;
using System.Threading.Tasks;
using Core.Messages;

namespace LkeServices.Messages
{
    public class MessagesService : IMessagesService
    {
        private readonly IMessagesTemplatesRepository _messagesTemplatesRepository;

        public static class MsgTokens
        {
            public const string FirstName = "@[FirstName]";
            public const string LastName = "@[LastName]";

            public const string LkkUsdAsk = "@[LkkUsdAsk]";
            public const string LkkUsdBid = "@[LkkUsdBid]";
            public const string BtcLkkAsk = "@[BtcLkkAsk]";
            public const string BtcLkkBid = "@[BtcLkkBid]";

            public const string AndroidAppUrl = "@[AndroidAppUrl]";
            public const string IosAppUrl = "@[IosAppUrl]";

            public const string SupportMail = "@[SupportMail]";
        }

        public MessagesService(IMessagesTemplatesRepository messagesTemplatesRepository)
        {
            _messagesTemplatesRepository = messagesTemplatesRepository;
        }

        public async Task<string> GetWelcomeMsg(string firstName, string lastName)
        {
            var msg = await _messagesTemplatesRepository.GetWelcomeMsgTemplate();

            if (msg.Contains(MsgTokens.FirstName))
            {
                msg = msg.Replace(MsgTokens.FirstName, firstName);
            }

            if (msg.Contains(MsgTokens.LastName))
            {
                msg = msg.Replace(MsgTokens.LastName, lastName);
            }

            return msg;
        }

        public async Task<string> GetStartPrivateMsg()
        {
            return await _messagesTemplatesRepository.GetStartPrivateMsgTemplate();
        }

        public async Task<string> GetGroupMsg()
        {
            return await _messagesTemplatesRepository.GetStartGroupMsgTemplate();
        }

        public async Task<string> GetLkkPriceMsg(double lkkUsdAsk, double lkkUsdBid, double btcLkkAsk, double btcLkkBid)
        {
            var msg = await _messagesTemplatesRepository.GetLkkPriceMsgTemplate();

            if (msg.Contains(MsgTokens.LkkUsdAsk))
            {
                msg = msg.Replace(MsgTokens.LkkUsdAsk, lkkUsdAsk.ToString(CultureInfo.InvariantCulture));
            }

            if (msg.Contains(MsgTokens.LkkUsdBid))
            {
                msg = msg.Replace(MsgTokens.LkkUsdBid, lkkUsdBid.ToString(CultureInfo.InvariantCulture));
            }

            if (msg.Contains(MsgTokens.BtcLkkAsk))
            {
                msg = msg.Replace(MsgTokens.BtcLkkAsk, btcLkkAsk.ToString(CultureInfo.InvariantCulture));
            }

            if (msg.Contains(MsgTokens.BtcLkkBid))
            {
                msg = msg.Replace(MsgTokens.BtcLkkBid, btcLkkBid.ToString(CultureInfo.InvariantCulture));
            }

            return msg;
        }

        public async Task<string> GetAndroidAppMsg(string anroidAppUrl)
        {
            var msg = await _messagesTemplatesRepository.GetAndroidAppMsgTemplate();

            if (msg.Contains(MsgTokens.AndroidAppUrl))
            {
                msg = msg.Replace(MsgTokens.AndroidAppUrl, anroidAppUrl);
            }

            return msg;
        }

        public async Task<string> GetIosAppMsg(string iosAppUrl)
        {
            var msg = await _messagesTemplatesRepository.GetIosAppMsgTemplate();

            if (msg.Contains(MsgTokens.IosAppUrl))
            {
                msg = msg.Replace(MsgTokens.IosAppUrl, iosAppUrl);
            }

            return msg;
        }

        public async Task<string> GetSupportMailMsg(string supportEmail)
        {
            var msg = await _messagesTemplatesRepository.GetSupportMailMsgTemplate();

            if (msg.Contains(MsgTokens.SupportMail))
            {
                msg = msg.Replace(MsgTokens.SupportMail, supportEmail);
            }

            return msg;
        }
    }
}
