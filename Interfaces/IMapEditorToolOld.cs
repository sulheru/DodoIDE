using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE.Controls.MapEditorInterface
{
    public interface IMapEditorToolOld
    {
        Cursor Cursor { get; }
        void OnMouseDown(MouseEventArgs e, DrawingObject d);
        void OnMouseUp(MouseEventArgs e, DrawingObject d);
        void OnMouseMove(MouseEventArgs e, DrawingObject d);
        void OnMousePress(MouseEventArgs e, DrawingObject d);
    }

    public class DrawingObject
    {
        public DrawingObject(Image src,Graphics canvas)
        {
            Graphic = Graphics.FromImage(src);
            Canvas = canvas;
            Brush = new SolidBrush(Color.Black);
            Pen = new Pen(Brush);
        }

        public Graphics Graphic { get; set; }
        public Brush Brush { get; set; }
        public Pen Pen { get; set; }
        public Graphics Canvas { get; set; }
    }
}
