using System;
using System.Linq;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using ScrollerEngineData;
using System.IO;

namespace ScrollerEngineGameEditor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string fileName = "GameEntryData.xml";
            GameEntry gameEntry = null;

            if (File.Exists(fileName))
                using (XmlReader xmlReader = XmlReader.Create(fileName))
                {
                    try
                    {
                        gameEntry = IntermediateSerializer.Deserialize<GameEntry>(xmlReader, null);
                    }
                    catch { }
                }

            if (gameEntry == null)
            {
                gameEntry = new GameEntry();
                gameEntry.FirstLevel = "Level001";
                gameEntry.DefaultFontName = "Comic_Sans_MS";

                var level002 = new EditorLevel();
                level002.TextureName = "Level002";
                level002.BackdropName = "Level002_Back";
                level002.GravityWind = new Vector2(0f, .5f);
                gameEntry.Levels.Add("Level002", level002);

                var level001 = new EditorLevel();
                level001.TextureName = "Level001";
                level001.BackdropName = "Level001_Back";
                level001.GravityWind = new Vector2(0f, .5f);
                gameEntry.Levels.Add("Level001", level001);

                var tinyDude = new Character();
                tinyDude.TextureName = "TinyDude";
                tinyDude.jumpSpeed = 20;
                tinyDude.runFactor = 5;
                gameEntry.AvailableCharacters.Add("TinyDude", tinyDude);

                var badDude = new Enemy();
                badDude.TextureName = "BadDude";
                badDude.jumpSpeed = 20;
                badDude.runFactor = 5;
                gameEntry.AvailableEnemies.Add("BadDude", badDude);
            }

            gameEntry.Levels = gameEntry.Levels
                .ToDictionary(
                    k => k.Key,
                    v => EditorLevel.Upgrade(v.Value) as Level
                );

            using (Game1 game = new Game1(gameEntry))
            {
                game.Run();
            }

            gameEntry.Levels = gameEntry.Levels.ToDictionary(
                k => k.Key,
                v => v.Value.GetClone());

            using (XmlWriter xmlWriter = XmlWriter.Create("GameEntryData.xml", new XmlWriterSettings() { Indent = true }))
            {
                IntermediateSerializer.Serialize(xmlWriter, gameEntry, null);
            }
        }
    }
}
