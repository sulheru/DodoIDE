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
    public partial class SelectionTool : UserControl, IMapEditorTool
    {
        private Pen _pen_black;
        private Pen _pen_white;
        private MapEditorControl _sender;
        private Cursor _cursor;

        public Cursor ToolCursor
        { get { return _cursor; } }

        public Point StartLocation { get; private set; }
        public Graphics Canvas { get; private set; }
        public bool IsDoingSelection { get; private set; }
        public Rectangle CurrentSelection { get; private set; }

        public event EventHandler SelectTool;

        public CheckBox GetToolButton()
        {
            CheckBox chk = new CheckBox();

            chk.Text = "";
            chk.Appearance = Appearance.Button;
            chk.BackgroundImage = Properties.Resources.edit;
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

            tt.SetToolTip(chk, "Selección");

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

        public new void MouseDown(MapEditorControl sender, MouseEventArgs e)
        {
            StartLocation = e.Location;
            Canvas = sender.CurrentCanvas.Controller.CreateGraphics();
            IsDoingSelection = true;
        }

        public new void ToolMouseMove(MapEditorControl sender, MouseEventArgs e)
        {
            Rectangle rect = GetRectangle(e.Location);
            Canvas.DrawRectangle(_pen_black, rect);
            Canvas.DrawRectangle(_pen_white, rect);
            sender.CurrentCanvas.Controller.Refresh();
        }

        private Rectangle GetRectangle(Point location)
        {
            Size size = new Size(location.X - StartLocation.X, location.Y - StartLocation.Y);
            return new Rectangle(StartLocation, size);
        }

        public new void MouseUp(MapEditorControl sender, MouseEventArgs e)
        {
            CurrentSelection = GetRectangle(e.Location);
            _sender = sender;
            IsDoingSelection = false;
        }

        public new void MouseWheel(MapEditorControl sender, MouseEventArgs e) { }
        public void MouseCapture(MapEditorControl sender, MouseEventArgs e) { }
        public new void MouseClick(MapEditorControl sender, MouseEventArgs e) { }
        public new void MouseDoubleClick(MapEditorControl sender, MouseEventArgs e) { }
        public new void MouseEnter(MapEditorControl sender, MouseEventArgs e) { }
        public new void MouseHover(MapEditorControl sender, MouseEventArgs e) { }
        public new void MouseLeave(MapEditorControl sender, MouseEventArgs e) { }

        public SelectionTool()
        {
            InitializeComponent();
            var bmp = Properties.Resources.Selection;
            _cursor = new Cursor(bmp.GetHicon());
            Timer time = new Timer();
            time.Interval = 500;
            time.Tick += Time_Tick;
            _pen_black = new Pen(Brushes.Black);
            _pen_black.DashPattern = new float[] { 2, 2, 2, 2 };
            _pen_white = new Pen(Brushes.White);
            _pen_white.DashPattern = new float[] { 4, 2, 2, 2 };
            time.Start();
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            _pen_black.CompoundArray[0] += 1;
            if (_pen_black.CompoundArray[0] > 4)
            { _pen_black.CompoundArray[0] = 2; }

            _pen_white.CompoundArray[0] += 1;
            if (_pen_white.CompoundArray[0] > 4)
            { _pen_white.CompoundArray[0] = 2; }

            if (!IsDoingSelection)
            {
                Canvas.DrawRectangle(_pen_black, CurrentSelection);
                Canvas.DrawRectangle(_pen_white, CurrentSelection);
                //_sender.CurrentCanvas.Refresh();
            }
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
    }
}
