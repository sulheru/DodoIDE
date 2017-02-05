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
using System.IO;
using DodoIDE.Classes;

namespace DodoIDE.Controls
{
    public partial class SourceCodeController : UserControl, ICommonMenuOptions
    {
        public PanelController<SourceEditorControl> CurrentSource { get; set; }

        public FileInfo FileInfo { get; set; }

        public SourceCodeController()
        {
            InitializeComponent();
            CurrentSource = new PanelController<SourceEditorControl>(new SourceEditorControl());
            CurrentSource.Controller.SelectControl += EditorSelectControl;
            CurrentSource.Controller.ParentController = this;
        }

        private void EditorSelectControl(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        public event EventHandler<ToolLoadEventArgs> ToolLoad;
        public event EventHandler SelectControl;
        public event EventHandler CloseAction;

        public void Copy()
        {
            CurrentSource.Controller.SourceEditorBox.Copy();
        }

        public void Delete()
        {
            int start = CurrentSource.Controller.SourceEditorBox.SelectionStart;
            int len = CurrentSource.Controller.SourceEditorBox.SelectedText.Length;
            CurrentSource.Controller.SourceEditorBox.DeleteRange(start, len);
        }

        public void LoadFile()
        {
            CurrentSource.Controller.SourceEditorBox.Text = File.ReadAllText(FileInfo.FullName);
            CurrentSource.Text = (new FileInfo(FileInfo.FullName)).Name;
        }

        public void Paste()
        {
            CurrentSource.Controller.SourceEditorBox.Paste();
        }

        public void Redo()
        {
            CurrentSource.Controller.SourceEditorBox.Redo();
        }

        public void SaveFile()
        {
            File.WriteAllText(FileInfo.FullName, CurrentSource.Controller.SourceEditorBox.Text);
        }

        public void Undo()
        {
            CurrentSource.Controller.SourceEditorBox.Undo();
        }

        private void SourceCodeController_Load(object sender, EventArgs e)
        {
            ToolLoadEventArgs tools = new ToolLoadEventArgs();
            ToolLoad?.Invoke(this, tools);
            CurrentSource.SetPanel(tools.ToolStrip, tools.Panel);
        }

        public override string ToString()
        {
            return "Propiedades";
        }

        public void CreateFile()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void SelectAll()
        {
            throw new NotImplementedException();
        }

        public void Find()
        {
            throw new NotImplementedException();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        public static TemplateObject Template()
        {
            var returned = new TemplateObject()
            {
                Icon = Properties.Resources.SourceFile,
                Item = new ListViewItem()
                {
                    Text = "Nuevo código fuente",
                    ImageKey = "prg",
                },
                FileName = "Sprite.inc",
                ImageListKey = "prg",
                CreateAction = delegate (string fn) {
                    File.Create(fn);
                }
            };
            returned.Item.Tag = returned;
            return returned;
        }

        public void SetMeOnTop()
        {
            CurrentSource.Controller.BringToFront();
        }
    }
}
