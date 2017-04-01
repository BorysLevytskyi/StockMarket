using System.Diagnostics;
using StockMarket.Model.Messages;

namespace StockMarket.Model.Traders
{
    [DebuggerDisplay("{Symbol} {Quantity}")]
    public struct Position
    {
        public string Symbol { get; }

        public int Quantity { get; }

        public Position(int quantity, string symbol)
        {
            Quantity = quantity;
            Symbol = symbol;
        }

        public Position Adjust(TradeType tradeType, int quantity)
        {
            if (tradeType == TradeType.Buy)
            {
                return new Position(Quantity + quantity, Symbol);
            }

            return new Position(Quantity - quantity, Symbol);
        }
    }
}