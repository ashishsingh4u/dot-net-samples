using System;
using System.Reactive.Linq;

namespace Rx101Samples
{
    public class SimpleAsyncOnDemand:IExecution
    {
        // Synchronous operation
        public string DoLongRunningOperation(string param)
        {
            return param;
        }

        public IObservable<string> LongRunningOperationAsync(string param)
        {
            return Observable.Create<string>(
                o => Observable.ToAsync((Func<string,string>)DoLongRunningOperation)(param).Subscribe(o)
            );
        }

        public void Execute()
        {
            DoLongRunningOperation("Synchronous");
            LongRunningOperationAsync("Asynchornous");
        }
    }
}
