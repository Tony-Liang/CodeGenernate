using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGenernate.Common;
using System.Windows.Forms;

namespace CodeGenernate.Design
{
    public class DocumentManager
    {
        private DatabaseManager database;
        private TemplateManager template;
        private ErrorManager error;
        private OutputManager output;
        private DeskManager desk;
        private CodeBuilderManager codebuild;

        private Form main;
        public DocumentManager(Form main)
        {
            this.main = main;
            this.database = new DatabaseManager();
            this.template = new TemplateManager();
            this.error = new ErrorManager();
            this.output = new OutputManager();
            this.desk = new DeskManager();
            this.codebuild = new CodeBuilderManager();
        }

        public void CreateDatabaseForm()
        {
            this.database.CreateInstance(this.main);
        }

        public void CreateTemplateForm()
        {
            this.template.CreateInstance(this.main);
        }

        public void CreateDeskForm()
        {
            this.desk.CreateInstance(this.main);
        }

        public void CreateErrorForm()
        {
            this.error.CreateInstance(this.main);
        }

        public void CreateOutputForm()
        {
            this.output.CreateInstance(this.main);
        }

        public void CreateCodeBuildForm(string code)
        {
            this.codebuild.CreateInstance(this.main, code);
        }
    }

    public delegate void ExceptionHandle<ExceptionArgs>(ExceptionArgs e);
    public class ExceptionArgs:EventArgs
    {
        private Exception exception;

        public Exception Exception
        {
          get { return exception; }
        }

        private object sender;
        public object Sender
        {
          get { return sender; }
        }

        public ExceptionArgs(object sender,Exception exception)
        {
            this.exception=exception;
            this.sender=sender;
        }
    }
    public enum DispatcherType
    {
        Error,
        Building
    }
}
