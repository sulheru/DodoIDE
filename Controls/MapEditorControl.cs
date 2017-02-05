using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DodoIDE.Interfaces;
using System.IO;
using DodoIDE.Classes;
using FenixLib.IO;
using FenixLib.Core;
using FenixLib.BitmapConvert;
using DodoIDE.Dialogs;
using System.Reflection;

namespace DodoIDE.Controls
{
    public partial class MapEditorControl : UserControl, ICommonMenuOptions
    {
        public SolidBrush ForeBrush { get; set; }
        public SolidBrush BackBrush { get; set; }

        public static Point[] PixelLock(Point p, Bitmap bmp, Color c, float t)
        {
            List<Point> LockedPixels = new List<Point>();
            PixelLockList(LockedPixels, p, bmp, c, t);
            return LockedPixels.ToArray();
        }

        public static void PixelLockList(List<Point> LockedPixels, Point p,
                                        Bitmap bmp, Color c, float t)
        {
            if (!LockedPixels.Contains(p))
            {
                while (t > 1) { t = t / 10; }
                int minR = c.R - (int)(256.0 * t);
                int minG = c.G - (int)(256.0 * t);
                int minB = c.B - (int)(256.0 * t);
                int maxR = c.R + (int)(256.0 * t);
                int maxG = c.G + (int)(256.0 * t);
                int maxB = c.B + (int)(256.0 * t);
                try
                {
                    if (minR < bmp.GetPixel(p.X, p.Y).R &&
                    minG < bmp.GetPixel(p.X, p.Y).G &&
                    minB < bmp.GetPixel(p.X, p.Y).B &&
                    maxR > bmp.GetPixel(p.X, p.Y).R &&
                    maxG > bmp.GetPixel(p.X, p.Y).G &&
                    maxB > bmp.GetPixel(p.X, p.Y).B)
                    {
                        LockedPixels.Add(p);
                        Point p11 = new Point(p.X - 1, p.Y - 1);
                        Point p12 = new Point(p.X - 1, p.Y);
                        Point p13 = new Point(p.X - 1, p.Y + 1);
                        Point p21 = new Point(p.X, p.Y - 1);
                        Point p23 = new Point(p.X, p.Y + 1);
                        Point p31 = new Point(p.X + 1, p.Y - 1);
                        Point p32 = new Point(p.X + 1, p.Y);
                        Point p33 = new Point(p.X + 1, p.Y + 1);
                        PixelLockList(LockedPixels, p11, bmp, c, t);
                        PixelLockList(LockedPixels, p12, bmp, c, t);
                        PixelLockList(LockedPixels, p13, bmp, c, t);
                        PixelLockList(LockedPixels, p21, bmp, c, t);
                        PixelLockList(LockedPixels, p23, bmp, c, t);
                        PixelLockList(LockedPixels, p31, bmp, c, t);
                        PixelLockList(LockedPixels, p32, bmp, c, t);
                        PixelLockList(LockedPixels, p33, bmp, c, t);
                    }
                }
                catch { return; }
            }
        }

        public MapEditorControl()
        {
            InitializeComponent();
            LoadingDialog dlg = new LoadingDialog();
            dlg.DoWork += Dlg_DoWork;
            dlg.RunLoaderAsync();
            var d = dlg.ShowDialog();
            Dock = DockStyle.Fill;

            Panel canvas = new Panel();
            canvas.BackColor = Color.Black;
            canvas.Dock = DockStyle.Fill;
            canvas.Visible = true;
            CurrentCanvas = new PanelController<Panel>(canvas);
            pictureBox1.Image = new Bitmap(50, 50);
            BackBrush = (SolidBrush)Brushes.Black;
            ForeBrush = (SolidBrush)Brushes.White;
        }

        private void Dlg_DoWork(object sender, DoWorkEventArgs e)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes();
            var tools = (from tp in types
                         where typeof(IMapEditorTool).IsAssignableFrom(tp)
                         && !tp.IsInterface
                         select tp).ToList();
            foreach(Type tp in tools)
            {
                ConstructorInfo ctor = tp.GetConstructor(new Type[0]);
                IMapEditorTool tool = (IMapEditorTool)ctor.Invoke(new object[0]);
                pnlToolBox.Controls.Add(tool.GetToolButton());
                tool.SelectTool += Tool_SelectTool;
            }

        }

        private void Tool_SelectTool(object sender, EventArgs e)
        {
            CurrentTool = (IMapEditorTool)sender;
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(CurrentTool.GetToolConfig());  
        }
        
        public FileInfo FileInfo { get; set; }
        public bool IsFromAssortment { get; private set; }
        public Bitmap CurrentImage { get; private set; }
        public SpriteAssortment CurrentAssortment { get; private set; }
        public SpriteAssortmentSprite CurrentMap { get; private set; }
        public IMapEditorTool CurrentTool { get; private set; }
        public bool IsInFpg { get; private set; }

        public PanelController<Panel> CurrentCanvas { get; private set; }

        public event EventHandler<ToolLoadEventArgs> ToolLoad;
        public event EventHandler SelectControl;
        public event EventHandler CloseAction;

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Copy()
        {
            throw new NotImplementedException();
        }

        public void CreateFile()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void Find()
        {
            throw new NotImplementedException();
        }

        public void LoadFile()
        {
            if (FileInfo == null) { frmMain.OpenFileDialog("Archivo de Sprite|*.map", FileInfo); }
            if (FileInfo != null)
            {
                CurrentMap = new SpriteAssortmentSprite(0, NativeFile.LoadMap(FileInfo.FullName));
                CurrentImage = CurrentMap.ToBitmap();
                paletteControl1.CurrentPalette = PaletteControl.DefaultPalette();
            }
        }

        private void PaletteControl1_ColorSelect(object sender, ColorSelectEventArgs e)
        {
            if (e.Button != MouseButtons.None)
            {
                if (e.Button == MouseButtons.Left) { ForeBrush = new SolidBrush(e.SystemColor); }
                else if (e.Button == MouseButtons.Right) { BackBrush = new SolidBrush(e.SystemColor); }
                Bitmap bmp = new Bitmap(50, 50);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.Black);
                    g.FillRectangle(BackBrush, new Rectangle(15, 15, 30, 30));
                    g.FillRectangle(ForeBrush, new Rectangle(5, 5, 30, 30));
                }
                pictureBox1.Image = bmp;
            }
        }

        public void Paste()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void SaveFile()
        {
            if (IsInFpg)
            {
                if (FileInfo == null) { frmMain.SaveFileDialog("Archivo de Sprite|*.map", FileInfo); }
                if (FileInfo != null) { NativeFile.SaveToMap(GetSprite(), FileInfo.FullName); }
            }
        }

        public void SelectAll()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public ISprite GetSprite()
        {
            return new Sprite(CurrentImage.ToGraphic());
        }
        
        public static TemplateObject Template()
        {
            var returned = new TemplateObject()
            {
                Icon = Properties.Resources.Image_file,
                Item = new ListViewItem()
                {
                    Text = "Nuevo sprite",
                    ImageKey = "map",
                },
                FileName = "Sprite.map",
                ImageListKey = "map",
                CreateAction = delegate (string fn)
                {
                    MapSizeDialog dlg = new MapSizeDialog();
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bmp = new Bitmap(dlg.SpriteWidth, dlg.SpriteHeight);
                        Graphic g = (Graphic)bmp.ToGraphic();                        
                        Sprite s = new Sprite(g);
                        NativeFile.SaveToMap(s, fn);
                    }
                    else { throw new IOException("File creation aborted"); }
                }
            };
            returned.Item.Tag = returned;
            return returned;
        }

        private void Control_Click(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        public void SetMeOnTop()
        {
            
        }

        private void MapEditorControl_Load(object sender, EventArgs e)
        {
            ToolLoadEventArgs tools = new ToolLoadEventArgs();
            ToolLoad?.Invoke(this, tools);
            CurrentCanvas.SetPanel(tools.ToolStrip, tools.Panel);
        }
    }
}
