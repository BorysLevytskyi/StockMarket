using System;
using System.Threading.Tasks;
using Akka.Actor;
using StockMarket.Model;
using StockMarket.Model.Messages;

namespace StockMarket.Console
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var market = StockMarketSystem.Create();

            RunStockMarket(market).Wait();
        }

        private static async Task RunStockMarket(StockMarketSystem market)
        {
// market.CreateExchange("APPL");

            market.CreateTrader("borys", "Borys Levytskyi");
            market.CreateExchange("APPL");

            await Task.Delay(TimeSpan.FromSeconds(1));

            var sendOrder = new SendOrder()
            {
                Symbol = "APPL",
                Quantity = 1000,
                PricePerShare = 234,
                Type = OrderType.Buy
            };

            market.TellExchange("appl", new OrderPlaced());
            market.TellTrader("borys", sendOrder);
        }
    }
}