﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace EEditor
{
    public partial class Replacer : Form
    {
        public NumericUpDown NU1 { get { return numericUpDown1; } set { numericUpDown1 = value; } }
        public NumericUpDown NU2 { get { return numericUpDown2; } set { numericUpDown2 = value; } }
        MainForm MainForm { get; set; }
        private int[,] replaced;
        private int[,] replaced1;
        private int[,] lastBlock;
        private int portalRotationFind = 0;
        private int portalRotationRepl = 0;
        Bitmap bmp = new Bitmap(1, 1);
        Bitmap img1;
        public Replacer(MainForm mainForm)
        {
            this.MainForm = mainForm;
            InitializeComponent();
        }

        private void Replacer_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(MainForm.editArea.Frames[0].Height, MainForm.editArea.Frames[0].Width);
            replaced = new int[MainForm.editArea.Frames[0].Height, MainForm.editArea.Frames[0].Width];
            replaced1 = new int[MainForm.editArea.Frames[0].Height, MainForm.editArea.Frames[0].Width];
            lastBlock = new int[MainForm.editArea.Frames[0].Height, MainForm.editArea.Frames[0].Width];
            MainForm.editArea.Back1 = MainForm.editArea.Back;
            numericUpDown1.Value = MainForm.editArea.Tool.PenID;
            ReplaceUnknownCheckBox.Checked = MainForm.userdata.replaceit;

            PortalRadioButton.Image = MainForm.miscBMD.Clone(new Rectangle(108 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            PortalRotFind.Image = MainForm.miscBMD.Clone(new Rectangle(108 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            PortalRotRepl.Image = MainForm.miscBMD.Clone(new Rectangle(108 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            PortalINVRadioButton.Image = MainForm.miscBMD.Clone(new Rectangle(112 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            WorldPortalRadioButton.Image = MainForm.miscBMD.Clone(new Rectangle(33 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            SignRadioButton.Image = MainForm.miscBMD.Clone(new Rectangle(255 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            LabelRadioButton.Image = MainForm.decosBMD.Clone(new Rectangle(176 * 16,0,16,16), MainForm.decosBMD.PixelFormat);
            if (ToolPen.rotation.ContainsKey(MainForm.editArea.Tool.PenID))
            {
                if (bdata.getRotation(MainForm.editArea.Tool.PenID, ToolPen.rotation[MainForm.editArea.Tool.PenID]) != null)
                {
                    RotationPictureBox1.Image = bdata.getRotation(MainForm.editArea.Tool.PenID, ToolPen.rotation[MainForm.editArea.Tool.PenID]);
                    findRotate.Value = ToolPen.rotation[MainForm.editArea.Tool.PenID];
                }
            }
            else
            {
                if (bdata.getRotation(MainForm.editArea.Tool.PenID, 0) != null)
                {
                    RotationPictureBox1.Image = bdata.getRotation(MainForm.editArea.Tool.PenID, 0);
                }
            }
            replaceRotate.Value = 1;
            numericUpDown2.Value = 0;
            //if (bdata.getRotation(numericUpDown1.Value, MainForm.editArea.Frames[0].BlockData[yy, xx]) != null) MainForm.editArea.Back = bdata.getRotation(MainForm.editArea.Frames[0].Foreground[yy, xx], MainForm.editArea.Frames[0].BlockData[yy, xx]);
            Bitmap img2 = MainForm.foregroundBMD.Clone(new Rectangle(0 * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
            ReplacePictureBox.Image = img2;

            ToolTip tp = new ToolTip();
            tp.SetToolTip(button4, "Finds the next block with ID " + numericUpDown1.Value + "\nand displays a red rectangle around it");
            tp.SetToolTip(button3, "Finds all blocks with ID " + numericUpDown1.Value + "\nand displays a red rectangle around them");
            tp.SetToolTip(button5, "Finds the next block with ID " + numericUpDown1.Value + "\nand replaces it with block ID " + numericUpDown2.Value);
            tp.SetToolTip(button1, "Finds all blocks with ID " + numericUpDown1.Value + "\nand replaces them with block ID " + numericUpDown2.Value);
            tp.SetToolTip(button8, "Removes all red rectangles around blocks");
            tp.SetToolTip(button9, "Finds all blocks you don't own and replaces them with block ID " + numericUpDown2.Value);
            tp.SetToolTip(rotateIcon1, "Rotation or count"); tp.SetToolTip(rotateIcon2, "Rotation or count"); tp.SetToolTip(findRotate, "Rotation or count"); tp.SetToolTip(replaceRotate, "Rotation or count");
            tp.SetToolTip(rbSpikes, "Replace any rotation of spikes to another color.");
            tp.SetToolTip(toolStripContainer1.ContentPanel, "Left click: insert ID to find box\nRight click: insert ID to replace box"); // Would be more useful if the tooltip was added to single blocks
            tp.SetToolTip(ClearBgsButton, "Clear background blocks that is behind normal blocks");

            this.Text = MainForm.debug ? "Find & replace - Debug Activated" : "Find & replace";

            for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
            {
                for (int x = 0; x < MainForm.editArea.Frames[0].Width; x++)
                {
                    lastBlock[y, x] = -1;
                }
            }
            this.ForeColor = MainForm.themecolors.foreground;
            this.BackColor = MainForm.themecolors.background;
            foreach (Control cntr in this.Controls)
            {
                if (cntr.GetType() == typeof(TabControl))
                {
                    TabControl.TabPageCollection tabs = ((TabControl)cntr).TabPages;
                    for (int i = 0; i < tabs.Count; i++)
                    {
                        tabs[i].UseVisualStyleBackColor = true;
                        tabs[i].ForeColor = MainForm.themecolors.foreground;
                        tabs[i].BackColor = MainForm.themecolors.accent;
                        foreach (Control cnt in tabs[i].Controls)
                        {
                            if (cnt.GetType() == typeof(NumericUpDown))
                            {
                                cnt.ForeColor = MainForm.themecolors.foreground;
                                cnt.BackColor = MainForm.themecolors.accent;
                            }
                            if (cnt.GetType() == typeof(Button))
                            {
                                cnt.ForeColor = MainForm.themecolors.foreground;
                                cnt.BackColor = MainForm.themecolors.accent;
                                ((Button)cnt).FlatStyle = FlatStyle.Flat;
                            }
                            if (cnt.GetType() == typeof(TextBox))
                            {
                                cnt.ForeColor = MainForm.themecolors.foreground;
                                cnt.BackColor = MainForm.themecolors.accent;
                            }
                            if (cnt.GetType() == typeof(PictureBox))
                            {
                                if (cnt.Name.StartsWith("rotateIcon"))
                                {
                                    Bitmap bmp = new Bitmap(((PictureBox)cnt).Image);
                                    Bitmap bmp1 = new Bitmap(((PictureBox)cnt).Image.Width, ((PictureBox)cnt).Image.Height);
                                    for (int x = 0; x < ((PictureBox)cnt).Image.Width; x++)
                                    {
                                        for (int y = 0; y < ((PictureBox)cnt).Image.Height; y++)
                                        {
                                            if (bmp.GetPixel(x, y).A > 80)
                                            {
                                                bmp1.SetPixel(x, y, MainForm.themecolors.imageColors);
                                            }
                                            else
                                            {
                                                bmp1.SetPixel(x, y, Color.Transparent);
                                            }
                                        }
                                    }
                                ((PictureBox)cnt).Image = bmp1;
                                }
                            }
                            if (cnt.GetType() == typeof(GroupBox))
                            {
                                cnt.ForeColor = MainForm.themecolors.foreground;
                                cnt.BackColor = MainForm.themecolors.background;
                                foreach (Control cn in cnt.Controls)
                                {
                                    if (cn.GetType() == typeof(Button))
                                    {
                                        cn.ForeColor = MainForm.themecolors.foreground;
                                        cn.BackColor = MainForm.themecolors.accent;
                                        ((Button)cn).FlatStyle = FlatStyle.Flat;
                                        

                                    }
                                    if (cn.GetType() == typeof(TextBox))
                                    {
                                        cn.ForeColor = MainForm.themecolors.foreground;
                                        cn.BackColor = MainForm.themecolors.accent;
                                    }
                                }
                            }
                        }
                    }
                }
                if (cntr.GetType() == typeof(GroupBox))
                {
                    cntr.ForeColor = MainForm.themecolors.foreground;
                    cntr.BackColor = MainForm.themecolors.background;
                    foreach (Control cntrl in cntr.Controls)
                    {
                        if (cntrl.GetType() == typeof(Button))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.accent;
                            ((Button)cntrl).FlatStyle = FlatStyle.Flat;
                            if (cntrl.Name == "button1" || cntrl.Name == "button3" || cntrl.Name == "button5" || cntrl.Name == "button4")
                            {
                                Bitmap bmp = new Bitmap(((Button)cntrl).Image);
                                Bitmap bmp1 = new Bitmap(((Button)cntrl).Image.Width, ((Button)cntrl).Image.Height);
                                for (int x = 0; x < ((Button)cntrl).Image.Width; x++)
                                {
                                    for (int y = 0; y < ((Button)cntrl).Image.Height; y++)
                                    {
                                        if (bmp.GetPixel(x, y).A > 80)
                                        {
                                            bmp1.SetPixel(x, y, MainForm.themecolors.imageColors);
                                        }
                                        else
                                        {
                                            bmp1.SetPixel(x, y, Color.Transparent);
                                        }
                                    }
                                }
                            ((Button)cntrl).Image = bmp1;
                            }
                        }
                        if (cntrl.GetType() == typeof(TextBox))
                        {
                            cntrl.ForeColor = MainForm.themecolors.foreground;
                            cntrl.BackColor = MainForm.themecolors.accent;
                        }
                    }
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.SetToolTip(button4, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around it");
            tp.SetToolTip(button3, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around them");
            tp.SetToolTip(button5, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd replaces it with block ID " + numericUpDown2.Value);
            tp.SetToolTip(button1, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd replaces them with block ID " + numericUpDown2.Value);
            if (numericUpDown1.Value < 500 || numericUpDown1.Value >= 1001 && numericUpDown1.Value < 3000)
            {
                if (MainForm.decosBMI[(int)numericUpDown1.Value] != 0)
                {
                    Bitmap img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                    FindPictureBox.Image = img1;
                    button4.Enabled = true;
                    button3.Enabled = true;
                    findRotate.Value = 1;
                    findRotation();
                }
                else if (MainForm.miscBMI[(int)numericUpDown1.Value] != 0 || (int)numericUpDown1.Value == 119)
                {

                    Bitmap img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                    FindPictureBox.Image = img1;
                    button4.Enabled = true;
                    button3.Enabled = true;
                    findRotate.Value = 1;
                    findRotation();
                }
                else if (MainForm.foregroundBMI[(int)numericUpDown1.Value] != 0 || (int)numericUpDown1.Value == 0)
                {
                    Bitmap img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                    FindPictureBox.Image = img1;
                    numericUpDown1.ForeColor = MainForm.themecolors.foreground;
                    button4.Enabled = true;
                    button3.Enabled = true;
                    findRotate.Value = 1;
                    findRotation();
                }
                else
                {
                    Bitmap temp = new Bitmap(16, 16);
                    Graphics gr = Graphics.FromImage(temp);
                    gr.Clear(Color.Black);
                    gr.DrawImage(Properties.Resources.unknown.Clone(new Rectangle(1 * 16, 0, 16, 16), Properties.Resources.unknown.PixelFormat), 0, 0);
                    FindPictureBox.Image = temp;
                }
            }
            else if (numericUpDown1.Value >= 500 && numericUpDown1.Value <= 999)
            {
                if (MainForm.backgroundBMI[(int)numericUpDown1.Value] != 0 || (int)numericUpDown1.Value == 500)
                {
                    Bitmap img7 = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                    FindPictureBox.Image = img7;
                    numericUpDown1.ForeColor = MainForm.themecolors.foreground;
                    button4.Enabled = true;
                    button3.Enabled = true;
                }
                else
                {
                    Bitmap temp = new Bitmap(16, 16);
                    Graphics gr = Graphics.FromImage(temp);
                    gr.Clear(Color.Black);
                    gr.DrawImage(Properties.Resources.unknown.Clone(new Rectangle(2 * 16, 0, 16, 16), Properties.Resources.unknown.PixelFormat), 0, 0);
                    FindPictureBox.Image = temp;

                }
            }

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.SetToolTip(button4, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around it");
            tp.SetToolTip(button3, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around them");
            tp.SetToolTip(button5, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd replaces it with block ID " + numericUpDown2.Value);
            tp.SetToolTip(button1, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd replaces them with block ID " + numericUpDown2.Value);
            if (numericUpDown2.Value < 500 || numericUpDown2.Value >= 1001 && numericUpDown2.Value < 3000)
            {
                if (MainForm.decosBMI[(int)numericUpDown2.Value] != 0)
                {
                    //if (MainForm.userdata.replaceit)
                    //{
                    Bitmap img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                    ReplacePictureBox.Image = img1;
                    numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                    label4.Text = null;
                    button5.Enabled = true;
                    button1.Enabled = true;
                    replaceRotate.Value = 1;
                    replaceRotating();
                    //}
                }

                else if (MainForm.miscBMI[(int)numericUpDown2.Value] != 0 || (int)numericUpDown2.Value == 119)
                {
                    //if (MainForm.userdata.replaceit)
                    //{
                    Bitmap img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                    ReplacePictureBox.Image = img1;
                    numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                    label4.Text = null;
                    button5.Enabled = true;
                    button1.Enabled = true;
                    replaceRotate.Value = 1;
                    replaceRotating();
                    // }
                }
                else
                {
                    // if (MainForm.userdata.replaceit)
                    //  {
                    Bitmap img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                    ReplacePictureBox.Image = img1;
                    numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                    label4.Text = null;
                    button5.Enabled = true;
                    button1.Enabled = true;
                    replaceRotate.Value = 1;
                    //  }

                }
            }

            else if (numericUpDown2.Value >= 500 && numericUpDown2.Value <= 999)
            {
                //if (MainForm.userdata.replaceit)
                //{
                Bitmap img6 = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                ReplacePictureBox.Image = img6;
                numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                label4.Text = null;
                button5.Enabled = true;
                button1.Enabled = true;
                //}
            }
        }

        private void numericUpDown2_KeyUp(object sender, KeyEventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.SetToolTip(button4, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around it");
            tp.SetToolTip(button3, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around them");
            tp.SetToolTip(button5, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd replaces it with block ID " + numericUpDown2.Value);
            tp.SetToolTip(button1, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd replaces them with block ID " + numericUpDown2.Value);
            if (numericUpDown2.Value < 500 || numericUpDown2.Value >= 1001 && numericUpDown2.Value < 3000)
            {
                if (MainForm.decosBMI[(int)numericUpDown2.Value] != 0)
                {
                    if (MainForm.userdata.replaceit)
                    {
                        Bitmap img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                        ReplacePictureBox.Image = img1;
                        button5.Enabled = true;
                        button1.Enabled = true;
                        label4.Text = null;
                        numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                    }
                }
                else if (MainForm.miscBMI[(int)numericUpDown2.Value] != 0 || numericUpDown2.Value == 119)
                {
                    if (MainForm.userdata.replaceit)
                    {
                        Bitmap img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                        ReplacePictureBox.Image = img1;
                        button5.Enabled = true;
                        button1.Enabled = true;
                        label4.Text = null;
                        numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                    }
                }
                else
                {
                    if (MainForm.userdata.replaceit)
                    {
                        Bitmap img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                        ReplacePictureBox.Image = img1;
                        button5.Enabled = true;
                        button1.Enabled = true;
                        label4.Text = null;
                        numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                    }
                }
            }
            else if (numericUpDown2.Value >= 500 && numericUpDown2.Value <= 999)
            {
                if (MainForm.userdata.replaceit)
                {
                    Bitmap img2 = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[Convert.ToInt32(numericUpDown2.Value)] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                    ReplacePictureBox.Image = img2;
                    button5.Enabled = true;
                    button1.Enabled = true;
                    label4.Text = null;
                    numericUpDown2.ForeColor = MainForm.themecolors.foreground;
                }
            }
        }
        private async void GrabTotalBlocks(int layer)
        {
            await Task.Run(() =>
            {
                int total = 0;
                for (int x = 0; x < MainForm.editArea.CurFrame.Width; x++)
                {
                    for (int y = 0; y < MainForm.editArea.CurFrame.Height; y++)
                    {
                        if (layer == 0)
                        {
                            if (MainForm.editArea.CurFrame.Foreground[y, x] == numericUpDown1.Value)
                            {
                                total += 1;
                            }
                        }
                        else
                        {
                            if (MainForm.editArea.CurFrame.Background[x, y] == numericUpDown1.Value)
                            {
                                total += 1;
                            }
                        }
                    }
                }
                if (label4.InvokeRequired)
                {
                    MainForm.editArea.Invoke((MethodInvoker)delegate
                    {
                        label4.Text = "The world has " + total + " blocks of ID " + numericUpDown1.Value + ".";
                    });
                }
            });
        }
        private void numericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            ToolTip tp = new ToolTip();
            tp.SetToolTip(button4, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around it");
            tp.SetToolTip(button3, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around them");
            tp.SetToolTip(button5, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd replaces it with block ID " + numericUpDown2.Value);
            tp.SetToolTip(button1, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd replaces them with block ID " + numericUpDown2.Value);
            if (numericUpDown1.Value < 500 || numericUpDown1.Value >= 1001 && numericUpDown1.Value < 3000)
            {
                if (MainForm.decosBMI[(int)numericUpDown1.Value] != 0)
                {
                    Bitmap img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                    FindPictureBox.Image = img1;
                }
                else if (MainForm.miscBMI[(int)numericUpDown1.Value] != 0 || (int)numericUpDown1.Value == 119)
                {
                    Bitmap img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                    FindPictureBox.Image = img1;
                }
                else if (MainForm.foregroundBMI[(int)numericUpDown1.Value] != 0 || numericUpDown1.Value == 0)
                {
                    Bitmap img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                    FindPictureBox.Image = img1;
                    numericUpDown1.ForeColor = MainForm.themecolors.foreground;
                }
                else
                {
                    Bitmap temp = new Bitmap(16, 16);
                    Graphics gr = Graphics.FromImage(temp);
                    gr.Clear(Color.Transparent);
                    gr.DrawImage(Properties.Resources.unknown.Clone(new Rectangle(1 * 16, 0, 16, 16), Properties.Resources.unknown.PixelFormat), 0, 0);
                    FindPictureBox.Image = temp;
                }
                GrabTotalBlocks(0);
            }
            else if (numericUpDown1.Value >= 500 && numericUpDown1.Value <= 999)
            {
                if (MainForm.backgroundBMI[Convert.ToInt32(numericUpDown1.Value)] != 0 || Convert.ToInt32(numericUpDown1.Value) == 500)
                {
                    Bitmap img4 = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[Convert.ToInt32(numericUpDown1.Value)] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                    FindPictureBox.Image = img4;
                    GrabTotalBlocks(1);
                }
                else
                {
                    Bitmap temp = new Bitmap(16, 16);
                    Graphics gr = Graphics.FromImage(temp);
                    gr.Clear(Color.Black);
                    gr.DrawImage(Properties.Resources.unknown.Clone(new Rectangle(2 * 16, 0, 16, 16), Properties.Resources.unknown.PixelFormat), 0, 0);
                    FindPictureBox.Image = temp;
                    GrabTotalBlocks(1);

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            toolStrip1.Items.Clear();
            toolStripContainer1.ContentPanel.Controls.Clear();
            if (!string.IsNullOrWhiteSpace(textBox1.Text))
            {

                for (int i = 0; i < MainForm.blocksdb.Count; i++)
                {
                    if (MainForm.blocksdb[i].name.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        int[] blocks = MainForm.blocksdb[i].blocks;
                        ToolStrip strip = new ToolStrip();
                        strip.Name = MainForm.blocksdb[i].name + i;
                        strip.GripStyle = ToolStripGripStyle.Hidden;
                        if (MainForm.userdata.darkTheme) strip.Renderer = new DarkTheme();
                        if (!MainForm.userdata.darkTheme) strip.Renderer = new LightTheme();
                        toolStripContainer1.ContentPanel.Controls.Add(strip);
                        for (int a = 0; a < blocks.Length; a++)
                        {
                            int bid = blocks[a];
                            if (bid < 500 || bid >= 1001)
                            {
                                if (MainForm.decosBMI[bid] != 0)
                                {
                                    img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[bid] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                                }
                                else if (MainForm.miscBMI[bid] != 0 || bid == 119)
                                {
                                    img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[bid] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                                }
                                else
                                {
                                    img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[bid] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                                }
                            }
                            else if (bid >= 500 && bid <= 999)
                            {
                                if (MainForm.backgroundBMI[bid] != 0 || bid == 500)
                                {
                                    img1 = MainForm.backgroundBMD.Clone(new Rectangle(MainForm.backgroundBMI[bid] * 16, 0, 16, 16), MainForm.backgroundBMD.PixelFormat);
                                }
                            }
                            ToolStripButton bt = new ToolStripButton();
                            bt.Image = img1;
                            bt.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                            bt.Name = blocks[a].ToString();
                            bt.MouseDown += Bt_MouseDown;
                            bt.ToolTipText = "Left click: insert ID to find box\nRight click: insert ID to replace box";
                            strip.Items.Add(bt);
                            strip.BackColor = MainForm.themecolors.accent;
                            strip.ForeColor = MainForm.themecolors.foreground;

                        }
                    }
                }
            }
        }

        private void Bt_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ToolStripButton der = (ToolStripButton)sender;
                numericUpDown2.Value = Convert.ToInt32(der.Name);
                if (!rbSpikes.Checked) replaceRotate.Value = 1;

            }
            else if (e.Button == MouseButtons.Left)
            {
                ToolStripButton der = (ToolStripButton)sender;
                numericUpDown1.Value = Convert.ToInt32(der.Name);
                if (!rbSpikes.Checked) findRotate.Value = 1;
            }
            ToolTip tp = new ToolTip();
            tp.SetToolTip(button4, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around it");
            tp.SetToolTip(button3, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd displays a red rectangle around them");
            tp.SetToolTip(button5, "Finds the next block with ID " + numericUpDown1.Value + "\nAnd replaces it with block ID " + numericUpDown2.Value);
            tp.SetToolTip(button1, "Finds all blocks with ID " + numericUpDown1.Value + "\nAnd replaces them with block ID " + numericUpDown2.Value);
        }


        private void button5_Click(object sender, EventArgs e)
        {
            int first = 0;
            for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
            {
                for (int x = 0; x < MainForm.editArea.Frames[0].Width; x++)
                {
                    replaced[y, x] = 0;
                    replaced1[y, x] = 0;
                    if (lastBlock[y, x] == numericUpDown1.Value)
                    {
                        first += 1;
                        if (first == 1)
                        {

                            Point p = new Point(x * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.X), y * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.Y));
                            MainForm.editArea.Frames[0].Foreground[y, x] = (int)numericUpDown2.Value;
                            MainForm.editArea.Draw(x, y, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);
                            MainForm.editArea.Invalidate();
                            label4.Text = "Finished replacing block ID " + numericUpDown1.Value + " with " + numericUpDown2.Value + "."; //Checks for last row, not last block unfortunately
                            lastBlock[y, x] = -1;
                            break;
                        }
                    }
                }
            }
            MainForm.editArea.Back1 = MainForm.editArea.Back;
            MainForm.editArea.Invalidate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Replacing();
        }
        private void Replacing()
        {

            int incr = 0;
            int total = MainForm.editArea.CurFrame.Height - 1;

            for (int yy = 0; yy < MainForm.editArea.Frames[0].Height; yy++)
            {
                for (int xx = 0; xx < MainForm.editArea.Frames[0].Width; xx++)
                {
                    if ((numericUpDown1.Value < 500 || numericUpDown1.Value >= 1001) && (numericUpDown2.Value < 500 || numericUpDown2.Value >= 1001))
                    {
                        if (MainForm.editArea.Frames[0].Foreground[yy, xx] == numericUpDown1.Value)
                        {

                            if (numericUpDown1.Value == numericUpDown2.Value)
                            {

                                if (SignRadioButton.Checked || WorldPortalRadioButton.Checked)
                                {
                                    if (MainForm.editArea.Frames[0].BlockData3[yy, xx] == FindTextBox.Text)
                                    {
                                        if (MainForm.editArea.Frames[0].Foreground[yy, xx] == 385 || MainForm.editArea.Frames[0].Foreground[yy, xx] == 374)
                                        {

                                            MainForm.editArea.Frames[0].BlockData3[yy, xx] = ReplaceTextBox.Text;
                                            MainForm.editArea.Frames[0].BlockData[yy, xx] = Convert.ToInt32(replaceRotate.Value);
                                            MainForm.editArea.Frames[0].Foreground[yy, xx] = (int)numericUpDown2.Value;

                                        }
                                    }
                                }
                                else if (NormalRadioButton.Checked && findRotate.Value != replaceRotate.Value)
                                {

                                    MainForm.editArea.Frames[0].BlockData[yy, xx] = Convert.ToInt32(replaceRotate.Value);
                                    MainForm.editArea.Frames[0].Foreground[yy, xx] = (int)numericUpDown2.Value;
                                    //ToolPen.rotation[(int)numericUpDown2.Value] = Convert.ToInt32(replaceRotate.Value);


                                }
                                else if (PortalRadioButton.Checked || PortalINVRadioButton.Checked)
                                {
                                    if (MainForm.editArea.Frames[0].BlockData[yy, xx] == portalRotationFind && MainForm.editArea.Frames[0].BlockData1[yy, xx] == Convert.ToInt32(PortalFindFrom.Value) && MainForm.editArea.Frames[0].BlockData2[yy, xx] == Convert.ToInt32(PortalFindTo.Value))
                                    {
                                        MainForm.editArea.Frames[0].BlockData[yy, xx] = portalRotationRepl;
                                        MainForm.editArea.Frames[0].BlockData1[yy, xx] = Convert.ToInt32(PortalReplaceFrom.Value);
                                        MainForm.editArea.Frames[0].BlockData2[yy, xx] = Convert.ToInt32(PortalReplaceTo.Value);
                                        if (PortalRadioButton.Checked) MainForm.editArea.Frames[0].Foreground[yy, xx] = 242;
                                        else MainForm.editArea.Frames[0].Foreground[yy, xx] = 381;
                                    }
                                }


                            }
                            else
                            {

                                if (rbSpikes.Checked)
                                {
                                    if (numericUpDown1.Value == 361 || numericUpDown1.Value == 1625 || numericUpDown1.Value == 1627 || numericUpDown1.Value == 1629 || numericUpDown1.Value == 1631 || numericUpDown1.Value == 1633 || numericUpDown1.Value == 1635)
                                    {
                                        if (numericUpDown2.Value == 361 || numericUpDown2.Value == 1625 || numericUpDown2.Value == 1627 || numericUpDown2.Value == 1629 || numericUpDown2.Value == 1631 || numericUpDown2.Value == 1633 || numericUpDown2.Value == 1635)
                                        {
                                            MainForm.editArea.Frames[0].Foreground[yy, xx] = (int)numericUpDown2.Value;
                                        }
                                    }
                                }
                                if (SignRadioButton.Checked || WorldPortalRadioButton.Checked)
                                {
                                    if (MainForm.editArea.Frames[0].Foreground[yy, xx] == 385 || MainForm.editArea.Frames[0].Foreground[yy, xx] == 374)
                                    {
                                        MainForm.editArea.Frames[0].Foreground[yy, xx] = (int)numericUpDown2.Value;
                                        MainForm.editArea.Frames[0].BlockData3[yy, xx] = ReplaceTextBox.Text;
                                        MainForm.editArea.Frames[0].BlockData[yy, xx] = Convert.ToInt32(replaceRotate.Value);
                                    }
                                }
                                else if (NormalRadioButton.Checked && findRotate.Value != replaceRotate.Value)
                                {
                                    MainForm.editArea.Frames[0].Foreground[yy, xx] = (int)numericUpDown2.Value;
                                    MainForm.editArea.Frames[0].BlockData[yy, xx] = Convert.ToInt32(replaceRotate.Value);
                                    //ToolPen.rotation[(int)numericUpDown2.Value] = Convert.ToInt32(replaceRotate.Value);
                                }
                                else if (PortalRadioButton.Checked || PortalINVRadioButton.Checked)
                                {
                                    if (MainForm.editArea.Frames[0].BlockData[yy, xx] == portalRotationFind && MainForm.editArea.Frames[0].BlockData1[yy, xx] == Convert.ToInt32(PortalFindFrom.Value) && MainForm.editArea.Frames[0].BlockData2[yy, xx] == Convert.ToInt32(PortalFindTo.Value))
                                    {
                                        MainForm.editArea.Frames[0].BlockData[yy, xx] = portalRotationRepl;
                                        MainForm.editArea.Frames[0].BlockData1[yy, xx] = Convert.ToInt32(PortalReplaceFrom.Value);
                                        MainForm.editArea.Frames[0].BlockData2[yy, xx] = Convert.ToInt32(PortalReplaceTo.Value);
                                        MainForm.editArea.Frames[0].Foreground[yy, xx] = Convert.ToInt32(numericUpDown2.Value);
                                    }
                                }


                            }
                            //incfg += (int)rp.NU2.Value + ":" + editArea.CurFrame.Foreground[yy, xx] + ":" + xx + ":" + yy + ":";
                            if (NormalRadioButton.Checked)
                            {
                                if (findRotate.Value == replaceRotate.Value)
                                {
                                    MainForm.editArea.Frames[0].Foreground[yy, xx] = (int)numericUpDown2.Value;
                                }
                            }

                            if (MainForm.editArea.InvokeRequired)
                            {
                                MainForm.editArea.Invoke((MethodInvoker)delegate
                                {
                                    MainForm.editArea.Draw(xx, yy, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);
                                });
                            }
                            else
                            {
                                MainForm.editArea.Draw(xx, yy, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);

                            }



                        }
                    }
                    if ((numericUpDown1.Value >= 500 && numericUpDown1.Value < 1000) && (numericUpDown2.Value >= 500 && numericUpDown2.Value < 1000) || numericUpDown1.Value == 0)
                    {
                        if (MainForm.editArea.Frames[0].Background[yy, xx] == numericUpDown1.Value || numericUpDown1.Value == 0)
                        {

                            if (numericUpDown1.Value != numericUpDown2.Value)
                            {
                                MainForm.editArea.Frames[0].Background[yy, xx] = (int)numericUpDown2.Value;
                                if (MainForm.editArea.InvokeRequired)
                                {
                                    MainForm.editArea.Invoke((MethodInvoker)delegate
                                    {
                                        MainForm.editArea.Draw(xx, yy, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);
                                    });
                                }
                                else
                                {
                                    MainForm.editArea.Draw(xx, yy, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);

                                }
                            }
                        }
                    }

                    Point p = new Point(xx * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.X), yy * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.Y));
                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke((MethodInvoker)delegate
                        {
                            double db = ((double)incr / total) * 100;
                            int value = Convert.ToInt32(db) > 100 ? 100 : Convert.ToInt32(db);
                            progressBar1.Value = value;
                        });
                    }
                    else
                    {
                        double db = ((double)incr / total) * 100;
                        int value = Convert.ToInt32(db) > 100 ? 100 : Convert.ToInt32(db);
                        progressBar1.Value = value;
                    }
                    incr += 1;



                }
            }
            MainForm.editArea.Invalidate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int first = 0;
            //int first1 = 0;
            for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
            {
                for (int x = 0; x < MainForm.editArea.Frames[0].Width; x++)
                {
                    if (MainForm.editArea.Frames[0].Foreground[y, x] == numericUpDown1.Value && replaced[y, x] == 0)
                    {
                        first += 1;
                        if (first == 1)
                        {

                            lastBlock[y, x] = MainForm.editArea.Frames[0].Foreground[y, x];
                            Point p = new Point(x * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.X), y * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.Y));
                            Graphics gr = Graphics.FromImage(MainForm.editArea.Back1);
                            gr.DrawRectangle(new Pen(Color.Red), new Rectangle(x * 16, y * 16, 15, 15));
                            replaced[y, x] = 1;


                            MainForm.editArea.AutoScrollPosition = new Point(x * 16, y * 16);
                            break;
                        }
                    }
                    if (replaced[y, x] == 1)
                    {
                        Bitmap img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[MainForm.editArea.Frames[0].Foreground[y, x]] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                        Graphics gr = Graphics.FromImage(MainForm.editArea.Back1);
                        gr.DrawImage(img1, new Point(x * 16, y * 16));
                        lastBlock[y, x] = -1;
                        //gr.DrawRectangle(new Pen(Color.FromArgb(255, 0, 0, 0)), new Rectangle(x * 16, y * 16, 16, 16));
                        //gr.FillRectangle(new SolidBrush(Color.Transparent), new Rectangle(x * 16, y * 16, 16, 16));
                    }
                }
            }
            MainForm.editArea.Invalidate();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MainForm.editArea.Back1 = null;
            for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
            {
                for (int x = 0; x < MainForm.editArea.Frames[0].Width; x++)
                {

                    replaced[y, x] = 0;
                    replaced1[y, x] = 0;
                    MainForm.editArea.Draw(x, y, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);

                }
            }
            MainForm.editArea.Invalidate();
            MainForm.editArea.Back1 = MainForm.editArea.Back;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
            {
                for (int x = 0; x < MainForm.editArea.Frames[0].Width; x++)
                {
                    if (MainForm.editArea.Frames[0].Foreground[y, x] == numericUpDown1.Value && replaced1[y, x] == 0)
                    {


                        Point p = new Point(x * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.X), y * 16 - Math.Abs(MainForm.editArea.AutoScrollPosition.Y));
                        Graphics gr = Graphics.FromImage(MainForm.editArea.Back1);
                        gr.DrawRectangle(new Pen(Color.Red), new Rectangle(x * 16, y * 16, 15, 15));
                        replaced1[y, x] = 1;
                    }
                }
            }
            MainForm.editArea.Invalidate();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button2.PerformClick();
            }
        }

        private void Replacer_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm.editArea.Back1 = null;
            RemoveSearchBlocks();
            MainForm.editArea.Back1 = MainForm.editArea.Back;
        }
        private async void RemoveSearchBlocks()
        {
            await Task.Run(() =>
            {
                for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
                {
                    for (int x = 0; x < MainForm.editArea.Frames[0].Width; x++)
                    {
                        replaced[y, x] = 0;
                        replaced1[y, x] = 0;
                        MainForm.editArea.Draw(x, y, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);
                        MainForm.editArea.Invalidate();

                    }
                }
            });
        }

        private void findRotate_ValueChanged(object sender, EventArgs e)
        {
            findRotation();

        }
        private void findRotation()
        {
            if (!rbSpikes.Checked)
            {
                var bid = (int)numericUpDown1.Value;
                if (bid < 500 || bid >= 1001)
                {

                    if (MainForm.decosBMI[bid] != 0)
                    {
                        img1 = bdata.getRotation(bid, Convert.ToInt32(findRotate.Value));
                    }
                    else if (MainForm.miscBMI[bid] != 0)
                    {
                        img1 = bdata.getRotation(bid, Convert.ToInt32(findRotate.Value));
                    }
                    if (img1 != null) RotationPictureBox1.Image = img1;
                    else RotationPictureBox1.Image = Properties.Resources.cross;
                }
            }
            else
            {
                RotationPictureBox1.Image = Properties.Resources.cross;
            }
        }
        private void ClearBgsButton_Click(object sender, EventArgs e)
        {
            removeBgBehindBlocks();
        }

        private async void removeBgBehindBlocks()
        {
            await Task.Run(() =>
            {
                int total1 = MainForm.editArea.Frames[0].Height - 1;
                int incr = 0;
                int totalReplaced = 0;
                for (int y = 0; y < MainForm.editArea.Frames[0].Height; y++)
                {
                    for (int x = 0; x < MainForm.editArea.Frames[0].Width; x++)
                    {
                        int bid = MainForm.editArea.Frames[0].Background[y, x];
                        int fid = MainForm.editArea.Frames[0].Foreground[y, x];
                        if (bid != 0 && fid != 0)
                        {
                            if (MainForm.userdata.IgnoreBlocks != null)
                            {
                                if (!MainForm.userdata.IgnoreBlocks.Contains(fid) && MainForm.decosBMI[fid] == 0 && MainForm.miscBMI[fid] == 0)
                                {
                                    MainForm.editArea.Frames[0].Background[y, x] = 0;
                                    MainForm.editArea.Draw(x, y, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);
                                    MainForm.editArea.Invalidate();
                                    totalReplaced += 1;
                                }
                            }
                            else
                            {
                                MainForm.editArea.Frames[0].Background[y, x] = 0;
                                MainForm.editArea.Draw(x, y, Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);
                                MainForm.editArea.Invalidate();
                                totalReplaced += 1;
                            }
                        }
                    }
                    if (progressBar1.InvokeRequired)
                    {
                        progressBar1.Invoke((MethodInvoker)delegate
                        {
                            double db = ((double)incr / total1) * 100;
                            progressBar1.Value = Convert.ToInt32(db);
                        });
                    }
                    if (!progressBar1.InvokeRequired)
                    {
                        double db = ((double)incr / total1) * 100;
                        progressBar1.Value = Convert.ToInt32(db);
                    }
                    incr += 1;

                }
                if (label4.InvokeRequired)
                {
                    label4.Invoke((MethodInvoker)delegate
                    {
                        label4.Text = "Replaced " + totalReplaced + " backgrounds behind blocks.";
                    });
                }
                else
                {
                    label4.Text = "Replaced " + totalReplaced + " backgrounds behind blocks.";
                }
            });
        }
        private void replaceRotate_ValueChanged(object sender, EventArgs e)
        {
            replaceRotating();
        }

        private void replaceRotating()
        {
            if (!rbSpikes.Checked)
            {
                var bid = (int)numericUpDown2.Value;
                if (bid < 500 || bid >= 1001)
                {

                    if (MainForm.decosBMI[bid] != 0)
                    {
                        img1 = bdata.getRotation(bid, Convert.ToInt32(replaceRotate.Value));
                    }
                    else if (MainForm.miscBMI[bid] != 0)
                    {
                        img1 = bdata.getRotation(bid, Convert.ToInt32(replaceRotate.Value));
                    }
                    if (img1 != null) RotationPictureBox2.Image = img1;
                    else RotationPictureBox2.Image = Properties.Resources.cross;
                }
            }
            else
            {
                RotationPictureBox2.Image = Properties.Resources.cross;
            }
        }
        private void PortalRadioButton_Click(object sender, EventArgs e)
        {
            PortalRadioButton.Checked = true;
            PortalRotFind.Image = MainForm.miscBMD.Clone(new Rectangle(108 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            PortalRotRepl.Image = MainForm.miscBMD.Clone(new Rectangle(108 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            PortalINVRadioButton.Checked = false;
            SignRadioButton.Checked = false;
            WorldPortalRadioButton.Checked = false;
            NormalRadioButton.Checked = false;
            rbSpikes.Checked = false;
            findRotate.Enabled = true;
            replaceRotate.Enabled = true;
        }

        private void PortalINVRadioButton_Click(object sender, EventArgs e)
        {
            PortalRadioButton.Checked = false;
            PortalRotFind.Image = MainForm.miscBMD.Clone(new Rectangle(112 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            PortalRotRepl.Image = MainForm.miscBMD.Clone(new Rectangle(112 * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
            PortalINVRadioButton.Checked = true;
            SignRadioButton.Checked = false;
            WorldPortalRadioButton.Checked = false;
            NormalRadioButton.Checked = false;
            rbSpikes.Checked = false;
            findRotate.Enabled = true;
            replaceRotate.Enabled = true;
        }

        private void NormalRadioButton_Click(object sender, EventArgs e)
        {
            PortalRadioButton.Checked = false;
            PortalINVRadioButton.Checked = false;
            SignRadioButton.Checked = false;
            WorldPortalRadioButton.Checked = false;
            rbSpikes.Checked = false;
            NormalRadioButton.Checked = true;
            findRotate.Enabled = true;
            replaceRotate.Enabled = true;
        }

        private void SignRadioButton_Click(object sender, EventArgs e)
        {
            PortalRadioButton.Checked = false;
            PortalINVRadioButton.Checked = false;
            SignRadioButton.Checked = true;
            WorldPortalRadioButton.Checked = false;
            NormalRadioButton.Checked = false;
            rbSpikes.Checked = false;
            findRotate.Enabled = true;
            replaceRotate.Enabled = true;
        }

        private void WorldPortalRadioButton_Click(object sender, EventArgs e)
        {
            PortalRadioButton.Checked = false;
            PortalINVRadioButton.Checked = false;
            SignRadioButton.Checked = false;
            WorldPortalRadioButton.Checked = true;
            NormalRadioButton.Checked = false;
            rbSpikes.Checked = false;
            findRotate.Enabled = true;
            replaceRotate.Enabled = true;
        }

        private void ClearBgsBlacklistButton_Click(object sender, EventArgs e)
        {
            BackgroundIgnore bgi = new BackgroundIgnore();
            bgi.Show();
        }

        private void ReplaceUnknownCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            MainForm.userdata.replaceit = ReplaceUnknownCheckBox.Checked;
        }

        public class removeBadRenderer : ToolStripSystemRenderer
        {
            public removeBadRenderer()
            {
            }

            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
            {
                //base.OnRenderToolStripBorder(e);
            }
        }

        private void PortalRotFind_Click(object sender, EventArgs e)
        {

            if (PortalRadioButton.Checked)
            {
                portalRotationFind += 1;
                if (portalRotationFind == 4) portalRotationFind = 0;
                PortalRotFind.Image = bdata.getRotation(242, portalRotationFind);
            }
            if (PortalINVRadioButton.Checked)
            {
                portalRotationFind += 1;
                if (portalRotationFind == 4) portalRotationFind = 0;
                PortalRotFind.Image = bdata.getRotation(381, portalRotationFind);
            }
        }

        private void PortalRotRepl_Click(object sender, EventArgs e)
        {
            if (PortalRadioButton.Checked)
            {
                portalRotationRepl += 1;
                if (portalRotationRepl == 4) portalRotationRepl = 0;
                PortalRotRepl.Image = bdata.getRotation(242, portalRotationRepl);

            }
            if (PortalINVRadioButton.Checked)
            {
                portalRotationRepl += 1;
                if (portalRotationRepl == 4) portalRotationRepl = 0;
                PortalRotRepl.Image = bdata.getRotation(381, portalRotationRepl);
            }
        }

        private void rbSpikes_Click(object sender, EventArgs e)
        {
            PortalRadioButton.Checked = false;
            PortalINVRadioButton.Checked = false;
            SignRadioButton.Checked = false;
            WorldPortalRadioButton.Checked = false;
            NormalRadioButton.Checked = false;
            rbSpikes.Checked = true;
            findRotate.Enabled = false;
            replaceRotate.Enabled = false;
            RotationPictureBox1.Image = Properties.Resources.cross;
            RotationPictureBox2.Image = Properties.Resources.cross;
        }

        private void rbSpikes_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void LabelRadioButton_Click(object sender, EventArgs e)
        {
            PortalRadioButton.Checked = false;
            PortalINVRadioButton.Checked = false;
            SignRadioButton.Checked = false;
            WorldPortalRadioButton.Checked = false;
            LabelRadioButton.Checked = true;
            NormalRadioButton.Checked = false;
            rbSpikes.Checked = false;
            findRotate.Enabled = true;
            replaceRotate.Enabled = true;
        }
    }
}
