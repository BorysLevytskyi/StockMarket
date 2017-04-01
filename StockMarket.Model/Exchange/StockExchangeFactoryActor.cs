using Akka.Actor;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Exchange
{
    public class StockExchangeFactoryActor : ReceiveActor, ILogMessages
    {
        public StockExchangeFactoryActor()
        {
            Receive<CreateExchange>(o => OnCreateExchange(o));
        }

        private void OnCreateExchange(CreateExchange createExchange)
        {
            var actor = Context.ActorOf(SymbolStockExchangeActor.Props(createExchange.Symbol), createExchange.Symbol.ToLower());
            this.Info($"created: {actor.Path}");
        }

        string ILogMessages.LoggingSource => Self.Path.ToString();
    }
}