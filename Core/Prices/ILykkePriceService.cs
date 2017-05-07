using System.Threading.Tasks;

namespace Core.Prices
{
    public class LkkPrice
    {
        public double? LkkUsdAsk { get; set; }
        public double? LkkUsdBid { get; set; }
        public double? LkkBtcAsk { get; set; }
        public double? LkkBtcBid { get; set; }
    }

    public interface ILykkePriceService
    {
        Task<LkkPrice> GetLkkPrice();
    }
}
