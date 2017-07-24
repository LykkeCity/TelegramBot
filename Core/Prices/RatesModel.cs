namespace Core.Prices
{
    public class RatesModel
    {
        public double Ask { get; set; }
        public double Bid { get; set; }
    }
    
    public class InverseRatesModel
    {
        public double? Ask { get; set; }
        public double? Bid { get; set; }
    }
}