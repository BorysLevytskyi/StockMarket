using Akka.Actor;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Traders
{
    public class TraderFactoryActor : ReceiveActor, ILogMessages
    {
        public TraderFactoryActor()
        {
            Receive<CreateTrader>(o => OnCreateActor(o));
        }

        private void OnCreateActor(CreateTrader c)
        {
            var actor = Context.ActorOf(TraderActor.Props(c.Id, c.Name), c.Id);
            this.Info($"created: {actor.Path}");
        }

        string ILogMessages.LoggingSource => Self.Path.ToString();
    }
}