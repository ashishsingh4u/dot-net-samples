using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;

namespace RxLabs.VWAP
{
    class Program
    {
        class Trade
        {
            public double Price { get; set; }
            public int Volume { get; set; }
        }

        class TradeWithVWAP
        {
            public Trade Trade { get; set; }
            public double VWAP { get; set; }
        }

        static void Main(string[] args)
        {
            // Market data simulator
            var rnd = new Random();
            IObservable<Trade> feed =
                Observable.Defer(() =>
                    Observable.Return(new Trade
                    {
                        Price = Math.Round(30.0 + 3 * rnd.NextDouble(), 2),
                        Volume = 1000 + rnd.Next(0, 100000) / 100 * 100
                    })
                    .Delay(TimeSpan.FromSeconds(3 * rnd.NextDouble())))
                .Repeat();

            // Build a wrapper feed to add running VWAP to each trade
            IObservable<TradeWithVWAP> vwap = BuildVwapFeed(
                feed,
                TimeSpan.FromSeconds(5));

            // Subsribe and print results
            vwap.Subscribe(f => Console.WriteLine(
                "Price={0}, Volume={1:#,##0}, VWAP={2}",
                f.Trade.Price,
                f.Trade.Volume,
                f.VWAP));
            Console.ReadKey(true);
        }

        private static IObservable<TradeWithVWAP> BuildVwapFeed(
            IObservable<Trade> feed,
            TimeSpan slidingWindowLength)
        {
            // Timestamp original feed
            var timestampted = feed.Timestamp();

            // Maintain a sliding window of recent trades
            var wnd = new List<System.Reactive.Timestamped<Trade>>();
            var slidingWindowFeed = timestampted.Do(f =>
            {
                // remove expired trades
                while (
                    wnd.Count > 0 &&
                    f.Timestamp - wnd.First().Timestamp > slidingWindowLength)
                {
                    wnd.RemoveAt(0);
                }
                // add newest trade
                wnd.Add(f);
            });

            // Calculate running VWAP from trades in the sliding window
            var vwap =
                from w in slidingWindowFeed
                let windowVolume = wnd.Sum(l => l.Value.Volume)
                let windowTotal = wnd.Sum(l => l.Value.Volume * l.Value.Price)
                select new TradeWithVWAP
                {
                    Trade = w.Value,
                    VWAP = Math.Round(windowTotal / windowVolume, 2)
                };
            return vwap;
        }
    }
}
