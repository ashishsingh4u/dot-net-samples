using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ThreadDemo
{
    public partial class Test : Form
    {
        private AutoResetEvent _resetEvent;
        public Test()
        {
            InitializeComponent();
        }

        private void Test_Load(object sender, EventArgs e)
        {
            _Cultures.ValueMember = "LCID";
            _Cultures.DisplayMember = "EnglishName";
            CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            _Cultures.DataSource = allCultures;
        }

        private void _setCulture_Click(object sender, EventArgs e)
        {
            CultureInfo culture = CultureInfo.GetCultureInfo((int)_Cultures.SelectedValue);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void _thread_Click(object sender, EventArgs e)
        {
            var thread = new Thread(o => MessageBox.Show(Thread.CurrentThread.CurrentCulture.Name));
            thread.Start(null);
        }

        private void _threadPoolWorker_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(o =>
                                             {
                                                 MessageBox.Show(Thread.CurrentThread.CurrentCulture.Name);
                                                 MessageBox.Show(Double.NaN.ToString());
                                             });
            MessageBox.Show(Thread.CurrentThread.CurrentCulture.Name);
            MessageBox.Show(Double.NaN.ToString());
        }

        private void _threadPoolSingle_Click(object sender, EventArgs e)
        {
            _resetEvent = new AutoResetEvent(true);
            ThreadPool.RegisterWaitForSingleObject(_resetEvent,
                                                   (o, b) => MessageBox.Show(Thread.CurrentThread.CurrentCulture.Name),
                                                   null, -1, true);
        }

        private void Test_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_resetEvent != null)
                _resetEvent.Close();
            _resetEvent = null;
        }

        private void _crash_Click(object sender, EventArgs e)
        {
            int i = 0;
            int k = 10/i;
        }
    }
}