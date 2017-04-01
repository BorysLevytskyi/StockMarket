using Akka.Actor;
using StockMarket.Model.Actors;
using StockMarket.Model.Messages;

namespace StockMarket.Model
{
    public class StockMarketSystem
    {
        private const string SystemName = "stock-market";

        private readonly ActorSystem _actorSystem;

        private StockMarketSystem(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
        }

        public static StockMarketSystem Create()
        {
            return new StockMarketSystem(ActorSystem.Create(SystemName));
        }

        public void CreateTrader(string id, string name)
        {
          _actorSystem.ActorOf<TraderFactoryActor>("trader")
                .Tell(new CreateTrader
                {
                    Id = id,
                    Name = name
                });
        }

        public void CreateExchange(string symbol)
        {
           _actorSystem.ActorOf<StockExchangeFactoryActor>("exchange")
                .Tell(new CreateExchange {Symbol = symbol});
        }

        public void TellTrader(string id, object message)
        {
             _actorSystem.ActorSelection(ActorPathResolver.ResolveTraderPath(id)).Tell(message, ActorRefs.NoSender);
        }

        public void TellExchange(string symbol, object message)
        {
            _actorSystem.ActorSelection(ActorPathResolver.ResolveExchangePath(symbol)).Tell(message, ActorRefs.NoSender);
        }
    }
}