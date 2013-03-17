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
using CodeGenernate.Design;

namespace CodeGenernate
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
        }
        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        public DataBasesTree databasetree;
        public Desk desk;
        public Error error;
        public Output output;
        public CodeTemplate template;

        private DocumentManager docmanager;
        public DockPanel DockPanel
        {
            get
            {
                return this.dockPanel;
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            docmanager = new DocumentManager(this);            
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            dockPanel.ActiveDocumentChanged += new EventHandler(dockPanel_ActiveDocumentChanged);
            
            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        public void Dispatcher(DispatcherType type,string str)//分发事件
        {
            if (DispatcherType.Error == type)
            {
                if (error != null)
                {
                    error.SetContent(str);
                }
            }
            else if (DispatcherType.Building == type)
            {
                if (output != null)
                {
                    output.SetContent(str);
                }
            }
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
            docmanager.CreateDatabaseForm();
            databasetree.Show(dockPanel);
        }

        private void toolBarButtonLayoutByXml_Click(object sender, EventArgs e)
        {
            docmanager.CreateTemplateForm();
            template.Show(dockPanel);
        }

        private void toolBarButtonStart_Click(object sender, EventArgs e)
        {
            if (this.dockPanel.ActiveDocument is VelocityCode)
            {
                VelocityCode velocity = (VelocityCode)this.dockPanel.ActiveDocument;
                docmanager.CreateCodeBuildForm(velocity.Scintilla.Text);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            docmanager.CreateDeskForm();
            desk.Show(dockPanel);
        }

        private void toolBarButtonTaskList_Click(object sender, EventArgs e)
        {
            docmanager.CreateErrorForm();
            error.Show(dockPanel);
        }

        private void toolBarButtonOutputWindow_Click(object sender, EventArgs e)
        {
            docmanager.CreateOutputForm();
            output.Show(dockPanel);
        }
    }
}
