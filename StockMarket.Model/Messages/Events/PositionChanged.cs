using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockMarket.Model.Messages.Events
{
    public class PositionChanged
    {
        public string TraderId { get; set; }

        public string Symbol { get; set; }

        public int OldQuantity { get; set; }

        public int NewQuantity { get; set; }
    }
}
