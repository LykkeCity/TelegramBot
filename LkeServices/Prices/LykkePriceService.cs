using System.Threading.Tasks;
using Core;
using Core.Prices;

namespace LkeServices.Prices
{
    public class LykkePriceService : ILykkePriceService
    {
        private readonly ILykkeApiClient _lykkeApiClient;

        public LykkePriceService(ILykkeApiClient lykkeApiClient)
        {
            _lykkeApiClient = lykkeApiClient;            
        }

        public async Task<LkkPrice> GetLkkPrice()
        {            
            var lkkUsd = await _lykkeApiClient.GetRates(Constants.LKKUSD);
            var lkkBtc = RatesConverter.Inverse(await _lykkeApiClient.GetRates(Constants.BTCLKK));                
            
            return new LkkPrice
            {
                LkkUsdAsk = lkkUsd.Ask,
                LkkBtcBid = lkkBtc.Bid,
                LkkBtcAsk = lkkBtc.Ask,
                LkkUsdBid = lkkUsd.Bid
            };
        }        
    }
}
