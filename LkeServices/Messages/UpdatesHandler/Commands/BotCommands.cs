using System.Collections.Generic;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public static class BotCommands
    {
        public const string Start = "/start";

        public const string Faq = "FAQ \uD83D\uDCD6";
        public const string FaqCommand = "/faq";

        public const string GetApp = "Get app \u2B07";
        public const string GetAppCommand = "/getapp";
        public const string AndroidApp = "Android app";
        public const string AndroidAppCommand = "/androidapp";
        public const string IosApp = "Ios app";
        public const string IosAppCommand = "/iosapp";

        public const string ExchangeRates = "Rates \uD83D\uDCCA";        
        public const string BtcUsd = "BTCUSD";
        public const string BtcUsdCommand = "/btcusd";
        public const string EthUsd = "ETHUSD";
        public const string EthUsdCommand = "/ethusd";
        public const string EthBtc = "ETHBTC";
        public const string EthBtcCommand = "/ethbtc";
        public const string LkkBtc = "LKKBTC";
        public const string LkkBtcCommand = "/lkkbtc";
        public const string LkkUsd = "LKKUSD";
        public const string LkkUsdCommand = "/lkkusd";
        public const string Lkk1Ybtc = "LKK1YBTC";
        public const string Lkk1YbtcCommand = "/lkk1ybtc";
        public const string TimeBtc = "TIMEBTC";
        public const string TimeBtcCommand = "/timebtc";
        public const string SlrBtc = "SLRBTC";
        public const string SlrBtcCommand = "/slrbtc";

        public const string SupportMail = "Support \u2709";
        public const string SupportMailCommand = "/mailsupport";        

        public const string UserJoined = "UserJoined";
        public const string UserLeft = "UserLeft";

        public const string Return = "Return \u2934"; // "return" arrow

        public static IEnumerable<string> TextCommands
        {
            get
            {
                yield return Faq;
                yield return GetApp;
                yield return AndroidApp;
                yield return IosApp;                
                yield return ExchangeRates;
                yield return SupportMail;
                yield return BtcUsd;
                yield return EthUsd;
                yield return EthBtc;
                yield return LkkBtc;
                yield return LkkUsd;
                yield return Lkk1Ybtc;
                yield return TimeBtc;
                yield return SlrBtc;
                yield return Return;
            }
        }        
    }
}