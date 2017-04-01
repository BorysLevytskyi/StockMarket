using Akka.Actor;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Actors
{
    public class StockExchangeFactoryActor : ReceiveActor, ILogMessages
    {
        public StockExchangeFactoryActor()
        {
            Receive<CreateExchange>(OnCreateExchange);
        }

        private void OnCreateExchange(CreateExchange createExchange)
        {
            var actor = Context.ActorOf(SymbolStockExchangeActor.Props(createExchange.Symbol), createExchange.Symbol.ToLower());
            this.Info($"created: {actor.Path}");
        }

        string ILogMessages.Source => Self.Path.ToString();
    }
}