using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StrategoXna
{
    [DebuggerDisplay("{PieceType}({PieceIndex}) - {Player}")]
    public class Piece : IPiece
    {
        private Texture2D Texture { get; set; }
        public PieceTypes PieceType { get; private set; }
        public int PieceIndex { get; private set; }
        public IPlayer Player { get; private set; }

        public int X { get; private set; }
        public int Y { get; private set; }

        public Rectangle Position
        {
            get
            {
                return new Rectangle(this.X * Globals.TileSize,
                                     this.Y * Globals.TileSize,
                                     Globals.TileSize,
                                     Globals.TileSize
                                     );
            }
        }

        private Color Color
        {
            get
            {
                switch (this.Player.PlayerIndex)
                {
                    default:
                    case PlayerIndex.One:
                        return Color.Red;

                    case PlayerIndex.Two:
                        return Color.Blue;

                    case PlayerIndex.Three:
                        return Color.Yellow;

                    case PlayerIndex.Four:
                        return Color.Green;
                }
            }
        }

        public Piece(PieceTypes pieceType, IPlayer player, int pieceIndex)
        {
            this.PieceType = pieceType;
            this.Player = player;
            this.PieceIndex = pieceIndex;
        }

        public string TextureName
        {
            get { return this.PieceType.ToString(); }
        }

        public AttackResults Attack(IPiece piece)
        {
            if (piece.PieceType == PieceTypes.Flag)
                return AttackResults.Flag;

            if (this.PieceType == piece.PieceType)
                return AttackResults.Tie;

            if (this.PieceType == PieceTypes.Miner
                && piece.PieceType == PieceTypes.Bomb)
                return AttackResults.Win;

            if (this.PieceType == PieceTypes.Spy
                && piece.PieceType == PieceTypes.Marshal)
                return AttackResults.Win;

            if (this.PieceType < piece.PieceType)
                return AttackResults.Win;

            return AttackResults.Loss;
        }

        public bool CanMove
        {
            get
            {
                if (this.PieceType == PieceTypes.Flag ||
                    this.PieceType == PieceTypes.Bomb)
                    return false;
                return true;
            }
        }

        public int RangeMove
        {
            get
            {
                if (!this.CanMove)
                    return 0;

                if (this.PieceType == PieceTypes.Scout)
                    return Globals.MaxRange;

                return 1;
            }
        }

        public void Load(Game game)
        {
            this.Texture = game.Content.Load<Texture2D>(this.TextureName);
        }

        public void Unload(Game game)
        {
        }

        public void Draw(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, this.Color);
        }
        
        public void SetPosition(int x, int y)
        {
            this.X = (int)MathHelper.Clamp(x, 0, Globals.MaxRange);
            this.Y = (int)MathHelper.Clamp(y, 0, Globals.MaxRange);
        }
    }
}
