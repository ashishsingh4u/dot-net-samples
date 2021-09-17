using System.Windows.Forms;

namespace ForexDashboard
{
    public partial class Dashboard : Form
    {
        private PricingEngine _pricingEngine;
        public Dashboard()
        {
            InitializeComponent();
            _pricingEngine = new PricingEngine();
        }

        private void OnGetFeedButtonClick(object sender, System.EventArgs e)
        {
        }
    }
}
