using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Messages;
using Core.Settings;
using Core.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

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

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.AndroidApp;
                yield return BotCommands.AndroidAppCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {            
            var msg = await _messagesService.GetAndroidAppMsg(_settings.AndroidAppUrl);
            await _telegramBotClient.SendTextMessageAsync(chatId, msg, ParseMode.Default, false, false, 0, KeyBoards.MainKeyboard);                        
        }
    }
}
