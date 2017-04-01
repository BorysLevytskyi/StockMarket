using System;
using System.Collections.Generic;
using System.Linq;
using StockMarket.Model.Traders;

namespace StockMarket.Model.Logging
{
    public static class LoggingExtensions
    {
        private static readonly object SyncRoot = new object();

        private static readonly Type[] DisabledLoggers =
        {
            typeof(TraderActor)
        };

        public static void Info(this ILogMessages actor, string message, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            lock (SyncRoot)
            {
                if (DisabledLoggers.Contains(actor.GetType()))
                {
                    return;
                }

                Console.ForegroundColor = color;
                Console.Write(actor.LoggingSource);
                Console.ResetColor();
                Console.WriteLine($": {message}");
            }
        }
    }

    public interface ILogMessages
    {
        string LoggingSource { get; }
    }
}