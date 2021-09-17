using System;
using System.Reactive.Linq;

namespace RxLabs.HiLo
{
    class Program
    {
        static void Main(string[] args)
        {
            // simulate market data
            var rnd = new Random();
            var feed = Observable.Defer(() =>
                Observable.Return(Math.Round(30.0 + rnd.NextDouble(), 2))
                .Delay(TimeSpan.FromSeconds(1 * rnd.NextDouble())))
                .Repeat();

            // Daily low price feed
            double min = double.MaxValue;
            var feedLo = feed
                .Where(p => p < min)
                .Do(p => min = Math.Min(min, p))
                .Select(p => "New LO: " + p);

            // Daily high price feed
            double max = double.MinValue;
            var feedHi = feed
                .Where(p => p > max)
                .Do(p => max = Math.Max(max, p))
                .Select(p => "New HI: " + p);

            // Combine hi and lo in one feed and subscribe to it
            feedLo.Merge(feedHi).Subscribe(Console.WriteLine);

            Console.ReadKey(true);
        }
    }
}
