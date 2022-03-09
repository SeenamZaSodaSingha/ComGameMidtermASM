
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComGameMidtermASM.GameObjs
{
    public class GameObj : ICloneable
    {
        public Texture2D _texture;

        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
        public Vector2 Velocity;
        public string Name;
        public Vector2 Direction;
        public bool IsActive;

        public int HitboxX;
        public int HitboxY;
        public int HitboxDX;
        public int HitboxDY;
        public string TextureDir;
        public int color_;
        public Color Color;


        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                    HitboxX,
                    HitboxY,
                    HitboxDX,
                    HitboxDY
                    );
            }
        }

        public GameObj(Texture2D texture)
        {
            _texture = texture;
            Position = Vector2.Zero;
            Scale = Vector2.One;
            Rotation = 0f;
            IsActive = true;

        }

        public virtual void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
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
