using System;
using StockMarket.Model.Core;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Exchange
{
    public class OfferFromTrader : IOfferFromTrader
    {
        public OfferFromTrader(IOfferFromTrader source)
        {
            Type = source.Type;
            Symbol = source.Symbol;
            Quantity = source.Quantity;
            PricePerShare = source.PricePerShare;
            TraderId = source.TraderId;
        }

        public OfferType Type { get; }

        public string Symbol { get; }

        public int Quantity { get; private set; }

        public decimal PricePerShare { get; }

        public string TraderId { get;}

        public ITransaction CreateTransaction(OfferFromTrader counterOffer)
        {
            // TODO: Check
            // Counter offer not of the same type
            // Not an empty offerN

            var buyerId = counterOffer.Type == OfferType.Buy ? counterOffer.TraderId : TraderId;
            var sellerId = Type == OfferType.Sell ? TraderId : counterOffer.TraderId;

            int quantity = CalculateQuantityToSell(counterOffer);

            Quantity -= quantity;
            counterOffer.Quantity -= quantity;

            return new Transaction
            {
                Quantity = quantity,
                SellerTraderId = sellerId,
                BuyerTraderId = buyerId,
                PricePerShare = PricePerShare,
                Symbol = Symbol
            };
        }

        private int CalculateQuantityToSell(OfferFromTrader counterPartyOffer)
        {
            return Math.Min(Quantity, counterPartyOffer.Quantity);
        }
    }
}