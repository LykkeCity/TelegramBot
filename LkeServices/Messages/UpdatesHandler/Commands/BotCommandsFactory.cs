using System;
using System.Threading.Tasks;
using Core.Telegram;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public interface IBotCommand
    {
        Task ExecuteCommand(bool isGroup, string chatId, User userJoined, User userLeft);
    }

    public class BotCommandsFactory
    {
        private readonly StartCommand _startCommand;
        private readonly LkkPriceCommand _lkkPriceCommand;
        private readonly AndroidAppCommand _androidAppCommand;
        private readonly IosAppCommand _iosAppCommand;
        private readonly SupportMailCommand _supportMailCommand;
        private readonly UserJoinedCommand _userJoinedCommand;
        private readonly UserLeftCommand _userLeftCommand;
        private readonly FaqCommand _faqCommand;
        public const string Start = "/start";
        public const string LkkPrice = "/lkkprice";
        public const string AndroidApp = "/androidapp";
        public const string IosApp = "/iosapp";
        public const string SupportMail = "/mailsupport";
        public const string Faq = "/faq";

        public const string UserJoined = "UserJoined";
        public const string UserLeft = "UserLeft";

        public BotCommandsFactory(StartCommand startCommand,
            LkkPriceCommand lkkPriceCommand, AndroidAppCommand androidAppCommand,
            IosAppCommand iosAppCommand, SupportMailCommand supportMailCommand,
            UserJoinedCommand userJoinedCommand, UserLeftCommand userLeftCommand,
            FaqCommand faqCommand)
        {
            _startCommand = startCommand;
            _lkkPriceCommand = lkkPriceCommand;
            _androidAppCommand = androidAppCommand;
            _iosAppCommand = iosAppCommand;
            _supportMailCommand = supportMailCommand;
            _userJoinedCommand = userJoinedCommand;
            _userLeftCommand = userLeftCommand;
            _faqCommand = faqCommand;
        }

        public IBotCommand GetCommand(string botCommand = null)
        {
            switch (botCommand)
            {
                case Start:
                    return _startCommand;
                case LkkPrice:
                    return _lkkPriceCommand;
                case AndroidApp:
                    return _androidAppCommand;
                case IosApp:
                    return _iosAppCommand;
                case SupportMail:
                    return _supportMailCommand;
                case UserJoined:
                    return _userJoinedCommand;
                case UserLeft:
                    return _userLeftCommand;
                case Faq:
                    return _faqCommand;
            }

            return null;
        }
    }
}
