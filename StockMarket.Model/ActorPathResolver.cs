namespace StockMarket.Model
{
    internal static class ActorPathResolver
    {
        public static string ResolveTraderPath(string id)
        {
            return $"/user/trader/{id}";
        }

        public static string ResolveExchangePath(string sendOrderSymbol)
        {
            return $"/user/exchange/{sendOrderSymbol.ToLower()}";
        }

        public static string ResolveLedgerPath()
        {
            return $"/user/ledger";
        }
    }
}