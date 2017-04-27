using System.Threading.Tasks;
using Core.Messages;
using Core.Settings;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class SupportMailCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly TelegramBotSettings _appSettings;

        public SupportMailCommand(IMessagesService messagesService,
            TelegramBotClient telegramBotClient, TelegramBotSettings appSettings)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
            _appSettings = appSettings;
        }

        public async Task ExecuteCommand(bool isGroup, string chatId, User userJoined, User userLeft)
        {
            var msg = isGroup
                ? await _messagesService.GetGroupMsg()
                : await _messagesService.GetSupportMailMsg(_appSettings.SupportMail);
                await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}
