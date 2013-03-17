using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CodeGenernate.Common;
using LCW.Framework.Common.Genernation.DataBases.Entities;
using CodeGenernate.Design.CodeTemplates;

namespace CodeGenernate.Design
{
    internal class DatabaseManager
    {
        public void CreateInstance(Form main)
        {
            if (main is Main)
            {
                Main form = (Main)main;
                if (form.databasetree == null)
                {
                    form.databasetree = new DataBasesTree();
                    form.databasetree.DocHandler += new EventHandler<DocumentEventArgs>((object sender, DocumentEventArgs e) =>
                    {
                        if (e.Sender is string)
                        {
                            form.CreateDocument(e.Tag, () =>
                            {
                                string msg = (string)e.Sender;
                                CodeDoc dummyDoc = new CodeDoc();
                                dummyDoc.SetMessage(msg, LauguaageType.mssql);
                                return dummyDoc;
                            });
                        }
                        else if (e.sender is TableEntity)
                        {
                            var obj=LCW.Framework.Common.Driver.DBDriverProvice.GetInstance;
                            TableEntity table = (TableEntity)e.sender;
                            form.CreateDocument(table.Name, () =>
                            {
                                IDictionary<string, object> dic = new Dictionary<string, object>();
                                dic.Add("table", table.Name);
                                dic.Add("columns", table.Columns);
                                string msg = VelocityWrapper.CreateCode(new GeneralCode().Execute(),dic);
                                CodeDoc dummyDoc = new CodeDoc();
                                dummyDoc.SetMessage(msg, LauguaageType.cs);
                                return dummyDoc;
                            });
                        }
                    });
                }
                form.databasetree.Show(form.DockPanel);
            }
        }
    }
}
