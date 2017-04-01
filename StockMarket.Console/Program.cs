using System;
using System.Threading.Tasks;
using StockMarket.Model;
using StockMarket.Model.Messages.Events;

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
            market.CreateTrader("john", "John Smith");
            market.CreateTrader("jack", "John Smith");

            market.SubsbribeTo<TraderPositionChanged>(p => System.Console.WriteLine($"Position changed: {p.TraderId} {p.Symbol} {p.OldQuantity} -> {p.NewQuantity}"));

            market.CreateExchange("APPL");

            await Task.Delay(TimeSpan.FromSeconds(1));

            var borys = market.Trader("borys");
            var john = market.Trader("john");
            var jack = market.Trader("jack");

            borys.Buy("APPL", 1000, 200);
            john.Sell("APPL", 500, 150);
            jack.Sell("APPL", 250, 200);

            System.Console.ReadLine();
        }
    }
}