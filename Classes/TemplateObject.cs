using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DodoIDE.Dialogs.TemplateDialog;

namespace DodoIDE.Classes
{
    public class TemplateObject
    {
        public ListViewItem Item { get; set; }
        public Image Icon { get; set; }
        public string ImageListKey { get; set; }
        public string FileName { get; internal set; }
        public CreateAction CreateAction { get; set; }
    }
}
