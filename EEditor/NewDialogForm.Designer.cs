namespace EEditor
{
    partial class NewDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewDialogForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSizeList = new System.Windows.Forms.Button();
            this.nUHeight = new System.Windows.Forms.NumericUpDown();
            this.lHeight = new System.Windows.Forms.Label();
            this.nUWidth = new System.Windows.Forms.NumericUpDown();
            this.lWidth = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.gbWorldOptions = new System.Windows.Forms.GroupBox();
            this.tbowner = new System.Windows.Forms.TextBox();
            this.tbtitle = new System.Windows.Forms.TextBox();
            this.lname = new System.Windows.Forms.Label();
            this.ltitle = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUWidth)).BeginInit();
            this.gbWorldOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSizeList);
            this.groupBox1.Controls.Add(this.nUHeight);
            this.groupBox1.Controls.Add(this.lHeight);
            this.groupBox1.Controls.Add(this.nUWidth);
            this.groupBox1.Controls.Add(this.lWidth);
            this.groupBox1.Location = new System.Drawing.Point(12, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 85);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Size";
            // 
            // btnSizeList
            // 
            this.btnSizeList.Location = new System.Drawing.Point(139, 38);
            this.btnSizeList.Name = "btnSizeList";
            this.btnSizeList.Size = new System.Drawing.Size(75, 26);
            this.btnSizeList.TabIndex = 4;
            this.btnSizeList.Text = "Size List";
            this.btnSizeList.UseVisualStyleBackColor = true;
            this.btnSizeList.Click += new System.EventHandler(this.btnSizeList_Click);
            // 
            // nUHeight
            // 
            this.nUHeight.Location = new System.Drawing.Point(56, 49);
            this.nUHeight.Maximum = new decimal(new int[] {
            637,
            0,
            0,
            0});
            this.nUHeight.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nUHeight.Name = "nUHeight";
            this.nUHeight.Size = new System.Drawing.Size(77, 20);
            this.nUHeight.TabIndex = 3;
            this.nUHeight.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nUHeight.ValueChanged += new System.EventHandler(this.nUBox_ValueChanged);
            // 
            // lHeight
            // 
            this.lHeight.AutoSize = true;
            this.lHeight.Location = new System.Drawing.Point(12, 51);
            this.lHeight.Name = "lHeight";
            this.lHeight.Size = new System.Drawing.Size(38, 13);
            this.lHeight.TabIndex = 2;
            this.lHeight.Text = "Height";
            // 
            // nUWidth
            // 
            this.nUWidth.Location = new System.Drawing.Point(56, 25);
            this.nUWidth.Maximum = new decimal(new int[] {
            637,
            0,
            0,
            0});
            this.nUWidth.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nUWidth.Name = "nUWidth";
            this.nUWidth.Size = new System.Drawing.Size(77, 20);
            this.nUWidth.TabIndex = 1;
            this.nUWidth.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nUWidth.ValueChanged += new System.EventHandler(this.nUBox_ValueChanged);
            // 
            // lWidth
            // 
            this.lWidth.AutoSize = true;
            this.lWidth.Location = new System.Drawing.Point(12, 27);
            this.lWidth.Name = "lWidth";
            this.lWidth.Size = new System.Drawing.Size(38, 13);
            this.lWidth.TabIndex = 0;
            this.lWidth.Text = "Width:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(68, 222);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 26);
            this.button1.TabIndex = 7;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbWorldOptions
            // 
            this.gbWorldOptions.Controls.Add(this.tbowner);
            this.gbWorldOptions.Controls.Add(this.tbtitle);
            this.gbWorldOptions.Controls.Add(this.lname);
            this.gbWorldOptions.Controls.Add(this.ltitle);
            this.gbWorldOptions.Location = new System.Drawing.Point(12, 8);
            this.gbWorldOptions.Name = "gbWorldOptions";
            this.gbWorldOptions.Size = new System.Drawing.Size(231, 117);
            this.gbWorldOptions.TabIndex = 32;
            this.gbWorldOptions.TabStop = false;
            this.gbWorldOptions.Text = "World Options";
            // 
            // tbowner
            // 
            this.tbowner.Location = new System.Drawing.Point(15, 81);
            this.tbowner.Name = "tbowner";
            this.tbowner.Size = new System.Drawing.Size(139, 20);
            this.tbowner.TabIndex = 3;
            this.tbowner.Text = "player";
            // 
            // tbtitle
            // 
            this.tbtitle.Location = new System.Drawing.Point(15, 41);
            this.tbtitle.Name = "tbtitle";
            this.tbtitle.Size = new System.Drawing.Size(190, 20);
            this.tbtitle.TabIndex = 2;
            this.tbtitle.Text = "Untitled World";
            // 
            // lname
            // 
            this.lname.AutoSize = true;
            this.lname.Location = new System.Drawing.Point(12, 65);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(41, 13);
            this.lname.TabIndex = 1;
            this.lname.Text = "Owner:";
            // 
            // ltitle
            // 
            this.ltitle.AutoSize = true;
            this.ltitle.Location = new System.Drawing.Point(12, 25);
            this.ltitle.Name = "ltitle";
            this.ltitle.Size = new System.Drawing.Size(30, 13);
            this.ltitle.TabIndex = 0;
            this.ltitle.Text = "Title:";
            // 
            // NewDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 262);
            this.Controls.Add(this.gbWorldOptions);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "NewDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New World";
            this.Load += new System.EventHandler(this.NewDialogForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUWidth)).EndInit();
            this.gbWorldOptions.ResumeLayout(false);
            this.gbWorldOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown nUHeight;
        private System.Windows.Forms.Label lHeight;
        private System.Windows.Forms.NumericUpDown nUWidth;
        private System.Windows.Forms.Label lWidth;
        private System.Windows.Forms.GroupBox gbWorldOptions;
        private System.Windows.Forms.TextBox tbowner;
        private System.Windows.Forms.TextBox tbtitle;
        private System.Windows.Forms.Label lname;
        private System.Windows.Forms.Label ltitle;
        private System.Windows.Forms.Button btnSizeList;
    }
}