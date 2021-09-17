using System.Windows.Forms;

namespace Server
{
    public partial class Broadcaster : Form
    {
        private readonly SocketManager.SocketManager _socketManager = new SocketManager.SocketManager();
        public Broadcaster()
        {
            InitializeComponent();
            _socketManager.InitializeSocket("127.0.0.1:8085","c:\\projects\\log.txt");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            _socketManager.Send(textBox1.Text);
        }
    }
}
