#define NewAPI
using System;
#if NewAPI
using System.Reactive.Linq;
#else
using System.Linq;
#endif
using System.Threading;

namespace RxLabs.Throattling
{
    class Program
    {
        static void Main(string[] args)
        {
            // simulate very fast (no delays) market data feed
            var rnd = new Random();
            var unthrottledFeed = Observable.Defer(() =>
                Observable.Return(30.0)
                .Select(p => Math.Round(p + rnd.NextDouble(), 2))
#if NewAPI      
                .Delay(TimeSpan.FromSeconds(0))
#endif
                .Repeat());

            // running unthottled feed as a demo
            ManualResetEvent unthrottledDone = new ManualResetEvent(false);
            int count = 0;

            unthrottledFeed
                .Do(p => ++count)
#if NewAPI
                .TakeUntil(Observable.Return("Done unthrottled feed")
#else 
                .Until(Observable.Return("Done unthrottled feed")
#endif
                .Delay(TimeSpan.FromSeconds(5)))
                .Subscribe(Console.WriteLine, () => unthrottledDone.Set());

            unthrottledDone.WaitOne();

            Console.WriteLine("\nDone unthrottled feed. Avg rate: {0} values per second", count / 5);

            // running same feed, now throttled
            Console.WriteLine("Now throttlig at 1 value per second...\n");
            var throttledFeed = unthrottledFeed.Sample(TimeSpan.FromSeconds(1));
            throttledFeed.Subscribe(Console.WriteLine);

            Console.ReadKey(true);
        }
    }
}
