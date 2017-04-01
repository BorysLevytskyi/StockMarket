using System;
using StockMarket.Model.Core;

namespace StockMarket.Model.Messages.Events
{
    public class TransactionCompleted : ITransaction, IMarketEvent
    {
        public TransactionCompleted()
        {
        }

        public TransactionCompleted(ITransaction transaction)
        {
            BuyerTraderId = transaction.BuyerTraderId;
            SellerTraderId = transaction.SellerTraderId;
            Symbol = transaction.Symbol;
            PricePerShare = transaction.PricePerShare;
            Quantity = transaction.Quantity;
        }

        public string BuyerTraderId { get; set; }

        public string SellerTraderId { get; set; }

        public string Symbol { get; set; }

        public decimal PricePerShare { get; set; }

        public int Quantity { get; set; }

        public DateTimeOffset CratedAt { get; set; }

        public DateTimeOffset ProcessedAt { get; set; }
    }
}
