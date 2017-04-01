using System;
using System.Threading;
using Akka.Actor;
using StockMarket.Model.Exchange;
using StockMarket.Model.Ledger;
using StockMarket.Model.Messages;
using StockMarket.Model.Traders;

namespace StockMarket.Model
{
    public class StockMarketSystem
    {
        private const string SystemName = "stock-market";

        private readonly ActorSystem _actorSystem;

        private Lazy<IActorRef> TraderFactory { get; }

        private Lazy<IActorRef> ExchangeFactory { get; }

        private StockMarketSystem(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;

            TraderFactory = new Lazy<IActorRef>(() => _actorSystem.ActorOf<TraderFactoryActor>("trader"), LazyThreadSafetyMode.ExecutionAndPublication);
            ExchangeFactory = new Lazy<IActorRef>(() => _actorSystem.ActorOf<StockExchangeFactoryActor>("exchange"));
            _actorSystem.ActorOf(Props.Create<LedgerActor>(), "ledger");
        }

        public static StockMarketSystem Create()
        {
            return new StockMarketSystem(ActorSystem.Create(SystemName));
        }

        public void CreateTrader(string id, string name)
        {
            TraderFactory.Value.Tell(new CreateTrader
                {
                    Id = id,
                    Name = name
                });
        }

        public void CreateExchange(string symbol)
        {
           ExchangeFactory.Value.Tell(new CreateExchange {Symbol = symbol});
        }

        public void TellTrader(string id, object message)
        {
             _actorSystem.ActorSelection(ActorPathResolver.ResolveTraderPath(id)).Tell(message, ActorRefs.NoSender);
        }

        public void TellExchange(string symbol, object message)
        {
            _actorSystem.ActorSelection(ActorPathResolver.ResolveExchangePath(symbol)).Tell(message, ActorRefs.NoSender);
        }

        public TraderHandler Trader(string traderId)
        {
            return new TraderHandler(this, traderId);
        }
    }
}