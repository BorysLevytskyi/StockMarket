using System;
using System.IO;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Actors
{
    public class TraderActor : ReceiveActor, ILogMessages
    {
        public string Name { get; }

        public string Id { get; set; }

        public TraderActor(string name, string id)
        {
            Name = name;
            Id = id;

            Receive<SendOrder>(OnSendOrder);
        }

        private void OnSendOrder(SendOrder sendOrder)
        {
            this.Info($"received: {sendOrder.GetType().Name} {sendOrder}");

            var exchangePath = ActorPathResolver.ResolveExchangePath(sendOrder.Symbol);

            try
            {
                //var ex = Context.ActorSelection(exchangePath)
                  //  .ResolveOne(TimeSpan.FromSeconds(2))
                    //.Result;

                var cmd = new OrderPlaced
                {
                    Type = sendOrder.Type,
                    Symbol = sendOrder.Symbol,
                    PricePerShare = sendOrder.PricePerShare,
                    Quantity = sendOrder.Quantity,
                    TraderId = Id
                };

                //ex.Tell(cmd);c

                Context.System.ActorSelection(exchangePath)
                    .Tell(cmd, Self);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            this.Info($"routed order to {exchangePath}");
        }

        public static Props Props(string name, string id)
        {
            return Akka.Actor.Props.Create(() => new TraderActor(name, id));
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine($"created: {Self.Path}");
            base.PostRestart(reason);
        }

        public string Source => Self.Path.ToString();
    }
}