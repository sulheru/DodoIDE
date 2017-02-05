using DodoIDE.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

namespace DodoIDE
{
    public class PanelController<T>
    {
        public T Controller { get; set; }
        private ToolStripButton _tabButton;
        private ToolStrip _currentBar;

        public string Text
        {
            get { return _tabButton.Text; }
            set { _tabButton.Text = value; }
        }

        public PanelController(T controller)
        {
            _tabButton = new ToolStripButton();
            _tabButton.Click += new EventHandler(setFocus);
            _tabButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            _tabButton.CheckOnClick = true;
            _tabButton.Visible = true;
            
            Controller = controller;
            var click = Controller.GetType().GetEvent("Click");
            var close = Controller.GetType().GetEvent("CloseAction");
            var dock = Controller.GetType().GetProperty("Dock");
            if (click != null) { CreateEvent(Controller, click, new EventHandler(setFocus)); }
            if (close != null) { CreateEvent(Controller, close, new EventHandler(CloseAction)); }
            if (dock != null) { dock.SetValue(Controller,DockStyle.Fill); }             
        }

        private void CreateEvent(T controller, EventInfo eventInfo, EventHandler handler)
        {            
            eventInfo.AddEventHandler(controller, handler);
        }

        public event EventHandler<EventArgs> GotFocus;

        public void setFocus(object sender, EventArgs e)
        {
            if (_currentBar != null)
            {
                ToolStripItem[] items = new ToolStripItem[_currentBar.Items.Count];
                _currentBar.Items.CopyTo(items, 0);
                var btns = (from item in items
                           where item.Alignment == ToolStripItemAlignment.Right
                           select item).ToArray();

                var tabs = (from item in items
                            where item.Alignment == ToolStripItemAlignment.Left 
                            && typeof(ToolStripButton).IsAssignableFrom(item.GetType())
                            select item).ToArray();

                foreach (ToolStripButton item in tabs)
                {
                    item.CheckState = CheckState.Unchecked;
                    _tabButton.Checked = false;
                }
                if (_tabButton.IsOnOverflow) { _currentBar.Items.Insert(btns.Length, _tabButton); }
                _tabButton.CheckState = CheckState.Checked;
                _tabButton.Checked = true;
                var btf = Controller.GetType().GetMethod("BringToFront");
                btf?.Invoke(Controller, new object[0]);
                var smot = Controller.GetType().GetMethod("SetMeOnTop");
                smot?.Invoke(Controller, new object[0]);
                GotFocus?.Invoke(sender, e);
            }
        }

        public void SetPanel(ToolStrip toolStrip,Panel panel)
        {
            Type tipo = typeof(T);
            var ctrl = (Control)((object)Controller);
            panel.Controls.Add(ctrl);
            ctrl.BringToFront();
            _currentBar = toolStrip;
            toolStrip.Items.Add(_tabButton);
        }

        public event EventHandler Closing;
        public event EventHandler Closed;

        private void CloseAction(object sender, EventArgs e)
        {
            var uc = ((UserControl)sender);
            Closing?.Invoke(this, new EventArgs());
            uc.Parent.Controls.Remove(uc);
            uc.Dispose();
            _tabButton.Dispose();
            Closed?.Invoke(this, new EventArgs());
        }
    }
}
