using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProducerConsumer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TaskScheduler ui = TaskScheduler.FromCurrentSynchronizationContext();

        public MainWindow()
        {
            InitializeComponent();
        }

        public static double SumRootN(int root)
        {
            double result = 0;
            for (int i = 1; i < 10000000; i++)
            {
                result += Math.Exp(Math.Log(i) / root);
            }
            return result;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            label1.Content = "Milliseconds: ";

            var results = new BlockingCollection<double>();
            var watch = Stopwatch.StartNew();
            List<Task> tasks = new List<Task>();

            var consume = Task.Factory.StartNew(() => display(results));

            for (int i = 2; i < 20; i++)
            {
                int j = i;
                var compute = Task.Factory.StartNew(() =>
                {
                    results.Add(SumRootN(j));
                });
                tasks.Add(compute);
            }

            Task.Factory.ContinueWhenAll(tasks.ToArray(),
                result =>
                {
                    results.CompleteAdding();
                    var time = watch.ElapsedMilliseconds;
                    label1.Content += time.ToString();
                }, CancellationToken.None, TaskContinuationOptions.None, ui);
        }

        public void display(BlockingCollection<double> results)
        {
            foreach (var item in results.GetConsumingEnumerable())
            {
                double currentItem = item;
                Task.Factory.StartNew(new Action(() =>
                     textBlock1.Text += currentItem.ToString() + Environment.NewLine),
                CancellationToken.None, TaskCreationOptions.None, ui);
            }
        }
    }
}
