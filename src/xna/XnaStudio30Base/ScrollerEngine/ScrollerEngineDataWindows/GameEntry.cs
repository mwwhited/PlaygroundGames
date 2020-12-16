using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.ObjectModel;

namespace ScrollerEngineData
{
    public class GameEntry : Cloneable<GameEntry>
    {
        public GameEntry() { }

        [ContentSerializerIgnore]
        public SpriteFont DefaultFont;
        [ContentSerializerIgnore]
        public string CurrentLevelIndex = null;
        [ContentSerializerIgnore]
        public Level CurrentLevel
        {
            get
            {
                if (string.IsNullOrEmpty(CurrentLevelIndex))
                    return null;
                return Levels[CurrentLevelIndex];
            }
        }
        [ContentSerializerIgnore]
        public List<Message> Messages = new List<Message>();

        protected virtual Enemy GetEnemy(string enemyName)
        {
            if (AvailableEnemies.ContainsKey(enemyName))
                return AvailableEnemies[enemyName];
            return null;
        }

        #region Handlers

        public void GotoLevel(object sender, string targetlevel)
        {
            if (string.IsNullOrEmpty(targetlevel))
                throw new ArgumentNullException("targetLevel");

            if (CurrentLevelIndex == targetlevel)
                return;

            CurrentLevelIndex = targetlevel;
            CurrentLevel.Characters.Clear();

            var enemies = from es in CurrentLevel.EnemyStartPoints
                          join e in AvailableEnemies on es.Value.EnemyName equals e.Key
                          let enemy = e.Value.GetClone()
                          let pos = enemy.Position = es.Value.StartPosition
                          select new
                          {
                              Key = es.Key,
                              Value = enemy
                          };

            CurrentLevel.Enemies = enemies.ToDictionary(k => k.Key, v => v.Value);

            var level = sender as Level;
            if (level != null)
                foreach (var item in level.Characters)
                {
                    item.Value.lastEnemyHit = null;
                    //item.Value.lastGoalHit = null;
                    item.Value.Position = CurrentLevel.RandomStart();
                    CurrentLevel.Characters.Add(item.Key, item.Value);
                }
        }

        public void PostMessage(object sender, string message)
        {
            Messages.Add(new Message()
            {
                Expiry = DateTime.Now.AddSeconds(10),
                Text = message
            });
        }

        #endregion

        public string FirstLevel = null;
        public Dictionary<string, Level> Levels = new Dictionary<string, Level>();
        public Dictionary<string, Character> AvailableCharacters = new Dictionary<string, Character>();
        public Dictionary<string, Enemy> AvailableEnemies = new Dictionary<string, Enemy>();
        public string DefaultFontName;

        public void LoadContent(ContentManager contentManager)
        {
            DefaultFont = contentManager.Load<SpriteFont>(DefaultFontName);

            foreach (var item in Levels.Values)
            {
                item.GotoLevel += new GotoLevelHandler(GotoLevel);
                item.PostMessage += new PostMessageHandler(PostMessage);
                item.LoadContent(contentManager);
            }

            foreach (var item in AvailableCharacters.Values)
                item.LoadContent(contentManager);

            foreach (var item in AvailableEnemies.Values)
                item.LoadContent(contentManager);

            GotoLevel(this, FirstLevel);
        }

        public void UnloadContent()
        {
            foreach (var item in Levels)
                item.Value.UnloadContent();

            foreach (var item in AvailableCharacters)
                item.Value.UnloadContent();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Color color)
        {
            CurrentLevel.Draw(spriteBatch, color);

            for (int i = 0; i < Messages.Count; i++)
            {
                spriteBatch.DrawString(DefaultFont, Messages[i].Text, new Vector2(0, i * 10), color);
            }
        }

        //private string lastLevel;
        public void Update(GameTime gameTime)
        {
            var expiredMessages = Messages.Where(m => m.Expiry < DateTime.Now || string.IsNullOrEmpty(m.Text)).ToArray();
            foreach (var item in expiredMessages)
                Messages.Remove(item);

            CurrentLevel.Update(gameTime, this, null);
        }

        public override object Clone()
        {
            var newGameEntry = new GameEntry();

            newGameEntry.FirstLevel = this.FirstLevel;
            foreach (var level in this.Levels)
                newGameEntry.Levels.Add(level.Key, level.Value.GetClone());

            foreach (var item in this.AvailableCharacters)
                newGameEntry.AvailableCharacters.Add(item.Key, item.Value.GetClone());

            newGameEntry.DefaultFontName = this.DefaultFontName;

            return newGameEntry;
        }

        public class GameEntryReader : ContentTypeReader<GameEntry>
        {
            protected override GameEntry Read(ContentReader input, GameEntry existingInstance)
            {
                existingInstance = new GameEntry();
                existingInstance.FirstLevel = input.ReadObject<string>();

                foreach (var level in input.ReadObject<Dictionary<string, Level>>())
                    existingInstance.Levels.Add(level.Key, level.Value.GetClone());

                foreach (var item in input.ReadObject<Dictionary<string, Character>>())
                    existingInstance.AvailableCharacters.Add(item.Key, item.Value.GetClone());

                foreach (var item in input.ReadObject<Dictionary<string, Enemy>>())
                    existingInstance.AvailableEnemies.Add(item.Key, item.Value.GetClone());

                existingInstance.DefaultFontName = input.ReadObject<string>();

                return existingInstance;
            }
        }
    }
}
