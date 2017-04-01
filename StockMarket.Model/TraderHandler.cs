using StockMarket.Model.Messages;

namespace StockMarket.Model
{
    public class TraderHandler
    {
        private readonly StockMarketSystem _stockMarket;
        private readonly string _traderId;

        public TraderHandler(StockMarketSystem stockMarket, string traderId)
        {
            _stockMarket = stockMarket;
            _traderId = traderId;
        }

        public TraderHandler Sell(string symbol, int quantity, decimal pricePerShare)
        {
            _stockMarket.TellTrader(_traderId, new SendOffer
            {
                PricePerShare = pricePerShare,
                Quantity = quantity,
                Symbol = symbol,
                Type = OfferType.Sell
            });

            return this;
        }
        public TraderHandler Buy(string symbol, int quantity, decimal pricePerShare)
        {
            _stockMarket.TellTrader(_traderId, new SendOffer
            {
                PricePerShare = pricePerShare,
                Quantity = quantity,
                Symbol = symbol,
                Type = OfferType.Buy
            });

            return this;
        }
    }
}