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
        private const int BtcAccuracy = 8;

        public LykkePriceService(TelegramBotSettings settings)
        {
            _settings = settings;
        }

        public async Task<LkkPrice> GetLkkPrice()
        {
            var httpClient = new HttpClient { BaseAddress = new Uri($"{_settings.PublicApiBaseUrl}") };
            var lkkUsd =
                (await httpClient.GetStringAsync($"api/AssetPairs/rate/{Constants.LKKUSD}"))
                .DeserializeJson<RatesResponse>();
            var btcLkk =
                (await httpClient.GetStringAsync($"api/AssetPairs/rate/{Constants.BTCLKK}"))
                .DeserializeJson<RatesResponse>();

            double? btcBid = null;
            if (btcLkk.Bid > double.Epsilon)
                btcBid = (1 / btcLkk.Bid).TruncateDecimalPlaces(BtcAccuracy);

            double? btcAsk = null;
            if (btcLkk.Ask > double.Epsilon)
                btcAsk = (1 / btcLkk.Ask).TruncateDecimalPlaces(BtcAccuracy);

            return new LkkPrice
            {
                LkkUsdAsk = lkkUsd.Ask,
                LkkBtcBid = btcBid,
                LkkBtcAsk = btcAsk,
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
