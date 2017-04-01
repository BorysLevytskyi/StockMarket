namespace StockMarket.Model.Messages
{
    public enum OfferType
    {
        Buy,
        Sell
    }

    public static class OrderTypeExtensions
    {
        public static OfferType CounterPartyType(this OfferType type)
        {
            return type == OfferType.Buy ? OfferType.Sell : OfferType.Buy;
        }
    }
}