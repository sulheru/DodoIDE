using System;
using System.Linq;
using System.Windows.Forms;
using FenixLib.Core;
using FenixLib.BitmapConvert;
using DodoIDE.Interfaces;
using FenixLib.IO;

namespace DodoIDE.Controls
{
    public partial class MapViewerControl : UserControl, ICommonMenuOptions
    {
        public bool ShowProperties
        {
            get { return graphProps.Visible; }
            set { graphProps.Visible = value; }
        }

        public MapViewerControl()
        {
            InitializeComponent();
        }

        public Sprite CurrentSprite { get; private set; }

        public bool TemplateExculded
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string TemplateLabel
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string TemplateGroup
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler<ToolLoadEventArgs> ToolLoad;

        public void Copy()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void LoadFile(string filename)
        {
            LoadFile(NativeFile.LoadMap(filename));
        }

        public void LoadFile(ISprite file)
        {
            CurrentSprite = (Sprite)file;
            Canvas.Image = CurrentSprite.ToBitmap();
            PivotPoint[] pps = file.PivotPoints.ToArray();
            foreach(PivotPoint pp in pps) { lstPivots.Items.Add(pp); }
            txtHi.Text = file.Height.ToString();
            txtWi.Text = file.Width.ToString();
            txtFormat.Text = file.GraphicFormat.ToString();
            TxtDesc.Text = file.Description;
        }

        public void Paste()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void SaveFile(string filename)
        {
            NativeFile.SaveToMap(CurrentSprite, filename);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        private void TxtDesc_TextChanged(object sender, EventArgs e)
        {
            CurrentSprite.Description = TxtDesc.Text;
        }

        public void CreateFile()
        {
            throw new NotImplementedException();
        }
    }
}
