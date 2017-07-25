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
    public class ExchangeRatesCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;

        public ExchangeRatesCommand(IMessagesService messagesService, TelegramBotClient telegramBotClient)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
        }

        public IEnumerable<string> SupportedCommands
        {
            get { yield return BotCommands.ExchangeRates; }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    new KeyboardButton(BotCommands.BtcUsd),
                    new KeyboardButton(BotCommands.EthUsd)                    
                },
                new[]
                {                    
                    new KeyboardButton(BotCommands.EthBtc),
                    new KeyboardButton(BotCommands.LkkBtc)
                },
                new[]
                {
                    new KeyboardButton(BotCommands.Lkk1Ybtc),
                    new KeyboardButton(BotCommands.TimeBtc),                    
                },
                new[]
                {                    
                    new KeyboardButton(BotCommands.SlrBtc),
                    new KeyboardButton(BotCommands.Return)
                }
            }, true);

            var msg = await _messagesService.GetPairsMsg();

            await _telegramBotClient.SendTextMessageAsync(chatId, msg, ParseMode.Default, false, false, 0,
                keyboard);            
        }
    }
}