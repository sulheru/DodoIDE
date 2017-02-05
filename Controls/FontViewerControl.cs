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
using FenixLib.IO;
using FenixLib.Core;
using FenixLib.BitmapConvert;
using System.IO;
using DodoIDE.Classes;
using FenixLib;
using DodoIDE.Dialogs;
using System.Drawing.Imaging;

namespace DodoIDE.Controls
{
    public partial class FontViewerControl : UserControl, ICommonMenuOptions
    {
        public static Bitmap CropGlyph(Bitmap glyph) { return CropGlyph(glyph, 0); }
        public static Bitmap CropGlyph(Bitmap glyph, int margin)
        {
            int left = glyph.Width;
            int right = 0;
            int pix = glyph.GetPixel(0, 0).ToArgb();
            for (int y = 0; y < glyph.Height; y++)
            {
                for (int x = 0; x < glyph.Width; x++)
                {
                    if (glyph.GetPixel(x, y).ToArgb() != pix)
                    {
                        if (x < left) left = x;
                        if (x > right) right = x;
                    }
                }
            }
            if (left == glyph.Width ||
             right == 0) { return glyph; }
            Rectangle crop = new Rectangle(left, 0, right - left, glyph.Height);
            Bitmap target = new Bitmap(crop.Width + 2 * margin, crop.Height, glyph.PixelFormat);

            using (Graphics g = Graphics.FromImage(target))
            {
                Rectangle destRect = new Rectangle((target.Width - crop.Width) / 2,
                    0, crop.Width, crop.Height);
                g.DrawImage(glyph, destRect, crop, GraphicsUnit.Pixel);
            }

            return target;
        }

        public FontViewerControl()
        {
            InitializeComponent();
            Canvas = new PanelController<Panel>(new Panel());
            Canvas.Controller.BackColor = Color.Black;
            FontFile = new BitmapFont(FontEncoding.CP850, new Palette());
        }

        public FileInfo FileInfo { get; set; }
        public PanelController<Panel> Canvas { get; set; }
        public BitmapFont FontFile { get; set; }

        public event EventHandler SelectControl;
        public event EventHandler<ToolLoadEventArgs> ToolLoad;
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
            NativeFile.LoadFnt(FileInfo.FullName);
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
            NativeFile.SaveToFnt(FontFile, FileInfo.FullName);
        }

        public void SelectAll()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        private void Control_Click(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        public static TemplateObject Template()
        {
            var returned = new TemplateObject()
            {
                Icon = Properties.Resources.FontIcon,
                Item = new ListViewItem()
                {
                    Text = "Nueva fuente de bitmaps",
                    ImageKey = "fnt",
                },
                FileName = "newFont.fnt",
                ImageListKey = "fnt",
                CreateAction = delegate (string fn) {
                    BitmapFont font = new BitmapFont(FontEncoding.ISO85591, GraphicFormat.Format16bppRgb565);
                    NativeFile.SaveToFnt(font, fn);
                }
            };
            returned.Item.Tag = returned;
            return returned;
        }

        public void SetMeOnTop()
        {
            Canvas.Controller.BringToFront();
        }

        private void FontViewerControl_Load(object sender, EventArgs e)
        {
            label1.Text = FormatFontDescription(fontDialog1.Font.ToString());
            ToolLoadEventArgs tools = new ToolLoadEventArgs();
            ToolLoad?.Invoke(this, tools);
            Canvas.SetPanel(tools.ToolStrip, tools.Panel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            label1.Text = FormatFontDescription(fontDialog1.Font.ToString());
        }

        private string FormatFontDescription(string v)
        { return v.Substring(1, v.Length - 2).Replace(", ", "\n"); }

        private void button4_Click(object sender, EventArgs e)
        {
            fntColor.ShowDialog();
            button4.BackColor = fntColor.Color;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            brdColor.ShowDialog();
            button4.BackColor = brdColor.Color;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                ColorDialog dlg = new ColorDialog();
                dlg.ShowDialog();
                ((Shadow)listBox1.SelectedItem).Color = dlg.Color;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(new Shadow());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox1.Items.Remove(listBox1.SelectedItem);
            }
        }

        private void posChange(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                ((Shadow)listBox1.SelectedItem).Location = new Point((int)brdWidth.Value, (int)posLeft.Value);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = ((Shadow)listBox1.SelectedItem);
            if (s == null) return;
            posLeft.Value = s.Location.X;
            posTop.Value = s.Location.Y;
            shdClrChange.BackColor = s.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadingDialog dlg = new LoadingDialog();
            dlg.DoWork += Dlg_DoWork;
            Shadow[] shadows = new Shadow[listBox1.Items.Count];
            listBox1.Items.CopyTo(shadows, 0);
            object[] args = new object[]
            {
                chrCapital.Checked,
                chrLower.Checked,
                chrNumbers.Checked,
                chrMisc.Checked,
                fontDialog1.Font,
                fntColor.Color,
                glyWidth.Value,
                glyHeight.Value,
                (int)brdWidth.Value,
                brdColor.Color,
                shadows,
                new PointF((float)padLeft.Value, (float)padTop.Value)
            };
            FontFile = new BitmapFont(FontEncoding.ISO85591, GraphicFormat.Format16bppRgb565);
            dlg.RunLoaderAsync(args);
            var d=dlg.ShowDialog();
            if (d == DialogResult.OK) { DrawFont(); }
        }

        private void DrawFont()
        {
            using (StringReader sr = new StringReader(richTextBox1.Text))
            {
                object str;
                int left = 10;
                int top = 10;
                using (Graphics g = Canvas.Controller.CreateGraphics())
                {
                    g.Clear(Color.Black);
                    while ((str = sr.ReadLine()) != null)
                    {
                        char[] chrs = str.ToString().ToCharArray();
                        int hi = 0; ;
                        foreach (char chr in chrs)
                        {
                            Bitmap glyph = FontFile[chr]?.ToBitmap();
                            if (glyph != null)
                            {
                                g.DrawImage(glyph, new Point(left, top));
                                left += glyph.Width;
                                hi = glyph.Height;
                            }
                        }
                        left = 10;
                        top += hi;
                    }
                }
            }
        }

        private void Dlg_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] args = (object[])e.Argument;
            Font font = (Font)args[4];
            Color color = (Color)args[5];
            int wi = Convert.ToInt32(args[6]);
            int hi = Convert.ToInt32(args[7]);
            int bWi = Convert.ToInt32(args[8]);
            Color bCl = (Color)args[9];
            Shadow[] shadows = (Shadow[])args[10];
            PointF point = (PointF)args[11];
            var glyph = CreateGlyph(' ',  font, color, point, bWi, bCl, shadows);
            FontFile[' '] = glyph;

            var bar = (LoadingDialog)sender;
            if ((bool)args[3])
            {
                for (byte c = 33; c < 48; c++)
                {
                    FontFile[Convert.ToChar(c)] = CreateGlyph(Convert.ToChar(c),  font, color, point, bWi, bCl, shadows);
                    bar.ReportProgress((c / 255) * 100);
                }
            }
            if ((bool)args[2])
            {
                for (byte c = 48; c < 58; c++)
                {
                    FontFile[Convert.ToChar(c)] = CreateGlyph(Convert.ToChar(c),  font, color, point, bWi, bCl, shadows);
                    bar.ReportProgress((c / 255) * 100);
                }
            }
            if ((bool)args[3])
            {
                for (byte c = 58; c < 65; c++)
                {
                    FontFile[Convert.ToChar(c)] = CreateGlyph(Convert.ToChar(c),  font, color, point, bWi, bCl, shadows);
                    bar.ReportProgress((c / 255) * 100);
                }
            }
            if ((bool)args[0])
            {
                for (byte c = 65; c < 91; c++)
                {
                    FontFile[Convert.ToChar(c)] = CreateGlyph(Convert.ToChar(c),  font, color, point, bWi, bCl, shadows);
                    bar.ReportProgress((c / 255) * 100);
                }
            }
            if ((bool)args[3])
            {
                for (byte c = 91; c < 97; c++)
                {
                    FontFile[Convert.ToChar(c)] = CreateGlyph(Convert.ToChar(c),  font, color, point, bWi, bCl, shadows);
                    bar.ReportProgress((c / 255) * 100);
                }
            }
            if ((bool)args[1])
            {
                for (byte c = 97; c < 123; c++)
                {
                    FontFile[Convert.ToChar(c)] = CreateGlyph(Convert.ToChar(c),  font, color, point, bWi, bCl, shadows);
                    bar.ReportProgress((c / 255) * 100);
                }
            }
            if ((bool)args[3])
            {
                for (byte c = 123; c < 255; c++)
                {
                    FontFile[Convert.ToChar(c)] = CreateGlyph(Convert.ToChar(c),  font, color, point, bWi, bCl, shadows);
                    bar.ReportProgress((c / 255) * 100);
                }
            }
        }

        private IGlyph CreateGlyph(char chr,  
            Font font, Color color,PointF padding, int bWi, 
            Color bCl, Shadow[] shadows)
        {
            int wi = (int)(font.Size * 4.0f / 3.0f) + bWi;

            Bitmap bmp = new Bitmap(wi, wi, PixelFormat.Format16bppRgb565);
            using(Graphics g = Graphics.FromImage(bmp))
            {
                Font fnt = new Font(font.FontFamily, font.Size + bWi);
                foreach(Shadow sh in shadows)
                {
                    PointF p = new PointF(padding.X + sh.Location.X, padding.Y + sh.Location.Y);
                    g.DrawString(chr.ToString(), fnt, new SolidBrush(sh.Color), p);
                }
                PointF b = new PointF(padding.X - bWi, padding.Y - bWi);
                g.DrawString(chr.ToString(), fnt, new SolidBrush(bCl), b);
                g.DrawString(chr.ToString(), font, new SolidBrush(color), padding);
            }
            return new Glyph(CropGlyph(bmp, (int)padding.X).ToGraphic());
        }

        public static PixelFormat GetPixelFormat(GraphicFormat graphicFormat)
        {
            if (graphicFormat == GraphicFormat.Format16bppRgb565) { return PixelFormat.Format16bppRgb565; }
            else if (graphicFormat == GraphicFormat.Format8bppIndexed) { return PixelFormat.Format8bppIndexed; }
            else if (graphicFormat == GraphicFormat.Format1bppMonochrome) { return PixelFormat.Format1bppIndexed; }
            throw new ArgumentOutOfRangeException();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            DrawFont();
        }
    }

    public class Shadow
    {
        public Color Color { get; set; }
        public Point Location { get; set; }

        public override string ToString()
        {
            return string.Format("Location: {0}, Color{1}", Location.ToString(), Color.ToString());
        }
    }
}
