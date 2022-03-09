using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM.GameObjs
{
    class MovingBall : Ball
    {
        public float MovingAngle;
        float ballspeed;


        public MovingBall(Texture2D texture) : base(texture)
        {
            // be false by default
            IsActive = false;
            ballspeed = Singleton.BALLSPEED;
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            // shoot a ball when mouse is click.
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed)
            {
                this.IsActive = true;
            }
            //

            // do a collision
            Collisionboarder(ObjInstances.boarder);

            Collisionballs(ObjInstances.ball);

            //update ball position
            if (this.IsActive)
            {
                this.Velocity.X = (float)(-ballspeed * Math.Cos(this.MovingAngle));
                this.Velocity.Y = (float)(-ballspeed * Math.Sin(this.MovingAngle));

                this.Position.X += this.Velocity.X;
                this.Position.Y += this.Velocity.Y;

            }
            base.Update(gameTime, GameObjs);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (IsActive)
            {
                Rotation = MovingAngle + MathHelper.ToRadians(-90f);
                Rotation = 0;
                spriteBatch.Draw(_texture, Position, null, Color.White, Rotation , new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
                Reset();
            }
        }

        public override void Reset()
        {
            base.Reset();
        }

        private void Collisionboarder(GameObj GameObj)
        {
            if (IsTouchingLeft(GameObj) && Velocity.X < 0)
            {
                MovingAngle = (float)Math.Acos(Velocity.X / ballspeed);
            }
            else if (IsTouchingRight(GameObj) && Velocity.X > 0)
            {
                MovingAngle = (float) Math.Acos(Velocity.X/ ballspeed);
            }

            else if (IsTouchingTop(GameObj) && Velocity.Y < 0)
            {
                //ballspeed = 0;
                //MovingAngle = (float) Math.Asin(Velocity.Y / ballspeed);
                IsActive = false;
            }
            else if (IsTouchingBottom(GameObj) && Velocity.Y > 0)
            {
                //ballspeed = 0;
                MovingAngle = (float) Math.Asin(Velocity.Y / ballspeed);
            }
        }

        private void Collisionball(GameObj GameObj)
        {
            if (IsTouchingLeft(GameObj) && Velocity.X < 0)
            {
                IsActive = false;
            }
            else if (IsTouchingRight(GameObj) && Velocity.X > 0)
            {
                IsActive = false;
            }
            else if (IsTouchingTop(GameObj) && Velocity.Y < 0)
            {
                IsActive = false;
            }
            else if (IsTouchingBottom(GameObj) && Velocity.Y > 0)
            {
                IsActive = false;
            }
        }

        private void Collisionballs(GameObjs.Ball[,] ball)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if (ObjInstances.ball[i, j] != null)
                    {
                        Collisionball(ObjInstances.ball[i, j]);
                        if (IsTouching(ObjInstances.ball[i, j]))
                        {
                            IsActive = false;
                            return;
                        }
                    }
                }
            }
        }

    }
}
