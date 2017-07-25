using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public static class KeyBoards
    {
        public static readonly ReplyKeyboardMarkup MainKeyboard = new ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                new KeyboardButton(BotCommands.Faq),                
                new KeyboardButton(BotCommands.GetApp),                
            },
            new[]
            {
                new KeyboardButton(BotCommands.ExchangeRates),
                new KeyboardButton(BotCommands.SupportMail)
            }
        }, true);
    }
}