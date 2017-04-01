using System.Runtime.InteropServices;

namespace StockMarket.Model.Messages
{
    public class CreateTrader
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}