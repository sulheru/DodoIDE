using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Permissions;
using System.IO;

namespace DodoIDE.Controls
{

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisible(true)]
    public partial class StartPageControl : UserControl
    {
        public StartPageControl()
        {
            InitializeComponent();
        }

        private void StartPageControl_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = this;
            // Uncomment the following line when you are finished debugging.
            //webBrowser1.ScriptErrorsSuppressed = true;

            webBrowser1.DocumentText = GetHtml();
        }

        private string GetHtml()
        {
            var sc = Properties.Settings.Default.Recent;
            string[] strArray = new string[sc.Count];
            sc.CopyTo(strArray, 0);

            return string.Format(Properties.Resources.StartPageHtml,
                (from rc in strArray
                 select string.Format(
                     "<li><a href=\"#\" onclick=\"window.external.openFile('{0}')\">{1}</a></li>",
                     rc, (new FileInfo(rc)).Name)).ToArray());
        }

        public void openFile(string message)
        {
            // evento
        }

        public void openFile()
        {
            // evento
        }

        public void newFile()
        {
            // evento
        }
    }
}
