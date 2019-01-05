/*
 * Created by SharpDevelop.
 * User: robit
 * Date: 28/10/2018
 * Time: 15:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Text;
using System.Net.Sockets;
namespace LightCosmosRat
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	/// 
	public partial class MainForm : Form
	{
        Form1 f1;
        Form2 f2;
        TcpClient connectionChecker;
		void StatusHandler(Object tbt){
			TextBox tb = (TextBox)tbt;
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                Thread.Sleep(3000);
                if(f1 != null && f1.Visible)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        tb.Text = "Client maker running...";
                    }));
                    
                }
                else if(f2 != null && f2.Visible)
                {
                    this.Invoke(new MethodInvoker(() =>
                    {
                        tb.Text = "Listening panel running...";
                    }));
                    
                }
                else
                {
                    sb.Clear();
                    sb.Append("Ready...");
                    sb.AppendLine();
                    sb.Append(DateTime.Now.ToLocalTime());
                    //check if the host the machine is connected to internet by visiting the official website
                    try
                    {
                        connectionChecker = new TcpClient();
                        connectionChecker.Connect("www.lightcosmosrat.wordpress.com", 80);
                        sb.AppendLine();
                        sb.AppendLine("Network connected");
                        string Ep = connectionChecker.Client.LocalEndPoint.ToString();
                        string[] IpInfo = Ep.Split(':');
                        sb.AppendLine();
                        sb.Append("Your local IP: "+ IpInfo[0]);
                        connectionChecker.Close();
                        Invoke(new MethodInvoker(() =>
                        {
                            tb.Text = sb.ToString();
                        }));
                        
                    }catch(Exception ex)
                    {
                        sb.AppendLine();
                        sb.AppendLine("Network Error");
                        Invoke(new MethodInvoker(() =>
                        {
                            tb.Text = sb.ToString();
                        }));
                    }
                }
            }

		}
		Thread t;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void Button1Click(object sender, EventArgs e)
		{
            f1 = new Form1();
            f1.Show();
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			t = new Thread(StatusHandler);
			t.Start(textBox1);
		}
		void MainFormFormClosing(object sender, FormClosingEventArgs e)
		{
			t.Abort();
		}
		void Button3Click(object sender, EventArgs e)
		{
			MessageBox.Show("This software was developed by Robitec97 ","Info",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
		void Button2Click(object sender, EventArgs e)
		{
            f2 = new Form2();
            f2.Show();
		}
	}
}
