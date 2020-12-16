using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BYBFSideScrollerData;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;

namespace BYBFSideScrollerMapMaker
{
    static class Program
    {
        static CellType GetCell(char input)
        {
            switch (input)
            {
                case '<':
                    return CellType.Back;
                case 'X':
                    return CellType.Box;
                case 'B':
                    return CellType.BoxBreakable;

                case 'E':
                default:
                    return CellType.Empty;

                case '>':
                    return CellType.Forward ;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //string test = typeof(CellReader).AssemblyQualifiedName;
            var cellMap = 
                "XEEEEEEEEEEEEEEEEEEX" +
                "XEBBBEEEEEEEEEEEEEE>" +
                "XEEEEEEEEEEEEEEEEEE>" +
                "XEEEEEEEEXXXXEEXXXE>" +
                "XEXXXEEEEEEEEEEEEEE>" +
                "XEEEEEEEEEEEEEEEEXXX" +
                "XEEEEEEXXXXEEEEEEEEX" +
                "XEEEEEEEEEEEEEEEEEEX" +
                "XEEEEXXBXXXXEEEEEEEX" +
                "XEEEEEEEEEEEEEEEEEEX" +
                "XEEEEEEEEEEEEEEEEEEX" +
                "BEXXBXXXEEEEEEEEEEEX" +
                "BEEEEEEEEEEEEEEEEEEX" +
                "BEEEEEEEEEEEEEEEEEEX" +
                "XXXXXXXXXXXXXXXXXXXX";

            var cellMap1 =
                "BEEEEEEEEEEEEEEEEEEB" +
                "BEXXXEEEEEEEEEEEEEEX" +
                "BEEEEEEEEEEEEEEEEEEX" +
                "BEEEEEEEEBBBBEEBBBEB" +
                "BEBBBEEEEEEEEEEEEEEE" +
                "BEEEEEEEEEEEEEEEEBBB" +
                "BEEEEEEBBBBEEEEEEEEB" +
                "BEEEEEEEEEEEEEEEEEEB" +
                "BEEEEBBXBBBBEEEEEEEB" +
                "BEEEEEEEEEEEEEEEEEEB" +
                "<EEEEEEEEEEEEEEEEEEB" +
                "<EBBXBBBEEEEEEEEEEEB" +
                "<EEEEEEEEEEEEEEEEEEB" +
                "<EEEEEEEEEEEEEEEEEEB" +
                "BBBBBBBBBBBBBBBBBBBB";

            var level = new Level();
            level.Screens = new Screen[]
            {
                new Screen(){
                    Cells = cellMap.ToCharArray().Select(c => new Cell { Type = GetCell(c) }).ToArray(),
                    CellTypeTexture = new[] {
                        new {Key = CellType.Back, Value="SimpleSidewalk"},
                        new {Key = CellType.Box, Value="SimpleBridge"},
                        new {Key = CellType.BoxBreakable, Value="SimpleGrass"},
                        new {Key = CellType.Empty, Value="SimpleEmpty"},
                        new {Key = CellType.Forward,Value="SimpleRoad"}
                    }.ToDictionary(k => k.Key, v => v.Value)
                },
                new Screen(){
                    Cells = cellMap1.ToCharArray().Select(c => new Cell { Type = GetCell(c) }).ToArray(),
                    CellTypeTexture = new[] {
                        new {Key = CellType.Back, Value="SimpleSidewalk"},
                        new {Key = CellType.Box, Value="SimpleBridge"},
                        new {Key = CellType.BoxBreakable, Value="SimpleGrass"},
                        new {Key = CellType.Empty, Value="SimpleEmpty"},
                        new {Key = CellType.Forward,Value="SimpleRoad"}
                    }.ToDictionary(k => k.Key, v => v.Value)
                }
            };

            using (XmlWriter xmlWriter = XmlWriter.Create("Level001.xml", new XmlWriterSettings() { Indent = true }))
            {
                IntermediateSerializer.Serialize(xmlWriter, level, null);
            }
        }
    }
}

