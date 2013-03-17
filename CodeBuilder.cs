using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LCW.Framework.Common.Genernation.DataBases;
using CodeGenernate.Properties;
using CodeGenernate.Common;
using NVelocity;
using CodeGenernate.Design;

namespace CodeGenernate
{
    public partial class CodeBuilder : Form
    {
        public CodeBuilder()
        {
            InitializeComponent();
        }
        private string templatecontent;
        public CodeBuilder(string templatepath)
            : this()
        {
            this.templatecontent = templatepath;
        }
        public event ExceptionHandle<ExceptionArgs> Handle;
        private void CodeBuilder_Load(object sender, EventArgs e)
        {
            this.cbo_database.DataSource = null;
            ServiceSite site = DataBaseSchemaBuilder.GetInstance();
            if (site != null)
            {
                this.cbo_database.DataSource = site.DataBases;
                this.cbo_database.DisplayMember = "Name";
                this.cbo_database.ValueMember = "Name";
            }
            this.txt_output.Text=Settings.Default.outputpath;
            this.txt_assembly.Text = Settings.Default.assembly;
            this.txt_namespace.Text = Settings.Default.namespaces;
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            Settings.Default.outputpath = this.txt_output.Text;
            Settings.Default.assembly = this.txt_assembly.Text;
            Settings.Default.namespaces = this.txt_namespace.Text;
            Settings.Default.Save();

            VelocityContext context = new VelocityContext();
            context.Put("servicesite", DataBaseSchemaBuilder.GetInstance());
            context.Put("Assembly",txt_assembly.Text);
            context.Put("NameSpace", txt_namespace.Text);
            try
            {
                VelocityWrapper.CreateCode(this.templatecontent, this.txt_output.Text, context);
            }
            catch (Exception ex)
            {
                if(Handle!=null)
                    Handle(new ExceptionArgs(null,ex));
            }
            this.Close();
        }

        
        private void btn_savedialog_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowNewFolderButton = true;
            if (DialogResult.OK == folderBrowserDialog1.ShowDialog())
            {
                this.txt_output.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}
