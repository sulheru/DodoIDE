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
    public partial class LineTool : UserControl, IMapEditorTool
    {
        private Point _start;
        public Cursor ToolCursor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public LineTool()
        {
            InitializeComponent();
        }

        public event EventHandler SelectTool;

        public CheckBox GetToolButton()
        {
            CheckBox chk = new CheckBox();

            chk.Text = "";
            chk.Appearance = Appearance.Button;
            chk.BackgroundImage = Properties.Resources.diagonal_line;
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

            tt.SetToolTip(chk, "Línea");

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

        public void ToolMouseLeave(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseClick(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseHover(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseCapture(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseDoubleClick(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseEnter(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseWheel(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseMove(MapEditorControl sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left) {
                using(Graphics g=sender.CurrentCanvas.Controller.CreateGraphics()){
                    g.DrawLine(new Pen(sender.ForeBrush),_start,e.Location);
                }
            }
        }

        public void ToolMouseUp(MapEditorControl sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left) {
                using(Graphics g=Graphics.FromImage(sender.CurrentImage)){
                    g.DrawLine(new Pen(sender.ForeBrush),_start,e.Location);
                }
            }
        }

        public void ToolMouseDown(MapEditorControl sender, MouseEventArgs e)
        {
            if(e.Button==MouseButtons.Left) _start=e.Location;
        }
    }
}
