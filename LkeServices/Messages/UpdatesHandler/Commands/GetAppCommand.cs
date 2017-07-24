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
    public class GetAppCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;

        public GetAppCommand(IMessagesService messagesService, TelegramBotClient telegramBotClient)
        {
            this._messagesService = messagesService;
            this._telegramBotClient = telegramBotClient;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.GetApp;
                yield return BotCommands.GetAppCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton(BotCommands.AndroidApp),
                new KeyboardButton(BotCommands.IosApp),
                new KeyboardButton(BotCommands.Return)
            }, true);

            var msg = await _messagesService.GetAppMsg();                

            await _telegramBotClient.SendTextMessageAsync(chatId, msg, ParseMode.Default, false, false, 0, keyboard);
        }
    }
}