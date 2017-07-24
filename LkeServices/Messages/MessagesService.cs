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

            public const string Pair = "@[Pair]";
            public const string PairAsk = "@[PairAsk]";
            public const string PairBid = "@[PairBid]";

            public const string LkkUsdAsk = "@[LkkUsdAsk]";
            public const string LkkUsdBid = "@[LkkUsdBid]";
            public const string LkkBtcAsk = "@[LkkBtcAsk]";
            public const string LkkBtcBid = "@[LkkBtcBid]";

            public const string AndroidAppUrl = "@[AndroidAppUrl]";
            public const string IosAppUrl = "@[IosAppUrl]";

            public const string SupportMail = "@[SupportMail]";

            public const string FaqUrl = "@[FaqUrl]";
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

        public async Task<string> GetRatesMsg(string pair, double? bid, double? ask)
        {
            var msg = await _messagesTemplatesRepository.GetRatesMsgTemplate();

            if (msg.Contains(MsgTokens.Pair))
            {
                msg = msg.Replace(MsgTokens.Pair, pair);
            }

            if (msg.Contains(MsgTokens.PairBid))
            {
                msg = msg.Replace(MsgTokens.PairBid, bid?.ToString("0.########") ?? "-");
            }

            if (msg.Contains(MsgTokens.PairAsk))
            {
                msg = msg.Replace(MsgTokens.PairAsk, ask?.ToString("0.########") ?? "-");
            }

            return msg;
        }

        public async Task<string> GetPairsMsg()
        {
            return await _messagesTemplatesRepository.GetPairsMsgTemplate();
        }

        public async Task<string> GetLkkPriceMsg(double? lkkUsdAsk, double? lkkUsdBid, double? lkkBtcAsk, double? lkkBtcBid)
        {
            var msg = await _messagesTemplatesRepository.GetLkkPriceMsgTemplate();

            if (msg.Contains(MsgTokens.LkkUsdAsk))
            {
                msg = msg.Replace(MsgTokens.LkkUsdAsk, lkkUsdAsk?.ToString("0.########") ?? "-");
            }

            if (msg.Contains(MsgTokens.LkkUsdBid))
            {
                msg = msg.Replace(MsgTokens.LkkUsdBid, lkkUsdBid?.ToString("0.########") ?? "-");
            }

            if (msg.Contains(MsgTokens.LkkBtcAsk))
            {
                msg = msg.Replace(MsgTokens.LkkBtcAsk, lkkBtcAsk?.ToString("0.########") ?? "-");
            }

            if (msg.Contains(MsgTokens.LkkBtcBid))
            {
                msg = msg.Replace(MsgTokens.LkkBtcBid, lkkBtcBid?.ToString("0.########") ?? "-");
            }

            return msg;
        }

        public async Task<string> GetAppMsg()
        {
            return await _messagesTemplatesRepository.GetAppMsgTemplate();            
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

        public async Task<string> GetFaqMsg(string faqUrl)
        {
            var msg = await _messagesTemplatesRepository.GetFaqMsgTemplate();

            if (msg.Contains(MsgTokens.FaqUrl))
            {
                msg = msg.Replace(MsgTokens.FaqUrl, faqUrl);
            }

            return msg;
        }
    }
}
