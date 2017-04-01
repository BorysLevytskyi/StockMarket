using StockMarket.Model.Exchange;

namespace StockMarket.Model.Messages
{
    public class ProcessTransaction : ITransaction
    {
        public ProcessTransaction(ITransaction source)
        {
            BuyerTraderId = source.BuyerTraderId;
            SellerTraderId = source.SellerTraderId;
            Symbol = source.Symbol;
            PricePerShare = source.PricePerShare;
            Quantity = source.Quantity;
        }

        public string BuyerTraderId { get; }

        public string SellerTraderId { get; }

        public string Symbol { get; }

        public decimal PricePerShare { get; }

        public int Quantity { get; }

        public override string ToString()
        {
            return $"{nameof(BuyerTraderId)}: {BuyerTraderId}, {nameof(SellerTraderId)}: {SellerTraderId}, {nameof(Symbol)}: {Symbol}, {nameof(PricePerShare)}: {PricePerShare}, {nameof(Quantity)}: {Quantity}";
        }
    }
}