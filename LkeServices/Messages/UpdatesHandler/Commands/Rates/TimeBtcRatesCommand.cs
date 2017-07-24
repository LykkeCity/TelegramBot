using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Messages;
using Core.Prices;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands.Rates
{
    public class TimeBtcRatesCommand : IBotCommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly ILykkeApiClient _lykkeApiClient;
        private readonly IMessagesService _messagesService;

        public TimeBtcRatesCommand(TelegramBotClient telegramBotClient, ILykkeApiClient lykkeApiClient, IMessagesService messagesService)
        {
            _telegramBotClient = telegramBotClient;
            _lykkeApiClient = lykkeApiClient;
            _messagesService = messagesService;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.TimeBtc;
                yield return BotCommands.TimeBtcCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var rates = await _lykkeApiClient.GetRates(Constants.TIMEBTC);

            var msg = await _messagesService.GetRatesMsg(Constants.TIMEBTC, rates.Bid, rates.Ask);

            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}