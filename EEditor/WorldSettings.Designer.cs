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
            this.lblCrewName = new System.Windows.Forms.Label();
            this.txtbCrewName = new System.Windows.Forms.TextBox();
            this.lblCrewID = new System.Windows.Forms.Label();
            this.txtbCrewID = new System.Windows.Forms.TextBox();
            this.lblOwnerID = new System.Windows.Forms.Label();
            this.txtbOwnerID = new System.Windows.Forms.TextBox();
            this.cbCampaign = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbowner
            // 
            this.tbowner.Location = new System.Drawing.Point(12, 73);
            this.tbowner.Name = "tbowner";
            this.tbowner.Size = new System.Drawing.Size(139, 20);
            this.tbowner.TabIndex = 10;
            this.tbowner.Text = "player";
            // 
            // tbtitle
            // 
            this.tbtitle.Location = new System.Drawing.Point(12, 28);
            this.tbtitle.Name = "tbtitle";
            this.tbtitle.Size = new System.Drawing.Size(208, 20);
            this.tbtitle.TabIndex = 9;
            this.tbtitle.Text = "Untitled World";
            // 
            // lname
            // 
            this.lname.AutoSize = true;
            this.lname.Location = new System.Drawing.Point(11, 52);
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
            this.pictureBox1.Location = new System.Drawing.Point(95, 189);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // btnColor
            // 
            this.btnColor.Location = new System.Drawing.Point(14, 190);
            this.btnColor.Name = "btnColor";
            this.btnColor.Size = new System.Drawing.Size(75, 23);
            this.btnColor.TabIndex = 15;
            this.btnColor.Text = "BG Color";
            this.btnColor.UseVisualStyleBackColor = true;
            this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 303);
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
            this.cbMinimap.Location = new System.Drawing.Point(139, 194);
            this.cbMinimap.Name = "cbMinimap";
            this.cbMinimap.Size = new System.Drawing.Size(65, 17);
            this.cbMinimap.TabIndex = 17;
            this.cbMinimap.Text = "Minimap";
            this.cbMinimap.UseVisualStyleBackColor = true;
            this.cbMinimap.CheckedChanged += new System.EventHandler(this.cbMinimap_CheckedChanged);
            // 
            // txtbDescr
            // 
            this.txtbDescr.Location = new System.Drawing.Point(14, 245);
            this.txtbDescr.MaxLength = 100;
            this.txtbDescr.Multiline = true;
            this.txtbDescr.Name = "txtbDescr";
            this.txtbDescr.Size = new System.Drawing.Size(206, 52);
            this.txtbDescr.TabIndex = 18;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 229);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(63, 13);
            this.lblDescription.TabIndex = 19;
            this.lblDescription.Text = "Description:";
            // 
            // lblCrewName
            // 
            this.lblCrewName.AutoSize = true;
            this.lblCrewName.Location = new System.Drawing.Point(11, 148);
            this.lblCrewName.Name = "lblCrewName";
            this.lblCrewName.Size = new System.Drawing.Size(65, 13);
            this.lblCrewName.TabIndex = 20;
            this.lblCrewName.Text = "Crew Name:";
            // 
            // txtbCrewName
            // 
            this.txtbCrewName.Location = new System.Drawing.Point(12, 164);
            this.txtbCrewName.Name = "txtbCrewName";
            this.txtbCrewName.Size = new System.Drawing.Size(100, 20);
            this.txtbCrewName.TabIndex = 21;
            // 
            // lblCrewID
            // 
            this.lblCrewID.AutoSize = true;
            this.lblCrewID.Location = new System.Drawing.Point(127, 148);
            this.lblCrewID.Name = "lblCrewID";
            this.lblCrewID.Size = new System.Drawing.Size(45, 13);
            this.lblCrewID.TabIndex = 22;
            this.lblCrewID.Text = "CrewID:";
            // 
            // txtbCrewID
            // 
            this.txtbCrewID.Location = new System.Drawing.Point(120, 164);
            this.txtbCrewID.Name = "txtbCrewID";
            this.txtbCrewID.Size = new System.Drawing.Size(100, 20);
            this.txtbCrewID.TabIndex = 23;
            // 
            // lblOwnerID
            // 
            this.lblOwnerID.AutoSize = true;
            this.lblOwnerID.Location = new System.Drawing.Point(10, 101);
            this.lblOwnerID.Name = "lblOwnerID";
            this.lblOwnerID.Size = new System.Drawing.Size(55, 13);
            this.lblOwnerID.TabIndex = 25;
            this.lblOwnerID.Text = "Owner ID:";
            // 
            // txtbOwnerID
            // 
            this.txtbOwnerID.Location = new System.Drawing.Point(13, 117);
            this.txtbOwnerID.Name = "txtbOwnerID";
            this.txtbOwnerID.Size = new System.Drawing.Size(140, 20);
            this.txtbOwnerID.TabIndex = 26;
            // 
            // cbCampaign
            // 
            this.cbCampaign.AutoSize = true;
            this.cbCampaign.Location = new System.Drawing.Point(139, 217);
            this.cbCampaign.Name = "cbCampaign";
            this.cbCampaign.Size = new System.Drawing.Size(73, 17);
            this.cbCampaign.TabIndex = 27;
            this.cbCampaign.Text = "Campaign";
            this.cbCampaign.UseVisualStyleBackColor = true;
            this.cbCampaign.CheckedChanged += new System.EventHandler(this.cbCampaign_CheckedChanged);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(120, 303);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(94, 23);
            this.btnReset.TabIndex = 28;
            this.btnReset.Text = "Reset to default";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // WorldSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 335);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.cbCampaign);
            this.Controls.Add(this.txtbOwnerID);
            this.Controls.Add(this.lblOwnerID);
            this.Controls.Add(this.txtbCrewID);
            this.Controls.Add(this.lblCrewID);
            this.Controls.Add(this.txtbCrewName);
            this.Controls.Add(this.lblCrewName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtbDescr);
            this.Controls.Add(this.cbMinimap);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnColor);
            this.Controls.Add(this.pictureBox1);
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
        private System.Windows.Forms.Label lblCrewName;
        private System.Windows.Forms.TextBox txtbCrewName;
        private System.Windows.Forms.Label lblCrewID;
        private System.Windows.Forms.TextBox txtbCrewID;
        private System.Windows.Forms.Label lblOwnerID;
        private System.Windows.Forms.TextBox txtbOwnerID;
        private System.Windows.Forms.CheckBox cbCampaign;
        private System.Windows.Forms.Button btnReset;
    }
}