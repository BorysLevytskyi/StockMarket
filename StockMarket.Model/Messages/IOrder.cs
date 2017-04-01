namespace StockMarket.Model.Messages
{
    public interface IOrder
    {
        OrderType Type { get; set; }
        string Symbol { get; set; }
        int Quantity { get; set; }
        decimal PricePerShare { get; set; }
    }
}