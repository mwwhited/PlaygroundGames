using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

//BYBFSideScrollerData.Level
namespace BYBFSideScrollerData
{
    public class Level
    {
        [ContentSerializerIgnore()]
        public int CurrentScreenIndex { get; set; }

        [ContentSerializerIgnore()]
        public Screen CurrentScreen
        {
            get
            {
                if (Screens.Length <= 0)
                    return null;

                if (CurrentScreenIndex < 0)
                    CurrentScreenIndex = 0;
                else if (CurrentScreenIndex >= Screens.Length)
                    CurrentScreenIndex = Screens.Length - 1;

                return Screens[CurrentScreenIndex];
            }
        }

        [ContentSerializer(SharedResource = true)]
        public Screen[] Screens { get; set; }

        #region ICloneable Members

        public Level GetClone()
        {
            return Clone() as Level;
        }

        public object Clone()
        {
            Screen[] screens = new Screen[Screens.Length];
            for (int i = 0; i < screens.Length; i++)
                screens[i] = Screens[i].Clone() as Screen;
            
            return new Level()
            {
                CurrentScreenIndex = 0,
                Screens = screens
                //Screens.Select(s => s.Clone() as Screen).ToArray()
            };
        }

        #endregion
    }

    public class LevelReader : ContentTypeReader<Level>
    {
        protected override Level Read(ContentReader input, Level existingInstance)
        {
            var returnData = existingInstance ?? new Level();
            returnData.Screens = input.ReadObject<Screen[]>();
            return returnData;
        }
    }
}
