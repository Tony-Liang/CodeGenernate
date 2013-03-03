using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using CodeGenernate.Common;
using NVelocity.App;
using LCW.Framework.Common.Genernation.DataBases;

namespace CodeGenernate
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            template = new CodeTemplate(this);
        }
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private DataBasesTree databasetree= new DataBasesTree();
        private CodeTemplate template;

        private void Main_Load(object sender, EventArgs e)
        {
            databasetree.DocHandler += new EventHandler<DocumentEventArgs>(databasetree_DocHandler);
            template.FileHandler += new EventHandler<DocumentEventArgs>(template_FileHandler);
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            dockPanel.ActiveDocumentChanged += new EventHandler(dockPanel_ActiveDocumentChanged);
            
            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        void dockPanel_ActiveDocumentChanged(object sender, EventArgs e)
        {
            if (this.dockPanel.ActiveDocument is VelocityCode)
            {
                this.toolBarButtonStart.Enabled = true;
            }
            else
            {
                this.toolBarButtonStart.Enabled = false;
            }
        }

        void template_FileHandler(object sender, DocumentEventArgs e)
        {
            CreateDocument(e.Tag, () =>
            {
                string msg = (string)e.Sender;
                CodeDoc dummyDoc = new CodeDoc();
                dummyDoc.SetMessage(msg, LauguaageType.cs);
                return dummyDoc;
            });
        }

        void databasetree_DocHandler(object sender, DocumentEventArgs e)
        {
            CreateDocument(e.Tag, () =>
            {
                string msg = (string)e.Sender;
                CodeDoc dummyDoc = new CodeDoc();
                dummyDoc.SetMessage(msg, LauguaageType.mssql);
                return dummyDoc;
            });
        }

        public void CreateDocument(string text,Func<DockContent> builder)
        {
            DockContent dockform=FindDocument(text);
            if (dockform == null)
            {
                if (builder != null)
                    dockform = builder();
            }
            if (dockform != null)
            {
                dockform.Text = text;
                dockform.BringToFront();
                if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                {
                    dockform.MdiParent = this;
                    dockform.Show();
                }
                else
                    dockform.Show(dockPanel);
            }
        }

        public DockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                {
                    if (form.Text == text)
                    {
                        return form as DockContent;
                    }
                }

                return null;
            }
            else
            {
                foreach (DockContent content in dockPanel.Documents)
                {
                    if (content.DockHandler.TabText == text)
                    {
                        return content;
                    }
                }

                return null;
            }
        }

        //private IDockContent FindDocument(string text)
        //{
        //    if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
        //    {
        //        foreach (Form form in MdiChildren)
        //            if (form.Text == text)
        //                return form as IDockContent;

        //        return null;
        //    }
        //    else
        //    {
        //        foreach (IDockContent content in dockPanel.Documents)
        //            if (content.DockHandler.TabText == text)
        //                return content;

        //        return null;
        //    }
        //}

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(DataBasesTree).ToString())
            {
                return databasetree;
            }
            return null;
        }

        private void toolStripButtonConnection_Click(object sender, EventArgs e)
        {
            databasetree.Show(dockPanel);
        }

        private void toolBarButtonLayoutByXml_Click(object sender, EventArgs e)
        {
            template.Show(dockPanel);
        }

        private void toolBarButtonStart_Click(object sender, EventArgs e)
        {
            if (this.dockPanel.ActiveDocument is VelocityCode)
            {
                VelocityCode velocity = (VelocityCode)this.dockPanel.ActiveDocument;
                CodeBuilder builder = new CodeBuilder(velocity.Template.Path);
                builder.ShowDialog();
            }
        }
    }
}
