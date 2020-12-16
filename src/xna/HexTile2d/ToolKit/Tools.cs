using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ToolKit
{
    public static class Tools
    {
        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180d;
        }
        public static Point MidPoint(this Point start, Point end)
        {
            return new Point((start.X + end.X) / 2, (start.Y + end.Y) / 2);
        }
        public static void WriteAsLines<T>(this IEnumerable<T> items, string filename)
        {
            using (var writer = new StreamWriter(filename))
                foreach (var item in items)
                    writer.WriteLine(item);
        }
    }
}
