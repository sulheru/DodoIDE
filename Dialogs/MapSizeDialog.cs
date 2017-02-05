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
    public partial class MapSizeDialog : Form
    {
        public int SpriteWidth { get { return (int)numericUpDown1.Value; } }
        public int SpriteHeight { get { return (int)numericUpDown2.Value; } }
        public MapSizeDialog()
        {
            InitializeComponent();
        }
    }
}
