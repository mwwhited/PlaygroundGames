using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ScrollerEngineData
{
    public delegate void GotoLevelHandler(object sender, string level);
    public delegate void PostMessageHandler(object sender, string message);

    public class Level : ViewableGameObject<Level>
    {
        [ContentSerializerIgnore]
        protected ContentManager Content;
        [ContentSerializerIgnore]
        public volatile Dictionary<PlayerIndex, Character> Characters = new Dictionary<PlayerIndex, Character>();
        [ContentSerializerIgnore]
        protected Texture2D Backdrop;
        [ContentSerializerIgnore]
        public Dictionary<int, Enemy> Enemies = new Dictionary<int, Enemy>();
        [ContentSerializerIgnore]
        protected GamePadState lastPad1;
        [ContentSerializerIgnore]
        protected GamePadState lastPad2;
        [ContentSerializerIgnore]
        protected GamePadState lastPad3;
        [ContentSerializerIgnore]
        protected GamePadState lastPad4;
        
        public Vector2 GravityWind = new Vector2(0f, .5f);
        public int DefaultStartPoint = 0;
        public Dictionary<int, StartPoint> StartPoints = new Dictionary<int, StartPoint>();
        public Dictionary<int, GoalPoint> Goals = new Dictionary<int, GoalPoint>();
        public Dictionary<int, EnemyStartPoint> EnemyStartPoints = new Dictionary<int, EnemyStartPoint>();
        public string BackdropName;

        public event GotoLevelHandler GotoLevel;
        public void ChangeLevel(string targetLevel)
        {
            if (GotoLevel != null)
                GotoLevel(this, targetLevel);
        }

        public event PostMessageHandler PostMessage;
        public void SendMessage(string message)
        {
            if (PostMessage != null)
                PostMessage(this, message);
        }

        protected void TryLoadNewCharacter(PlayerIndex playerIndex, Character character)
        {
            if (!Characters.ContainsKey(playerIndex))
            {
                var newCharacter = character.GetClone();
                newCharacter.PlayerIndex = playerIndex;
                newCharacter.LoadContent(Content);
                newCharacter.Position = RandomStart();
                Characters.Add(playerIndex, newCharacter);
            }
        }

        public Vector2 RandomStart()
        {
            try
            {
                if (StartPoints.Count > 0)
                {
                    var startIndex = Randomizer.Value.Next(StartPoints.Count);
                    var start = StartPoints.Skip(startIndex).First().Value;
                    if (start != null)
                        return start.StartPosition;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(string.Format("Level.RandomStart(): \r\n{0}", ex.ToString()));
            }
            return new Vector2();
        }

        public override void LoadContent(ContentManager contentManager)
        {
            Content = contentManager;
            base.LoadContent(contentManager);

            Backdrop = Content.Load<Texture2D>(BackdropName);

            foreach (var item in Characters.Values)
                item.LoadContent(contentManager);

            foreach (var item in Enemies.Values)
                item.LoadContent(contentManager);
        }

        public override void Draw(SpriteBatch spriteBatch, Color color)
        {
            GameObjectUtilities.Draw(spriteBatch, color, Backdrop, Bounds);

            base.Draw(spriteBatch, color);

            foreach (var character in Characters.Values)
                character.Draw(spriteBatch, color);

            foreach (var item in Enemies.Values)
                item.Draw(spriteBatch, color);
        }

        public override void Update(GameTime gameTime, GameEntry gameEntry, Level level)
        {
            GamePadState currentPad1 = GamePad.GetState(PlayerIndex.One);
            GamePadState currentPad2 = GamePad.GetState(PlayerIndex.Two);
            GamePadState currentPad3 = GamePad.GetState(PlayerIndex.Three);
            GamePadState currentPad4 = GamePad.GetState(PlayerIndex.Four);

            if (currentPad1.Buttons.LeftShoulder == ButtonState.Pressed && lastPad1.Buttons.LeftShoulder == ButtonState.Released)
                TryLoadNewCharacter(PlayerIndex.One, gameEntry.AvailableCharacters.First().Value);

            if (currentPad2.Buttons.LeftShoulder == ButtonState.Pressed && lastPad2.Buttons.LeftShoulder == ButtonState.Released)
                TryLoadNewCharacter(PlayerIndex.Two, gameEntry.AvailableCharacters.First().Value);

            if (currentPad3.Buttons.LeftShoulder == ButtonState.Pressed && lastPad3.Buttons.LeftShoulder == ButtonState.Released)
                TryLoadNewCharacter(PlayerIndex.Three, gameEntry.AvailableCharacters.First().Value);

            if (currentPad4.Buttons.LeftShoulder == ButtonState.Pressed && lastPad4.Buttons.LeftShoulder == ButtonState.Released)
                TryLoadNewCharacter(PlayerIndex.Four, gameEntry.AvailableCharacters.First().Value);

            foreach (var item in Characters.Values)
                item.Update(gameTime, gameEntry, this);

            foreach (var item in Enemies.Values)
                item.Update(gameTime, gameEntry, this);

            lastPad1 = currentPad1;
            lastPad2 = currentPad2;
            lastPad3 = currentPad3;
            lastPad4 = currentPad4;
        }

        public override object Clone()
        {
            var newLevel = new Level();

            newLevel.TextureName = this.TextureName;


            newLevel.GravityWind = this.GravityWind;
            newLevel.DefaultStartPoint = this.DefaultStartPoint;

            foreach (var item in this.StartPoints)
                newLevel.StartPoints.Add(item.Key, item.Value.GetClone());

            foreach (var item in this.Goals)
                newLevel.Goals.Add(item.Key, item.Value.GetClone());

            foreach (var item in this.Enemies)
                newLevel.Enemies.Add(item.Key, item.Value.GetClone());

            foreach (var item in this.EnemyStartPoints)
                newLevel.EnemyStartPoints.Add(item.Key, item.Value.GetClone());

            newLevel.BackdropName = this.BackdropName;

            newLevel.Texture = this.Texture;
            newLevel.Bounds = this.Bounds;
            return newLevel;
        }

        public class LevelReader : ContentTypeReader<Level>
        {
            protected override Level Read(ContentReader input, Level existingInstance)
            {
                existingInstance = new Level();

                existingInstance.TextureName = input.ReadObject<string>();

                existingInstance.GravityWind = input.ReadObject<Vector2>();
                existingInstance.DefaultStartPoint = input.ReadObject<int>();

                foreach (var item in input.ReadObject<Dictionary<int, StartPoint>>())
                    existingInstance.StartPoints.Add(item.Key, item.Value.GetClone());

                foreach (var item in input.ReadObject<Dictionary<int, GoalPoint>>())
                    existingInstance.Goals.Add(item.Key, item.Value.GetClone());

                foreach (var item in input.ReadObject<Dictionary<int, EnemyStartPoint>>())
                    existingInstance.EnemyStartPoints.Add(item.Key, item.Value.GetClone());

                existingInstance.BackdropName = input.ReadObject<string>();

                return existingInstance;
            }
        }
    }
}
