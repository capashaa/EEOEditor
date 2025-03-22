using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEditor
{
    class ToolPenRound
    {
        private int PenId = 0;

        public void plotCircle(int centerX, int centerY, int r, int penId)
        {
            PenId = penId;
            int x = 0;
            int y = r;
            int d = 3 - 2 * r;

            while (y >= x)
            {
                for (int i = centerX - x; i <= centerX + x; i++)
                {
                    setPixel(i, centerY + y);
                    setPixel(i, centerY - y);
                }
                for (int i = centerX - y; i <= centerX + y; i++)
                {
                    setPixel(i, centerY + x);
                    setPixel(i, centerY - x);
                }
                setPixel(centerX + x, centerY + y);
                setPixel(centerX - x, centerY + y);
                setPixel(centerX + x, centerY - y);
                setPixel(centerX - x, centerY - y);
                setPixel(centerX + y, centerY + x);
                setPixel(centerX - y, centerY + x);
                setPixel(centerX + y, centerY - x);
                setPixel(centerX - y, centerY - x);
                if (d < 0)
                {
                    d += 4 * x + 6;
                }
                else
                {
                    d += 4 * (x - y) + 10;
                    y--;
                }
                x++;
            }
            MainForm.editArea.Invalidate();
        }
        public void setPixel(int x, int y)
        {
            if (x >= 0 && x < MainForm.editArea.BlockWidth && y >= 0 && y < MainForm.editArea.BlockHeight)
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
}
