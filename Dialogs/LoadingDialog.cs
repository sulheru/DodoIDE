using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DodoIDE.Dialogs
{
    public partial class LoadingDialog : Form
    {

        public override string Text
        {
            get { return label1?.Text; }
            set { label1.Text = value; }
        }

        public string Title
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public bool Closeable
        {
            get { return ControlBox; }
            set { ControlBox = value; }
        }

        public bool Cancelable
        {
            get { return button1.Visible; }
            set { button1.Visible = value; }
        }

        public ProgressBarStyle ProgressBarStyle
        {
            get { return progressBar1.Style; }
            set { progressBar1.Style = value; }
        }

        public LoadingDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DoWork?.Invoke(this, e);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null) { label1.Text = e.UserState.ToString(); }
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        public void RunLoaderAsync() { backgroundWorker1.RunWorkerAsync(); }
        public void RunLoaderAsync(object argument) { backgroundWorker1.RunWorkerAsync(argument); }
        public void ReportProgress(int percentProgress, string state) { backgroundWorker1.ReportProgress(percentProgress, state); }
        public void ReportProgress(int percentProgress) { backgroundWorker1.ReportProgress(percentProgress); }

        public event DoWorkEventHandler DoWork;
    }
}
