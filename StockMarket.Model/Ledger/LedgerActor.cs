using System;
using Akka.Actor;
using StockMarket.Model.Exchange;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Ledger
{
    public class LedgerActor : ReceiveActor, ILogMessages
    {
        public string LoggingSource => Self.Path.ToString();

        public LedgerActor()
        {
            Receive<ProcessTransaction>(t => OnProcessTransaction(t));
        }

        private void OnProcessTransaction(ITransaction transaction)
        {
            this.Info($"Transaction: {transaction.SellerTraderId} sells to {transaction.BuyerTraderId} {transaction.Quantity} of {transaction.Symbol} shares at {transaction.PricePerShare:c} price per share for {transaction.Quantity * transaction.PricePerShare:c} total", ConsoleColor.Cyan);
        }
    }
}
