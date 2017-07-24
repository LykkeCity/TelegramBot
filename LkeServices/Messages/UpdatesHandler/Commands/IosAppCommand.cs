using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Messages;
using Core.Settings;
using Core.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class IosAppCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly TelegramBotSettings _settings;

        public IosAppCommand(IMessagesService messagesService,
            TelegramBotClient telegramBotClient, TelegramBotSettings settings)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
            _settings = settings;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.IosApp;
                yield return BotCommands.IosAppCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var msg = await _messagesService.GetIosAppMsg(_settings.IosAppUrl);

            await _telegramBotClient.SendTextMessageAsync(chatId, msg, ParseMode.Default, false, false, 0, KeyBoards.MainKeyboard);
        }
    }
}
