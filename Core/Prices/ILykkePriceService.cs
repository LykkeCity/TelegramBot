using System.Threading.Tasks;

namespace Core.Prices
{
    public class LkkPrice
    {
        public double LkkUsdAsk { get; set; }
        public double LkkUsdBid { get; set; }
        public double BtcLkkAsk { get; set; }
        public double BtcLkkBid { get; set; }
    }

    public interface ILykkePriceService
    {
        Task<LkkPrice> GetLkkPrice();
    }
}
