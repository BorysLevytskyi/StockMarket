using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockMarket.Model.Core;

namespace StockMarket.Model.Messages.Events
{
    public class TraderPositionChanged : IMarketEvent
    {
        public string TraderId { get; set; }

        public string Symbol { get; set; }

        public int OldQuantity { get; set; }

        public int NewQuantity { get; set; }
    }
}
