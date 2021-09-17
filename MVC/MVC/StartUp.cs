using System.Windows.Forms;
using Controller;
using Model;
using View;

namespace MVC
{
    public partial class StartUp : Form
    {
        public StartUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            new GridController(new GridModel(), new GridView());
        }
    }
}
