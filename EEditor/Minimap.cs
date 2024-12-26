using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
//using static System.Windows.Forms.MonthCalendar;

namespace EEditor
{
    public partial class Minimap : UserControl
    {
        public int BlockWidth { get; set; }
        public int BlockHeight { get; set; }

        
        public static Bitmap Bitmap { get; set; }
        public static uint[] Colors = new uint[3000];
        public static bool[] ImageColor = new bool[Colors.Length];
        private bool mouseDown { get; set; } = false;
        private bool draw { get; set;} = false;
        private bool move { get; set; } = false;

        private int pos { get; set; } = 0;
        static Minimap()
        {
            for (int i = 0; i < Colors.Length; ++i)
            {
                Colors[i] = 321;
            }
          
        }
        public Minimap()
        {
            InitializeComponent();
           
            this.BackColor = Color.Transparent;
            this.SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            System.Windows.Forms.Timer scrollTimer = new System.Windows.Forms.Timer() { Interval = 15 };
            scrollTimer.Tick += ScrollTimer_Tick;
            scrollTimer.Start();
        }

        private void ScrollTimer_Tick(object sender, EventArgs e)
        {
            if (mouseDown && MainForm.PaintOnMinimap && MainForm.editArea.Tool is ToolPen)
            {
                System.Drawing.Point mouse = PointToClient(MousePosition);
                if (ClientRectangle.Contains(mouse))
                {
                    mouse = GetLocation(mouse);
                    if (prevLocation.X >= 0 && prevLocation != mouse)
                        DrawLine(prevLocation, mouse);
                    prevLocation = mouse;
                }
                else
                {
                    prevLocation = new System.Drawing.Point(-1, -1);
                }
            }
            else
            {
                prevLocation = new System.Drawing.Point(-1, -1);
            }
        }

        private System.Drawing.Point prevLocation = new System.Drawing.Point(-1, -1);

        void DrawLine(System.Drawing.Point P, System.Drawing.Point Q)
        {

            int x0 = P.X, y0 = P.Y;
            int x1 = Q.X, y1 = Q.Y;
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;
            List<int> xs = new List<int>();
            List<int> ys = new List<int>();
            while (true)
            {
                xs.Add(x0);
                ys.Add(y0);
                if (x0 == x1 && y0 == y1) break;
                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }
                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
            Rectangle r = new Rectangle(0, 0, MainForm.Zoom, MainForm.Zoom);
            for (int i = 0; i < xs.Count; ++i)
            {
                SetPixel(xs[i], ys[i], 9);
                MainForm.editArea.Frames[0].Foreground[ys[i], xs[i]] = MainForm.editArea.Tool.PenID;
                MainForm.editArea.Draw(xs[i], ys[i], Graphics.FromImage(MainForm.editArea.Back), MainForm.userdata.thisColor);
                r.X = xs[i];
                r.Y = ys[i];
            }
            MainForm.editArea.Invalidate();
            Invalidate();

        }
        public System.Drawing.Point GetLocation(System.Drawing.Point p)
        {
            int x = p.X;
            int y = p.Y;
            x = Math.Max(0, x);
            y = Math.Max(0, y);
            x = Math.Min(x, BlockWidth - 1);
            y = Math.Min(y, BlockHeight - 1);
            return new System.Drawing.Point(x, y);
        }
        public void Init(int width, int height)
        {

            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    BlockWidth = width;
                    BlockHeight = height;
                    Size = new System.Drawing.Size(width, height);
                    Bitmap = new Bitmap(width, height);
                    for (int x = 0; x < width; ++x) for (int y = 0; y < height; ++y) Bitmap.SetPixel(x, y, Color.Black);
                    System.Drawing.Point relativePos = new System.Drawing.Point(-25, -25);
                    Location = new System.Drawing.Point(Parent.ClientSize.Width - Width + relativePos.X, Parent.ClientSize.Height - Height + relativePos.Y);
                });
            }
            else
            {
                BlockWidth = width;
                BlockHeight = height;
                Size = new System.Drawing.Size(width, height);
                Bitmap = new Bitmap(width, height);
                for (int x = 0; x < width; ++x) for (int y = 0; y < height; ++y) Bitmap.SetPixel(x, y, Color.Black);
                System.Drawing.Point relativePos = new System.Drawing.Point(-25, -25);
                Location = new System.Drawing.Point(Parent.ClientSize.Width - Width + relativePos.X, Parent.ClientSize.Height - Height + relativePos.Y);
            }

        }

        public void SetPixel(int x, int y, int id)
        {
            uint color = 4278190080;
            if (id < Colors.Length && Colors[id] != 321) color = Colors[id];

            if (this.InvokeRequired) { this.Invoke((MethodInvoker)delegate { Bitmap.SetPixel(x, y, Color.FromArgb((int)color)); }); }
            else { Bitmap.SetPixel(x, y, Color.FromArgb((int)color)); }
            Invalidate(new Rectangle(x, y, 1, 1));
        }

        public void SetColor(int x, int y, Color color)
        {
            Bitmap.SetPixel(x, y, color);
            Invalidate(new Rectangle(x, y, 1, 1));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            base.OnPaint(e);
            e.Graphics.DrawImage(Bitmap, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            
        }
        private void Minimap_Load(object sender, EventArgs e)
        {
        }

        private void Minimap_Click(object sender, EventArgs e)
        {
        }

        private void Minimap_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (mouseDown && !MainForm.PaintOnMinimap)
            {
                MainForm.editArea.AutoScrollPosition = new System.Drawing.Point((e.X * 16) - 768, (e.Y * 16) - 256);
            }
            if (mouseDown && MainForm.PaintOnMinimap)
            {
                //SetPixel(e.X, e.Y, 9);
            }

        }

        private void Minimap_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void Minimap_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Minimap_MouseClick(object sender, MouseEventArgs e)
        {
            if (!draw) MainForm.editArea.AutoScrollPosition = new System.Drawing.Point((e.X * 16) - 768, (e.Y * 16) - 256);
        }

        private protected void Minimap_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Control || e.KeyCode == Keys.ControlKey)
            {
                System.Drawing.Point point = new System.Drawing.Point();
                switch (pos)
                {
                    case 0:
                        pos = 0;
                        //center
                        //Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                        point = new System.Drawing.Point((Parent.ClientSize.Width - BlockWidth - 25) / 2, Parent.ClientSize.Height - Height - 25);
                        break;
                    case 1:
                        pos++;
                        //left
                         point = new System.Drawing.Point(((Parent.ClientSize.Width - BlockWidth) + 25) / Width, Parent.ClientSize.Height - Height - 25);
                        break;
                    case 2:
                        pos++;
                        //left - center
                        point = new System.Drawing.Point(((Parent.ClientSize.Width - BlockWidth) + 25) / Width, (Parent.ClientSize.Height - BlockHeight - 25) / 2);
                        break;
                    case 3:
                        pos++;
                        //center - center
                        point = new System.Drawing.Point(((Parent.ClientSize.Width - BlockWidth) - 25) / 2, (Parent.ClientSize.Height - BlockHeight - 25) / 2);
                        break;
                    case 4:
                        pos++;
                        //right - center
                        point = new System.Drawing.Point(((Parent.ClientSize.Width - BlockWidth) - 25), (Parent.ClientSize.Height - BlockHeight - 25) / 2);
                        break;
                    case 5:
                        pos++;
                        //right - up
                        point = new System.Drawing.Point(((Parent.ClientSize.Width - Width) - 25), (Parent.ClientSize.Height - BlockHeight + 25) % 2);
                        break;
                    case 6:
                        pos = 0;
                        //right
                        point = new System.Drawing.Point(Parent.ClientSize.Width - Width - 25, Parent.ClientSize.Height - Height - 25);
                        break;
                }
                Location = point;
                //this.Invalidate(true);
                //Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                this.Invalidate(true);

            }*/
            

        }

    }
}
