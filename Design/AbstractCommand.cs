using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGenernate.Design
{
    public abstract class AbstractCommand
    {
        private Main form;
        private DocumentManager doc;

        public AbstractCommand(Main form)
        {
            this.form = form;
            doc = new DocumentManager(form);
        }

        public abstract void Execute();
    }
}
