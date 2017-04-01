using System;
using System.Threading.Tasks;
using Akka.Actor;
using StockMarket.Model.Messages;
using StockMarket.Model.Logging;

namespace StockMarket.Model.Actors
{
    public class TraderFactoryActor : ReceiveActor, ILogMessages
    {
        public TraderFactoryActor()
        {
            Receive<CreateTrader>(OnCreateActor);
        }

        private void OnCreateActor(CreateTrader c)
        {
            var actor = Context.ActorOf(TraderActor.Props(c.Id, c.Name), c.Id);
            this.Info($"created: {actor.Path}");
        }

        string ILogMessages.Source => Self.Path.ToString();
    }
}