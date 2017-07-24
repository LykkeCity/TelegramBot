using Common;
using Core.Prices;

namespace LkeServices.Prices
{
    public static class RatesConverter
    {
        private const int BtcAccuracy = 8;

        public static InverseRatesModel Inverse(RatesModel rates)
        {
            double? inverseBid = null;
            if (rates.Bid > double.Epsilon)
                inverseBid = (1 / rates.Bid).TruncateDecimalPlaces(BtcAccuracy);

            double? inverseAsk = null;
            if (rates.Ask > double.Epsilon)
                inverseAsk = (1 / rates.Ask).TruncateDecimalPlaces(BtcAccuracy);

            return new InverseRatesModel
            {
                Ask = inverseAsk,
                Bid = inverseBid
            };
        }
    }
}