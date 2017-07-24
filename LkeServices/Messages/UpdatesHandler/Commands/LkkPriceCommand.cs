﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Messages;
using Core.Prices;
using Core.Telegram;
using Telegram.Bot;

namespace LkeServices.Messages.UpdatesHandler.Commands
{
    public class LkkPriceCommand : IBotCommand
    {
        private readonly IMessagesService _messagesService;
        private readonly TelegramBotClient _telegramBotClient;
        private readonly ILykkePriceService _lykkePriceService;

        public LkkPriceCommand(IMessagesService messagesService,
            TelegramBotClient telegramBotClient, ILykkePriceService lykkePriceService)
        {
            _messagesService = messagesService;
            _telegramBotClient = telegramBotClient;
            _lykkePriceService = lykkePriceService;
        }

        public IEnumerable<string> SupportedCommands
        {
            get
            {
                yield return BotCommands.LkkPrice;
                yield return BotCommands.LkkPriceCommand;
            }
        }

        public async Task ExecuteCommand(string chatId, User userJoined, User userLeft)
        {
            var prices = await _lykkePriceService.GetLkkPrice();
            var msg = await _messagesService.GetLkkPriceMsg(prices.LkkUsdAsk, prices.LkkUsdBid, prices.LkkBtcAsk, prices.LkkBtcBid);

            await _telegramBotClient.SendTextMessageAsync(chatId, msg);
        }
    }
}
