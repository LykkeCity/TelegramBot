using System.Threading.Tasks;
using Core.Messages;
using Core.Settings;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class AndroidAppCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly TelegramBotSettings _settings;

        public AndroidAppCommand(IMessagesService messagesService,
            TelegramBotClient telegramBotClient, TelegramBotSettings settings)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
            _settings = settings;
        }

        public async Task ExecuteCommand(bool isGroup, string chatId, User userJoined, User userLeft)
        {
            var msg = isGroup
                ? await _messagesService.GetGroupMsg()
                : await _messagesService.GetAndroidAppMsg(_settings.AndroidAppUrl);
            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}
