using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace CodeGenernate
{
    public partial class Output : DockContent
    {
        public Output()
        {
            InitializeComponent();
        }

        public void SetContent(string str)
        {
            this.richTextBox1.AppendText(str);
        }
    }
}
