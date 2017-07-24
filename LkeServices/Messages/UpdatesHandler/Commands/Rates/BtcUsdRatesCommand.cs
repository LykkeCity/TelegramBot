using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Messages;
using Core.Prices;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands.Rates
{
    public class BtcUsdRatesCommand : IBotCommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly ILykkeApiClient _lykkeApiClient;
        private readonly IMessagesService _messagesService;

        public BtcUsdRatesCommand(TelegramBotClient telegramBotClient, ILykkeApiClient lykkeApiClient, IMessagesService messagesService)
        {
            _telegramBotClient = telegramBotClient;
            _lykkeApiClient = lykkeApiClient;
            _messagesService = messagesService;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.BtcUsd;
                yield return BotCommands.BtcUsdCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var rates = await _lykkeApiClient.GetRates(Constants.BTCUSD);

            var msg = await _messagesService.GetRatesMsg(Constants.BTCUSD, rates.Bid, rates.Ask);

            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}