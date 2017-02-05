using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using DodoIDE.Dialogs;
using DodoIDE.Interfaces;

namespace DodoIDE.Controls
{
    public partial class ProjectExplorerControl : UserControl, ICommonMenuOptions
    {
        public class ProjectObject
        {
            public string ProjectName { get; set; }
            public string ProjectFileName
            {
                get { return ProjectFile.FullName; }
                set { ProjectFile = new FileInfo(value); }
            }
            public string MainFileName
            {
                get { return MainFile.FullName; }
                set { MainFile = new FileInfo(value); }
            }
            public string[] ResourcesFileList
            {
                get {
                    return (from gr in GameResources
                           select gr.FullName).ToArray();
                }
                set {
                    GameResources = (from fn in value
                                     select new FileInfo(fn)).ToList();
                }
            }

            [XmlIgnore]
            public FileInfo ProjectFile { get; set; }
            [XmlIgnore]
            public FileInfo MainFile { get; set; }
            [XmlIgnore]
            public List<FileInfo> GameResources { get; set; }

            public ProjectObject()
            {
                GameResources = new List<FileInfo>();
            }
        }

        public class ItemClickEventArgs : EventArgs
        {
            public FileInfo FileInfo { get; private set; }

            public ItemClickEventArgs(FileInfo fi)
            {
                FileInfo = fi;
            }
        }

        public TreeNode SelectedNode
        { get { return treeView1.SelectedNode; } }

        public TreeNode ProjectNodes { get; set; }

        public ProjectExplorerControl()
        {
            InitializeComponent();
            GameConsole = new PanelController<ConsoleControl>(new ConsoleControl()) { Text = "Salida" };
        }
        
        public ProjectObject GameProject
        {
            get { return GameConsole.Controller.CurrentProject; }
            set { GameConsole.Controller.CurrentProject = value; }
        }
        public FileInfo CurrentProjectFile { get; private set; }
        public PanelController<ConsoleControl> GameConsole { get; private set; }

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

        public TreeNode CurrentNode
        { get { return treeView1.SelectedNode; } }

        public FileInfo FileInfo { get; set; }

        public event EventHandler<ItemClickEventArgs> OnItemDoubleClick;
        public event EventHandler<ToolLoadEventArgs> ToolLoad;

        public void CrerateGame(string path)
        {
            ProjectObject _gameProject = new ProjectObject();
            FileInfo = _gameProject.ProjectFile = new FileInfo(path);
            _gameProject.ProjectName = "Nuevo projecto de Dodo";
            FileInfo prg = new FileInfo(_gameProject.ProjectFile.DirectoryName + "\\Program.prg");
            File.WriteAllText(prg.FullName, Properties.Resources.prg);
            _gameProject.GameResources.Add(prg);
            _gameProject.MainFile = prg;
            GameProject = _gameProject;
            SaveFile();
            PopulateTree();
        }

        public void LoadFile(string filename, FileMode mode)
        {
            if (!_fileOk(filename)) { throw new FileNotFoundException(); }

            XmlSerializer prj = new XmlSerializer(typeof(ProjectObject));

            FileStream myFileStream =
            new FileStream(filename, mode);

            GameProject = (ProjectObject)prj.Deserialize(myFileStream);
            
            CurrentProjectFile = new FileInfo(filename);
            Directory.SetCurrentDirectory(CurrentProjectFile.DirectoryName);
            PopulateTree();
            //Properties.Settings.Default.Recent.Add(filename);
        }

        private void PopulateTree()
        {
            LoadingDialog dlg = new LoadingDialog();
            dlg.DoWork += Dlg_DoWork;
            dlg.Cancelable = false;
            dlg.Closeable = false;
            treeView1.Nodes.Clear();
            dlg.RunLoaderAsync("\\");
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                treeView1.Nodes.Add(ProjectNodes);
                treeView1.SelectedNode = ProjectNodes;
            }
        }

        public void CreateNode(TreeNode currentnode, FileInfo path)
        {
            string name = path.Name.Replace(path.Extension, "");
            TreeNode nNode = new TreeNode(name);
            nNode.Tag = path;
            if (currentnode.Tag.GetType() == typeof(DirectoryInfo))
            { currentnode.Nodes.Add(nNode); }
            else { currentnode.Parent.Nodes.Add(nNode); }
            GameProject.GameResources.Add(path);
            nNode.SelectedImageIndex = nNode.ImageIndex = _getIcon(path);
        }

        private void Dlg_DoWork(object sender, DoWorkEventArgs e)
        {
            ProjectNodes = new TreeNode(GameProject.ProjectName);
            ProjectNodes.Tag = GameProject.MainFile.Directory;
            ProjectNodes.SelectedImageIndex = ProjectNodes.ImageIndex = 6;

            if (GameProject == null) { return; }

            TreeNode thisnode = ProjectNodes;
            TreeNode currentnode;
            char[] cachedpathseparator = e.Argument.ToString().ToCharArray();

            var paths = from p in GameProject.GameResources
                        select p.FullName;
            int percent = 0;
            foreach (FileInfo path in GameProject.GameResources)
            {
                currentnode = thisnode;
                string[] pSep = path.FullName.Replace(GameProject.ProjectFile.DirectoryName + "\\", "").Split(cachedpathseparator);
                string relPath = GameProject.ProjectFile.DirectoryName;
                foreach (string subPath in pSep)
                {
                    relPath += "\\" + subPath;
                    if (null == currentnode.Nodes[subPath])
                    {
                        string name = path.Name.Replace(path.Extension, "");
                        currentnode = currentnode.Nodes.Add(subPath, name);
                        currentnode.ToolTipText = string.Format(
                            "Nombre: {0}\nTamaño: {1}\nTipo: {2}",
                            new string[] {name,
                                _getFileSize(path.Length),
                                _getFileType(path.Extension) }
                            );
                        if (IsDirectory(relPath))
                        {
                            currentnode.Tag = new DirectoryInfo(relPath);
                            currentnode.SelectedImageIndex = currentnode.ImageIndex = 0;
                        }
                        else
                        {
                            currentnode.Tag = new FileInfo(relPath);
                            currentnode.SelectedImageIndex = currentnode.ImageIndex = _getIcon((FileInfo)currentnode.Tag); 
                        }
                    }
                    else { currentnode = currentnode.Nodes[subPath]; }
                    ((LoadingDialog)sender).ReportProgress((GameProject.GameResources.IndexOf(path) / GameProject.GameResources.Count) * 100, path.Name);
                    if (percent < GameProject.GameResources.Count) { percent++; }
                }
            }
        }

        private int _getIcon(FileInfo tag)
        {
            int ii = 7;
            switch (tag.Extension.ToLower())
            {
                case ".prg": ii = 1; break;
                case ".fnt": ii = 2; break;
                case ".fpg": ii = 3; break;
                case ".map": ii = 4; break;
                case ".pal": ii = 5; break;
            }
            return ii;
        }

        public void AddResource(FileInfo fpg)
        {
            GameProject.GameResources.Add(fpg);
            PopulateTree();
        }

        private bool IsDirectory(string path)
        {
            FileAttributes attr = File.GetAttributes(path);

            //detect whether its a directory or file
            return ((attr & FileAttributes.Directory) == FileAttributes.Directory);
        }

        private string _getFileType(string extension)
        {
            string returned = "Desconocido";
            switch (extension.ToLower())
            {
                case ".prg": returned = "Código Fuente"; break;
                case ".fnt": returned = "Fuente bitmap"; break;
                case ".fpg": returned = "Fichero para imagenes"; break;
                case ".map": returned = "Graphico"; break;
                case ".pal": returned = "Paleta de colores"; break;
            }
            return returned;
        }

        private string _getFileSize(long length)
        {
            string[] scales = new string[] { "Bytes", "KB", "MB", "GB", "TB" };
            int i = 0;
            while (length > 1024 && i < scales.Length - 1)
            {
                length /= 1024;
                i++;
            }
            return string.Format("{0} {1}", length.ToString(), scales[i]);
        }

        public void CreateNode(TreeNode currentnode, DirectoryInfo path)
        {
            string name = path.Name;
            TreeNode nNode = new TreeNode(name);
            nNode.Tag = path;
            if (currentnode.Tag.GetType() == typeof(DirectoryInfo))
            { currentnode.Nodes.Add(nNode); }
            else { currentnode.Parent.Nodes.Add(nNode); }
            nNode.SelectedImageIndex = nNode.ImageIndex = 0;
        }

        private bool _fileOk(string filename)
        {
            FileInfo _file = new FileInfo(filename);
            return _file.Exists;
        }

        public void SaveFile()
        {
            Type po = typeof(ProjectObject);
            XmlSerializer prj = new XmlSerializer(po);

            StreamWriter myProject = new StreamWriter(FileInfo.FullName);
            prj.Serialize(myProject, GameProject);
            myProject.Close();
        }

        public void LoadFile()
        {
            LoadFile(FileInfo.FullName, FileMode.OpenOrCreate);
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

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag != null &&
                e.Node.Tag.GetType() == typeof(FileInfo))
            {
                FileInfo fi = (FileInfo)e.Node.Tag;
                OnItemDoubleClick?.Invoke(this, new ItemClickEventArgs(fi));
            }
        }

        public void CreateFile()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
           // throw new NotImplementedException();
        }

        public void SelectAll()
        {
            throw new NotImplementedException();
        }

        public void Find()
        {
            throw new NotImplementedException();
        }

        private void treeView1_Click(object sender, EventArgs e)
        {
            SelectControl?.Invoke(this, e);
        }

        public void SetMeOnTop()
        {
            BringToFront();
        }

        public event EventHandler SelectControl;
        public event EventHandler CloseAction;
    }
}
