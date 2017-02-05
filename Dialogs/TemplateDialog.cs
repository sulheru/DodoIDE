using DodoIDE.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DodoIDE.Dialogs
{
    public partial class TemplateDialog : Form
    {
        public TemplateDialog()
        {
            InitializeComponent();
            _items = new TreeNode();
        }

        public string Filename
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public delegate void CreateAction(string Filename); 

        public CreateAction Create { get; set; }

        private TreeNode _items;

        private void TemplateDialog_Load(object sender, EventArgs e)
        {
            LoadingDialog dlg = new LoadingDialog();
            dlg.DoWork += Dlg_DoWork;
            //dlg.Cancelable = false;
            treeView1.Nodes.Clear();
            dlg.RunLoaderAsync();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                treeView1.Nodes.Add(_items);
                treeView1.ExpandAll();
                var arr = ((List<ListViewItem>)_items.Tag)?.ToArray();
                if (arr != null)
                {
                    listView1.Items.Clear();
                    listView1.Items.AddRange(arr);
                }
            }
        }

        private void Dlg_DoWork(object sender, DoWorkEventArgs e)
        {
            var types = (from tp in Assembly.GetExecutingAssembly().GetTypes()
                        where typeof(Interfaces.ICommonMenuOptions).IsAssignableFrom(tp)
                        select tp).ToArray();
            
            int len = types.ToArray().Length;
            _items = new TreeNode("Recursos de proyecto");
            List<ListViewItem> templates = new List<ListViewItem>();
            _items.Tag = templates;
            foreach(Type tp in types)
            {
                var methods = (from mt in tp.GetMethods()
                               where mt.ReturnType == typeof(TemplateObject)
                               && mt.IsStatic && mt.IsPublic
                               select mt).ToArray();
                if (methods.Length > 0)
                {
                    List<ListViewItem> group = new List<ListViewItem>();
                    foreach (MethodInfo mt in methods)
                    {
                        TemplateObject item = (TemplateObject)mt.Invoke(null, null);
                        imageList1.Images.Add(item.ImageListKey, item.Icon);
                        templates.Add(item.Item);
                        group.Add(item.Item);
                    }
                    _items.Nodes.Add(DisplayCamelCaseString(tp.Name)).Tag = group;                    
                }
            }
        }

        public static string DisplayCamelCaseString(string camelCase)
        {
            List<char> chars = new List<char>();
            chars.Add(camelCase[0]);
            foreach (char c in camelCase.Skip(1))
            {
                if (char.IsUpper(c))
                {
                    chars.Add(' ');
                }
                chars.Add(c);
            }

            return new string(chars.ToArray());
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var arr = ((List<ListViewItem>)treeView1.SelectedNode.Tag)?.ToArray();
            if (arr != null)
            {
                listView1.Items.Clear();
                listView1.Items.AddRange(arr);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                var item = ((TemplateObject)listView1.SelectedItems[0].Tag);
                textBox1.Text = item.FileName;
                Create = item.CreateAction;
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
