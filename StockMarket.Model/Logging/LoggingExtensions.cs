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

        public static void Error(this ILogMessages actor, Exception error)
        {
            lock (SyncRoot)
            {
                if (DisabledLoggers.Contains(actor.GetType()))
                {
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(actor.LoggingSource);
                Console.WriteLine($": ERROR {error}");
                Console.ResetColor();
            }
        }
    }

    public interface ILogMessages
    {
        string LoggingSource { get; }
    }
}