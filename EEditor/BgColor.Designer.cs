namespace EEditor
{
    partial class BgColor
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
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.pbColor = new System.Windows.Forms.PictureBox();
            this.btnChoose = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).BeginInit();
            this.SuspendLayout();
            // 
            // cbEnable
            // 
            this.cbEnable.AutoSize = true;
            this.cbEnable.Location = new System.Drawing.Point(77, 45);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(59, 17);
            this.cbEnable.TabIndex = 0;
            this.cbEnable.Text = "Enable";
            this.cbEnable.UseVisualStyleBackColor = true;
            this.cbEnable.CheckedChanged += new System.EventHandler(this.cbEnable_CheckedChanged);
            // 
            // pbColor
            // 
            this.pbColor.Location = new System.Drawing.Point(12, 12);
            this.pbColor.Name = "pbColor";
            this.pbColor.Size = new System.Drawing.Size(59, 50);
            this.pbColor.TabIndex = 1;
            this.pbColor.TabStop = false;
            // 
            // btnChoose
            // 
            this.btnChoose.Location = new System.Drawing.Point(77, 16);
            this.btnChoose.Name = "btnChoose";
            this.btnChoose.Size = new System.Drawing.Size(75, 23);
            this.btnChoose.TabIndex = 2;
            this.btnChoose.Text = "Choose";
            this.btnChoose.UseVisualStyleBackColor = true;
            this.btnChoose.Click += new System.EventHandler(this.btnChoose_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(12, 74);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(140, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply And Close";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // BgColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(170, 109);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnChoose);
            this.Controls.Add(this.pbColor);
            this.Controls.Add(this.cbEnable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BgColor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Background Color";
            this.Load += new System.EventHandler(this.BgColor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbEnable;
        private System.Windows.Forms.PictureBox pbColor;
        private System.Windows.Forms.Button btnChoose;
        private System.Windows.Forms.Button btnApply;
    }
}