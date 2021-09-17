using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadFibonacci
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1Load(object sender, EventArgs e)
        {
            const int fibonacciCalculations = 10;

            // One event is used for each Fibonacci object
            var doneEvents = new ManualResetEvent[fibonacciCalculations];
            var fibArray = new Fibonacci[fibonacciCalculations];
            var r = new Random();

            // Configure and launch threads using ThreadPool:
            Console.WriteLine(@"launching {0} tasks...", fibonacciCalculations);
            for (int i = 0; i < fibonacciCalculations; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                var f = new Fibonacci(r.Next(20, 40), doneEvents[i]);
                fibArray[i] = f;
                ThreadPool.QueueUserWorkItem(f.ThreadPoolCallback, i);
            }

            // Wait for all threads in pool to calculation...
            WaitHandle.WaitAll(doneEvents);
            MessageBox.Show(@"All calculations are complete.");

            // Display the results...
            for (int i = 0; i < fibonacciCalculations; i++)
            {
                Fibonacci f = fibArray[i];
                MessageBox.Show(string.Format(@"Fibonacci({0}) = {1}", f.N, f.FibOfN));
            }

        }

        private void Button1Click(object sender, EventArgs e)
        {

        }
    }
}
