namespace EEditor
{
    partial class WorldSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WorldSettings));
            this.rbEEOffline = new System.Windows.Forms.RadioButton();
            this.rbEEOEditor = new System.Windows.Forms.RadioButton();
            this.lblMade = new System.Windows.Forms.Label();
            this.tbowner = new System.Windows.Forms.TextBox();
            this.tbtitle = new System.Windows.Forms.TextBox();
            this.lname = new System.Windows.Forms.Label();
            this.ltitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnColor = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cbMinimap = new System.Windows.Forms.CheckBox();
            this.txtbDescr = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // rbEEOffline
            // 
            this.rbEEOffline.AutoSize = true;
            this.rbEEOffline.Checked = true;
            this.rbEEOffline.Location = new System.Drawing.Point(78, 94);
            this.rbEEOffline.Name = "rbEEOffline";
            this.rbEEOffline.Size = new System.Drawing.Size(72, 17);
            this.rbEEOffline.TabIndex = 13;
            this.rbEEOffline.TabStop = true;
            this.rbEEOffline.Text = "EE Offline";
            this.rbEEOffline.UseVisualStyleBackColor = true;
            // 
            // rbEEOEditor
            // 
            this.rbEEOEditor.AutoSize = true;
            this.rbEEOEditor.Location = new System.Drawing.Point(152, 94);
            this.rbEEOEditor.Name = "rbEEOEditor";
            this.rbEEOEditor.Size = new System.Drawing.Size(67, 17);
            this.rbEEOEditor.TabIndex = 12;
            this.rbEEOEditor.Text = "EEOditor";
            this.rbEEOEditor.UseVisualStyleBackColor = true;
            // 
            // lblMade
            // 
            this.lblMade.AutoSize = true;
            this.lblMade.Location = new System.Drawing.Point(10, 96);
            this.lblMade.Name = "lblMade";
            this.lblMade.Size = new System.Drawing.Size(62, 13);
            this.lblMade.TabIndex = 11;
            this.lblMade.Text = "Made With:";
            // 
            // tbowner
            // 
            this.tbowner.Location = new System.Drawing.Point(12, 68);
            this.tbowner.Name = "tbowner";
            this.tbowner.Size = new System.Drawing.Size(139, 20);
            this.tbowner.TabIndex = 10;
            this.tbowner.Text = "player";
            // 
            // tbtitle
            // 
            this.tbtitle.Location = new System.Drawing.Point(12, 28);
            this.tbtitle.Name = "tbtitle";
            this.tbtitle.Size = new System.Drawing.Size(190, 20);
            this.tbtitle.TabIndex = 9;
            this.tbtitle.Text = "Untitled World";
            // 
            // lname
            // 
            this.lname.AutoSize = true;
            this.lname.Location = new System.Drawing.Point(10, 51);
            this.lname.Name = "lname";
            this.lname.Size = new System.Drawing.Size(41, 13);
            this.lname.TabIndex = 8;
            this.lname.Text = "Owner:";
            // 
            // ltitle
            // 
            this.ltitle.AutoSize = true;
            this.ltitle.Location = new System.Drawing.Point(9, 12);
            this.ltitle.Name = "ltitle";
            this.ltitle.Size = new System.Drawing.Size(30, 13);
            this.ltitle.TabIndex = 7;
            this.ltitle.Text = "Title:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(93, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(12, 118);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(75, 23);
            this.btnColor.TabIndex = 15;
            this.btnColor.Text = "Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(75, 224);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbMinimap
            // 
            this.cbMinimap.AutoSize = true;
            this.cbMinimap.Checked = true;
            this.cbMinimap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbMinimap.Location = new System.Drawing.Point(137, 122);
            this.cbMinimap.Name = "cbMinimap";
            this.cbMinimap.Size = new System.Drawing.Size(65, 17);
            this.cbMinimap.TabIndex = 17;
            this.cbMinimap.Text = "Minimap";
            this.cbMinimap.UseVisualStyleBackColor = true;
            this.cbMinimap.CheckedChanged += new System.EventHandler(this.cbMinimap_CheckedChanged);
            // 
            // txtbDescr
            // 
            this.txtbDescr.Location = new System.Drawing.Point(12, 160);
            this.txtbDescr.MaxLength = 100;
            this.txtbDescr.Multiline = true;
            this.txtbDescr.Name = "txtbDescr";
            this.txtbDescr.Size = new System.Drawing.Size(207, 52);
            this.txtbDescr.TabIndex = 18;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(10, 144);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 19;
            this.lblDescription.Text = "Description:";
            // 
            // WorldSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(231, 259);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtbDescr);
            this.Controls.Add(this.cbMinimap);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.rbEEOffline);
            this.Controls.Add(this.rbEEOEditor);
            this.Controls.Add(this.lblMade);
            this.Controls.Add(this.tbowner);
            this.Controls.Add(this.tbtitle);
            this.Controls.Add(this.lname);
            this.Controls.Add(this.ltitle);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WorldSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.WorldSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbEEOffline;
        private System.Windows.Forms.RadioButton rbEEOEditor;
        private System.Windows.Forms.Label lblMade;
        private System.Windows.Forms.TextBox tbowner;
        private System.Windows.Forms.TextBox tbtitle;
        private System.Windows.Forms.Label lname;
        private System.Windows.Forms.Label ltitle;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnColor;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.CheckBox cbMinimap;
        private System.Windows.Forms.TextBox txtbDescr;
        private System.Windows.Forms.Label lblDescription;
    }
}