using System;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using static DodoIDE.Controls.ProjectExplorerControl;
using System.Text.RegularExpressions;

namespace DodoIDE.Controls
{
    public partial class ConsoleControl : UserControl
    {
        private StringWriter output;

        public ProjectObject CurrentProject { get; set; }
        public FileInfo OutputFile { get; private set; }

        public ConsoleControl()
        {
            InitializeComponent();
        }

        private string ConfigureConsole()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Ejecutable *.exe|*.exe";
            dlg.ShowDialog();
            return dlg.FileName;
        }

        private void toolStripComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar=='\n'|| e.KeyChar == '\r')
            {
                SendCommand(cmdHistory.Text);
            }
        }

        private void SendCommand(string text)
        {
            if (!cmdHistory.Items.Contains(text)) { cmdHistory.Items.Add(text); }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            SendCommand(cmdHistory.Text);
        }

        public void CompileAndRunGame(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            int ExitCode = BuildGame();
            timer1_Tick(sender, e);
            if (ExitCode == 1) { RunGame(); }
            timer1_Tick(sender, e);
        }

        public int RunGame()
        {
            if (!File.Exists(Properties.Settings.Default.Runner))
            {
                MessageBox.Show("El interprete está sin configurar. Para continuar seleccione un interprete.", "Configuración del Interprete", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Properties.Settings.Default.Runner = ConfigureConsole();
                Properties.Settings.Default.Save();
            }
            if (!File.Exists(Properties.Settings.Default.Runner)) { return 404; }

            if (OutputFile == null) { throw new NullReferenceException(); }
            timer1.Start();
            Directory.SetCurrentDirectory(CurrentProject.ProjectFile.DirectoryName);
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo();
            proc.StartInfo.Arguments = OutputFile.FullName;
            proc.StartInfo.FileName = Properties.Settings.Default.Runner;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;

            proc.OutputDataReceived += Prc_OutputDataReceived;

            proc.Start();
            proc.BeginOutputReadLine(); ;
            proc.WaitForExit();
            while (!proc.HasExited) { Application.DoEvents(); }
            timer1.Stop();
            return proc.ExitCode;
        }

        public int BuildGame()
        {

            if (!File.Exists(Properties.Settings.Default.Compiler))
            {
                MessageBox.Show("El compilador está sin configurar. Para continuar seleccione un compilador.", "Configuración del Compilador", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Properties.Settings.Default.Compiler = ConfigureConsole();
                Properties.Settings.Default.Save();
            }
            if (!File.Exists(Properties.Settings.Default.Compiler)) { return 404; }

            if (CurrentProject == null) { throw new NullReferenceException(); }
            Directory.SetCurrentDirectory(CurrentProject.ProjectFile.DirectoryName);
            output = new StringWriter();
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo();
            proc.StartInfo.Arguments = CurrentProject.MainFile.FullName;
            proc.StartInfo.FileName = Properties.Settings.Default.Compiler;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;

            proc.OutputDataReceived += Prc_OutputDataReceived;

            proc.Start();
            proc.BeginOutputReadLine();
            proc.WaitForExit();
            while (!proc.HasExited) { Application.DoEvents(); }
            return proc.ExitCode;
        }

        private void Prc_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (e.Data != null)
            {
                Regex r = new Regex(@"File (.+) compiled \((\d+) bytes\):");
                var m = r.Match(e.Data);
                if (m.Groups.Count == 3) { OutputFile = new FileInfo(m.Groups[1].Value); }
                output.WriteLine(e.Data);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (richTextBox1.Text != output.ToString())
            {
                richTextBox1.Text = output.ToString();
                richTextBox1.SelectionStart = richTextBox1.TextLength - 1;
                richTextBox1.ScrollToCaret();
            }
        }
    }
}
