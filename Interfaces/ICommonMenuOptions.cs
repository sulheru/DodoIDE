using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE.Interfaces
{
    public interface ICommonMenuOptions
    {
        FileInfo FileInfo { get; set; }

        event EventHandler<ToolLoadEventArgs> ToolLoad;
        event EventHandler SelectControl;
        event EventHandler CloseAction;

        void Close();
        void Copy();
        void CreateFile();
        void Delete();
        void Find();
        void LoadFile();
        void Paste();
        void Redo();
        void SaveFile();
        void SetMeOnTop();
        void SelectAll();
        void Undo();
    }

    public class ToolLoadEventArgs : EventArgs
    {
        public ToolStrip ToolStrip { get; set; }
        public Panel Panel { get; set; }
    }
}
