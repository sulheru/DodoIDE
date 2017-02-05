using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DodoIDE.Interfaces;

namespace DodoIDE.Controls.MapEditorInterface
{
    public partial class SprayTool : UserControl,IMapEditorTool
    {
        public Cursor ToolCursor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public SprayTool()
        {
            InitializeComponent();
        }

        public event EventHandler SelectTool;
        public CheckBox GetToolButton()
        {
            CheckBox chk = new CheckBox();

            chk.Text = "";
            chk.Appearance = Appearance.Button;
            chk.BackgroundImage = Properties.Resources.spray;
            chk.BackgroundImageLayout = ImageLayout.Zoom;
            chk.Size = new Size(32, 32);
            chk.Tag = this;
            chk.UseVisualStyleBackColor = true;
            chk.Click += Chk_Click;

            ToolTip tt = new ToolTip();

            // Set up the delays for the ToolTip.
            tt.AutoPopDelay = 5000;
            tt.InitialDelay = 1000;
            tt.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            tt.ShowAlways = true;

            tt.SetToolTip(chk, "Spray");

            return chk;
        }

        private void Chk_Click(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            foreach (Control cb in ((CheckBox)sender).Parent.Controls)
            { if (cb != sender) { ((CheckBox)cb).Checked = false; } }
            SelectTool?.Invoke(this, e);
        }

        public UserControl GetToolConfig()
        {
            return this;
        }

        public void ToolMouseCapture(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseClick(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseDoubleClick(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseDown(MapEditorControl sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Right){ _sprayMe(sender.CurrentImage, sender.BackBrush.Color, e.Location); }
            else if(e.Button==MouseButtons.Left){ _sprayMe(sender.CurrentImage, sender.ForeBrush.Color, e.Location); }
            
        }

        public void _sprayMe(Bitmap bmp, Color c, Point e)
        {
            Random rnd = new Random();
            double x = 2 * rnd.NextDouble() * trSize.Value - trSize.Value;
            double y = 2 * rnd.NextDouble() * trSize.Value - trSize.Value;
            if (trSize.Value >= Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2)))
            {
                bmp.SetPixel(e.X + (int)x, e.Y + (int)y, c);
            }
        }

        public void ToolMouseEnter(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseHover(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseLeave(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseMove(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseUp(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseWheel(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
