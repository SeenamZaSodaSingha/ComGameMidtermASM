using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ComGameMidtermASM.GameObj
{
    class BallShooter : GameObj
    {
        float angle;
        bool mRelease = false;
        Ball ball;
        Texture2D gunTexture;
        Texture2D ballTexture;
        Texture2D crosshairTexture;
        Random rand = new Random();
        int colorID;
        bool shooting = false;
        public BallShooter(Texture2D texture, Texture2D crosshairtexture, Texture2D balltexture, int colorID) 
            : base(texture)
        {
            gunTexture = texture;
            crosshairTexture = crosshairtexture;
            ballTexture = balltexture;
            this.colorID = colorID;
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObj)
        {
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            if (Singleton.Instance.CurrentMouse.Position.Y <= 480 || (Singleton.Instance.CurrentMouse.Position.Y <= 540 && Singleton.Instance.CurrentMouse.Position.X > 75))
            {
                //angle = (float)(Math.Atan2(-Singleton.Instance.CurrentMouse.Position.Y + gunTexture.Height / 2 + Position.Y,
                //                            -Singleton.Instance.CurrentMouse.Position.X + gunTexture.Width / 2 + Position.X));
                angle = (float) - (Math.Atan2((-Singleton.Instance.CurrentMouse.Position.X + gunTexture.Width / 2 + Position.X), -Singleton.Instance.CurrentMouse.Position.Y + gunTexture.Height / 2 + Position.Y));
                if(!shooting && Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Pressed && mRelease)
                {
                    ball = new Ball(ballTexture, colorID)
                    {
                        Position = new Vector2(Position.X, Position.Y),
                        IsActive = true,
                        speed = 20,
                        angle = angle + MathHelper.ToRadians(-90f),
                        ballshooter = Position,
                        Velocity = new Vector2(100, 100)
                    };
                    shooting = true;
                }
            }
            if (shooting)
            {
                ball.Update(gameTime, GameObj);
            }
            if (Singleton.Instance.CurrentMouse.LeftButton == ButtonState.Released)
            {
                mRelease = true;
            }



            //Ref https://community.monogame.net/t/calculate-the-angle-between-two-points/6919/2
            crosshairPosition = Singleton.Instance.CurrentMouse.Position.ToVector2();
            //var ball = Ball.Clone() as Ball;
        }

        private void CreateBall(List<GameObj> GameObj)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(crosshairTexture, new Vector2(crosshairPosition.X, crosshairPosition.Y), null, Color.White);
            spriteBatch.Draw(gunTexture, Position, null, Color.White, angle, new Vector2(gunTexture.Width / 2, gunTexture.Height / 2), 1, SpriteEffects.None, 0f);
            if (!shooting)
                spriteBatch.Draw(ballTexture, new Vector2(Position.X - ballTexture.Width / 2, Position.Y - ballTexture.Height), Color.White);
            else
                ball.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }


        
    }
}
