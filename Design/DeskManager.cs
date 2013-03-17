using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenernate.Design
{
    internal class DeskManager
    {
        public void CreateInstance(Form main)
        {
            if (main is Main)
            {
                Main form = (Main)main;
                if (form.desk == null)
                {
                    form.desk = new Desk();
                }
                form.desk.Show(form.DockPanel);
            }
        }
    }
}
