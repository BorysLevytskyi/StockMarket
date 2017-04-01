using System;
using System.Linq;
using System.Threading;
using Akka.Actor;
using Akka.Event;
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

        private StockMarketSystem(ActorSystem actorSystem)
        {
            _actorSystem = actorSystem;
            Actors = new KnownActors(actorSystem);

            _actorSystem.ActorOf(Props.Create<LedgerActor>(), "ledger");
        }

        public KnownActors Actors { get; }

        public static StockMarketSystem Create()
        {
            return new StockMarketSystem(ActorSystem.Create(SystemName));
        }

        public void CreateTrader(string id, string name)
        {
            Actors.TraderFactory.Value.Tell(new CreateTrader
                {
                    Id = id,
                    Name = name
                });
        }

        public void CreateExchange(string symbol)
        {
            Actors.ExchangeFactory.Value.Tell(new CreateExchange {Symbol = symbol});
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

        public void SubsbribeTo<T>(Action<T> action)
        {
            Actors.MarketEventSubscriber.Value
                .Tell(new SubscriberActor.MarkedEventSubscription
                {
                    Handle = msg => action((T)msg),
                    MessageType = typeof(T)
                });
        }
    }
}