
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComGameMidtermASM.GameObj
{
    public class GameObj : ICloneable
    {
        protected Texture2D _texture;

        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public Vector2 Velocity;
        public string Name;
        public Vector2 Direction;
        public bool IsActive;
        public Rectangle Viewport;
        public Vector2 Origin;

        public Vector2 crosshairPosition;
        public Vector2 gunPosition;

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, Viewport.Width, Viewport.Height);
            }
        }

        public GameObj(Texture2D texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;
            Origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
        }

        public virtual void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, Origin, Scale, SpriteEffects.None, 0);
        }

        public virtual void Reset()
        {
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #region Collision
        protected bool IsTouching(GameObj g)
        {
            return IsTouchingLeft(g) ||
                IsTouchingTop(g) ||
                IsTouchingRight(g) ||
                IsTouchingBottom(g);
        }

        protected bool IsTouchingLeft(GameObj g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Left &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        protected bool IsTouchingRight(GameObj g)
        {
            return this.Rectangle.Right > g.Rectangle.Right &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }

        protected bool IsTouchingTop(GameObj g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Top &&
                    this.Rectangle.Top < g.Rectangle.Top;
        }

        protected bool IsTouchingBottom(GameObj g)
        {
            return this.Rectangle.Right > g.Rectangle.Left &&
                    this.Rectangle.Left < g.Rectangle.Right &&
                    this.Rectangle.Bottom > g.Rectangle.Bottom &&
                    this.Rectangle.Top < g.Rectangle.Bottom;
        }
        #endregion
    }
}
