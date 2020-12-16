using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace SpriteSheetMaker
{
    class Program
    {

        static void Main(string[] args)
        {
            string inputPath = args.First();
            var sheetBuilder = new SheetBuilder(inputPath);

            sheetBuilder.Run();
        }
    }

    public class SheetBuilder
    {
        public SheetBuilder(string basePath) { _basePath = basePath; }

        private List<string> _imagePaths = new List<string>();
        private List<ImageData> _images = new List<ImageData>();
        private string _basePath;
        private string _searchString = "*.png";

        private string _outName = "SpriteSheet.png";
        private string _outData = "SpriteSheetData.xml";

        public void Run()
        {
            _imagePaths.Clear();
            GetFiles(new DirectoryInfo(_basePath));

            int sheetHeight = (int)Math.Sqrt(_imagePaths.Count);
            int sheetWidth = (int)Math.Ceiling((Decimal)(_imagePaths.Count / sheetHeight));
            int x = 0, y = 0;
            int tileDimen = 128;

            using (var newSheet = new Bitmap(sheetWidth * tileDimen, sheetHeight * tileDimen, PixelFormat.Format32bppArgb))
            using (var graph = Graphics.FromImage(newSheet))
            {
                foreach (var imagePath in _imagePaths)
                {
                    if (Path.GetFileName(imagePath) == _outName)
                        continue;

                    using (var currentImage = new Bitmap(imagePath))
                    {
                        int left = x * tileDimen;
                        int top = y * tileDimen;

                        _images.Add(
                            new ImageData()
                            {
                                Name = imagePath.Replace(_basePath + "\\", "").Replace(".png", ""),
                                Top = top,
                                Left = left,
                                Width = tileDimen,
                                Height = tileDimen
                            }
                            );

                        graph.DrawImage(currentImage, left, top, tileDimen, tileDimen);
                    }

                    x++;
                    if (x >= sheetWidth)
                    {
                        x = 0;
                        y++;
                    }
                }

                newSheet.Save(_basePath + "\\" + _outName, ImageFormat.Png);
                new ImageDataSet() { Images = _images }.WriteXml(_basePath + "\\" + _outData);
            }
        }

        private void GetFiles(DirectoryInfo directoryInfo)
        {
            _imagePaths.AddRange(directoryInfo.GetFiles(_searchString).Select(f => f.FullName));
            foreach (var item in directoryInfo.GetDirectories())
                GetFiles(item);
        }
    }
}
