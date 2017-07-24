using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.Messages;
using Core.Prices;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands.Rates
{
    public class SlrBtcRatesCommand : IBotCommand
    {
        private readonly TelegramBotClient _telegramBotClient;
        private readonly ILykkeApiClient _lykkeApiClient;
        private readonly IMessagesService _messagesService;

        public SlrBtcRatesCommand(TelegramBotClient telegramBotClient, ILykkeApiClient lykkeApiClient, IMessagesService messagesService)
        {
            _telegramBotClient = telegramBotClient;
            _lykkeApiClient = lykkeApiClient;
            _messagesService = messagesService;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.SlrBtc;
                yield return BotCommands.SlrBtcCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var rates = await _lykkeApiClient.GetRates(Constants.SLRBTC);

            var msg = await _messagesService.GetRatesMsg(Constants.SLRBTC, rates.Bid, rates.Ask);

            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}