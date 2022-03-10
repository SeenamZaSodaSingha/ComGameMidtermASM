using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace ComGameMidtermASM.GameObjs
{
    class Gun : GameObj
    {
        float angle;
        Vector2 Distance;
        Vector2 crosshairPosition;
        Texture2D crosshairTexture;

        public Gun(List<Texture2D> _textures, Texture2D crosshairtexture, List<Texture2D> balltextures) : base(_textures)
        {
            ObjInstances.movingball = new MovingBall(balltextures);
            SetBallColor(0);
            crosshairTexture = crosshairtexture;
            Position.X = Singleton.GUNPOSITIONX;
            Position.Y = Singleton.GUNPOSITIONY;

        }


        public void SetColor(int color_)
        {
            this.color_ = color_;
            switch (color_)
            {
                case 0:
                    {
                        _texture = _textures[0];
                        SetBallColor(0);
                        break;
                    }
                case 1:
                    {
                        _texture = _textures[1];
                        SetBallColor(1);
                        break;
                    }
                case 2:
                    {
                        _texture = _textures[2];
                        SetBallColor(2);
                        break;
                    }
                case 3:
                    {
                        _texture = _textures[3];
                        SetBallColor(3);
                        break;
                    }
                case 4:
                    {
                        _texture = _textures[4];
                        SetBallColor(4);
                        break;
                    }
                case 5:
                    {
                        _texture = _textures[5];
                        SetBallColor(5);
                        break;
                    }
                case 6:
                    {
                        _texture = _textures[6];
                        SetBallColor(6);
                        break;
                    }
            }
        }

        public float GetAngle()
        {
            return angle;
        }

        protected void SetBallColor(int color)
        {
            ObjInstances.movingball.SetColor(color);
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

                angle = (float)Math.Atan2(Distance.Y, Distance.X);
            }

            //Ref https://community.monogame.net/t/calculate-the-angle-between-two-points/6919/2
            crosshairPosition = Singleton.Instance.CurrentMouse.Position.ToVector2();
            base.Update(gameTime, GameObjs);
        }
    }
}
