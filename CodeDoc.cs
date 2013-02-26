using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using ScintillaNET;

namespace CodeGenernate
{
    public partial class CodeDoc : DockContent
    {
        public CodeDoc()
        {
            InitializeComponent();
        }

        private string message;

        public void SetMessage(string msg,LauguaageType type)
        {
            this.message = msg;
            this.scintilla1.Text = this.message;
            this.scintilla1.Margins.Margin0.Width = 35;
            SetLanguage(type.ToString());
        }
        
        protected override void OnLoad(EventArgs e)
        {
            //this.scintilla1.IsReadOnly = true;
        }

        public Scintilla ScintillaObj
        {
            get
            {
                return scintilla1;
            }
        }
        private void SetLanguage(string language)
        {
            if ("ini".Equals(language, StringComparison.OrdinalIgnoreCase))
            {
                // Reset/set all styles and prepare _scintilla for custom lexing
                //ActiveDocument.IniLexer = true;
                //IniLexer.Init(ActiveDocument.Scintilla);
            }
            else
            {
                // Use a built-in lexer and configuration
                //ActiveDocument.IniLexer = false;
                //ActiveDocument.Scintilla.ConfigurationManager.Language = language;
                this.scintilla1.ConfigurationManager.Language = language;
                // Smart indenting...
                if ("cs".Equals(language, StringComparison.OrdinalIgnoreCase))
                    this.scintilla1.Indentation.SmartIndentType = SmartIndent.CPP;
                else
                    this.scintilla1.Indentation.SmartIndentType = SmartIndent.None;
            }
        }

        private void CodeDoc_Load(object sender, EventArgs e)
        {

        }
    }

    public enum LauguaageType
    {
        cs,
        cpp,
        html,
        js,
        mssql,
        pgsql,
        python,
        vbscript,
        xml
    }
}
