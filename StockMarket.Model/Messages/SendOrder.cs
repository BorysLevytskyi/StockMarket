using System;

namespace StockMarket.Model.Messages
{
    public class SendOrder : IOrder
    {
        public OrderType Type { get; set; }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerShare { get; set; }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}, {nameof(Symbol)}: {Symbol}, {nameof(Quantity)}: {Quantity}, {nameof(PricePerShare)}: {PricePerShare}";
        }
    }
}