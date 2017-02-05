using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DodoIDE.Interfaces;
using FenixLib.IO;
using FenixLib.Core;
using FenixLib.BitmapConvert;
using DodoIDE.Classes;
using System.IO;
using DodoIDE.Dialogs;
using System.Drawing.Imaging;

namespace DodoIDE.Controls
{
    public partial class FileViewerControl : UserControl, ICommonMenuOptions
    {
        private ListViewItem[] _list;

        public static Bitmap BppConverter(Image img, PixelFormat format)
        {
            var bmp = new Bitmap(img.Width, img.Height, format);
            using (Graphics gr = Graphics.FromImage(bmp))
            { gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height)); }
            return bmp;
        }

        public static Bitmap BppConverter(Image img, GraphicFormat format)
        {
            if (format == GraphicFormat.Format1bppMonochrome) { return BppConverter(img, PixelFormat.Format8bppIndexed); }
            else if (format == GraphicFormat.Format8bppIndexed) { return BppConverter(img, PixelFormat.Format1bppIndexed); }
            else if (format == GraphicFormat.Format16bppRgb565) { return BppConverter(img, PixelFormat.Format16bppRgb565); }

            throw new ArgumentException("GraphicFormat is not supported");
        }

        public FileViewerControl()
        {
            InitializeComponent();
            CurrentViewer = new PanelController<SpriteViewerControl>(new SpriteViewerControl());
            CurrentViewer.Controller.ParentController = this;
        }

        public ISpriteAssortment CurrentAssortment { get; private set; }

        public FileInfo FileInfo { get; set; }
        public PanelController<SpriteViewerControl> CurrentViewer { get; private set; }
        
        public event EventHandler<ToolLoadEventArgs> ToolLoad;
        public event EventHandler SelectControl;
        public event EventHandler CloseAction;

        public void Copy()
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem[] arr = new ListViewItem[listView1.SelectedItems.Count];
                listView1.SelectedItems.CopyTo(arr, 0);
                Clipboard.SetDataObject(arr);
            }
        }

        public void Delete()
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                listView1.Items.Remove(item);
            }
        }

        public void LoadFile()
        {
            CurrentAssortment = NativeFile.LoadFpg(FileInfo.FullName);
            PopulateList();
        }

        private void PopulateList()
        {
            imageList1.Images.Clear();
            listView1.Items.Clear();
            foreach (SpriteAssortmentSprite sprite in CurrentAssortment)
            {
                AddSprite(sprite);
            }
        }
        private void AddSprite(int id, Sprite sprite, string description)
        {
            var sp = new SpriteAssortmentSprite(id, sprite) { Description = description };
            AddSprite(sp);
        }

        private void AddSprite(SpriteAssortmentSprite sprite)
        {
            Bitmap bmp = GetMiniature(sprite.ToBitmap());
            string key = GetSpriteId(sprite.Id, 3);
            imageList1.Images.Add(key, bmp);
            var item = listView1.Items.Add(key, key);
            item.Tag = BppConverter(sprite.ToBitmap(), sprite.GraphicFormat);
            item.SubItems.Add(sprite.Description);
        }

        public static string GetSpriteId(int id, int len)
        {
            string key = id.ToString();
            while (key.Length < len) { key = "0" + key; }
            return key;
        }

        private Bitmap GetMiniature(Bitmap origin)
        {
            Bitmap bmp = new Bitmap(100, 100);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                float w = 100.0f;
                float h = 100.0f;
                if (origin.Height > origin.Width)
                { w = (100.0f / (float)origin.Height) * (float)origin.Width; }
                else { h = (100.0f / (float)origin.Width) * (float)origin.Height; }
                float t = (100.0f - h) / 2;
                float l = (100.0f - w) / 2;
                RectangleF rect = new RectangleF(l, t, w, h);

                g.DrawImage(origin, rect);

            }
            return bmp;
        }

        public void Paste()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                object obj = Clipboard.GetDataObject();
                if (obj.GetType() == typeof(ListViewItem[]))
                {
                    ListViewItem[] arr = (ListViewItem[])obj;
                    int i = listView1.Items.IndexOf(listView1.SelectedItems[0]);
                    foreach (ListViewItem item in arr)
                    { listView1.Items.Insert(i, item); }
                }
            }
        }

        public void SaveFile()
        {
            LoadingDialog dlg = new LoadingDialog();
            dlg.DoWork += Dlg_DoWork;
            _list = new ListViewItem[listView1.Items.Count];
            listView1.Items.CopyTo(_list, 0);
            dlg.RunLoaderAsync(FileInfo.FullName);
            dlg.ShowDialog();
        }

        private void Dlg_DoWork(object sender, DoWorkEventArgs e)
        {
            var assortment = new SpriteAssortment(CurrentAssortment.GraphicFormat, (CurrentAssortment.Palette == null) ? new Palette() : CurrentAssortment.Palette);
            string filename = e.Argument.ToString();
            List<ListViewItem> items = new List<ListViewItem>();
            items.AddRange(_list);

            foreach (ListViewItem item in items)
            {
                int id = Convert.ToInt32(item.Text);
                Bitmap bmp = BppConverter(((Bitmap)item.Tag), assortment.GraphicFormat);
                var sprite = new SpriteAssortmentSprite(id, new Sprite(bmp.ToGraphic()));
                sprite.Description = item.SubItems[1]?.Text;
                assortment.Add(id, sprite);
                //assortment.Sprites.Add(sprite);
                float i = items.IndexOf(item);
                float l = items.Count;
                float p = (i / l) * 100.0f;
                string msg = string.Format("Objeto {0}", item.Text);
                ((LoadingDialog)sender).ReportProgress((int)p, msg);
            }

            CurrentAssortment = assortment;
            NativeFile.SaveToFpg(CurrentAssortment, filename);
        }

        public static TemplateObject Template1bpp()
        {
            var returned = new TemplateObject()
            {
                Icon = Properties.Resources.Package,
                Item = new ListViewItem()
                {
                    Text = "Nueva colección de sprites de 8bpp",
                    ImageKey = "fpg",
                },
                FileName = "SpriteFile.fpg",
                ImageListKey = "fpg",
                CreateAction = delegate (string fn) { NativeFile.SaveToFpg(new SpriteAssortment(GraphicFormat.Format1bppMonochrome, new Palette()), fn); }
            };
            returned.Item.Tag = returned;
            return returned;
        }

        public static TemplateObject Template8bpp()
        {
            var returned = new TemplateObject()
            {
                Icon = Properties.Resources.Package,
                Item = new ListViewItem()
                {
                    Text = "Nueva colección de sprites de 8bpp",
                    ImageKey = "fpg",
                },
                FileName = "SpriteFile.fpg",
                ImageListKey = "fpg",
                CreateAction = delegate (string fn) { NativeFile.SaveToFpg(new SpriteAssortment(GraphicFormat.Format8bppIndexed, new Palette()), fn); }
            };
            returned.Item.Tag = returned;
            return returned;
        }

        public static TemplateObject Template16bpp()
        {
            var returned = new TemplateObject()
            {
                Icon = Properties.Resources.Package,
                Item = new ListViewItem()
                {
                    Text = "Nueva colección de sprites de 16bpp",
                    ImageKey = "fpg",
                },
                FileName = "SpriteFile.fpg",
                ImageListKey = "fpg",
                CreateAction = delegate (string fn) { NativeFile.SaveToFpg(new SpriteAssortment(GraphicFormat.Format16bppRgb565, new Palette()), fn); }
            };
            returned.Item.Tag = returned;
            return returned;
        }

        public void Close()
        {
            CurrentViewer.Controller.Close();
            CloseAction?.Invoke(this, new EventArgs());
        }

        public void Find()
        {
            InputDialog dlg = new InputDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                listView1.FindItemWithText(dlg.Text);
            }
        }

        private void Control_Click(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        private void existenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlg.FileNames)
                {
                    Bitmap bmp = BppConverter(new Bitmap(file), CurrentAssortment.GraphicFormat);
                    AddSprite(listView1.Items.Count + 1, new Sprite(bmp.ToGraphic()), (new FileInfo(file)).Name);
                }
            }
        }

        private void iniciarAsistenteDeImportaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpriteImportAsistantDialog dlg = new SpriteImportAsistantDialog();
            var d = dlg.ShowDialog();
            if (d == DialogResult.OK)
            {
                foreach (Bitmap bmp in dlg.Sprites)
                {
                    AddSprite(listView1.Items.Count + 1, new Sprite(bmp.ToGraphic()), "");
                }
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listView1.SelectedItems)
            {
                listView1.Items.Remove(item);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            { CurrentViewer.Controller.Sprite = listView1.SelectedItems[0]; }
        }

        private void FileViewerControl_Load(object sender, EventArgs e)
        {
            ToolLoadEventArgs args = new ToolLoadEventArgs();
            ToolLoad?.Invoke(this, args);
            CurrentViewer.Text = FileInfo.Name;
            CurrentViewer.Controller.FileViewerParent = this;
            CurrentViewer.SetPanel(args.ToolStrip, args.Panel);
        }

        public override string ToString()
        {
            return "Sprite List View";
        }

        public void SetMeOnTop()
        {
            CurrentViewer.Controller.BringToFront();
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
