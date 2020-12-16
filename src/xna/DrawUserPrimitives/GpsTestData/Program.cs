using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GpsContentPipeLine.Linq;

namespace GpsTestData
{
    class Program
    {
        static void Main(string[] args)
        {
            var file = "JohnstownNewark.gpx";
            var vectors = file.AsGpxVectors();

            var min = vectors.Min(v => v.Z);
            var max = vectors.Max(v => v.Z);

            var percents = from v in vectors
                           select (v.Z - min) / (max - min);
        }
    }
}
