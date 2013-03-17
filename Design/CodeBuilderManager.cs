using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGenernate.Design
{
    internal class CodeBuilderManager
    {
        public void CreateInstance(Form main,string content)
        {
            if (main is Main)
            {
                Main form = (Main)main;
                CodeBuilder builder = new CodeBuilder(content);
                builder.Handle += new ExceptionHandle<ExceptionArgs>((ExceptionArgs e) =>
                {
                    form.Dispatcher(DispatcherType.Error, e.Exception.Message.ToString());
                });
                builder.ShowDialog();
            }
        } 
    }
}
