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
        Vector2 Distance;
        //bool mRelease;
        Vector2 crosshairPosition;
        Texture2D crosshairTexture;
        Texture2D balltexture;
        MovingBall ball;

        //bool shot = false;

        public enum COLOR
        {
            cyan,
            magenta,
            orange,
            pink,
            red,
            yellow
        }

        public COLOR color;

        protected string textureDir;

        public string GetTextureDir()
        {
            return textureDir;
        }

        public void Setcolor(int color_)
        {
            switch (color_)
            {
                case 0:
                    {
                        color = COLOR.cyan;
                        textureDir = "cannon/cannon-original-cyan";
                        break;
                    }
                case 1:
                    {
                        color = COLOR.magenta;
                        textureDir = "cannon/cannon-original-magenta";
                        break;
                    }
                case 2:
                    {
                        color = COLOR.orange;
                        textureDir = "cannon/cannon-original-orange";
                        break;
                    }
                case 3:
                    {
                        color = COLOR.pink;
                        textureDir = "cannon/cannon-original-pink";
                        break;
                    }
                case 4:
                    {
                        color = COLOR.red;
                        textureDir = "cannon/cannon-original-red";
                        break;
                    }
                case 5:
                    {
                        color = COLOR.yellow;
                        textureDir = "cannon/cannon-original-yellow";
                        break;
                    }
            }
        }

        public BallShooter(Texture2D texture, Texture2D crosshairtexture, Texture2D balltexture) : base(texture)
        {
            _texture = texture;
            ball = new MovingBall(balltexture);
            crosshairTexture = crosshairtexture;
            Position.X = Singleton.GUNPOSITIONX;
            Position.Y = Singleton.GUNPOSITIONY;
            ball.Position = Position;
        }

        public void SetTexture(Texture2D texture)
        {
            _texture = texture;
        }

        public float GetAngle()
        {
            return angle;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, angle + MathHelper.ToRadians(-90f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(crosshairTexture, crosshairPosition, null, Color.White);
            ball.Draw(spriteBatch);
            Reset();
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            ball.Update(gameTime, GameObjs, angle);
            Singleton.Instance.CurrentMouse = Mouse.GetState();
            if (Singleton.Instance.CurrentMouse.Position.Y <= Singleton.SCREENWIDTH)
            {
                Distance.Y = -Singleton.Instance.CurrentMouse.Position.Y + (_texture.Height / 2) + Position.Y;
                Distance.X = -Singleton.Instance.CurrentMouse.Position.X + (_texture.Width / 2) + Position.X;

                angle = (float)Math.Atan2(Distance.Y,Distance.X);
            }

            //Ref https://community.monogame.net/t/calculate-the-angle-between-two-points/6919/2
            crosshairPosition = Singleton.Instance.CurrentMouse.Position.ToVector2();
            base.Update(gameTime, GameObjs);
        }
    }
}
