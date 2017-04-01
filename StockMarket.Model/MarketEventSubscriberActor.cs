using System;
using System.Collections.Generic;
using Akka.Actor;
using StockMarket.Model.Core;
using StockMarket.Model.Logging;

namespace StockMarket.Model
{
    public class MarketEventSubscriberActor : ReceiveActor, ILogMessages
    {
        public List<MarketEventSubscription> Subscriptions { get; } = new List<MarketEventSubscription>();

        public MarketEventSubscriberActor()
        {
            Receive<MarketEventSubscription>(subscription =>
            {
                Subscriptions.Add(subscription);
                Context.System.EventStream.Subscribe(Self, subscription.MessageType);
            });

            Receive<IMarketEvent>(e => OnMessageReceived(e));
        }

        private void OnMessageReceived(object msg)
        {
            foreach (var s in Subscriptions)
            {
                if (s.MessageType == msg.GetType())
                {
                    try
                    {
                        s.Handle(msg);
                    }
                    catch (Exception e)
                    {
                        this.Error(e);
                    }
                }
            }
        }

        public string LoggingSource => Self.Path.ToString();

        public class MarketEventSubscription
        {
            public Type MessageType { get; set; }
            
            public Action<object> Handle { get; set; }
        }
    }
}