using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Event;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Actors
{
    public class SymbolStockExchangeActor : ReceiveActor, ILogMessages
    {
        private readonly string _symbol;

        public SymbolStockExchangeActor(string symbol)
        {
            _symbol = symbol;

            Receive<OrderPlaced>(o => OnOrderPlaced(o));
            ReceiveAny(m => this.Info($"received: {m}", ConsoleColor.Blue));
        }

        private void OnOrderPlaced(OrderPlaced orderPlaced)
        {
            this.Info($"received: {orderPlaced.GetType().Name} {orderPlaced}", ConsoleColor.Blue);
        }

        public static Props Props(string symbol)
        {
            return Akka.Actor.Props.Create(() => new SymbolStockExchangeActor(symbol));
        }

        public string Source => Self.Path.ToString();
    }
}