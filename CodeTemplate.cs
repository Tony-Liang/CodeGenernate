using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeGenernate.Common;
using LCW.Framework.Common.SysFile;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeGenernate
{
    public partial class CodeTemplate : ToolWindow
    {
        public CodeTemplate()
        {
            InitializeComponent();
        }

        public CodeTemplate(Main form)
            : this()
        {
            this.parentform = form;
        }
        private Main parentform;

        private void CodeTemplate_Load(object sender, EventArgs e)
        {
            TemplateFile root = TemplateManager.GetInstance("Template");
            FileNode node = new FileNode(root.Name);
            node.Tag = root;
            node.SelectedImageIndex = node.ImageIndex = 0;
            node.LoadData += new EventHandler(node_LoadData);
            this.treeView1.BeforeExpand += new TreeViewCancelEventHandler(treeView1_BeforeExpand);
            this.treeView1.Nodes.Add(node);
        }


        public event EventHandler<DocumentEventArgs> FileHandler;
        void node_LoadData(object sender, EventArgs e)
        {
            FileNode node = (FileNode)sender;
            if (node != null)
            {
                TemplateFile f = (TemplateFile)node.Tag;
                IList<TemplateFile> list=f.Files;
                if (list != null && list.Count > 0)
                {
                    foreach (var m in list)
                    {
                        FileNode t = new FileNode(m.Name);
                        t.LoadData += new EventHandler(node_LoadData);
                        t.Tag = m;
                        t.SelectedImageIndex = t.ImageIndex = 0;
                        if (m.Type == FileType.File)
                        {
                            t.Nodes.Clear();
                            t.SelectedImageIndex = t.ImageIndex = 1;
                            EventHandler handler=new EventHandler(openfile);
                            t.ContextMenu = new ContextMenu(new MenuItem[]{
                                new MenuItem("查看",handler)
                            });
                        }
                        node.Nodes.Add(t);
                    }
                }
            }
        }
        void openfile(object sender,EventArgs e)
        {
            TreeNode node=this.treeView1.SelectedNode;
            if (node != null)
            {
                TemplateFile temp = (TemplateFile)node.Tag;
                parentform.CreateDocument(temp.Name, () =>
                {
                    string content = FileHelper.ReadFile(temp.Path);
                    VelocityCode dummyDoc = new VelocityCode();
                    dummyDoc.SetMessage(content,temp);
                    return dummyDoc;
                });

                    
                    //if (FileHandler != null)
                    //{
                    //    FileHandler(temp, new DocumentEventArgs(temp.Name, content));
                    //}
            }
        }

        void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode obj = e.Node;
            if (obj is FileNode)
            {
                string oldtxt = obj.Text;
                try
                {
                    e.Node.Text = string.Format("{0} (expanding...)", e.Node.Text);
                    this.treeView1.Refresh();
                    this.treeView1.BeginUpdate();
                    ((FileNode)e.Node).Load();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    this.treeView1.EndUpdate();
                    obj.Text = oldtxt;
                }
            }
        }
    }


    public class DummyFileNode : TreeNode
    {
        public DummyFileNode()
            : base("DUMMY")
        {
            this.Name = "DUMMY";
        }
    }

    public class FileNode : TreeNode
    {
        public FileNode(string text):base(text)
        {
            this.Nodes.Add(new DummyFileNode());
        }

        public virtual void Load()
        {
            if (this.Nodes.ContainsKey("DUMMY"))
            {
                this.Reload();
            }
        }

        public virtual void Reload()
        {
            this.Nodes.Clear();
            LoadDatabaseObjects();
        }

        protected virtual void LoadDatabaseObjects()
        {
            if (LoadData != null)
            {
                LoadData(this, new EventArgs());
            }
        }
        public event EventHandler LoadData;
    }
}
