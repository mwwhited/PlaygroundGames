using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BYBFSideScrollerData
{
    public class Screen : ICloneable 
    {
        //public Screen() { }

        //40x20 
        public Cell[] Cells { get; set; }

        public Dictionary<CellType, string> CellTypeTexture { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            var cells = new Cell[Cells.Length];
            for (int i = 0; i < cells.Length; i++)
                cells[i] = Cells[i].Clone() as Cell;

            var cellTypeTexture = new Dictionary<CellType, string>();
            foreach (var item in CellTypeTexture)
                cellTypeTexture.Add(item.Key, item.Value.Clone() as string);

            return new Screen()
            {
                Cells = cells,
                CellTypeTexture = cellTypeTexture
                //Cells = Cells.Select(c => c.Clone() as Cell).ToArray(),
                //CellTypeTexture = CellTypeTexture.Select(kv => kv).ToDictionary(k => k.Key, v => v.Value)
            };
        }

        #endregion
    }

    public class ScreenReader : ContentTypeReader<Screen>
    {
        protected override Screen Read(ContentReader input, Screen existingInstance)
        {
            existingInstance = new Screen();
            existingInstance.Cells = input.ReadObject<Cell[]>();
            existingInstance.CellTypeTexture = input.ReadObject<Dictionary<CellType, string>>();
            return existingInstance;
        }
    }
}
