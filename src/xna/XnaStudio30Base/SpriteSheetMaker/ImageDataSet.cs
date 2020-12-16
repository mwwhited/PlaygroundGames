using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace SpriteSheetMaker
{
    public class BatchImageDataSet
    {
        public BatchImageDataSet() { SpriteSheetData = new Dictionary<string, ImageDataSet>(); }

        public Dictionary<string, ImageDataSet> SpriteSheetData { get;set;}
    }

    public class ImageDataSet
    {
        public ImageDataSet() { }

        public List<ImageData> Images { get; set; }

        public void WriteXml(string fileName)
        {
            using (var ms = new MemoryStream())
            {
                var xser = new XmlSerializer(typeof(ImageDataSet));
                xser.Serialize(ms, this);
                File.WriteAllBytes(fileName, ms.ToArray());
            }
        }

        public static ImageDataSet ReadXml(string fileName)
        {
            return ParseXml(File.ReadAllBytes(fileName));
        }

        public static ImageDataSet ParseXml(string content)
        {
            return ParseXml(ASCIIEncoding.ASCII.GetBytes(content));
        }

        public static ImageDataSet ParseXml(byte[] content)
        {
            using (var ms = new MemoryStream(content))
            {
                var xser = new XmlSerializer(typeof(ImageDataSet));
                return xser.Deserialize(ms) as ImageDataSet;
            }
        }
    }
}
