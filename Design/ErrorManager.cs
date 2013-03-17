using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenernate.Design
{
    internal class ErrorManager
    {
        public void CreateInstance(Form main)
        {
            if (main is Main)
            {
                Main form = (Main)main;
                if (form.error == null)
                {
                    form.error = new Error();
                }
                form.error.Show(form.DockPanel);
            }
        }
    }
}
