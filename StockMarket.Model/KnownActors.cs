using System;
using System.Threading;
using Akka.Actor;
using StockMarket.Model.Exchange;
using StockMarket.Model.Traders;

namespace StockMarket.Model
{
    public class KnownActors
    {
        public KnownActors(ActorSystem actorSystem)
        {
            TraderFactory = new Lazy<IActorRef>(() => actorSystem.ActorOf<TraderFactoryActor>("trader"), LazyThreadSafetyMode.ExecutionAndPublication);
            ExchangeFactory = new Lazy<IActorRef>(() => actorSystem.ActorOf<StockExchangeFactoryActor>("exchange"), LazyThreadSafetyMode.ExecutionAndPublication);
            MarketEventSubscriber = new Lazy<IActorRef>(() => actorSystem.ActorOf<SubscriberActor>("market-event-subscriber"), LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public Lazy<IActorRef> TraderFactory { get; }
        public Lazy<IActorRef> ExchangeFactory { get; }
        public Lazy<IActorRef> MarketEventSubscriber { get; }
    }
}