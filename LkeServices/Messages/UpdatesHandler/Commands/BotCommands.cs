using System.Collections.Generic;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public static class BotCommands
    {
        public const string Start = "/start";

        public const string Faq = "FAQ";
        public const string FaqCommand = "/faq";

        public const string GetApp = "Get app";
        public const string GetAppCommand = "/getapp";
        public const string AndroidApp = "Android app";
        public const string AndroidAppCommand = "/androidapp";
        public const string IosApp = "Ios app";
        public const string IosAppCommand = "/iosapp";

        public const string LkkPrice = "Current LKK price";
        public const string LkkPriceCommand = "/lkkprice";

        public const string ExchangeRates = "Exchange rates";        
        public const string BtcUsd = "BTCUSD";
        public const string BtcUsdCommand = "/btcusd";
        public const string EthUsd = "ETHUSD";
        public const string EthUsdCommand = "/ethusd";
        public const string EthBtc = "ETHBTC";
        public const string EthBtcCommand = "/ethbtc";
        public const string LkkBtc = "LKKBTC";
        public const string LkkBtcCommand = "/lkkbtc";
        public const string Lkk1Ybtc = "LKK1YBTC";
        public const string Lkk1YbtcCommand = "/lkk1ybtc";
        public const string TimeBtc = "TIMEBTC";
        public const string TimeBtcCommand = "/timebtc";
        public const string SlrBtc = "SLRBTC";
        public const string SlrBtcCommand = "/slrbtc";

        public const string SupportMail = "Lykke support";
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
                yield return LkkPrice;
                yield return ExchangeRates;
                yield return SupportMail;
                yield return BtcUsd;
                yield return EthUsd;
                yield return EthBtc;
                yield return LkkBtc;
                yield return Lkk1Ybtc;
                yield return TimeBtc;
                yield return SlrBtc;
                yield return Return;
            }
        }        
    }
}