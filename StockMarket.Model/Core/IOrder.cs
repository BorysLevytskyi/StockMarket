using StockMarket.Model.Messages;

namespace StockMarket.Model.Core
{
    public interface IOrder
    {
        OfferType Type { get; }

        string Symbol { get; }

        int Quantity { get; }

        decimal PricePerShare { get; }
    }
}