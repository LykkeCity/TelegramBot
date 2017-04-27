using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Core;
using Core.Prices;
using Core.Settings;

namespace LkeServices.Prices
{
    public class LykkePriceService : ILykkePriceService
    {
        private readonly TelegramBotSettings _settings;

        public LykkePriceService(TelegramBotSettings settings)
        {
            _settings = settings;
        }

        public async Task<LkkPrice> GetLkkPrice()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri($"{_settings.PublicApiBaseUrl}")};
            var lkkUsd =
                (await httpClient.GetStringAsync($"api/AssetPairs/rate/{Constants.LKKUSD}"))
                .DeserializeJson<RatesResponse>();
            var btcLkk =
                (await httpClient.GetStringAsync($"api/AssetPairs/rate/{Constants.BTCLKK}"))
                .DeserializeJson<RatesResponse>();

            return new LkkPrice
            {
                LkkUsdAsk = lkkUsd.Ask,
                BtcLkkBid = btcLkk.Bid,
                BtcLkkAsk = btcLkk.Ask,
                LkkUsdBid = lkkUsd.Bid
            };
        }

        public class RatesResponse
        {
            public double Ask { get; set; }
            public double Bid { get; set; }
        }
    }
}
