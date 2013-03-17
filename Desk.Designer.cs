namespace CodeGenernate
{
    partial class Desk
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
            this.components = new System.ComponentModel.Container();
            this.folderTreeView1 = new CodeGenernate.Controls.FolderTreeView(this.components);
            this.SuspendLayout();
            // 
            // folderTreeView1
            // 
            this.folderTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTreeView1.Location = new System.Drawing.Point(0, 0);
            this.folderTreeView1.Name = "folderTreeView1";
            this.folderTreeView1.Size = new System.Drawing.Size(258, 262);
            this.folderTreeView1.TabIndex = 0;
            // 
            // Desk
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(258, 262);
            this.Controls.Add(this.folderTreeView1);
            this.HideOnClose = true;
            this.Name = "Desk";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRightAutoHide;
            this.Text = "Desk";
            this.Load += new System.EventHandler(this.Desk_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.FolderTreeView folderTreeView1;



    }
}