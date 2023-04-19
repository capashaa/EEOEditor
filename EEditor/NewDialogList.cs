using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EEditor
{
    public partial class NewDialogList : Form
    {
        public string worldSize { get; set; }
        public NewDialogList()
        {
            InitializeComponent();
        }

        private void NewDialogList_Load(object sender, EventArgs e)
        {
            Gen("25x25", "Small");
            Gen("40x30", "Home");
            Gen("50x50", "Medium");
            Gen("100x100", "Large");
            Gen("100x400", "Tall");
            Gen("150x150", "Big");
            Gen("110x110", "Low Gravity");
            Gen("200x200", "Massive");
            Gen("200x400", "Vertical Great");
            Gen("300x300", "Huge");
            Gen("400x50", "Wide");
            Gen("400x200", "Great");
            Gen("636x50", "Ultra Wide");
            Gen("637x460", "Lictor special 1");
            Gen("460x637", "Lictor special 2");
            this.BackColor = MainForm.themecolors.background;
            this.ForeColor= MainForm.themecolors.foreground;
            Listviewlos.BackColor = MainForm.themecolors.background;
            Listviewlos.ForeColor= MainForm.themecolors.foreground;
        }
        private void Gen(string size,string name)
        {
            ListViewItem listItem = new ListViewItem();
            listItem.SubItems.Add(name);
            listItem.Text = size;
            Listviewlos.Items.Add(listItem);
          
        }


        private void NewDialogList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (worldSize != null && worldSize.Contains("x")) this.DialogResult = DialogResult.OK;
            
        }

        private void Listviewlos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Listviewlos.SelectedIndices.Count > 0)
            {

                worldSize = Listviewlos.SelectedItems[0].Text;
                this.Close();
            }
            
        }
    }
}
