using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Data.SqlClient;
using LCW.Framework.Common.DataAccess.Schema;
using CodeGenernate.Common;

namespace CodeGenernate
{
    public partial class DataBasesTree : ToolWindow
    {
        public DataBasesTree()
        {
            InitializeComponent();
            this.treeView1.BeforeExpand += new TreeViewCancelEventHandler(treeView1_BeforeExpand);
        }

        void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode obj=e.Node;
            if (obj is BaseNode)
            {
                string oldtxt = obj.Text;
                try
                {
                    e.Node.Text = string.Format("{0} (expanding...)", e.Node.Text);
                    this.treeView1.Refresh();
                    this.treeView1.BeginUpdate();
                    ((BaseNode)e.Node).Load();
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

        private void DataBasesTree_Load(object sender, EventArgs e)
        {
            this.treeView1.Nodes.Clear();
            /*test*/
            SqlConnectionStringBuilder str = new SqlConnectionStringBuilder();
            str.DataSource = @"L-PC\MYSQL";
            str.UserID = "sa";
            str.Password = "123abcDD";
            DBSchemaProvider.GetInstance().ConnectionStringBuilder = str;
            DbConnectionStringBuilder builder = DBSchemaProvider.GetInstance().ConnectionStringBuilder;
            BaseNode root = new BaseNode(builder.ServiceName(), builder);
            root.ImageIndex =root.SelectedImageIndex= 0;
            root.LoadData += new EventHandler(root_LoadData);
            this.treeView1.Nodes.Add(root);
        }

        void root_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            IList<DataBaseSchema> list =DBSchemaProvider.GetInstance().GetDataBases();
            if (list != null)
            {
                foreach (DataBaseSchema schema in list)
                {
                    DbConnectionStringBuilder builder = obj.ConnectionStringBuilder.NewInstance(schema.Name);
                    BaseNode database=new BaseNode(schema.Name, builder);
                    database.ImageIndex =database.SelectedImageIndex= 1;
                    database.Nodes.Clear();
                    //database.Image = ResourcesHelper.FetchDB_icon(p => p.Schema);
                    BaseNode tables = new BaseNode("Tables", builder);
                    tables.ImageIndex =tables.SelectedImageIndex= 7;
                    tables.LoadData += new EventHandler(tables_LoadData);
                    database.Nodes.Add(tables);

                    BaseNode views = new BaseNode("Views", builder);
                    views.ImageIndex =views.SelectedImageIndex= 7;
                    views.LoadData += new EventHandler(views_LoadData);
                    database.Nodes.Add(views);

                    BaseNode proc = new BaseNode("Proc", builder);
                    proc.ImageIndex =proc.SelectedImageIndex= 7;
                    proc.LoadData += new EventHandler(proc_LoadData);
                    database.Nodes.Add(proc);

                    BaseNode triggers = new BaseNode("Triggers", builder);
                    triggers.ImageIndex =triggers.SelectedImageIndex= 7;
                    triggers.LoadData += new EventHandler(triggers_LoadData);
                    database.Nodes.Add(triggers);
                    obj.Nodes.Add(database);
                }
            }
        }

        void triggers_LoadData(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        void proc_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            IList<ProceduresSchema> list = DBSchemaProvider.GetInstance().GetProcedures(obj.ConnectionStringBuilder);
            if (list != null)
            {
                foreach (ProceduresSchema schema in list)
                {
                    BaseNode proc = new BaseNode(schema.Name, obj.ConnectionStringBuilder);
                    proc.ImageIndex =proc.SelectedImageIndex= 5;
                    obj.Nodes.Add(proc);
                }
            }
        }

        void views_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            IList<ViewSchema> list = DBSchemaProvider.GetInstance().GetViews(obj.ConnectionStringBuilder);
            if (list != null)
            {
                foreach (ViewSchema schema in list)
                {
                    BaseNode view = new BaseNode(schema.Name, obj.ConnectionStringBuilder);
                    view.ImageIndex =view.SelectedImageIndex= 3;
                    obj.Nodes.Add(view);
                }
            }
        }

        void tables_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            IList<TableSchema> list = DBSchemaProvider.GetInstance().GetTables(obj.ConnectionStringBuilder);
            if (list != null)
            {
                foreach (TableSchema schema in list)
                {
                    BaseNode node = new BaseNode(schema.Description,obj.ConnectionStringBuilder);
                    node.Name = schema.Name;
                    node.ImageIndex =node.SelectedImageIndex= 2;
                    node.LoadData += new EventHandler(node_LoadData);
                    obj.Nodes.Add(node);
                }
            }
        }

        void node_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            IList<ColumnSchema> list = DBSchemaProvider.GetInstance().GetColumns(obj.ConnectionStringBuilder, obj.Name);
            if (list != null)
            {
                foreach (ColumnSchema schema in list)
                {
                    TreeNode col = new TreeNode(schema.Name);
                    col.ImageIndex=col.SelectedImageIndex =6;
                    obj.Nodes.Add(col);
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

    public class BaseNode : TreeNode
    {
        public BaseNode(string text, DbConnectionStringBuilder builder)
            : base(text)
        {
            this.ConnectionStringBuilder = builder;
            this.Nodes.Add(new DummyNode());
        }
        private DbConnectionStringBuilder connectionstringbuilder;
        public DbConnectionStringBuilder ConnectionStringBuilder
        {
            get
            {
                return connectionstringbuilder;
            }
            set
            {
                connectionstringbuilder = value;
            }
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
                LoadData(this,new EventArgs());
        }

        public event EventHandler LoadData;
    }
}
