using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Messages;
using Core.Settings;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class FaqCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly TelegramBotSettings _appSettings;

        public FaqCommand(IMessagesService messagesService,
            TelegramBotClient telegramBotClient, TelegramBotSettings appSettings)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
            _appSettings = appSettings;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.Faq;
                yield return BotCommands.FaqCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var msg = await _messagesService.GetFaqMsg(_appSettings.FaqUrl);

            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}
