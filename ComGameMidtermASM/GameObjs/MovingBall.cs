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
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
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

        //private void Collision(GameObj GameObj)
        //{
        //    if ( IsTouchingLeft(GameObj) || IsTouchingRight(GameObj) )
        //    {
        //        Velocity.X *= -1;
        //    }
            
        //}
    }
}
