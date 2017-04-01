using System.Collections.Generic;
using System.Linq;
using StockMarket.Model.Core;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Exchange
{
    public class OfferBook
    {
        private List<OfferFromTrader> Offers { get; } = new List<OfferFromTrader>();

        public IEnumerable<IOfferFromTrader> GetSellOrders()
        {
            return Offers.Where(o => o.Type == TradeType.Sell);
        }

        public IEnumerable<IOfferFromTrader> GetBuyOrders()
        {
            return Offers.Where(o => o.Type == TradeType.Buy);
        }

        public void RegisterOrder(OfferFromTrader offer)
        {
            Offers.Add(offer);
        }

        public IEnumerable<OfferFromTrader> GetOrdersFrom(string traderId)
        {
            return Offers.Where(o => o.TraderId == traderId);
        }

        public IEnumerable<OfferFromTrader> FindMatchinCounterOffers(IOfferFromTrader offer)
        {
            // Find matching offers (cheapest first)
            return
                Offers.Where(o => o.Type == offer.Type.CounterPartyType() && IsPriceMatches(offer, o))
                    .OrderBy(o => o.PricePerShare);
        }

        private static bool IsPriceMatches(IOfferFromTrader offer, OfferFromTrader o)
        {
            if (offer.Type == TradeType.Buy)
            {
                return offer.PricePerShare >= o.PricePerShare;
            }

            return offer.PricePerShare <= o.PricePerShare;
        }

        public void PurgeFullfilledOffers()
        {
            Offers.RemoveAll(o => o.Quantity == 0);
        }
    }
}