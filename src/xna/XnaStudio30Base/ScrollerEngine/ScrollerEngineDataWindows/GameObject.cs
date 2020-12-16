using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace ScrollerEngineData
{
    public abstract class GameObject<T> : Cloneable<T>
    {
        [ContentSerializerIgnore]
        public virtual Rectangle Bounds { get; set; }
    }
}
