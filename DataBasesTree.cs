﻿using System;
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
using LCW.Framework.Common.Genernation.DataBases;
using LCW.Framework.Common.Genernation.DataBases.Entities;

namespace CodeGenernate
{
    public partial class DataBasesTree : ToolWindow
    {
        public DataBasesTree()
        {
            InitializeComponent();
            this.treeView1.BeforeExpand += new TreeViewCancelEventHandler(treeView1_BeforeExpand);
        }

        public event EventHandler<DocumentEventArgs> DocHandler;

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
            //MySql.Data.MySqlClient.MySqlConnectionStringBuilder str = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();
            //str.Server = "127.0.0.1";
            //str.UserID = "root";
            //str.Password = "root";
            //DBSchemaProvider.NewInstance("LCW.Framework.Common.DataAccess.Schema.Mysql.MySqlProvider");
            //DBSchemaProvider.GetInstance().ConnectionStringBuilder = str;
            //DbConnectionStringBuilder builder = DBSchemaProvider.GetInstance().ConnectionStringBuilder;
            //BaseNode root = new BaseNode(builder.ServiceName(), builder, DataType.service);
            //root.ImageIndex =root.SelectedImageIndex= 0;
            //root.LoadData += new EventHandler(root_LoadData);
            //this.treeView1.Nodes.Add(root);
            ServiceSite service=DataBaseSchemaBuilder.NewInstance(str, "Sql");
            BaseNode root = new BaseNode(service.Name,service.DbConnectionStringBuilder, DataType.service);
            root.ImageIndex = root.SelectedImageIndex = 0;
            root.Tag = service;
            root.LoadData += new EventHandler(root_LoadData);
            this.treeView1.Nodes.Add(root);
        }

        void root_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            //IList<DataBaseSchema> list =DBSchemaProvider.GetInstance().GetDataBases();
            //if (list != null)
            //{
            //    foreach (DataBaseSchema schema in list)
            //    {
            //        DbConnectionStringBuilder builder = obj.ConnectionStringBuilder.NewInstance(schema.Name);
            //        BaseNode database = new BaseNode(schema.Name, builder, DataType.database);
            //        database.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
            //            Refresh()
            //        });
            //        database.ImageIndex =database.SelectedImageIndex= 1;
            //        database.LoadData += new EventHandler(database_LoadData);
            //        //database.Image = ResourcesHelper.FetchDB_icon(p => p.Schema);
            //        obj.Nodes.Add(database);
            //    }
            //}
            if (obj.Tag != null)
            {
                ServiceSite service = (ServiceSite)obj.Tag;
                IList<DataBaseEntity> list = service.DataBases;
                if (list != null)
                {
                    foreach (DataBaseEntity schema in list)
                    {
                        BaseNode database = new BaseNode(schema.Name,schema.DbConnectionStringBuilder, DataType.database);
                        database.Tag = schema;
                        database.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                        Refresh()
                    });
                        database.ImageIndex = database.SelectedImageIndex = 1;
                        database.LoadData += new EventHandler(database_LoadData);
                        //database.Image = ResourcesHelper.FetchDB_icon(p => p.Schema);
                        obj.Nodes.Add(database);
                    }
                }
            }
        }

        void database_LoadData(object sender, EventArgs e)
        {
            BaseNode database = (BaseNode)sender;
            BaseNode tables = new BaseNode("Tables", database.ConnectionStringBuilder, DataType.package);
            tables.Tag = database.Tag;
            tables.ImageIndex = tables.SelectedImageIndex = 7;
            tables.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                        Refresh()
                    });
            tables.LoadData += new EventHandler(tables_LoadData);
            database.Nodes.Add(tables);

            BaseNode views = new BaseNode("Views", database.ConnectionStringBuilder, DataType.package);
            views.Tag = database.Tag;
            views.ImageIndex = views.SelectedImageIndex = 7;
            views.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                        Refresh()
                    });
            views.LoadData += new EventHandler(views_LoadData);
            database.Nodes.Add(views);

            BaseNode proc = new BaseNode("Proc", database.ConnectionStringBuilder, DataType.package);
            proc.Tag = database.Tag;
            proc.ImageIndex = proc.SelectedImageIndex = 7;
            proc.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                        Refresh()
                    });
            proc.LoadData += new EventHandler(proc_LoadData);
            database.Nodes.Add(proc);

            BaseNode triggers = new BaseNode("Triggers", database.ConnectionStringBuilder, DataType.package);
            triggers.Tag = database.Tag;
            triggers.ImageIndex = triggers.SelectedImageIndex = 7;
            triggers.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                        Refresh()
                    });
            triggers.LoadData += new EventHandler(triggers_LoadData);
            database.Nodes.Add(triggers);
            
        }

        void triggers_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            //IList<TriggersSchema> list = DBSchemaProvider.GetInstance().GetTriggers(obj.ConnectionStringBuilder);
            //if (list != null)
            //{
            //    foreach (TriggersSchema schema in list)
            //    {
            //        BaseNode proc = new BaseNode(schema.Name, obj.ConnectionStringBuilder, DataType.triggers);
            //        proc.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
            //            Refresh(),Open()
            //        });
            //        proc.ImageIndex = proc.SelectedImageIndex = 4;
            //        obj.Nodes.Add(proc);
            //    }
            //}
            if (obj.Tag != null)
            {
                DataBaseEntity database = (DataBaseEntity)obj.Tag;
                IList<TriggersEntity> list=database.Triggers;
                if (list != null)
                {
                    foreach (TriggersEntity schema in list)
                    {
                        BaseNode proc = new BaseNode(schema.Name, schema.DbConnectionStringBuilder, DataType.triggers);
                        proc.Tag=schema;
                        proc.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                            Refresh(),Open()
                        });
                        proc.ImageIndex = proc.SelectedImageIndex = 4;
                        obj.Nodes.Add(proc);
                    }
                }
            }
        }

        void proc_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            //IList<ProceduresSchema> list = DBSchemaProvider.GetInstance().GetProcedures(obj.ConnectionStringBuilder);
            //if (list != null)
            //{
            //    foreach (ProceduresSchema schema in list)
            //    {
            //        BaseNode proc = new BaseNode(schema.Name, obj.ConnectionStringBuilder, DataType.procedure);
            //        proc.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
            //            Refresh(),Open()
            //        });
            //        proc.ImageIndex =proc.SelectedImageIndex= 5;
            //        obj.Nodes.Add(proc);
            //    }
            //}
            if (obj.Tag != null)
            {
                DataBaseEntity database = (DataBaseEntity)obj.Tag;
                IList<ProcedureEntity> list=database.Procedures;
                if (list != null)
                {
                    foreach (ProcedureEntity schema in list)
                    {
                        BaseNode proc = new BaseNode(schema.Name, schema.DbConnectionStringBuilder, DataType.procedure);
                        proc.Tag=schema;
                        proc.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                            Refresh(),Open()
                        });
                        proc.ImageIndex = proc.SelectedImageIndex = 5;
                        obj.Nodes.Add(proc);
                    }
                }
            }
        }

        void views_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            //IList<ViewSchema> list = DBSchemaProvider.GetInstance().GetViews(obj.ConnectionStringBuilder);
            //if (list != null)
            //{
            //    foreach (ViewSchema schema in list)
            //    {
            //        BaseNode view = new BaseNode(schema.Name, obj.ConnectionStringBuilder, DataType.view);
            //        view.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
            //            Refresh(),Open()
            //        });
            //        view.ImageIndex =view.SelectedImageIndex= 3;
            //        obj.Nodes.Add(view);
            //    }
            //}
            if (obj.Tag != null)
            {
                DataBaseEntity database = (DataBaseEntity)obj.Tag;
                IList<ViewEntity> list=database.Views;
                if (list != null)
                {
                    foreach (ViewEntity schema in list)
                    {
                        BaseNode proc = new BaseNode(schema.Name, schema.DbConnectionStringBuilder, DataType.view);
                        proc.Tag=schema;
                        proc.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                            Refresh(),Open()
                        });
                        proc.ImageIndex = proc.SelectedImageIndex = 3;
                        obj.Nodes.Add(proc);
                    }
                }
            }
        }

        void tables_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;            
            if (obj.Tag != null)
            {
                DataBaseEntity database = (DataBaseEntity)obj.Tag;
                IList<TableEntity> list=database.Tables;
                if (list != null)
                {
                    foreach (TableEntity schema in list)
                    {
                        BaseNode node = new BaseNode(schema.Description, schema.DbConnectionStringBuilder, DataType.table);
                        node.Tag = schema;
                        node.ContextMenu = new System.Windows.Forms.ContextMenu(new MenuItem[]{
                                    Refresh(),Open(),BuildCode()
                                });
                        node.Name = schema.Name;
                        node.ImageIndex = node.SelectedImageIndex = 2;
                        node.LoadData += new EventHandler(node_LoadData);
                        obj.Nodes.Add(node);
                    }
                }
            }
        }

        void node_LoadData(object sender, EventArgs e)
        {
            BaseNode obj = (BaseNode)sender;
            //IList<ColumnSchema> list = DBSchemaProvider.GetInstance().GetColumns(obj.ConnectionStringBuilder, obj.Name);
            //if (list != null)
            //{
            //    foreach (ColumnSchema schema in list)
            //    {
            //        BaseNode col = new BaseNode(schema.Name, obj.ConnectionStringBuilder, DataType.column);
            //        col.ImageIndex=col.SelectedImageIndex =6;
            //        col.Nodes.Clear();
            //        obj.Nodes.Add(col);
            //    }
            //}
            if (obj.Tag != null)
            {
                TableEntity table = (TableEntity)obj.Tag;
                IList<ColumnEntity> list=table.Columns;
                if (list != null)
                {
                    foreach (ColumnEntity schema in list)
                    {
                        BaseNode col = new BaseNode(schema.Description, schema.DbConnectionStringBuilder, DataType.column);
                        col.Tag=schema;
                        col.ImageIndex=col.SelectedImageIndex =6;
                        col.Nodes.Clear();
                        obj.Nodes.Add(col);
                    }
                }
            }
        }

        private MenuItem Open()
        {
            MenuItem item = new MenuItem();
            item.Name = "open";
            item.Text = "查看";
            item.Click += new EventHandler(open_Click);
            return item;
        }
        private MenuItem Refresh()
        {
            MenuItem item = new MenuItem();
            item.Name = "refresh";
            item.Text = "刷新";
            item.Click += new EventHandler(item_Click);
            return item;
        }
        private MenuItem BuildCode()
        {
            MenuItem item = new MenuItem();
            item.Name = "buildcode";
            item.Text = "代码";
            item.Click += new EventHandler(buildcode_Click);
            return item;
        }

        void item_Click(object sender, EventArgs e)
        {
           TreeNode node=this.treeView1.SelectedNode;
           if (node != null)
           {
               if (node is BaseNode)
               {
                   BaseNode temp = (BaseNode)node;                   
                   temp.Reload();
                   temp.Collapse();
               }
           }
        }
        void open_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                if (node is BaseNode)
                {
                    BaseNode temp = (BaseNode)node;
                    String message = string.Empty;
                    if (DataType.procedure == temp.NodeType)
                    {
                        if (DocHandler != null)
                        {
                            ProcedureEntity p = (ProcedureEntity)node.Tag;
                            message = p.Content;
                            DocHandler(p, new DocumentEventArgs(p.Name,message));
                        }
                    }
                    else if (DataType.triggers == temp.NodeType)
                    {
                        if (DocHandler != null)
                        {
                            TriggersEntity t = (TriggersEntity)node.Tag;
                            message = t.Content;
                            DocHandler(t, new DocumentEventArgs(t.Name,message));
                        }
                    }
                    else if (DataType.view == temp.NodeType)
                    {
                        if (DocHandler != null)
                        {
                            ViewEntity v = (ViewEntity)node.Tag;
                            message = v.Content;
                            DocHandler(v, new DocumentEventArgs(v.Name,message));
                        }
                    }
                }
            }
        }
        void buildcode_Click(object sender, EventArgs e)
        {
            TreeNode node = this.treeView1.SelectedNode;
            if (node != null)
            {
                if (node is BaseNode)
                {
                    BaseNode temp = (BaseNode)node;
                    if (DataType.table == temp.NodeType)
                    {
                        if (DocHandler != null)
                        {
                            TableEntity v = (TableEntity)node.Tag;
                            DocHandler(v, new DocumentEventArgs(v.Name,v));
                        }
                    }
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
        public BaseNode(string text, DbConnectionStringBuilder builder,DataType nodetype)
            : base(text)
        {
            this.ConnectionStringBuilder = builder;
            this.datatype = nodetype;
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

        private DataType datatype;
        public DataType NodeType
        {
            get
            {
                return datatype;
            }
        }

        public event EventHandler LoadData;
    }

    public enum DataType
    {
        service,
        database,
        package,
        table,
        view,
        procedure,
        triggers,
        column
    }
}
