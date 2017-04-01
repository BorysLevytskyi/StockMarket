using System;
using System.Collections.Generic;
using Akka.Actor;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;
using StockMarket.Model.Messages.Events;

namespace StockMarket.Model.Traders
{
    public class TraderActor : ReceiveActor, ILogMessages
    {
        public string Name { get; }

        public string Id { get; set; }

        public Dictionary<string, Position> Positions { get; } = new Dictionary<string, Position>(StringComparer.OrdinalIgnoreCase);

        public TraderActor(string id, string name)
        {
            Name = name;
            Id = id;

            Receive<SendOffer>(o => OnSendOrder(o));
            Receive<TransactionCompleted>(o => OnTransactionCompleted(o));
        }

        private void OnTransactionCompleted(TransactionCompleted tran)
        {
            var key = tran.Symbol;
            var tradeType = tran.BuyerTraderId == Id ? TradeType.Buy : TradeType.Sell;

            Position oldPos;
            if(!Positions.TryGetValue(key, out oldPos))
            {
                oldPos = new Position(0, tran.Symbol);
            }

            var newPos = oldPos.Adjust(tradeType, tran.Quantity);

            Positions[key] = newPos;

            Context.System.EventStream.Publish(new TraderPositionChanged
            {
                TraderId = Id,
                OldQuantity = oldPos.Quantity,
                NewQuantity = newPos.Quantity
            });
        }

        private void OnSendOrder(SendOffer sendOffer)
        {
            this.Info($"received: {sendOffer.GetType().Name} {sendOffer}");

            var exchangePath = ActorPathResolver.ResolveExchangePath(sendOffer.Symbol);

            Context.System.ActorSelection(exchangePath)
                .Tell(new OfferPlaced
                {
                    Type = sendOffer.Type,
                    Symbol = sendOffer.Symbol,
                    PricePerShare = sendOffer.PricePerShare,
                    Quantity = sendOffer.Quantity,
                    TraderId = Id
                }, Self);

            this.Info($"routed offer to {exchangePath}");
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

        public string LoggingSource => Self.Path.ToString();
    }
}