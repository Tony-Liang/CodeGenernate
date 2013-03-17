using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeGenernate.Common;
using LCW.Framework.Common.SysFile;

namespace CodeGenernate.Design
{
    internal class TemplateManager
    {
        public void CreateInstance(Form main)
        {
            if (main is Main)
            {
                Main form = (Main)main;
                if (form.template == null)
                {
                    form.template = new CodeTemplate();
                    form.template.FileHandler += new EventHandler<DocumentEventArgs>((object sender, DocumentEventArgs e) =>
                    {
                        form.CreateDocument(e.Tag, () =>
                        {
                            TemplateFile temp = (TemplateFile)e.Sender;
                            string content = FileHelper.ReadFile(temp.Path);
                            VelocityCode dummyDoc = new VelocityCode();                       
                            dummyDoc.SetMessage(content, temp);
                            return dummyDoc;
                        });
                    });
                }
                form.template.Show(form.DockPanel);
            }
        }
    }
}
