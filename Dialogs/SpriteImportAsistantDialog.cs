using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE.Dialogs
{
    public partial class SpriteImportAsistantDialog : Form
    {
        public Point _sPoint;
        private RectangleF[] _rects;

        public Bitmap[] Sprites { get; private set; }

        public SpriteImportAsistantDialog()
        {
            InitializeComponent();
        }

        private void LoadPicture(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            var d = dlg.ShowDialog();
            if (d == DialogResult.OK)
            {
                canvas.BackgroundImage = new Bitmap(dlg.FileName);
            } else { DialogResult = d; }
        }

        private void StartScroll(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { _sPoint = e.Location; }
        }

        private void WhileScrolling(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                using (Graphics g = canvas.CreateGraphics())
                {
                    SizeF rs = new SizeF(e.X - _sPoint.X, e.Y - _sPoint.Y);
                    SizeF fs = new SizeF(rs.Width / (float)numCols.Value, rs.Height / (float)numRows.Value);
                    _rects = new RectangleF[(int)(numCols.Value * numRows.Value)];
                    int i = 0;

                    for (float x = 0; x < rs.Width; x+=fs.Width)
                    {
                        for (float y = 0; y < rs.Height; y += fs.Height)
                        {
                            PointF p = new PointF(x, y);
                            _rects[i] = new RectangleF(p, fs);
                            i++;
                        }
                    }
                    canvas.Refresh();
                    g.DrawRectangles(Pens.Black, _rects);
                }
            }
        }

        private void EndScroll(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sprites = new Bitmap[_rects.Length];
            for (int i = 0; i < _rects.Length; i++)
            {
                Sprites[i] = new Bitmap((int)_rects[i].Width, (int)_rects[i].Height);
                using(Graphics g = Graphics.FromImage(Sprites[i]))
                {
                    g.DrawImage(canvas.BackgroundImage, 0, 0, _rects[i], GraphicsUnit.Pixel);
                }
            }
        }
    }
}
