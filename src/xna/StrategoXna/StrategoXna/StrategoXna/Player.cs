using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StrategoXna
{
    [DebuggerDisplay("{PlayerIndex}")]
    public class Player : IPlayer
    {
        private static readonly Dictionary<PlayerIndex, Player> _players = new Dictionary<PlayerIndex, Player>();

        public PlayerIndex PlayerIndex { get; private set; }
        public IEnumerable<IPiece> Pieces { get; private set; }
        public ICursor Cursor { get; private set; }

        public Player(PlayerIndex playerIndex)
        {
            _players.Add(playerIndex, this);

            this.PlayerIndex = playerIndex;

            var pieces = new[] { new Piece(PieceTypes.Marshal, this, 0) }
                 .Concat(new[] { new Piece(PieceTypes.General, this, 0) })
                 .Concat(Enumerable.Range(0, 2).Select(i => new Piece(PieceTypes.Colonel, this, i)))
                 .Concat(Enumerable.Range(0, 3).Select(i => new Piece(PieceTypes.Major, this, i)))
                 .Concat(Enumerable.Range(0, 4).Select(i => new Piece(PieceTypes.Captain, this, i)))
                 .Concat(Enumerable.Range(0, 4).Select(i => new Piece(PieceTypes.Lieutenant, this, i)))
                 .Concat(Enumerable.Range(0, 2).Select(i => new Piece(PieceTypes.Sergeant, this, i)))
                 .Concat(Enumerable.Range(0, 5).Select(i => new Piece(PieceTypes.Miner, this, i)))
                 .Concat(Enumerable.Range(0, 8).Select(i => new Piece(PieceTypes.Scout, this, i)))
                 .Concat(new[] { new Piece(PieceTypes.Spy, this, 0) })
                 .Concat(Enumerable.Range(0, 8).Select(i => new Piece(PieceTypes.Bomb, this, i)))
                 .Concat(new[] { new Piece(PieceTypes.Flag, this, 0) })
                 .ToList()
                 ;

            this.Pieces = pieces;

            this.Cursor = new Cursor(this);
        }

        public void Load(Game game)
        {
            foreach (var piece in this.Pieces)
                piece.Load(game);

            this.Cursor.Load(game);
        }

        public void Unload(Game game)
        {
            this.Cursor.Unload(game);

            foreach (var piece in this.Pieces)
                piece.Unload(game);
        }

        public void Draw(Game game, GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var piece in this.Pieces)
                piece.Draw(game, gameTime, spriteBatch);

            //this.Cursor.Draw(game, gameTime, spriteBatch);
        }
    }
}
