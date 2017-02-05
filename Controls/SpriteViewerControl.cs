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

namespace DodoIDE.Controls
{
    public partial class SpriteViewerControl : UserControl, ICommonMenuOptions
    {
        private ListViewItem _sprite;

        public event EventHandler<ToolLoadEventArgs> ToolLoad;
        public event EventHandler SelectControl;
        public event EventHandler CloseAction;

        public ListViewItem Sprite
        {
            get { return _sprite; }
            set
            {
                _sprite = value;
                numericUpDown1.Value = Convert.ToDecimal(value.Text);
                pictureBox1.Image = (Bitmap)value.Tag;
                richTextBox1.Text = value.SubItems[1]?.Text;
                sizeLabel.Text = pictureBox1.Image.Size.ToString();
            }
        }

        public FileViewerControl FileViewerParent { get; internal set; }

        public FileInfo FileInfo
        {
            get { return FileViewerParent.FileInfo; }
            set { FileViewerParent.FileInfo = value; }
        }

        public FileViewerControl ParentController { get; internal set; }

        public SpriteViewerControl()
        {
            InitializeComponent();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _sprite.Text = FileViewerControl.GetSpriteId(((int)numericUpDown1.Value), 3);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (_sprite.SubItems.Count <= 1)
            { _sprite.SubItems.Add(richTextBox1.Text); }
            else { _sprite.SubItems[1].Text = richTextBox1.Text; }
        }

        public void Close()
        {
            Dispose();
        }

        public void Copy()
        {
            FileViewerParent.Copy();
        }

        public void Paste()
        {
            FileViewerParent.Paste();
        }

        public void Delete()
        {
            FileViewerParent.Delete();
        }

        public void LoadFile()
        {
            FileViewerParent.LoadFile();
        }

        public void SaveFile()
        {
            FileViewerParent.SaveFile();
        }

        public void Find()
        {
            FileViewerParent.Find();
        }

        public void SetMeOnTop()
        {
            ParentController.BringToFront();
        }

        public void CreateFile()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void SelectAll()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
