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
    public partial class PoligoneTool : UserControl,IMapEditorTool
    {
        private List<Point> _points;
        public Cursor ToolCursor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public PoligoneTool()
        {
            InitializeComponent();
        }

        public event EventHandler SelectTool;

        public CheckBox GetToolButton()
        {
            CheckBox chk = new CheckBox();

            chk.Text = "";
            chk.Appearance = Appearance.Button;
            chk.BackgroundImage = Properties.Resources.polygon;
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

            tt.SetToolTip(chk, "Polígono");

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

        public void ToolMouseMove(MapEditorControl sender, MouseEventArgs e)
        {
            if(_points==null)_points=new List<Point>();
            if(_points.Count==1)
            {
                using(Graphics g=sender.CreateGraphics())
                { g.DrawLine(new Pen(sender.ForeBrush),_points[0],e.Location); }
                sender.Refresh();
            } else if(_points.Count>1)
            {
                using(Graphics g=sender.CreateGraphics())
                { 
                    Point[] p=new Point[_points.Count+1];
                    _points.CopyTo(p);
                    p[_points.Count]=e.Location;
                    g.FillPolygon(sender.BackBrush, p);
                    g.DrawPolygon(new Pen(sender.ForeBrush), p);
                }
                sender.Refresh();
            }
        }

        public void ToolMouseLeave(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseClick(MapEditorControl sender, MouseEventArgs e)
        {
            if(_points==null)_points=new List<Point>();
            _points.Add(e.Location);
        }

        public void ToolMouseHover(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseUp(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseDown(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseCapture(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseDoubleClick(MapEditorControl sender, MouseEventArgs e)
        {
            if(_points==null)_points=new List<Point>();
            if(_points.Count==1)
            {
                using(Graphics g=sender.CreateGraphics())
                { g.DrawLine(new Pen(sender.ForeBrush),_points[0],e.Location); }
            } else if(_points.Count>1)
            {
                using(Graphics g=sender.CreateGraphics())
                { 
                    _points.Add(e.Location);
                    g.FillPolygon(sender.BackBrush, _points.ToArray());
                    g.DrawPolygon(new Pen(sender.ForeBrush), _points.ToArray());
                }
            }
        }

        public void ToolMouseEnter(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void ToolMouseWheel(MapEditorControl sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
