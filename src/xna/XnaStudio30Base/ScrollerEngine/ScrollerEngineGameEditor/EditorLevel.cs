using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ScrollerEngineData;

namespace ScrollerEngineGameEditor
{
    public class EditorLevel : Level
    {
        private Texture2D startPointTexture;
        private Texture2D goalPointTexture;
        private Texture2D enemyStartPointTexture;

        private GameEntry _gameEntry;

        public void AddStartPoint(Rectangle bounds)
        {
            int nextId = StartPoints.Keys.OrderBy(k => k).LastOrDefault() + 1;
            StartPoints.Add(nextId,
                new StartPoint()
                {
                    Bounds = bounds
                });
        }

        public void DeleteStartPoint(int id)
        {
            if (StartPoints.ContainsKey(id))
                StartPoints.Remove(id);
        }

        public void AddGoalPoint(Rectangle bounds)
        {
            int nextId = Goals.Keys.OrderBy(k => k).LastOrDefault() + 1;
            Goals.Add(nextId,
                new GoalPoint()
                {
                    Bounds = bounds
                });
        }

        public void DeleteGoalPoint(int id)
        {
            if (Goals.ContainsKey(id))
                Goals.Remove(id);
        }


        public void AddEnemy(Rectangle bounds)
        {
            int nextId = this.EnemyStartPoints.Keys.OrderBy(k => k).LastOrDefault() + 1;
            var enemyStartPoint = new EnemyStartPoint()
            {
                 EnemyName = "BadDude",
                 SavedBounds = bounds
             };
            EnemyStartPoints.Add(nextId, enemyStartPoint);
        }

        public void DeleteEnemy(int id)
        {
            if (EnemyStartPoints.ContainsKey(id))
                EnemyStartPoints.Remove(id);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            base.LoadContent(contentManager);

            startPointTexture = contentManager.Load<Texture2D>("StartPoint");
            goalPointTexture = contentManager.Load<Texture2D>("GoalPoint");
            enemyStartPointTexture = contentManager.Load<Texture2D>("FakeEnemy");
        }

        public override void Update(GameTime gameTime, GameEntry gameEntry, Level level)
        {
            if (_gameEntry == null)
                _gameEntry = gameEntry;

            if (!Characters.ContainsKey(PlayerIndex.One))
            {
                var newCharacter = new EditorCharacter();
                newCharacter.LoadContent(Content);
                Characters.Add(PlayerIndex.One, newCharacter);
            }
                
            foreach (var item in Characters.Values)
                item.Update(gameTime, gameEntry, this);
        }

        public static EditorLevel Upgrade(Level level)
        {
            return level.Upgrade<EditorLevel>();
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            if (Enemies.Count > 0)
                Enemies.Clear();

            base.Draw(spriteBatch, color);

            foreach (var item in StartPoints.Values)
                GameObjectUtilities.Draw(spriteBatch, color, startPointTexture, item.Bounds);

            foreach (var item in Goals.Values)
                GameObjectUtilities.Draw(spriteBatch, color, goalPointTexture, item.Bounds);

            foreach (var item in EnemyStartPoints.Values)
            {
                GameObjectUtilities.Draw(spriteBatch, color, enemyStartPointTexture, item.Bounds);
                var enemy = _gameEntry.AvailableEnemies[item.EnemyName];

                GameObjectUtilities.Draw(spriteBatch, Color.LightGray, enemy.Texture, item.Bounds);                
            }
        }
    }
}
