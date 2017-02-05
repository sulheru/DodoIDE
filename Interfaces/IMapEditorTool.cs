using DodoIDE.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE.Interfaces
{
    public interface IMapEditorTool
    {
        Cursor ToolCursor { get; }
        void ToolMouseMove(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseLeave(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseClick(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseHover(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseUp(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseDown(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseCapture(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseDoubleClick(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseEnter(MapEditorControl sender, MouseEventArgs e);
        void ToolMouseWheel(MapEditorControl sender, MouseEventArgs e);
        CheckBox GetToolButton();
        UserControl GetToolConfig();
        event EventHandler SelectTool;
    }
}
