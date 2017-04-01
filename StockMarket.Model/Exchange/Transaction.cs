using StockMarket.Model.Core;

namespace StockMarket.Model.Exchange
{
    public class Transaction : ITransaction
    {
        public string BuyerTraderId { get; set; }

        public string SellerTraderId { get; set; }

        public string Symbol { get; set; }

        public decimal PricePerShare { get; set; }

        public int Quantity { get; set; }

        public override string ToString()
        {
            return $"{nameof(BuyerTraderId)}: {BuyerTraderId}, {nameof(SellerTraderId)}: {SellerTraderId}, {nameof(Symbol)}: {Symbol}, {nameof(PricePerShare)}: {PricePerShare}, {nameof(Quantity)}: {Quantity}";
        }
    }
}