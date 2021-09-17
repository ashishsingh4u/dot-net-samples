using System;
using System.Reactive.Linq;
using System.Threading;

namespace Rx101Samples
{
    public class Simple:IExecution
    {
        public void Execute()
        {
            var o = Observable.Start(() =>
                                         {
                                             Console.WriteLine("Calculating...");
                                             Thread.Sleep(3000);
                                             Console.WriteLine("Done.");
                                         });
            o.First(); // subscribe and wait for completion of background operation
        }
    }
}
