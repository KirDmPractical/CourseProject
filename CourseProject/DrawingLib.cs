using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject
{
    class DrawingLib
    {
        public Image draw_grid(int width, int heith)
        {            
            Bitmap bmp = new Bitmap(width, heith-59);
            Graphics g = Graphics.FromImage(bmp);
            for (int j = 1; j < 10; j++)
            {
                g.DrawLine(new Pen(Color.Gray), new Point(0, j * 42), new Point(width, j * 42));
            }
            for (int i = 1; i < 10; i++)
            {
                g.DrawLine(new Pen(Color.Gray), new Point(i*42, 0), new Point(i * 42, heith - 60));
            }
            return bmp;
        }
    }
}
