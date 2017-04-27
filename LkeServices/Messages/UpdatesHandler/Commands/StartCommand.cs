using System.Threading.Tasks;
using Core.Messages;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class StartCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;

        public StartCommand(IMessagesService messagesService,
            TelegramBotClient telegramBotClient)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
        }

        public async Task ExecuteCommand(bool isGroup, string chatId, User userJoined, User userLeft)
        {
            var msg = isGroup
                ? await _messagesService.GetGroupMsg()
                : await _messagesService.GetStartPrivateMsg();
            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}
