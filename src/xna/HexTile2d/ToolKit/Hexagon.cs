using System;
using System.Drawing;

namespace ToolKit
{
    public class Hexagon
    {
        public static double CalculateCornerHeight(double side)
        {
            return Math.Sin(30d.ToRadians()) * side;
        }
        public static double CalculateCornerWidth(double side)
        {
            return Math.Cos(30d.ToRadians()) * side;
        }
        public static double CalculateWidth(double side)
        {
            return CalculateCornerWidth(side) * 2;
        }
        public static double CalculateHeight(double side)
        {
            return side + 2 * CalculateCornerHeight(side);
        }

        public static Point GetCenter(double side, int xoffset, int yoffset)
        {
            var b = (int)Hexagon.CalculateHeight(side);
            var a = (int)Hexagon.CalculateWidth(side);
            var h = (int)Hexagon.CalculateCornerHeight(side);
            var r = (int)Hexagon.CalculateCornerWidth(side);
            var s = (int)side;

            var center = new Point(r + xoffset, h + s / 2 + yoffset);
            return center;
        }
        public static Point[] GetRegularPoints(double side, int xoffset, int yoffset)
        {
            var s = (int)side;
            var b = (int)Hexagon.CalculateHeight(side);
            var a = (int)Hexagon.CalculateWidth(side);
            var h = (int)Hexagon.CalculateCornerHeight(side);
            var r = (int)Hexagon.CalculateCornerWidth(side);

            var points = new[] {
                new Point(r+xoffset,0+yoffset),
                new Point(r*2+xoffset,h+yoffset),
                new Point(r*2+xoffset,h+s+yoffset),
                new Point(r+xoffset,2*h+s+yoffset),
                new Point(0+xoffset,h+s+yoffset),
                new Point(0+xoffset,h+yoffset),
                new Point(r+xoffset,0+yoffset),
            };

            return points;
        }
    }
}
