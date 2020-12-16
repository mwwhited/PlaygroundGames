using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ScrollerEngineData
{
    public class EnemyStartPoint : GameObject<EnemyStartPoint>
    {
        public Rectangle SavedBounds;
        public string EnemyName;

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
            return new EnemyStartPoint()
            {
                SavedBounds = this.SavedBounds,
                EnemyName = this.EnemyName
            };
        }

        public class EnemyStartPointReader : ContentTypeReader<EnemyStartPoint>
        {
            protected override EnemyStartPoint Read(ContentReader input, EnemyStartPoint existingInstance)
            {
                existingInstance = new EnemyStartPoint();

                existingInstance.SavedBounds = input.ReadObject<Rectangle>();
                existingInstance.EnemyName = input.ReadObject<string>();

                return existingInstance;
            }
        }
    }
}
