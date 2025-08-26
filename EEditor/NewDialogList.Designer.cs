namespace EEditor
{
    partial class NewDialogList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDialogList));
            this.Listviewlos = new System.Windows.Forms.ListView();
            this.cHSizeNum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cHSizeName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // Listviewlos
            // 
            this.Listviewlos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cHSizeNum,
            this.cHSizeName});
            this.Listviewlos.FullRowSelect = true;
            this.Listviewlos.GridLines = true;
            this.Listviewlos.HideSelection = false;
            this.Listviewlos.Location = new System.Drawing.Point(12, 12);
            this.Listviewlos.MultiSelect = false;
            this.Listviewlos.Name = "Listviewlos";
            this.Listviewlos.Size = new System.Drawing.Size(273, 284);
            this.Listviewlos.TabIndex = 0;
            this.Listviewlos.UseCompatibleStateImageBehavior = false;
            this.Listviewlos.View = System.Windows.Forms.View.Details;
            this.Listviewlos.SelectedIndexChanged += new System.EventHandler(this.Listviewlos_SelectedIndexChanged);
            // 
            // cHSizeNum
            // 
            this.cHSizeNum.Text = "Size";
            this.cHSizeNum.Width = 132;
            // 
            // cHSizeName
            // 
            this.cHSizeName.Text = "Name";
            this.cHSizeName.Width = 137;
            // 
            // NewDialogList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 308);
            this.Controls.Add(this.Listviewlos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(318, 347);
            this.MinimumSize = new System.Drawing.Size(318, 347);
            this.Name = "NewDialogList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Word List Sizes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewDialogList_FormClosing);
            this.Load += new System.EventHandler(this.NewDialogList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView Listviewlos;
        private System.Windows.Forms.ColumnHeader cHSizeNum;
        private System.Windows.Forms.ColumnHeader cHSizeName;
    }
}