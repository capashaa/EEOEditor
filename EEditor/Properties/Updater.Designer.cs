namespace EEditor.Properties
{
    partial class Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnDownload = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rtbChangelog = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCheck = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.lbliLastUpdate = new System.Windows.Forms.Label();
            this.lblNewest = new System.Windows.Forms.Label();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.lbliNewest = new System.Windows.Forms.Label();
            this.lbliCurrent = new System.Windows.Forms.Label();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 382);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(286, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(109, 411);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(75, 23);
            this.btnDownload.TabIndex = 14;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rtbChangelog);
            this.groupBox3.Location = new System.Drawing.Point(12, 156);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(286, 220);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Changelog and Logs";
            // 
            // rtbChangelog
            // 
            this.rtbChangelog.Location = new System.Drawing.Point(6, 19);
            this.rtbChangelog.Name = "rtbChangelog";
            this.rtbChangelog.Size = new System.Drawing.Size(271, 191);
            this.rtbChangelog.TabIndex = 0;
            this.rtbChangelog.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCheck);
            this.groupBox2.Location = new System.Drawing.Point(12, 104);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 46);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Options";
            // 
            // btnCheck
            // 
            this.btnCheck.Location = new System.Drawing.Point(76, 17);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(116, 23);
            this.btnCheck.TabIndex = 7;
            this.btnCheck.Text = "Check for updates";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLastUpdate);
            this.groupBox1.Controls.Add(this.lbliLastUpdate);
            this.groupBox1.Controls.Add(this.lblNewest);
            this.groupBox1.Controls.Add(this.lblCurrent);
            this.groupBox1.Controls.Add(this.lbliNewest);
            this.groupBox1.Controls.Add(this.lbliCurrent);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 86);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Version Information";
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.Location = new System.Drawing.Point(114, 52);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(65, 13);
            this.lblLastUpdate.TabIndex = 5;
            this.lblLastUpdate.Text = "1 day(s) ago";
            // 
            // lbliLastUpdate
            // 
            this.lbliLastUpdate.AutoSize = true;
            this.lbliLastUpdate.Location = new System.Drawing.Point(6, 52);
            this.lbliLastUpdate.Name = "lbliLastUpdate";
            this.lbliLastUpdate.Size = new System.Drawing.Size(102, 13);
            this.lbliLastUpdate.TabIndex = 4;
            this.lbliLastUpdate.Text = "Last Update Check:";
            // 
            // lblNewest
            // 
            this.lblNewest.AutoSize = true;
            this.lblNewest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblNewest.Location = new System.Drawing.Point(231, 29);
            this.lblNewest.Name = "lblNewest";
            this.lblNewest.Size = new System.Drawing.Size(31, 13);
            this.lblNewest.TabIndex = 3;
            this.lblNewest.Text = "0.0.0";
            // 
            // lblCurrent
            // 
            this.lblCurrent.AutoSize = true;
            this.lblCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblCurrent.Location = new System.Drawing.Point(94, 29);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(31, 13);
            this.lblCurrent.TabIndex = 2;
            this.lblCurrent.Text = "0.0.0";
            // 
            // lbliNewest
            // 
            this.lbliNewest.AutoSize = true;
            this.lbliNewest.Location = new System.Drawing.Point(142, 29);
            this.lbliNewest.Name = "lbliNewest";
            this.lbliNewest.Size = new System.Drawing.Size(83, 13);
            this.lbliNewest.TabIndex = 1;
            this.lbliNewest.Text = "Newest version:";
            // 
            // lbliCurrent
            // 
            this.lbliCurrent.AutoSize = true;
            this.lbliCurrent.Location = new System.Drawing.Point(6, 29);
            this.lbliCurrent.Name = "lbliCurrent";
            this.lbliCurrent.Size = new System.Drawing.Size(82, 13);
            this.lbliCurrent.TabIndex = 0;
            this.lbliCurrent.Text = "Current Version:";
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 450);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Updater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Software Updater";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Updater_FormClosed);
            this.Load += new System.EventHandler(this.Updater_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtbChangelog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.Label lbliLastUpdate;
        private System.Windows.Forms.Label lblNewest;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.Label lbliNewest;
        private System.Windows.Forms.Label lbliCurrent;
    }
}