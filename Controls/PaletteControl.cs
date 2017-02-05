using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FenixLib.Core;
using DodoIDE.Interfaces;
using FenixLib.IO;
using System.IO;
using DodoIDE.Classes;

namespace DodoIDE.Controls
{
    public partial class PaletteControl : UserControl, ICommonMenuOptions
    {
        private const int SQUARE = 16;
        private Rectangle[] _rects = new Rectangle[256];
        private Palette _pal;

        public PaletteControlDisplayMode DisplayMode { get; set; }
        public Palette CurrentPalette
        {
            get { return (_pal == null) ? DefaultPalette() : _pal; }
            set
            {
                _pal = value;
                DrawPalette();
            }
        }

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

        public FileInfo FileInfo { get; set; }

        public static Palette DefaultPalette()
        {
            var pal = new Palette();
            int i = 0;

            for(int c = 15; c < 256; c += 16)
            { pal[i] = new PaletteColor(c, 0, 0); i++; } // 1 Rojos

            for (int c = 15; c < 256; c += 16)
            { pal[i] = new PaletteColor(0, c, 0); i++; } // 2 Verdes

            for (int c = 15; c < 256; c += 16)
            { pal[i] = new PaletteColor(0, 0, c); i++; } // 3 Azules

            for (int c = 15; c < 256; c += 16)
            { pal[i] = new PaletteColor(c, c, 0); i++; } // 4 Amarillos

            for (int c = 15; c < 256; c += 16)
            { pal[i] = new PaletteColor(c, 0, c); i++; } // 5 Violetas

            for (int c = 15; c < 256; c += 16)
            { pal[i] = new PaletteColor(0, c, c); i++; } // 6 Celestes

            for (int c = 7; c < 128; c += 8)
            { pal[i] = new PaletteColor(c*2, c, 0); i++; } // 7 Naranjas Oscuros

            for (int c = 135; c < 256; c += 8)
            { pal[i] = new PaletteColor(255, c, 0); i++; } // 8 Naranjas Claros

            for (int c = 7; c < 128; c += 8)
            { pal[i] = new PaletteColor(c * 2, 0, c); i++; } // 9 Fucsias Oscuros

            for (int c = 135; c < 256; c += 8)
            { pal[i] = new PaletteColor(255, 0, c); i++; } // 10 Fucsias Claros

            for (int c = 7; c < 128; c += 8)
            { pal[i] = new PaletteColor(0, c * 2, c); i++; } // 11 Celestes Oscuros

            for (int c = 135; c < 256; c += 8)
            { pal[i] = new PaletteColor(0, 255, c); i++; } // 12 Celestes Claros

            for (int c = 7; c < 128; c += 8)
            { pal[i] = new PaletteColor(c, c, c * 2); i++; } // 13 Violetas Oscuros

            for (int c = 135; c < 256; c += 8)
            { pal[i] = new PaletteColor(c, 0, 255); i++; } // 14 Violetas Claros

            for (int c = 7; c < 256; c += 8)
            { pal[i] = new PaletteColor(c, c, c); i++; } // 15, 16
            
            return pal;
        }

        public PaletteControl()
        {
            InitializeComponent();
            CurrentPalette = DefaultPalette();
        }

        public void LoadFile()
        {
            LoadFile(NativeFile.LoadPal(FileInfo.FullName));
        }

        public void LoadFile(Palette palette)
        {
            CurrentPalette = palette;            
            DrawPalette();
        }

        private void DrawGird()
        {
            Bitmap bmp = new Bitmap(256, 256);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < CurrentPalette.Colors.Length; i++)
                {
                    int x = i % SQUARE;
                    int y = i / SQUARE;
                    _rects[i] = new Rectangle(x * 16 + 1, y * 16 + 1, SQUARE - 2, SQUARE - 2);
                    var pc = CurrentPalette.Colors[i];
                    Color c = Color.FromArgb(pc.R, pc.G, pc.B);
                    g.FillRectangle(new SolidBrush(c), _rects[i]);
                }
            }
            pictureBox1.Image = bmp;
        }

        private void DrawSequence()
        {
            Bitmap bmp = new Bitmap(256 * 16, 32);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                for (int i = 0; i < CurrentPalette.Colors.Length; i++)
                {
                    _rects[i] = new Rectangle(i * 16 + 1, 1, SQUARE - 2, SQUARE*2 - 2);
                    var pc = CurrentPalette.Colors[i];
                    Color c = Color.FromArgb(pc.R, pc.G, pc.B);
                    g.FillRectangle(new SolidBrush(c), _rects[i]);
                }
            }
            pictureBox1.Image = bmp;
        }

        public void SaveFile()
        {
            NativeFile.SaveToPal(CurrentPalette, FileInfo.FullName);
        }

        private void DrawPalette()
        {
            switch (DisplayMode)
            {
                case PaletteControlDisplayMode.Gird: DrawGird(); break;
                case PaletteControlDisplayMode.Sequence: DrawSequence(); break;
            }
        }

        private void PaletteControl_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < 256; i++)
            {
                if (_rects[i].Contains(e.Location))
                {
                    ColorSelectEventArgs ev = new ColorSelectEventArgs(i, CurrentPalette[i]);
                    ev.Button = e.Button;
                    ColorSelect?.Invoke(this, ev);
                    break;
                }
            }
        }

        public event EventHandler<ColorSelectEventArgs> ColorSelect;
        public event EventHandler<ToolLoadEventArgs> ToolLoad;
        public event EventHandler SelectControl;
        public event EventHandler CloseAction;

        public void ChangeColorDialog(int index)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                CurrentPalette[index] = new PaletteColor(
                    dlg.Color.R, dlg.Color.G, dlg.Color.B);
                DrawPalette();
            }
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public void Redo()
        {
            throw new NotImplementedException();
        }

        public void Copy()
        {
            throw new NotImplementedException();
        }

        public void Paste()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        public static TemplateObject Template()
        {
            var returned = new TemplateObject()
            {
                Icon = Properties.Resources.Palette,
                Item = new ListViewItem()
                {
                    Text = "Nueva paleta de colores",
                    ImageKey = "pal",
                },
                FileName = "newPalette.pal",
                ImageListKey = "pal",
                CreateAction = delegate (string fn) {
                    NativeFile.SaveToPal(new Palette(), fn);
                }
            };
            returned.Item.Tag = returned;
            return returned;
        }

        public void SetMeOnTop()
        {
            BringToFront();
        }
    }

    public class ColorSelectEventArgs : EventArgs
    {
        private PaletteColor _paletteColor;

        public int ColorIndex { get; private set; }

        public PaletteColor PaletteColor
        {
            get { return _paletteColor; }
        }

        public Color SystemColor
        {
            get
            {
                return Color.FromArgb(
              _paletteColor.R,
              _paletteColor.G,
              _paletteColor.B
              );
            }
        }

        public MouseButtons Button { get; internal set; }

        public ColorSelectEventArgs(int i, PaletteColor paletteColor)
        {
            ColorIndex = i;
            _paletteColor = paletteColor;
        }
    }

    public enum PaletteControlDisplayMode
    {
        Gird, Sequence
    }
}
