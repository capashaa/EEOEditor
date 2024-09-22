using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEditor
{
    class ToolCircleSize
    {
        private int PenId = 0;
        public void CircleSize(Point start, Point end,int penid)
        {
            PenId = penid;
            //img1 = MainForm.foregroundBMD.Clone(new Rectangle(MainForm.foregroundBMI[PenID] * 16, 0, 16, 16), MainForm.foregroundBMD.PixelFormat);
            int x0 = (int)Math.Min(start.X, end.X);
            int x1 = (int)Math.Max(start.X, end.X);
            int y0 = (int)Math.Min(start.Y, end.Y);
            int y1 = (int)Math.Max(start.Y, end.Y);
            //int yy = 0;
            int a = Math.Abs(x1 - x0),
            b = Math.Abs(y1 - y0),
            b1 = b & 1;
            long dx = 4 * (1 - a) * b * b, dy = 4 * (b1 + 1) * a * a;
            long err = dx + dy + b1 * a * a, e2; 

            if (x0 > x1) { x0 = x1; x1 += a; } 
            if (y0 > y1) y0 = y1; 
            y0 += (b + 1) / 2; y1 = y0 - b1;   
            a *= 8 * a; b1 = 8 * b * b;

            do
            {
                paintPixel(x1, y0); 
                paintPixel(x0, y0); 
                paintPixel(x0, y1); 
                paintPixel(x1, y1); 
                e2 = 2 * err;
                if (e2 <= dy) { y0++; y1--; err += dy += a; }  
                if (e2 >= dx || 2 * err > dy) { x0++; x1--; err += dx += b1; } 
            } while (x0 <= x1);

            while (y0 - y1 < b)
            {  
                paintPixel(x0 - 1, y0); 
                paintPixel(x1 + 1, y0++);
                paintPixel(x0 - 1, y1);
                paintPixel(x1 + 1, y1--);
            }
            MainForm.editArea.Invalidate();

        }
        public void paintPixel(int x, int y)
        {

            using (Graphics g = Graphics.FromImage(MainForm.editArea.Back))
            {

                if (PenId >= 500 && PenId <= 999)
                {
                    MainForm.editArea.CurFrame.Background[y, x] = PenId;
                }
                else if (PenId < 500 || PenId >= 1001)
                {
                    MainForm.editArea.CurFrame.Foreground[y, x] = PenId;
                }
                MainForm.editArea.Draw(x, y, g, MainForm.userdata.thisColor);

            }
        }
    }
}
