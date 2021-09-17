using System;
using System.Reactive.Linq;

namespace RxLabs.FeedFailover
{
    class Program
    {
        static void Main(string[] args)
        {
            // simulate market data feed
            var rnd = new Random();
            var feedSimulator = Observable.Defer(() =>
                Observable.Return(Math.Round(30.0 + rnd.NextDouble(), 2))
                .Delay(TimeSpan.FromSeconds(rnd.NextDouble())))
                .Repeat();

            // Simulate fatal problem with feed 5 seconds down the road
            //var feedProblem =
            //    Observable.Throw<double>(new Exception("Fatal Error!")).Delay(TimeSpan.FromSeconds(5)); //Doesn't work with new API.

            // Simulate fatal problem with feed 5 seconds down the road
            var feedProblem = Observable.Never<double>().Timeout(TimeSpan.FromSeconds(5),
                                                   Observable.Throw<double>(new Exception("Fatal Error!")));

            // Bloomberg feed: merging simulator with error generator
            var bloombergFeed = from p in feedSimulator.Merge(feedProblem) select new { Feed = "Bloomberg", Price = p };
            // Reuters feed
            var reutersFeed = from p in feedSimulator select new { Feed = "Reuters", Price = p };

            // Automatic, transparent failover feed
            var failoverFeed = bloombergFeed.Catch(reutersFeed);

            // Subscribe and print results to console
            bloombergFeed.Subscribe(p => { }, x => Console.WriteLine("Bloomberg feed: {0}", x.Message));
            failoverFeed.Subscribe(Console.WriteLine);

            Console.ReadKey(true);
        }
    }
}
