using System;
using System.Threading;
using System.Windows.Forms;

namespace CustomConfigTest
{
    public partial class Form1 : Form
    {
        //private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private RegisteredWaitHandle _handle;
        private readonly SynchronizationContext _context;
        public Form1()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
            GetRemoteOnly("pageAppearanceGroup/pageAppearance");
            //_handle = ThreadPool.RegisterWaitForSingleObject(_resetEvent, ProcessPendingTasks, null, 200, false);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            var pageSection =
                (PageAppearanceSection) System.Configuration.ConfigurationManager.GetSection(
                    "pageAppearanceGroup/pageAppearance");
            foreach (ThingElement thing in pageSection.ExampleThingElements)
            {
                var item = thing;
            }
        }

        private bool GetRemoteOnly(string section)
        {
            var pageSection =
                System.Configuration.ConfigurationManager.GetSection(
                    section) as PageAppearanceSection;
            if (pageSection != null)
                return pageSection.RemoteOnly;
            return false;
        }

        private void ProcessPendingTasks(object state, bool signaled)
        {
            _context.Post(o => Text = DateTime.Now.ToString(), null);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_handle != null)
            {
                _handle.Unregister(null);
                _handle = null;
            }
        }
        static void ThreadMethod()
        {
            CountClass cClass;

            // Create 100,000 instances of CountClass.
            for (int i = 0; i < 100000; i++)
            {
                cClass = new CountClass();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(new ThreadStart(ThreadMethod));
            Thread thread2 = new Thread(new ThreadStart(ThreadMethod));
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();

            // Have the garbage collector run the finalizer for each
            // instance of CountClass and wait for it to finish.
            GC.Collect();
            GC.WaitForPendingFinalizers();

            MessageBox.Show(string.Format(@"UnsafeInstanceCount: {0}" +
                @"\nSafeCountInstances: {1}",
                CountClass.UnsafeInstanceCount.ToString(),
                CountClass.SafeInstanceCount.ToString()));
        }
    }
    class CountClass
    {
        static volatile int unsafeInstanceCount = 0;
        static int safeInstanceCount = 0;

        static public int UnsafeInstanceCount
        {
            get { return unsafeInstanceCount; }
        }

        static public int SafeInstanceCount
        {
            get { return safeInstanceCount; }
        }

        public CountClass()
        {
            unsafeInstanceCount++;
            Interlocked.Increment(ref safeInstanceCount);
        }

        ~CountClass()
        {
            unsafeInstanceCount--;
            Interlocked.Decrement(ref safeInstanceCount);
        }
    }
}
