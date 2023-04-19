using System;
using System.Windows.Forms;

namespace EEditor
{
    public partial class ToolpickerSettings : Form
    {
        public bool start = false;
        public string hex = null;
        public ToolpickerSettings()
        {
            InitializeComponent();
        }

        private void SettingsFGCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!start) MainForm.userdata.ColorFG = SettingsFGCheckBox.Checked;
        }

        private void SettingsBGCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!start) MainForm.userdata.ColorBG = SettingsBGCheckBox.Checked;
        }

        private void SelectColorButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtbHex.Text)) hex = txtbHex.Text;
        }

        private void ToolpickerSettings_Load(object sender, EventArgs e)
        {
            start = true;
            SettingsFGCheckBox.Checked = MainForm.userdata.ColorFG;
            SettingsBGCheckBox.Checked = MainForm.userdata.ColorBG;
            start = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
