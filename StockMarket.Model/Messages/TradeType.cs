namespace StockMarket.Model.Messages
{
    public enum TradeType
    {
        Buy,
        Sell
    }

    public static class OrderTypeExtensions
    {
        public static TradeType CounterPartyType(this TradeType type)
        {
            return type == TradeType.Buy ? TradeType.Sell : TradeType.Buy;
        }
    }
}