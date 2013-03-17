using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CodeGenernate.Controls
{
    public partial class FolderTreeView : TreeView
    {
        public FolderTreeView()
        {
            InitializeComponent();
        }

        public FolderTreeView(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
            init();
        }

        private TreeNodeEx CurrentNode;

        private void init()
        {
            this.ImageList = this.imageList1;
            this.Nodes.Clear();
            TreeNodeEx Root = new TreeNodeEx("桌面",NodeType.ROOT);
            Root.ImageIndex = 3;
            Root.SelectedImageIndex = 3;
            CurrentNode = Root;
            this.Nodes.Add(Root);
        }

        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            TreeNode obj =e.Node;
            if (obj is TreeNodeEx)
            {
                string oldtxt = obj.Text;
                try
                {
                    e.Node.Text = string.Format("{0} (expanding...)", e.Node.Text);
                    this.Refresh();
                    this.BeginUpdate();
                    load(((TreeNodeEx)e.Node));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
                finally
                {
                    this.EndUpdate();
                    obj.Text = oldtxt;
                }
            }            
        }

        private void load(TreeNodeEx node)
        {
            if (node.Nodes.ContainsKey("DUMMY"))
            {
                node.Nodes.Clear();
                switch (node.NodeType)
                {
                    case NodeType.ROOT:
                        string[] drivesList = Environment.GetLogicalDrives();
                        for (int i = 0; i < drivesList.Length; i++)
                        {
                            TreeNodeEx dri = new TreeNodeEx(drivesList[i].ToUpperInvariant(),NodeType.DRIVES);
                            dri.Tag = drivesList[i].ToUpperInvariant();
                            dri.ImageIndex = 0;
                            dri.SelectedImageIndex = 0;
                            node.Nodes.Add(dri);
                        }
                        break;
                    case NodeType.FOLDER:
                    case NodeType.DRIVES:
                        try
                        {
                            DirectoryInfo dirInfo = new DirectoryInfo((string)node.Tag);
                            foreach (FileSystemInfo di in dirInfo.GetFileSystemInfos())
                            {
                                try
                                {
                                    if (di is DirectoryInfo)
                                    {
                                        TreeNodeEx newFolderNode = new TreeNodeEx(di.Name,NodeType.FOLDER);
                                        newFolderNode.Tag = (string)di.FullName;
                                        newFolderNode.ImageIndex = 1;
                                        newFolderNode.SelectedImageIndex = 1;
                                        node.Nodes.Add(newFolderNode);
                                    }
                                    else if (di is FileInfo)
                                    {
                                        TreeNodeEx fileNode = new TreeNodeEx(di.Name, NodeType.FILE);
                                        fileNode.Tag = (string)di.FullName;
                                        fileNode.ImageIndex = 2;
                                        fileNode.SelectedImageIndex = 2;
                                        fileNode.Nodes.Clear();
                                        node.Nodes.Add(fileNode);
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        catch
                        {
                        }
                        break;                                            
                }
            }
        }
    }

    public class DummyNode : TreeNode
    {
        public DummyNode()
            : base("DUMMY")
        {
            this.Name = "DUMMY";
        }
    }

    public class TreeNodeEx : TreeNode
    {
        public TreeNodeEx(string text,NodeType nodeType)
            : base(text)
        {
            this.nodeType = nodeType;
            this.Nodes.Add(new DummyNode());
        }

        private NodeType nodeType;
        public NodeType NodeType
        {
            get
            {
                return nodeType;
            }
        }
    }

    public enum NodeType
    {
        ROOT,
        DRIVES,
        FOLDER,
        FILE
    }
}
