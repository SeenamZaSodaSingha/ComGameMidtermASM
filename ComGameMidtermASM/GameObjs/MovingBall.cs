using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM.GameObjs
{
    class MovingBall : Ball
    {
        public float MovingAngle;


        public MovingBall(Texture2D texture) : base(texture)
        {
            // be false by default
            IsActive = false;
            Viewport = new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
        }

        public void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            // shoot a this when mouse is click.
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                this.IsActive = true;
            }
            //

            foreach (GameObj g in GameObjs)
            {
                Collision(g);
            }


            if (this.IsActive)
            {
                this.Velocity.X = (float)(-Singleton.BALLSPEED * Math.Cos(this.MovingAngle));
                this.Velocity.Y = (float)(-Singleton.BALLSPEED * Math.Sin(this.MovingAngle));

                this.Position.X += this.Velocity.X;
                this.Position.Y += this.Velocity.Y;

            }
            base.Update(gameTime, GameObjs);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                spriteBatch.Draw(_texture, Position, null, Color.White, MovingAngle + MathHelper.ToRadians(-90f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
                Reset();
            }
        }

        public override void Reset()
        {
            base.Reset();
        }

        private void Collision(GameObj GameObj)
        {
            if (string.Compare(GameObj.Name, "boarder") == 0)
            {
                if (IsTouchingLeft(GameObj) && Velocity.X < 0)
                {
                    MovingAngle = (float)Math.Acos(Velocity.X / Singleton.BALLSPEED);
                    //Velocity.X *= -1;
                }
                else if (IsTouchingRight(GameObj) && Velocity.X > 0)
                {
                    MovingAngle = (float) Math.Acos(Velocity.X/Singleton.BALLSPEED);
                    //Velocity.X *= -1;
                }
                else if (IsTouchingTop(GameObj) && Velocity.Y < 0)
                {
                    MovingAngle = (float)Math.Asin(Velocity.Y / Singleton.BALLSPEED);
                    Velocity.Y *= -1;
                }
                else if (IsTouchingBottom(GameObj) && Velocity.Y > 0)
                {
                    MovingAngle = (float)Math.Asin(Velocity.Y / Singleton.BALLSPEED);
                    Velocity.Y *= -1;
                }

            }
        }
    }
}
