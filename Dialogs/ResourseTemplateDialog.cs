using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE.Dialogs
{
    public partial class ResourseTemplateDialog : Form
    {
        public ListViewItem Selected
        { get { return TemplatesView.SelectedItems[0]; } }

        public ResourseTemplateDialog()
        {
            InitializeComponent();
        }

        private void TemplatesView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TemplatesView.SelectedItems.Count == 1)
            {
                if (TemplatesView.SelectedItems[0] == TemplatesView.Items[0]) { Description.Rtf = Properties.Resources.SourceFileDescription; }
                else if (TemplatesView.SelectedItems[0] == TemplatesView.Items[1]) { Description.Rtf = Properties.Resources.FicheroParaGraficos; }
                else if (TemplatesView.SelectedItems[0] == TemplatesView.Items[2]) { Description.Rtf = Properties.Resources.SourceFileDescription; }
                else if (TemplatesView.SelectedItems[0] == TemplatesView.Items[3]) { Description.Rtf = Properties.Resources.SourceFileDescription; }
                else if (TemplatesView.SelectedItems[0] == TemplatesView.Items[4]) { Description.Rtf = Properties.Resources.SourceFileDescription; }
            }
        }

        
    }
}
