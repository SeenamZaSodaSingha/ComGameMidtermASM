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
        Vector2 crosshairPosition;
        Texture2D crosshairTexture;

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

        public string GetTextureDir()
        {
            return TextureDir;
        }

        public void SetColor(int color_)
        {
            this.color_ = color_;
            switch (color_)
            {
                case 0:
                    {
                        color = COLOR.cyan;
                        TextureDir = "cannon/canon-original-cyan";
                        SetBallColor(color);
                        break;
                    }
                case 1:
                    {
                        color = COLOR.magenta;
                        TextureDir = "cannon/canon-original-magen";
                        SetBallColor(color);
                        break;
                    }
                case 2:
                    {
                        color = COLOR.orange;
                        TextureDir = "cannon/canon-original-orange";
                        SetBallColor(color);
                        break;
                    }
                case 3:
                    {
                        color = COLOR.pink;
                        TextureDir = "cannon/canon-original-pink";
                        SetBallColor(color);
                        break;
                    }
                case 4:
                    {
                        color = COLOR.red;
                        TextureDir = "cannon/canon-original-red";
                        SetBallColor(color);
                        break;
                    }
                case 5:
                    {
                        color = COLOR.yellow;
                        TextureDir = "cannon/canon-original-yellow";
                        SetBallColor(color);
                        break;
                    }
            }
        }

        public void SetColor(COLOR color_)
        {
            color = color_;
            switch (color_)
            {
                case COLOR.cyan:
                    {
                        TextureDir = "cannon/canon-original-cyan";
                        SetBallColor(color);
                        break;
                    }
                case COLOR.magenta:
                    {
                        TextureDir = "cannon/canon-original-magen";
                        SetBallColor(color);
                        break;
                    }
                case COLOR.orange:
                    {
                        TextureDir = "cannon/canon-original-orange";
                        SetBallColor(color);
                        break;
                    }
                case COLOR.pink:
                    {
                        TextureDir = "cannon/canon-original-pink";
                        SetBallColor(color);
                        break;
                    }
                case COLOR.red:
                    {
                        TextureDir = "cannon/canon-original-red";
                        SetBallColor(color);
                        break;
                    }
                case COLOR.yellow:
                    {
                        TextureDir = "cannon/canon-original-yellow";
                        SetBallColor(color);
                        break;
                    }
            }
        }

        public BallShooter(Texture2D texture, Texture2D crosshairtexture, Texture2D balltexture) : base(texture)
        {
            _texture = texture;
            ObjInstances.movingball = new MovingBall(balltexture);
            crosshairTexture = crosshairtexture;
            Position.X = Singleton.GUNPOSITIONX;
            Position.Y = Singleton.GUNPOSITIONY;
            
        }

        public float GetAngle()
        {
            return angle;
        }

        protected void SetBallColor(COLOR color)
        {
            ObjInstances.movingball.SetColor( (GameObjs.Ball.COLOR) color );
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, angle + MathHelper.ToRadians(-90f), new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            spriteBatch.Draw(crosshairTexture, crosshairPosition, null, Color.White);
            ObjInstances.movingball.Draw(spriteBatch);
            Reset();
            base.Draw(spriteBatch);
        }

        public void Reset(Texture2D texture)
        {
            _texture = texture;
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            if (!ObjInstances.movingball.IsActive)
            {
                ObjInstances.movingball.Position = Position;
                ObjInstances.movingball.MovingAngle = angle;
            }
            ObjInstances.movingball.Update(gameTime, GameObjs);
            Singleton.Instance.CurrentMouse = Mouse.GetState();

            if (Singleton.Instance.CurrentMouse.Position.Y <= 560)
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
