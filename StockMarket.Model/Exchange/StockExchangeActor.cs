using System;
using System.Linq;
using Akka.Actor;
using StockMarket.Model.Logging;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Exchange
{
    public class SymbolStockExchangeActor : ReceiveActor, ILogMessages
    {
        private readonly string _symbol;

        private OfferBook OfferBook { get; } = new OfferBook();

        public SymbolStockExchangeActor(string symbol)
        {
            _symbol = symbol;

            Receive<OfferPlaced>(o => OnOfferPlaced(o));
        }

        private void OnOfferPlaced(OfferPlaced offer)
        {
            this.Info($"received offer from {offer.TraderId}", ConsoleColor.Blue);

            var matchingOffers = OfferBook.FindMatchinCounterOffers(offer).ToArray();
            var newOffer = new OfferFromTrader(offer);

            if (matchingOffers.Any())
            {
                foreach (var matchOffer in matchingOffers)
                {
                    if (newOffer.Quantity == 0)
                    {
                        this.Info("New Offer fullfilled");
                        break;
                    }

                    if (matchOffer.Quantity == 0)
                    {
                        this.Info("Match offer fulfilled.");
                        continue;
                    }

                    var transaction = newOffer.CreateTransaction(matchOffer);

                    Context.System.ActorSelection(ActorPathResolver.ResolveLedgerPath())
                        .Tell(new ProcessTransaction(transaction));
                }
            }
            else
            {
                this.Info($"No matching offers for {offer.TraderId}");
            }

            if (newOffer.Quantity > 0)
            {
                OfferBook.RegisterOrder(newOffer);
                this.Info($"Registered offer from {offer.TraderId}");
            }
        }

        public static Props Props(string symbol)
        {
            return Akka.Actor.Props.Create(() => new SymbolStockExchangeActor(symbol));
        }

        public string LoggingSource => Self.Path.ToString();
    }
}