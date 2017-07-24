using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Core.Prices;
using Core.Settings;

namespace LkeServices.Prices
{
    public class LykkeApiClient : ILykkeApiClient
    {
        private readonly TelegramBotSettings _settings;
        
        public LykkeApiClient(TelegramBotSettings settings)
        {
            _settings = settings;
        }

        public async Task<RatesModel> GetRates(string pair)
        {
            using (var httpClient = new HttpClient { BaseAddress = new Uri($"{_settings.PublicApiBaseUrl}") })
            {
                return (await httpClient.GetStringAsync($"api/AssetPairs/rate/{pair}"))
                    .DeserializeJson<RatesModel>();
            }
        }            
    }
}