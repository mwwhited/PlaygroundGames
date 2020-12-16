using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ScrollerEngineData
{
    public class StartPoint : GameObject<StartPoint>
    {
        public Rectangle SavedBounds;

        public override Rectangle Bounds
        {
            get { return SavedBounds; }
            set { SavedBounds = value; }
        }

        [ContentSerializerIgnore]
        public Vector2 StartPosition
        {
            get { return new Vector2(SavedBounds.X, SavedBounds.Y); }
        }

        public override object Clone()
        {
            return new StartPoint()
            {
                SavedBounds = this.SavedBounds 
            };
        }

        public class StartPointReader : ContentTypeReader<StartPoint>
        {
            protected override StartPoint Read(ContentReader input, StartPoint existingInstance)
            {
                existingInstance = new StartPoint();

                existingInstance.SavedBounds = input.ReadObject<Rectangle>();

                return existingInstance;
            }
        }
    }
}
