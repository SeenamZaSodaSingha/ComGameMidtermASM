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
        float MovingAngle;

        public MovingBall(Texture2D texture, Vector2 position, float MovingAngle) : base(texture)
        {
            this.MovingAngle = MovingAngle;
            Velocity.X = (float) (Singleton.BALLSPEED * Math.Cos(this.MovingAngle));
            Velocity.Y = (float) (Singleton.BALLSPEED * Math.Sin(this.MovingAngle));
            base.Position = position;
        }

        public void update(GameTime gameTime, List<GameObj> GameObjs)
        {
            foreach (GameObj g in GameObjs)
            {
                Collision(g);
            }

            Position.X += (int) Velocity.X;
            Position.Y += (int) Velocity.Y;
            base.Update(gameTime, GameObjs);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public void Reset()
        {
            base.Reset();
        }

        private void Collision(GameObj GameObj)
        {
            if ( IsTouchingLeft(GameObj) || IsTouchingRight(GameObj) )
            {
                Velocity.X *= -1;
            }
            
        }
    }
}
