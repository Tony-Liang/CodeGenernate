using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenernate.Design
{
    internal class OutputManager
    {
        public void CreateInstance(Form main)
        {
            if (main is Main)
            {
                Main form = (Main)main;
                if (form.output == null)
                {
                    form.output = new Output();
                }
                form.output.Show(form.DockPanel);
            }
        }
    }
}
