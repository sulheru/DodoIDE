using DodoIDE;
using DodoIDE.Controls;
using DodoIDE.Dialogs;
using DodoIDE.Interfaces;
using FenixLib.Core;
using FenixLib.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE
{
    public partial class frmMain : Form
    {
        public static void OpenFileDialog(string filter, FileInfo file)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK)
            { file = new FileInfo(dlg.FileName); }
        }

        public static void SaveFileDialog(string filter, FileInfo file)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK)
            { file = new FileInfo(dlg.FileName); }
        }

        private Point iniMouse;

        private PanelController<ProjectExplorerControl> ProjectExplorer;

        public Dictionary<FileInfo, ICommonMenuOptions> OpenedFiles { get; set; }
        public ICommonMenuOptions CurrentControl { get; private set; }

        public frmMain()
        {
            InitializeComponent();

            OpenedFiles = new Dictionary<FileInfo, ICommonMenuOptions>();

            ProjectExplorer = new PanelController<ProjectExplorerControl>(new ProjectExplorerControl());
            ProjectExplorer.SetPanel(panelBarRight, ToolPanelRight);
            ProjectExplorer.Controller.SelectControl += FrmMain_Click;
            ProjectExplorer.GotFocus += Controller_GotFocus;
            ProjectExplorer.Controller.OnItemDoubleClick += Controller_OnItemDoubleClick;
            ProjectExplorer.Text = "Explorador de Proyectos";
            ProjectExplorer.Controller.GameConsole.SetPanel(panelBarBottom, ToolPanelBottom);
        }

        private void Controller_OnItemDoubleClick(object sender, ProjectExplorerControl.ItemClickEventArgs e)
        {
            if (!OpenedFiles.ContainsKey(e.FileInfo))
            {
                switch (e.FileInfo.Extension.ToLower())
                {
                    case ".prg": OpenFile(e.FileInfo, typeof(SourceCodeController)); break;
                    case ".fnt": OpenFile(e.FileInfo, typeof(FontViewerControl)); break;
                    case ".fpg": OpenFile(e.FileInfo, typeof(FileViewerControl)); break;
                    case ".map": OpenFile(e.FileInfo, typeof(MapEditorControl)); break;
                    case ".pal": OpenFile(e.FileInfo, typeof(PaletteControl)); break;
                }
            }
            else
            {
                OpenedFiles[e.FileInfo].SetMeOnTop();
                ((UserControl)OpenedFiles[e.FileInfo]).BringToFront();
            }
        }

        private void OpenFile(FileInfo fileinfo, Type type)
        {
            var cons = type.GetConstructor(Type.EmptyTypes);
            var obj = cons.Invoke(new object[0]);
            var proObj = new PanelController<UserControl>((UserControl)obj);
            ((ICommonMenuOptions)proObj.Controller).FileInfo = fileinfo;
            ((ICommonMenuOptions)proObj.Controller).LoadFile();
            ((ICommonMenuOptions)proObj.Controller).ToolLoad += FrmMain_ToolLoad;
            ((ICommonMenuOptions)proObj.Controller).FileInfo = fileinfo;
            ((ICommonMenuOptions)proObj.Controller).SelectControl += FrmMain_Click;
            proObj.Closing += ProObj_Closing;
            proObj.Text = obj.ToString();
            if (!OpenedFiles.ContainsKey(fileinfo)) OpenedFiles.Add(fileinfo, ((ICommonMenuOptions)proObj.Controller));
            proObj.SetPanel(panelBarLeft, ToolPanelLeft);                                    
        }

        private void ProObj_Closing(object sender, EventArgs e)
        {
            var obj = (PanelController<ICommonMenuOptions>)sender;
            OpenedFiles.Remove(obj.Controller.FileInfo);
        }

        private void FrmMain_Click(object sender, EventArgs e)
        {
            CurrentControl = (ICommonMenuOptions)sender;
        }

        private void FrmMain_ToolLoad(object sender, ToolLoadEventArgs e)
        {
            e.ToolStrip = panelBarMain;
            e.Panel = MainPanel;
        }

        private void sldLeft_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < iniMouse.X) ((Button)sender).Parent.Width += e.X;
                if (e.X > iniMouse.X) ((Button)sender).Parent.Width += e.X;
            }
        }

        private void sldRight_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X < iniMouse.X) ((Button)sender).Parent.Width -= e.X;
                if (e.X > iniMouse.X) ((Button)sender).Parent.Width -= e.X;
            }
        }

        private void sldBottom_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Y < iniMouse.Y) ((Button)sender).Parent.Height -= e.Y;
                if (e.Y > iniMouse.Y) ((Button)sender).Parent.Height -= e.Y;
            }
        }

        private void viewTool_Click(object sender, EventArgs e)
        {
            string name = ((ToolStripMenuItem)sender).Name;
        }

        private void proyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Proyecto de DodoIDE|*.ddproj";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ProjectExplorer.Controller.CrerateGame(dlg.FileName);
            }
        }

        private void CreateResource(object sender, EventArgs e)
        {
            TemplateDialog dlg = new TemplateDialog();
            DialogResult r = dlg.ShowDialog();
            if (r == DialogResult.OK)
            {
                try
                {
                    var fn = _generateResource(dlg.Filename);
                    dlg.Create.Invoke(fn);
                    ProjectExplorer.Controller.CreateNode(ProjectExplorer.Controller.CurrentNode, new FileInfo(fn));
                }
                catch { }
            }
        }

        private string _generateResource(string filename)
        {
            TreeNode node = ProjectExplorer.Controller.CurrentNode;
            string fileName = "";
            if (node.Tag.GetType() == typeof(DirectoryInfo))
            { fileName = ((DirectoryInfo)node.Tag).FullName + "\\" + filename; }
            else { fileName = ((FileInfo)node.Tag).DirectoryName + "\\" + filename; }
            return fileName;
        }

        private void xXOpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Proyecto de DodoIDE|*.ddproj";
            var d = dlg.ShowDialog();
            if (d == DialogResult.OK)
            {
                ProjectExplorer.Controller.FileInfo = new FileInfo(dlg.FileName);
                ProjectExplorer.Controller.LoadFile();
            }
        }

        private void xXSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentControl.FileInfo == null)
            { xXSaveAsToolStripMenuItem_Click(sender, e); }
            if (CurrentControl.FileInfo != null)
            { CurrentControl.SaveFile(); }
        }

        private void xXSaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            DialogResult r = dlg.ShowDialog();
            if (r == DialogResult.OK)
            { CurrentControl.FileInfo = new FileInfo(dlg.FileName); }
        }

        private void SaveAllFiles(object sender, EventArgs e)
        {
            foreach (KeyValuePair<FileInfo, ICommonMenuOptions> fi in OpenedFiles)
            { fi.Value.SaveFile(); }
        }

        private void xXCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Close();
        }

        private void xXExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void xXUndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Undo();
        }

        private void xXRedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Redo();
        }

        private void xXCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Copy();
            CurrentControl.Delete();
        }

        private void xXCopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Copy();
        }

        private void xXPasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Paste();
        }

        private void xXDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Delete();
        }

        private void xXSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.SelectAll();
        }

        private void xXSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurrentControl.Find();
        }

        private void xXMainSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupDialog dlg = new SetupDialog();
            dlg.ShowDialog();
        }

        private void agregarRecursoExistenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            var d=dlg.ShowDialog();
            if (d == DialogResult.OK)
            {
                FileInfo origen = new FileInfo(dlg.FileName);
                FileInfo destin = new FileInfo(_generateResource(origen.Name));
                if (destin.Exists)
                {
                    if (MessageBox.Show(
                        "Archivo Duplicado",
                        "El archivo ya existe en el directorio seleccionado. ¿Desea sobreescribirlo?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Exclamation
                        )
                        == DialogResult.Yes) destin.Delete();
                    else return;
                }
                File.Copy(origen.FullName, destin.FullName);
                ProjectExplorer.Controller.CreateNode(ProjectExplorer.Controller.ProjectNodes, new FileInfo(destin.FullName));
            }
        }

        private void crearCarpetaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputDialog dlg = new InputDialog();
            dlg.Message = "Crear una carpeta nueva en el directorio seleccionado";
            dlg.Title = "Crear carpeta";
            dlg.Text = "Nueva carpeta";
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                var dir = _generateResource(dlg.Text);
                DirectoryInfo di = new DirectoryInfo(dir);
                di.Create();
                ProjectExplorer.Controller.CreateNode(ProjectExplorer.Controller.ProjectNodes, di);
            }
        }

        private void eliminarRecursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = ProjectExplorer.Controller.SelectedNode.Tag;
            if(item.GetType()==typeof(DirectoryInfo))
            { ((DirectoryInfo)item).Delete(); }
            else { ((FileInfo)item).Delete(); }
            ProjectExplorer.Controller.SelectedNode.Parent.Nodes.Remove(ProjectExplorer.Controller.SelectedNode);
        }

        private void compilarYEjecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAllFiles(sender, e);
            ProjectExplorer.Controller.GameConsole.Controller.CompileAndRunGame(sender, e);
        }

        private void soloCompilarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAllFiles(sender, e);
            ProjectExplorer.Controller.GameConsole.Controller.BuildGame();
        }

        private void soloEjecutarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAllFiles(sender, e);
            ProjectExplorer.Controller.GameConsole.Controller.RunGame();
        }

        private void configuraciónGeneralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupDialog dlg = new SetupDialog();
            var d = dlg.ShowDialog();
            if (d == DialogResult.OK) { Properties.Settings.Default.Save(); }
            else { Properties.Settings.Default.Reload(); }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            var bottom= Properties.Settings.Default.PanelBottomHeight;
            var left = Properties.Settings.Default.PanelLeftWidth;
            var right = Properties.Settings.Default.PanelRightWidth;
            ToolPanelBottom.Height = bottom;
            ToolPanelLeft.Width = left;
            ToolPanelRight.Width = right;
        }

        private void sldEndMove(object sender, MouseEventArgs e)
        {
            Properties.Settings.Default.PanelBottomHeight = ToolPanelBottom.Height;
            Properties.Settings.Default.PanelLeftWidth = ToolPanelLeft.Width;
            Properties.Settings.Default.PanelRightWidth = ToolPanelRight.Width;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void pin_click(object sender, EventArgs e)
        {

        }

        private void Controller_GotFocus(object sender, EventArgs e)
        {

        }
    }
}
