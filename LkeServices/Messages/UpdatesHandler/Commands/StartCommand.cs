using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Messages;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using User = Core.Telegram.User;

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

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.Start;
                yield return BotCommands.Return;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {            
            var msg = await _messagesService.GetStartPrivateMsg();
            
            await _telegramBotClient.SendTextMessageAsync(chatId, msg, ParseMode.Default, false, false, 0, KeyBoards.MainKeyboard);
        }
    }
}
