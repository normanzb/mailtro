namespace ItemCollection
{
    partial class ItemCollection
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMain = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMain
            // 
            this.txtMain.BackColor = System.Drawing.SystemColors.Menu;
            this.txtMain.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.txtMain.Location = new System.Drawing.Point(3, 0);
            this.txtMain.Multiline = true;
            this.txtMain.Name = "txtMain";
            this.txtMain.Size = new System.Drawing.Size(100, 20);
            this.txtMain.TabIndex = 0;
            this.txtMain.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMain_KeyDown);
            // 
            // ItemCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtMain);
            this.Name = "ItemCollection";
            this.Size = new System.Drawing.Size(212, 22);
            this.Load += new System.EventHandler(this.ItemCollection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMain;

    }
}
