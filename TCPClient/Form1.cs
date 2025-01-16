using System.Net;

namespace TCPClient
{
    public partial class Form1 : Form
    {
        Client client;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new(IPAddress.Parse("127.0.0.1"), 13000);
            client.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
            client.Send(SharedObjects.StatusCode.ConnectionClose, "Bye!");
            client.Stop();
        }
    }
}
