using System;
using Akka.Actor;

namespace StockMarket.Model.Logging
{
    public static class LoggingExtensions
    {
        private static object _syncRoot = new object();

        public static void Info(this ILogMessages actor, string message, ConsoleColor color = ConsoleColor.DarkGreen)
        {
            lock (_syncRoot)
            {
                Console.ForegroundColor = color;
                Console.WriteLine($"{actor.Source} {message}");
                Console.ResetColor();
            }
        }
    }

    public interface ILogMessages
    {
        string Source { get; }
    }
}