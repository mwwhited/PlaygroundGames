using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using ScrollerEngine;
using ScrollerEngineData;
using System.Threading;

namespace ScrollerEngineGameEditor
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : ScrollerGame
    {
        EnemySelector currentEnemySelector;

        public Game1(GameEntry ge)
        {
            gameEntry = ge;

            new Thread(new ThreadStart(delegate
            {
                Thread.Sleep(100);
                currentEnemySelector = new EnemySelector(gameEntry);
                currentEnemySelector.Current = gameEntry;
                currentEnemySelector.ShowDialog();
            }))
            {
                ApartmentState = ApartmentState.STA,
                Name="This is the other thread"
            }.Start();

        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);  

            //gameEntry = Content.Load<GameEntry>("GameEntryData");
            gameEntry.LoadContent(Content);
        }
    }
}
