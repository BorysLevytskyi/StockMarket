using StockMarket.Model.Core;

namespace StockMarket.Model.Messages
{
    public class OfferPlaced : IOfferFromTrader
    {
        public string TraderId { get; set; }

        public TradeType Type { get; set; }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public decimal PricePerShare { get; set; }

        public override string ToString()
        {
            return $"{TraderId}: {Type} {Quantity} shares {Symbol} at {PricePerShare:c} per share";
        }
    }
}