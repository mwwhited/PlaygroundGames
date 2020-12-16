using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteSheetMaker
{
    public class ImageData
    {
        public ImageData() { }

        public string Name { get; set; }
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
