using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE.Controls
{
    public partial class NewsPageControl : UserControl
    {
        public NewsPageControl()
        {
            InitializeComponent();
        }

        private void NewsPageControl_Load(object sender, EventArgs e)
        {
            webBrowser1.Navigate(@"http://dodoide.org/");
        }
    }
}
