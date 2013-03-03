namespace CodeGenernate
{
    partial class CodeBuilder
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbo_database = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_output = new System.Windows.Forms.TextBox();
            this.btn_savedialog = new System.Windows.Forms.Button();
            this.btn_save = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_namespace = new System.Windows.Forms.TextBox();
            this.txt_assembly = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "数据库：";
            // 
            // cbo_database
            // 
            this.cbo_database.FormattingEnabled = true;
            this.cbo_database.Location = new System.Drawing.Point(71, 15);
            this.cbo_database.Name = "cbo_database";
            this.cbo_database.Size = new System.Drawing.Size(230, 20);
            this.cbo_database.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "输出路径：";
            // 
            // txt_output
            // 
            this.txt_output.Location = new System.Drawing.Point(71, 275);
            this.txt_output.Name = "txt_output";
            this.txt_output.Size = new System.Drawing.Size(194, 21);
            this.txt_output.TabIndex = 3;
            // 
            // btn_savedialog
            // 
            this.btn_savedialog.Location = new System.Drawing.Point(271, 273);
            this.btn_savedialog.Name = "btn_savedialog";
            this.btn_savedialog.Size = new System.Drawing.Size(75, 23);
            this.btn_savedialog.TabIndex = 4;
            this.btn_savedialog.Text = "浏览";
            this.btn_savedialog.UseVisualStyleBackColor = true;
            this.btn_savedialog.Click += new System.EventHandler(this.btn_savedialog_Click);
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(271, 307);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(75, 23);
            this.btn_save.TabIndex = 5;
            this.btn_save.Text = "确认";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "命名空间：";
            // 
            // txt_namespace
            // 
            this.txt_namespace.Location = new System.Drawing.Point(71, 94);
            this.txt_namespace.Name = "txt_namespace";
            this.txt_namespace.Size = new System.Drawing.Size(230, 21);
            this.txt_namespace.TabIndex = 7;
            // 
            // txt_assembly
            // 
            this.txt_assembly.Location = new System.Drawing.Point(71, 52);
            this.txt_assembly.Name = "txt_assembly";
            this.txt_assembly.Size = new System.Drawing.Size(230, 21);
            this.txt_assembly.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "程序集：";
            // 
            // CodeBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 342);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_assembly);
            this.Controls.Add(this.txt_namespace);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_savedialog);
            this.Controls.Add(this.txt_output);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbo_database);
            this.Controls.Add(this.label1);
            this.Name = "CodeBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CodeBuilder";
            this.Load += new System.EventHandler(this.CodeBuilder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbo_database;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_output;
        private System.Windows.Forms.Button btn_savedialog;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_namespace;
        private System.Windows.Forms.TextBox txt_assembly;
        private System.Windows.Forms.Label label4;
    }
}