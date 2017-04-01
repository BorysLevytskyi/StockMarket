using StockMarket.Model.Core;

namespace StockMarket.Model.Messages
{
    public class SendOffer : IOrder
    {
        public OfferType Type { get; set; }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerShare { get; set; }

        public override string ToString()
        {
            return $"{nameof(Type)}: {Type}, {nameof(Symbol)}: {Symbol}, {nameof(Quantity)}: {Quantity}, {nameof(PricePerShare)}: {PricePerShare}";
        }
    }
}