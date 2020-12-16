using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace GpsContentPipeLine.Linq
{
    public static class GpxTools
    {
        public static IEnumerable<Vector3> AsGpxVectors(this string filename)
        {
            var xml = XElement.Load(filename);

            var trackName = XName.Get("trk", "http://www.topografix.com/GPX/1/1");
            var trackSegmentName = XName.Get("trkseg", "http://www.topografix.com/GPX/1/1");
            var trackPointName = XName.Get("trkpt", "http://www.topografix.com/GPX/1/1");
            var latitudeName = XName.Get("lat");
            var longitudeName = XName.Get("lon");
            var elevationName = XName.Get("ele", "http://www.topografix.com/GPX/1/1");

            var track = xml.Element(trackName);
            var trackSegment = track.Element(trackSegmentName);
            var trackpoints = trackSegment.Elements(trackPointName);

            var vectors = from e in trackpoints
                          //let x = e.Attribute(latitudeName).Value 
                          //let y = e.Attribute(longitudeName).Value 
                          //let z = e.Element(elevationName).Value 
                          //select new { x,y,z };
                          let x = float.Parse(e.Attribute(latitudeName).Value)
                          let y = float.Parse(e.Attribute(longitudeName).Value)
                          let z = float.Parse(e.Element(elevationName).Value)
                          select new Vector3(x, y, z);

            return vectors;
        }
    }
}
