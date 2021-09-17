using System.Windows.Forms;
using ExampleProject.Contracts;
using ExampleProject.Presenters;
using ExampleProject.Model;

namespace ExampleProject.Application
{
    public partial class Form1 : Form
    {
        private readonly IClockPresenter _presenter;
        private readonly ITimeModel _model;

        public Form1()
        {
            InitializeComponent();

            // Wire up here - it's best to use
            // some IOC controller, such as Unity or similar
            // in order to perform dependency injection.

            _model = new TimeModel();
            _presenter = new ClockPresenter(_model);

            // Attach two views
            clockViewControl1.AttachToPresenter(_presenter, true);
            clockViewControl2.AttachToPresenter(_presenter, true);

        }
    }
}
