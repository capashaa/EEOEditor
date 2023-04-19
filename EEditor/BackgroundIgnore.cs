using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace EEditor
{
    public partial class BackgroundIgnore : Form
    {
        private ImageList imglist = new ImageList();
        public BackgroundIgnore()
        {
            InitializeComponent();
        }

        private void BackgroundIgnore_Load(object sender, EventArgs e)
        {
            loaddata();
        }

        private void loaddata()
        {
            Console.WriteLine(MainForm.userdata.IgnoreBlocks.Count);
            listView1.View = View.Tile;
            listView1.TileSize = new Size(200, 24);
            listView1.Items.Clear();
            listView2.View = View.Tile;
            listView2.TileSize = new Size(200, 24);
            listView2.Items.Clear();
            ImageList imglist2 = new ImageList();
            imglist2.ColorDepth = ColorDepth.Depth32Bit;
            imglist2.ImageSize = new Size(16, 16);
            imglist.ColorDepth = ColorDepth.Depth32Bit;
            imglist.ImageSize = new Size(16, 16);
            var width = MainForm.decosBMD.Width / 16 + MainForm.miscBMD.Width / 16 + MainForm.foregroundBMD.Width / 16;
            if (imglist.Images.Count == 0)
            {
                Bitmap img1 = new Bitmap(width, 16);
                for (int i = 0; i < width; i++)
                {
                    if (i < 500 || i >= 1001)
                    {
                        if (MainForm.decosBMI[i] != 0)
                        {
                            img1 = MainForm.decosBMD.Clone(new Rectangle(MainForm.decosBMI[i] * 16, 0, 16, 16), MainForm.decosBMD.PixelFormat);
                        }
                        else if (MainForm.miscBMI[i] != 0)
                        {
                            img1 = MainForm.miscBMD.Clone(new Rectangle(MainForm.miscBMI[i] * 16, 0, 16, 16), MainForm.miscBMD.PixelFormat);
                        }
                        else if (MainForm.foregroundBMI[i] != 0)
                        {
                            img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[i] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
                        }
                        else
                        {
                            Bitmap temp = new Bitmap(16, 16);
                            Graphics gr = Graphics.FromImage(temp);
                            gr.Clear(Color.Transparent);
                            gr.DrawImage(Properties.Resources.unknown.Clone(new Rectangle(1 * 16, 0, 16, 16), Properties.Resources.unknown.PixelFormat), 0, 0);
                            img1 = temp;
                        }
                        imglist2.Images.Add(img1);
                        imglist.Images.Add(img1);
                    }
                    
                }
                listView1.LargeImageList = imglist;
                listView2.LargeImageList = imglist2;
            }

            if (MainForm.userdata.IgnoreBlocks.Count > 0)
            {
                for (int i = 0; i < MainForm.userdata.IgnoreBlocks.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem() {
                        Text = "BlockID: " + MainForm.userdata.IgnoreBlocks[i],
                        Name = MainForm.userdata.IgnoreBlocks[i].ToString(),
                        ImageIndex = (int)MainForm.userdata.IgnoreBlocks[i]
                    };

                    listView1.Items.Add(lvi);
                }
            }
            foreach (var item in MainForm.blocksdata)
            {
                
                if (item < 500 || item >= 1001)
                {
                    if (!MainForm.userdata.IgnoreBlocks.Contains(item))
                    {
                        ListViewItem lvi = new ListViewItem()
                        {
                            Text = "BlockID: " + item,
                            Name = item.ToString(),
                            ImageIndex = item
                        };
                        if (!listView2.Items.Contains(lvi)) listView2.Items.Add(lvi);
                    }
                }
                
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count > 0)
            {
                JToken val = Convert.ToInt32(listView1.Items[0].Name);
                MainForm.userdata.IgnoreBlocks.Remove(val);
                loaddata();
            }
        }

        private void ClearAllButton_Click(object sender, EventArgs e)
        {
            MainForm.userdata.IgnoreBlocks.Clear();
            loaddata();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (listView2.SelectedItems.Count == 1)
            {
                Console.WriteLine(listView2.SelectedItems[0].Text);
            }
        }
    }
}
