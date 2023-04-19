using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlayerIOClient;
using System.Threading;

namespace EEditor
{
    public partial class NewDialogForm : Form
    {
        public int SizeWidth { get; private set; }
        public int SizeHeight { get; private set; }
        public Frame MapFrame { get; private set; }
        public bool RealTime { get; }
        public bool notsaved { get; set; }
        public MainForm MainForm { get; set; }
        public MainForm mainform { get; set; }
        private Dictionary<string, string> data = new Dictionary<string, string>();
        private Semaphore s = new Semaphore(0, 1);
        private Semaphore s1 = new Semaphore(0, 1);
        public NewDialogForm(MainForm mainForm)
        {
            InitializeComponent();
            MainForm = mainForm;
            mainform = mainForm;
            CheckForIllegalCrossThreadCalls = false;
            notsaved = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int w = Convert.ToInt32(nUWidth.Value);
            int h = Convert.ToInt32(nUHeight.Value);
            if (w <= 637 && h <= 460 || w <= 460 && h <= 637)
            {
                MainForm.SetPenTool();
                if (Clipboard.ContainsData("EEBrush")) Clipboard.Clear();
                ToolPen.undolist.Clear();
                ToolPen.redolist.Clear();
                ToolPen.rotation.Clear();
                //Clipboard.Clear();
                MainForm.tsc.Items.Clear();
                MainForm.tsc.Items.Add("Background");
                MainForm.tsc.Text = "Background";
                var title = tbtitle.Text;
                var name = tbowner.Text;
                if (string.IsNullOrEmpty(title)) title = "Untitled World";
                if (string.IsNullOrEmpty(name)) name = "player";

                MainForm.Text = $"({title}) [{name.ToLower()}] ({nUWidth.Value}x{nUHeight.Value}) - EEOditor {MainForm.ProductVersion}";
                #region Listbox selection
                SizeWidth = Convert.ToInt32(nUWidth.Value);
                SizeHeight = Convert.ToInt32(nUHeight.Value);
                MainForm.WONickname = name.ToLower();
                MainForm.WOTitle = title;
                MainForm.WOMade = rbEEOffline.Checked ? "made offline" : "Created by EEOditor";
                #endregion
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Can't load this world. It's too big.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog dg = new ColorDialog();
            if (dg.ShowDialog() == DialogResult.OK)
            {
                MainForm.userdata.useColor = true;
                MainForm.userdata.thisColor = dg.Color;
            }
            else
            {
                MainForm.userdata.useColor = false;
                MainForm.userdata.thisColor = Color.Transparent;
            }
        }


        private Color UIntToColor(uint color)
        {
            byte a = (byte)(color >> 24);
            byte r = (byte)(color >> 16);
            byte g = (byte)(color >> 8);
            byte b = (byte)(color >> 0);
            return Color.FromArgb(a, r, g, b);
        }


        private void NewDialogForm_Load(object sender, EventArgs e)
        {
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            foreach (Control cntr in this.Controls)
            {
                if (cntr.GetType() == typeof(Button))
                {
                    ((Button)cntr).ForeColor = MainForm.themecolors.foreground;
                    ((Button)cntr).BackColor = MainForm.themecolors.accent;
                    ((Button)cntr).FlatStyle = FlatStyle.Flat;
                }
                if (cntr.GetType() == typeof(GroupBox))
                {
                    ((GroupBox)cntr).ForeColor = MainForm.themecolors.groupbox;
                    foreach (Control cntrl in cntr.Controls)
                    {
                        if (cntrl.GetType() == typeof(Button))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.accent;
                            ((Button)cntrl).FlatStyle = FlatStyle.Flat;
                        }
                        if (cntrl.GetType() == typeof(TextBox))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.accent;
                        }
                        if (cntrl.GetType() == typeof(System.Windows.Forms.Label))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.background;
                        }
                        if (cntrl.GetType() == typeof(NumericUpDown))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.accent;
                        }
                        if (cntrl.GetType() == typeof(RadioButton))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.background;
                        }
                    }
                }

            }

            nUHeight.TextChanged += new EventHandler(nuBox_TextChanged);
            nUWidth.TextChanged += new EventHandler(nuBox_TextChanged);
        }
        private void nuBox_TextChanged(object sender, EventArgs e)
        {
            if (nUWidth.Value == 637 && nUHeight.Value == 460)
            {
                nUWidth.Maximum = 637;
                nUHeight.Maximum = 460;
            }
            else if (nUWidth.Value == 460 && nUHeight.Value == 637)
            {
                nUWidth.Maximum = 460;
                nUHeight.Maximum = 637;
            }
            else if (nUHeight.Value > 460 && nUWidth.Value == 637)
            {
                nUHeight.Maximum = 460;
            }
            else if (nUHeight.Value > 637 && nUWidth.Value == 460)
            {
                nUHeight.Maximum = 637;
            }
            else if (nUHeight.Value == 460 && nUWidth.Value > 637)
            {
                nUWidth.Maximum = 637;
            }
            else if (nUHeight.Value == 637 && nUWidth.Value > 460)
            {
                nUWidth.Maximum = 460;
            }
        }
        private void btnSizeList_Click(object sender, EventArgs e)
        {
            NewDialogList lst = new NewDialogList();
            if (lst.ShowDialog() == DialogResult.OK)
            {
                if (lst.worldSize != null)
                {
                    if (lst.worldSize.Contains("x"))
                    {
                        string[] split = lst.worldSize.Split('x');
                        if (split.Length == 2)
                        {
                            nUHeight.Maximum = 637;
                            nUWidth.Maximum = 637;
                            nUWidth.Value = Convert.ToDecimal(split[0]);
                            nUHeight.Value = Convert.ToDecimal(split[1]);
                        }
                    }
                }
            }
        }

        private void nUBox_ValueChanged(object sender, EventArgs e)
        {
            if (nUWidth.Value == 637 && nUHeight.Value == 460)
            {
                nUWidth.Maximum = 637;
                nUHeight.Maximum = 460;
            }
            else if (nUWidth.Value == 460 && nUHeight.Value == 637)
            {
                nUWidth.Maximum = 460;
                nUHeight.Maximum = 637;
            }
            else if (nUHeight.Value > 460 && nUWidth.Value == 637)
            {
                nUHeight.Maximum = 460;
            }
            else if (nUHeight.Value > 637 && nUWidth.Value == 460)
            {
                nUHeight.Maximum = 637;
            }
            else if (nUHeight.Value == 460 && nUWidth.Value > 637)
            {
                nUWidth.Maximum = 637;
            }
            else if (nUHeight.Value == 637 && nUWidth.Value > 460)
            {
                nUWidth.Maximum = 460;
            }
        }

    }
}
