using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using CodeGenernate.Design;

namespace CodeGenernate
{
    public partial class Error : ToolWindow
    {
        public Error()
        {
            InitializeComponent();
        }

        private static Error m_Instance;
        public static Error GetInstance()
        {
            if (m_Instance == null)
                m_Instance = new Error();
            return m_Instance;
        }

        public void SetContent(string str)
        {            
            ListViewItem[] p = new ListViewItem[2];
            p[0] = new ListViewItem(new string[] {"aaaa","bbbb"});
            p[1] = new ListViewItem(new string[] {"cccc", "ggggg" });
            this.listView1.Items.AddRange(p);
            //也可以用this.listView1.Items.Add();不过需要在使用的前后添加Begin... 和End...防止界面自动刷新
        }
    }
}
