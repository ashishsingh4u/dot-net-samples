using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace GettingStarted
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        public MainWindow()
        {
            InitializeComponent();
        }

        public double SumRootN(int root)
        {
            double result = 0;
            for (int i = 1; i < 10000000; i++)
            {
                tokenSource.Token.ThrowIfCancellationRequested();
                result += Math.Exp(Math.Log(i) / root);
            }
            return result;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            tokenSource = new CancellationTokenSource();

            textBlock1.Text = "";
            label1.Content = "Milliseconds: ";

            var watch = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();
            var ui = TaskScheduler.FromCurrentSynchronizationContext();
            for (int i = 2; i < 20; i++)
            {
                int j = i;
                var compute = Task.Factory.StartNew(() =>
                {
                    return SumRootN(j);
                }, tokenSource.Token);

                tasks.Add(compute);

                var displayResults = compute.ContinueWith(resultTask =>
                                     textBlock1.Text += "root " + j.ToString() + " " +
                                                            compute.Result.ToString() +
                                                            Environment.NewLine,
                                         CancellationToken.None,
                                         TaskContinuationOptions.OnlyOnRanToCompletion,
                                         ui);

                var displayCancelledTasks = compute.ContinueWith(resultTask =>
                                               textBlock1.Text += "root " + j.ToString() +
                                                                  " canceled" +
                                                                  Environment.NewLine,
                                               CancellationToken.None,
                                               TaskContinuationOptions.OnlyOnCanceled, ui);
            }

            Task.Factory.ContinueWhenAll(tasks.ToArray(),
                result =>
                {
                    var time = watch.ElapsedMilliseconds;
                    label1.Content += time.ToString();
                }, CancellationToken.None, TaskContinuationOptions.None, ui);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
            textBlock1.Text += "Cancel" + Environment.NewLine;
        }

    }
}
