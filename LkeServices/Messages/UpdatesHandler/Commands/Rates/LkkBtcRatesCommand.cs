using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Messages;
using Core.Prices;
using Core.Telegram;
using LkeServices.Prices;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands.Rates
{
    public class LkkBtcRatesCommand : IBotCommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly ILykkeApiClient _lykkeApiClient;
        private readonly IMessagesService _messagesService;

        public LkkBtcRatesCommand(TelegramBotClient telegramBotClient, ILykkeApiClient lykkeApiClient, IMessagesService messagesService)
        {
            _telegramBotClient = telegramBotClient;
            _lykkeApiClient = lykkeApiClient;
            _messagesService = messagesService;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.LkkBtc;
                yield return BotCommands.LkkBtcCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var rates = RatesConverter.Inverse(await _lykkeApiClient.GetRates("BTCLKK"));

            var msg = await _messagesService.GetRatesMsg(Constants.LKKBTC, rates.Bid, rates.Ask);

            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}