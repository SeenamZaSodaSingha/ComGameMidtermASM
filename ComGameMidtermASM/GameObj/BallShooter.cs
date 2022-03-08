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
        //bool mRelease;

        Texture2D gunTexture;

        Texture2D crosshairTexture;

        //bool shot = false;
        public BallShooter(Texture2D texture, Texture2D crosshairtexture) 
            : base(texture)
        {
            gunTexture = texture;
            crosshairTexture = crosshairtexture;
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObj)
        {
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            if (Singleton.Instance.CurrentMouse.Position.Y <= 600)
            {
                angle = (float)(Math.Atan2(-Singleton.Instance.CurrentMouse.Position.Y + 25 + Position.Y,
                                            -Singleton.Instance.CurrentMouse.Position.X + 25 + Position.X));
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
            spriteBatch.Draw(crosshairTexture, new Vector2(crosshairPosition.X- 25, crosshairPosition.Y - 25), null, Color.White);
            spriteBatch.Draw(gunTexture, Position, null, Color.White, angle + MathHelper.ToRadians(-90f), new Vector2(25, 25), 1, SpriteEffects.None, 0f);
            base.Draw(spriteBatch);
        }


        
    }
}
