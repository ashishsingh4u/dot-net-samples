using System;
using System.Reactive.Linq;
using System.Threading;

namespace Rx101Samples
{
    public class ForkJoin : IExecution
    {
        public void Execute()
        {
            //var o = Observable.Merge(    //Replaced ForkJoin() with Merge()
            //    Observable.Start(() =>
            //                         {
            //                             Console.WriteLine("Executing 1st on Thread: {0}",
            //                                               Thread.CurrentThread.ManagedThreadId);
            //                             return "Result A";
            //                         }),
            //    Observable.Start(() =>
            //                         {
            //                             Console.WriteLine("Executing 2nd on Thread: {0}",
            //                                               Thread.CurrentThread.ManagedThreadId);
            //                             return "Result B";
            //                         }),
            //    Observable.Start(() =>
            //                         {
            //                             Console.WriteLine("Executing 3rd on Thread: {0}",
            //                                               Thread.CurrentThread.ManagedThreadId);
            //                             return "Result C";
            //                         })
            //    ).Finally(() => Console.WriteLine("Done!"));

            //foreach (string r in o.Next()) //Replace First() with Next()
            //    Console.WriteLine(r);

            var o = Observable.CombineLatest(
                Observable.Start(() =>
                                     {
                                         Console.WriteLine("Executing 1st on Thread: {0}",
                                                           Thread.CurrentThread.ManagedThreadId);
                                         return "Result A";
                                     }),
                Observable.Start(() =>
                                     {
                                         Console.WriteLine("Executing 2nd on Thread: {0}",
                                                           Thread.CurrentThread.ManagedThreadId);
                                         return "Result B";
                                     }),
                (args0, args1) => new[] {args0, args1}).Finally(() => Console.WriteLine("Done!"));

            foreach (string r in o.First())
                Console.WriteLine(r);
        }
    }
}
