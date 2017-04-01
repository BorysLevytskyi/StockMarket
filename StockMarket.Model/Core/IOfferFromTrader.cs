namespace StockMarket.Model.Core
{
    public interface IOfferFromTrader : IOrder
    {
        string TraderId { get; }
    }
}