
namespace StockMarket.Model.Core
{
    public interface ITransaction
    {
        string BuyerTraderId { get; }

        string SellerTraderId { get; }

        string Symbol { get; }

        decimal PricePerShare { get; }

        int Quantity { get; }
    }
}