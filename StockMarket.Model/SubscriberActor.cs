using System;
using System.Collections.Generic;
using Akka.Actor;
using StockMarket.Model.Logging;

namespace StockMarket.Model
{
    public class SubscriberActor : ReceiveActor, ILogMessages
    {
        public List<MarkedEventSubscription> Subscriptions { get; } = new List<MarkedEventSubscription>();

        public SubscriberActor()
        {
            Receive<MarkedEventSubscription>(subscription =>
            {
                Subscriptions.Add(subscription);
                Context.System.EventStream.Subscribe(Self, subscription.MessageType);
            });

            ReceiveAny(msg =>
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
            });
        }

        public string LoggingSource => Self.Path.ToString();

        public class MarkedEventSubscription
        {
            public Type MessageType { get; set; }
            
            public Action<object> Handle { get; set; }
        }
    }
}