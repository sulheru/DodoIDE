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
    public partial class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return (textBox1 != null) ? textBox1.Text : ""; }
            set { textBox1.Text = value; }
        }

        public string Title
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        public string Message
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }


    }
}
