using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace BYBFSideScrollerData
{
    public class Cell : ICloneable
    {
        //[ContentSerializer(SharedResource = true)]
        public CellType Type { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            return new Cell { Type = Type };
        }

        #endregion
    }

    public class CellReader : ContentTypeReader<Cell>
    {
        protected override Cell Read(ContentReader input, Cell existingInstance)
        {
            existingInstance = new Cell();
            existingInstance.Type = input.ReadObject<CellType>();
            return existingInstance;
        }
    }
}
