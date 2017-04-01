using System;
using Akka.Actor;
using StockMarket.Model.Exchange;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;
using StockMarket.Model.Messages.Events;

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

            var evt = new TransactionCompleted(transaction)
            {
                CratedAt = DateTimeOffset.Now,
                ProcessedAt = DateTimeOffset.Now
            };

            // Publish events to buyer and seller separately so the wouldn't have to subscribe to
            // the bus and consume all the transaction events
            // TODO: what if they do want to subribe to all TransactionCompleted events, 
            // in that case they will receive 2 messages
            Context.ActorSelection(ActorPathResolver.ResolveTraderPath(transaction.BuyerTraderId))
                .Tell(evt);

            Context.ActorSelection(ActorPathResolver.ResolveTraderPath(transaction.SellerTraderId))
                .Tell(evt);

            Context.System.EventStream.Publish(evt);
        }
    }
}
