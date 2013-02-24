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
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (File.Exists(configFile))
                dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
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
