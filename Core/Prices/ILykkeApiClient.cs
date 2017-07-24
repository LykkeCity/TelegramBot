using System.Threading.Tasks;

namespace Core.Prices
{
    public interface ILykkeApiClient
    {
        Task<RatesModel> GetRates(string pair);
    }
}