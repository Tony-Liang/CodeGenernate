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
        private DataBasesTree databasetree= new DataBasesTree();
        private void Main_Load(object sender, EventArgs e)
        {
            databasetree.DocHandler += new EventHandler(databasetree_DocHandler);
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
        }

        void databasetree_DocHandler(object sender, EventArgs e)
        {
            string msg = (string)sender;
            CodeDoc dummyDoc = new CodeDoc();
            dummyDoc.SetMessage(msg,LauguaageType.mssql);
            int count = 1;
            string text = "Document" + count.ToString();
            while (FindDocument(text) != null)
            {
                count++;
                text = "Document" + count.ToString();
            }
            dummyDoc.Text = text;

            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                dummyDoc.MdiParent = this;
                dummyDoc.Show();
            }
            else
                dummyDoc.Show(dockPanel);
        }

        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }

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
    }
}
