﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.MonthCalendar;

namespace EEditor
{
    public partial class WorldSettings : Form
    {
        public string title { get; set; }
        public string owner { get; set; }

        public string madeBy { get; set; }
        public bool useColor { get; set; }
        public string description { get; set; }
        public bool minimp { get; set; }

        public string crewName { get; set; }

        public string crewId { get; set; }

        public bool campaign { get; set; }

        public Color bgColor { get; set; }
        public WorldSettings()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            title = string.IsNullOrEmpty(tbtitle.Text) ? "Untitled World" : tbtitle.Text;
            owner = string.IsNullOrEmpty(tbowner.Text) ? "player" : tbowner.Text.ToLower();
            madeBy = txtbOwnerID.Text;
            minimp = cbMinimap.Checked;
            campaign = cbCampaign.Checked;
            crewId = txtbCrewID.Text;
            crewName = txtbCrewName.Text;
            description = string.IsNullOrEmpty(txtbDescr.Text) ? null : txtbDescr.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog dg = new ColorDialog();
            if (dg.ShowDialog() == DialogResult.OK)
            {
                useColor = true;
                bgColor = dg.Color;
                Bitmap bmp = new Bitmap(24, 24);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(bgColor);
                    gr.DrawRectangle(new Pen(MainForm.themecolors.foreground), new Rectangle(0, 0, 23, 23));
                }
                pictureBox1.Image = bmp;
            }
            else
            {
                useColor = false;
                bgColor = Color.Transparent;
                Bitmap bmp = new Bitmap(24, 24);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(bgColor);
                    gr.DrawImage(Properties.Resources.cross, 5, 5);
                    gr.DrawRectangle(new Pen(MainForm.themecolors.foreground), new Rectangle(0, 0, 23, 23));
                }
                pictureBox1.Image = bmp;
            }
        }

        private void WorldSettings_Load(object sender, EventArgs e)
        {
            tbtitle.Text = string.IsNullOrEmpty(MainForm.WOTitle) ? "Untitled World" : MainForm.WOTitle;
            tbowner.Text = string.IsNullOrEmpty(MainForm.WONickname) ? "player" : MainForm.WONickname.ToLower();
            txtbDescr.Text = string.IsNullOrEmpty(MainForm.WODescription) ? "" : MainForm.WODescription;
            tbtitle.Select(tbtitle.TextLength, tbtitle.TextLength - 1);
            cbMinimap.Checked = MainForm.WOMinimap;
            cbCampaign.Checked = MainForm.WOCampaign;
            txtbCrewID.Text = MainForm.WOCrewID;
            txtbCrewName.Text = MainForm.WOCrewName;
            txtbOwnerID.Text = MainForm.WOMade;

            if (MainForm.userdata.useColor)
            {
                Bitmap bmp = new Bitmap(24, 24);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(MainForm.userdata.thisColor);
                    gr.DrawRectangle(new Pen(MainForm.themecolors.foreground), new Rectangle(0, 0, 23, 23));
                }
                pictureBox1.Image = bmp;
            }
            else
            {
                Bitmap bmp = new Bitmap(24, 24);
                using (Graphics gr = Graphics.FromImage(bmp))
                {
                    gr.Clear(Color.Transparent);

                    gr.DrawImage(Properties.Resources.cross, 5, 5);
                    gr.DrawRectangle(new Pen(MainForm.themecolors.foreground), new Rectangle(0, 0, 23, 23));
                }
                pictureBox1.Image = bmp;
            }
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
                if (cntr.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)cntr).ForeColor = MainForm.themecolors.foreground;
                }
                if (cntr.GetType() == typeof(TextBox))
                {
                    cntr.ForeColor = MainForm.themecolors.foreground;
                    cntr.BackColor = MainForm.themecolors.accent;
                }
                if (cntr.GetType() == typeof(NumericUpDown))
                {
                    cntr.ForeColor = MainForm.themecolors.foreground;
                    cntr.BackColor = MainForm.themecolors.accent;
                }


            }
        }

        private void cbMinimap_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.WOMinimap = cbMinimap.Checked;
        }

        private void cbCampaign_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.WOCampaign = cbCampaign.Checked;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            cbMinimap.Checked = true;
            cbCampaign.Checked = false;
            txtbCrewID.Text = "";
            txtbCrewName.Text = "";
            txtbOwnerID.Text = "made offline";
            tbtitle.Text = "Untitled World";
            tbowner.Text = "player";
            txtbDescr.Text = "";
            useColor = false;
            bgColor = Color.Transparent;
            Bitmap bmp = new Bitmap(24, 24);
            using (Graphics gr = Graphics.FromImage(bmp))
            {

                gr.Clear(Color.Transparent);
                gr.DrawImage(Properties.Resources.cross, 5, 5);
                gr.DrawRectangle(new Pen(MainForm.themecolors.foreground), new Rectangle(0, 0, 23, 23));
            }
            pictureBox1.Image = bmp;
        }
    }
}
