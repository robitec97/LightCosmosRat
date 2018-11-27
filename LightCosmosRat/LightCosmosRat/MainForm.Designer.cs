/*
 * Created by SharpDevelop.
 * User: robit
 * Date: 28/10/2018
 * Time: 15:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LightCosmosRat
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.button1 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button1.ForeColor = System.Drawing.Color.LawnGreen;
			this.button1.Location = new System.Drawing.Point(12, 27);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(188, 43);
			this.button1.TabIndex = 0;
			this.button1.Text = "CREATE A CLIENT";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// button3
			// 
			this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
			this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button3.Location = new System.Drawing.Point(667, 268);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(89, 67);
			this.button3.TabIndex = 2;
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// textBox1
			// 
			this.textBox1.BackColor = System.Drawing.Color.Black;
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.ForeColor = System.Drawing.Color.LawnGreen;
			this.textBox1.Location = new System.Drawing.Point(272, 35);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(484, 217);
			this.textBox1.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.SpringGreen;
			this.label1.Location = new System.Drawing.Point(440, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(119, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "CURRENT STATUS:";
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.button2.ForeColor = System.Drawing.Color.LawnGreen;
			this.button2.Location = new System.Drawing.Point(12, 144);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(188, 43);
			this.button2.TabIndex = 5;
			this.button2.Text = "START LISTENING";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(776, 354);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "LightCosmosRat";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
