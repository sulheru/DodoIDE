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
    public partial class BrushControl : UserControl
    {
        public TextureBrush Brush { get; set; }

        public BrushControl()
        {
            InitializeComponent();
            for (int i = 0; i < imgBrushes.Images.Count; i++)
            {
                ListViewItem brush = new ListViewItem();
                brush.ImageIndex = i;
                listBrushes.Items.Add(brush);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBrushes.SelectedIndices.Count == 1)
            {
                float scale = (float)trackBar1.Value / (float)trackBar1.Maximum;
                int i = listBrushes.SelectedIndices[0];
                Bitmap origin = (Bitmap)imgBrushes.Images[i];
                int width = (int)((float)origin.Width*scale);
                int height = (int)((float)origin.Height * scale);
                Bitmap destiny = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(destiny))
                { g.DrawImage(origin, new Rectangle(0, 0, width, height)); }
                Brush = new TextureBrush(destiny);                
                BrushChangeEventArgs ev = new BrushChangeEventArgs(Brush);
                BrushChanged?.Invoke(this, ev);
            }
        }

        public event EventHandler<BrushChangeEventArgs> BrushChanged;
    }
    public class BrushChangeEventArgs : EventArgs
    {
        public TextureBrush Brush { get; private set; }

        public BrushChangeEventArgs(TextureBrush brush)
        {
            Brush = brush;
        }
    }
}
