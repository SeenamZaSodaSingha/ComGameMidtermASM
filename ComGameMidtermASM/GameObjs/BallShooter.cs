using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ComGameMidtermASM.GameObjs
{
    class BallShooter : GameObj
    {
        float angle;
        //bool mRelease;
        Vector2 gunPosition;
        Texture2D gunTexture;
        Vector2 crosshairPosition;
        Texture2D crosshairTexture;

        //bool shot = false;
        public BallShooter(Texture2D texture, Texture2D crosshairtexture) : base(texture)
        {
            gunTexture = texture;
            crosshairTexture = crosshairtexture;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(gunTexture, gunPosition, null, Color.White, angle + MathHelper.ToRadians(-90f), new Vector2(gunTexture.Width / 2, gunTexture.Height / 2), 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(crosshairTexture, crosshairPosition, null, Color.White);
            Reset();
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            if (Singleton.Instance.CurrentMouse.Position.Y <= 600)
            {
                angle = (float)(Math.Atan2(-Singleton.Instance.CurrentMouse.Position.Y + gunTexture.Height / 2 + gunPosition.Y,
                                            -Singleton.Instance.CurrentMouse.Position.X + gunTexture.Width / 2 + gunPosition.X));
            }

            //Ref https://community.monogame.net/t/calculate-the-angle-between-two-points/6919/2
            crosshairPosition = Singleton.Instance.CurrentMouse.Position.ToVector2();
            base.Update(gameTime, GameObjs);
        }
    }
}
