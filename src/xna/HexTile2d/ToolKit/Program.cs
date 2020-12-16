using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace ToolKit
{
    class Program
    {
        static void Main(string[] args)
        {
            var bases = new[] {
                "LLLLLL",
                "WLLLLL",
                "WWLLLL",
                "WWWLLL",
                "WWWWLL",
                "WWWWWL",
                "WWWWWW",
            };

            Func<char, Color> getcolor = z => z == 'L' ? Color.DarkGreen : z == 'W' ? Color.DarkBlue : Color.Transparent;
            Func<string, int, string> slide = (z, i) => new string(z.Skip(i).Concat(z.Take(i)).ToArray());

            var cellSet = (from z in bases
                           from i in Enumerable.Range(0, z.Length)
                           select slide(z, i)
                            ).Distinct().OrderBy(z => z).ToList();
            cellSet.WriteAsLines("lines.txt");


            var s = 64;
            var b = (int)Hexagon.CalculateHeight(s);
            var a = (int)Hexagon.CalculateWidth(s);

            var xsize = a + 2;
            var ysize = b + 0;

            var gridPen = Pens.Black;
            var cellIndex = 0;

            using (var bmp = new Bitmap(xsize * 8, ysize * 4))
            using (var g = Graphics.FromImage(bmp))
            {
                //g.FillRectangle(Brushes.Magenta, new Rectangle(0, 0, bmp.Width, bmp.Height));

                for (var xoffset = 0; xoffset < bmp.Width; xoffset += xsize)
                    for (var yoffset = 0; yoffset < bmp.Height; yoffset += ysize)
                    {
                        var center = Hexagon.GetCenter(s, xoffset, yoffset);
                        var points = Hexagon.GetRegularPoints(s, xoffset, yoffset);

                        //var cell = cellSet.Skip(cellIndex).First();
                        //if (cell != null)
                        //{
                        //    var lastColor = Color.Transparent;
                        //    var lastPoint = points[5];
                        //    for (var c = 0; c < cell.Length; c++)
                        //    {
                        //        var color = getcolor(cell[c]);
                        //        if (c == 0) lastColor = color;
                        //        var brush = new SolidBrush(color);
                        //        var pen = new Pen(brush);

                        //        //g.DrawLine(pen, center, points[c]);

                        //        var midpoint = points[c].MidPoint(lastPoint);
                        //        g.FillPie(
                        //            brush,
                        //            midpoint.X - s / 2, midpoint.Y - s / 2,
                        //            s, s,
                        //            60 * c, 120);
                        //        //g.FillEllipse(brush, midpoint.X - 9, midpoint.Y - 9, 20, 20);

                        //        lastPoint = points[c];
                        //        lastColor = color;
                        //    }
                        //}

                        g.DrawLines(gridPen, points);
                        cellIndex++;
                    }

                g.Save();
                bmp.Save("outfile.png", ImageFormat.Png);
            }
        }
    }
}
