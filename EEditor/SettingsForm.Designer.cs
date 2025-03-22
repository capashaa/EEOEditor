namespace EEditor
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.usePenToolCheckBox = new System.Windows.Forms.CheckBox();
            this.selectAllBorderCheckBox = new System.Windows.Forms.CheckBox();
            this.clearComboBox = new System.Windows.Forms.ComboBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.confirmCloseCheckBox = new System.Windows.Forms.CheckBox();
            this.FasterShapeStyleCheckBox = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusTextToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.StatusColorToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.DarkThemeCheckBox = new System.Windows.Forms.CheckBox();
            this.cBHotkeyBar = new System.Windows.Forms.CheckBox();
            this.rbBackground = new System.Windows.Forms.RadioButton();
            this.rbEmpty = new System.Windows.Forms.RadioButton();
            this.gbSelectionTool = new System.Windows.Forms.GroupBox();
            this.statusStrip1.SuspendLayout();
            this.gbSelectionTool.SuspendLayout();
            this.SuspendLayout();
            // 
            // usePenToolCheckBox
            // 
            this.usePenToolCheckBox.AutoSize = true;
            this.usePenToolCheckBox.Location = new System.Drawing.Point(11, 15);
            this.usePenToolCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.usePenToolCheckBox.Name = "usePenToolCheckBox";
            this.usePenToolCheckBox.Size = new System.Drawing.Size(184, 17);
            this.usePenToolCheckBox.TabIndex = 8;
            this.usePenToolCheckBox.Text = "Select Pen tool after block switch";
            this.usePenToolCheckBox.UseVisualStyleBackColor = true;
            this.usePenToolCheckBox.CheckedChanged += new System.EventHandler(this.usePenToolCheckBox_CheckedChanged);
            // 
            // selectAllBorderCheckBox
            // 
            this.selectAllBorderCheckBox.AutoSize = true;
            this.selectAllBorderCheckBox.Location = new System.Drawing.Point(11, 36);
            this.selectAllBorderCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.selectAllBorderCheckBox.Name = "selectAllBorderCheckBox";
            this.selectAllBorderCheckBox.Size = new System.Drawing.Size(165, 17);
            this.selectAllBorderCheckBox.TabIndex = 18;
            this.selectAllBorderCheckBox.Text = "Include borders with select all";
            this.selectAllBorderCheckBox.UseVisualStyleBackColor = true;
            this.selectAllBorderCheckBox.CheckedChanged += new System.EventHandler(this.selectAllBorderCheckBox_CheckedChanged);
            // 
            // clearComboBox
            // 
            this.clearComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.clearComboBox.Items.AddRange(new object[] {
            "Clear settings...",
            "",
            "Block hotkeys (0-9)",
            "Blocks in unknown tab",
            "",
            "Old EEditor settings & logins",
            "Current EEOditor settings"});
            this.clearComboBox.Location = new System.Drawing.Point(11, 232);
            this.clearComboBox.Name = "clearComboBox";
            this.clearComboBox.Size = new System.Drawing.Size(139, 21);
            this.clearComboBox.TabIndex = 27;
            this.clearComboBox.SelectedIndexChanged += new System.EventHandler(this.clearComboBox_SelectedIndexChanged);
            // 
            // clearButton
            // 
            this.clearButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.clearButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.clearButton.Location = new System.Drawing.Point(156, 230);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(49, 23);
            this.clearButton.TabIndex = 28;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // confirmCloseCheckBox
            // 
            this.confirmCloseCheckBox.AutoSize = true;
            this.confirmCloseCheckBox.Location = new System.Drawing.Point(11, 57);
            this.confirmCloseCheckBox.Margin = new System.Windows.Forms.Padding(2);
            this.confirmCloseCheckBox.Name = "confirmCloseCheckBox";
            this.confirmCloseCheckBox.Size = new System.Drawing.Size(125, 17);
            this.confirmCloseCheckBox.TabIndex = 29;
            this.confirmCloseCheckBox.Text = "Confirm EEOditor exit";
            this.confirmCloseCheckBox.UseVisualStyleBackColor = true;
            this.confirmCloseCheckBox.CheckedChanged += new System.EventHandler(this.confirmCloseCheckBox_CheckedChanged);
            // 
            // FasterShapeStyleCheckBox
            // 
            this.FasterShapeStyleCheckBox.AutoSize = true;
            this.FasterShapeStyleCheckBox.Enabled = false;
            this.FasterShapeStyleCheckBox.Location = new System.Drawing.Point(12, 209);
            this.FasterShapeStyleCheckBox.Name = "FasterShapeStyleCheckBox";
            this.FasterShapeStyleCheckBox.Size = new System.Drawing.Size(111, 17);
            this.FasterShapeStyleCheckBox.TabIndex = 30;
            this.FasterShapeStyleCheckBox.Text = "Faster shape style";
            this.FasterShapeStyleCheckBox.UseVisualStyleBackColor = true;
            this.FasterShapeStyleCheckBox.Visible = false;
            this.FasterShapeStyleCheckBox.CheckedChanged += new System.EventHandler(this.FasterShapeStyleCheckBox_CheckedChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusTextToolStripStatusLabel,
            this.StatusToolStripStatusLabel,
            this.StatusColorToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 263);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(259, 22);
            this.statusStrip1.TabIndex = 31;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusTextToolStripStatusLabel
            // 
            this.StatusTextToolStripStatusLabel.Name = "StatusTextToolStripStatusLabel";
            this.StatusTextToolStripStatusLabel.Size = new System.Drawing.Size(42, 17);
            this.StatusTextToolStripStatusLabel.Text = "Status:";
            this.StatusTextToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // StatusToolStripStatusLabel
            // 
            this.StatusToolStripStatusLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.StatusToolStripStatusLabel.Name = "StatusToolStripStatusLabel";
            this.StatusToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // StatusColorToolStripStatusLabel
            // 
            this.StatusColorToolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.StatusColorToolStripStatusLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.StatusColorToolStripStatusLabel.Name = "StatusColorToolStripStatusLabel";
            this.StatusColorToolStripStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // DarkThemeCheckBox
            // 
            this.DarkThemeCheckBox.AutoSize = true;
            this.DarkThemeCheckBox.Location = new System.Drawing.Point(11, 79);
            this.DarkThemeCheckBox.Name = "DarkThemeCheckBox";
            this.DarkThemeCheckBox.Size = new System.Drawing.Size(85, 17);
            this.DarkThemeCheckBox.TabIndex = 34;
            this.DarkThemeCheckBox.Text = "Dark Theme";
            this.DarkThemeCheckBox.UseVisualStyleBackColor = true;
            this.DarkThemeCheckBox.CheckedChanged += new System.EventHandler(this.DarkThemeCheckBox_CheckedChanged);
            // 
            // cBHotkeyBar
            // 
            this.cBHotkeyBar.AutoSize = true;
            this.cBHotkeyBar.Location = new System.Drawing.Point(11, 102);
            this.cBHotkeyBar.Name = "cBHotkeyBar";
            this.cBHotkeyBar.Size = new System.Drawing.Size(106, 17);
            this.cBHotkeyBar.TabIndex = 35;
            this.cBHotkeyBar.Text = "Show HotkeyBar";
            this.cBHotkeyBar.UseVisualStyleBackColor = true;
            this.cBHotkeyBar.CheckedChanged += new System.EventHandler(this.cBHotkeyBar_CheckedChanged);
            // 
            // rbBackground
            // 
            this.rbBackground.AutoSize = true;
            this.rbBackground.Checked = true;
            this.rbBackground.Location = new System.Drawing.Point(7, 19);
            this.rbBackground.Name = "rbBackground";
            this.rbBackground.Size = new System.Drawing.Size(120, 17);
            this.rbBackground.TabIndex = 36;
            this.rbBackground.TabStop = true;
            this.rbBackground.Text = "Ignore empty blocks";
            this.rbBackground.UseVisualStyleBackColor = true;
            this.rbBackground.CheckedChanged += new System.EventHandler(this.rbBackground_CheckedChanged);
            // 
            // rbEmpty
            // 
            this.rbEmpty.AutoSize = true;
            this.rbEmpty.Location = new System.Drawing.Point(7, 42);
            this.rbEmpty.Name = "rbEmpty";
            this.rbEmpty.Size = new System.Drawing.Size(109, 17);
            this.rbEmpty.TabIndex = 37;
            this.rbEmpty.Text = "Use empty blocks";
            this.rbEmpty.UseVisualStyleBackColor = true;
            this.rbEmpty.CheckedChanged += new System.EventHandler(this.rbEmpty_CheckedChanged);
            // 
            // gbSelectionTool
            // 
            this.gbSelectionTool.Controls.Add(this.rbBackground);
            this.gbSelectionTool.Controls.Add(this.rbEmpty);
            this.gbSelectionTool.Location = new System.Drawing.Point(11, 125);
            this.gbSelectionTool.Name = "gbSelectionTool";
            this.gbSelectionTool.Size = new System.Drawing.Size(176, 78);
            this.gbSelectionTool.TabIndex = 38;
            this.gbSelectionTool.TabStop = false;
            this.gbSelectionTool.Text = "Selection Tool";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 285);
            this.Controls.Add(this.gbSelectionTool);
            this.Controls.Add(this.cBHotkeyBar);
            this.Controls.Add(this.DarkThemeCheckBox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.FasterShapeStyleCheckBox);
            this.Controls.Add(this.usePenToolCheckBox);
            this.Controls.Add(this.selectAllBorderCheckBox);
            this.Controls.Add(this.confirmCloseCheckBox);
            this.Controls.Add(this.clearComboBox);
            this.Controls.Add(this.clearButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEOditor Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gbSelectionTool.ResumeLayout(false);
            this.gbSelectionTool.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox usePenToolCheckBox;
        private System.Windows.Forms.CheckBox selectAllBorderCheckBox;
        private System.Windows.Forms.ComboBox clearComboBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.CheckBox confirmCloseCheckBox;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusTextToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusToolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel StatusColorToolStripStatusLabel;
        private System.Windows.Forms.CheckBox FasterShapeStyleCheckBox;
        private System.Windows.Forms.CheckBox DarkThemeCheckBox;
        private System.Windows.Forms.CheckBox cBHotkeyBar;
        private System.Windows.Forms.RadioButton rbBackground;
        private System.Windows.Forms.RadioButton rbEmpty;
        private System.Windows.Forms.GroupBox gbSelectionTool;
    }
}