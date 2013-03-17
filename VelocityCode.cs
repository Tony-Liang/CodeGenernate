using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using CodeGenernate.Common;
using ScintillaNET;

namespace CodeGenernate
{
    public partial class VelocityCode : DockContent
    {
        public VelocityCode()
        {
            InitializeComponent();
        }

        private string message;
        public string Message
        {
            get
            {
                return message;
            }
        }
        private TemplateFile template;
        public TemplateFile Template
        {
            get
            {
                return template;
            }
        }

        public Scintilla Scintilla
        {
            get
            {
                return this.scintilla1;
            }
        }
        public void SetMessage(string msg,TemplateFile file)
        {
            this.message = msg;
            this.template = file;
            this.scintilla1.Text = this.message;
            //this.scintilla1.IsReadOnly = true;
            this.scintilla1.ConfigurationManager.Language = LauguaageType.cs.ToString();
                this.scintilla1.Indentation.SmartIndentType = SmartIndent.CPP;
            this.scintilla1.Margins.Margin0.Width = 35;
        }
    }
}
