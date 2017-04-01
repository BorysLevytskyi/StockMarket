using StockMarket.Model.Messages;

namespace StockMarket.Model.Core
{
    public interface IOrder
    {
        TradeType Type { get; }

        string Symbol { get; }

        int Quantity { get; }

        decimal PricePerShare { get; }
    }
}