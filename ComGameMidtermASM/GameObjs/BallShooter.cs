﻿using System;
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

        //NOTE :Enum is useless?
        public enum COLOR
        {
            none,
            cyan,
            magenta,
            orange,
            pink,
            red,
            yellow
        }

        public COLOR color;

        public BallShooter(List<Texture2D> _textures, Texture2D crosshairtexture, List<Texture2D> balltextures) : base(_textures)
        {
            ObjInstances.movingball = new MovingBall(balltextures);
            crosshairTexture = crosshairtexture;
            Position.X = Singleton.GUNPOSITIONX;
            Position.Y = Singleton.GUNPOSITIONY;

        }

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
                        color = COLOR.none;
                        TextureDir = "cannon/canon-original";
                        _texture = _textures[0];
                        SetBallColor(color);
                        break;
                    }
                case 1:
                    {
                        color = COLOR.cyan;
                        TextureDir = "cannon/canon-original-cyan";
                        _texture = _textures[1];
                        SetBallColor(color);
                        break;
                    }
                case 2:
                    {
                        color = COLOR.magenta;
                        TextureDir = "cannon/canon-original-magen";
                        _texture = _textures[2];
                        SetBallColor(color);
                        break;
                    }
                case 3:
                    {
                        color = COLOR.orange;
                        TextureDir = "cannon/canon-original-orange";
                        _texture = _textures[3];
                        SetBallColor(color);
                        break;
                    }
                case 4:
                    {
                        color = COLOR.pink;
                        TextureDir = "cannon/canon-original-pink";
                        _texture = _textures[4];
                        SetBallColor(color);
                        break;
                    }
                case 5:
                    {
                        color = COLOR.red;
                        TextureDir = "cannon/canon-original-red";
                        _texture = _textures[5];
                        SetBallColor(color);
                        break;
                    }
                case 6:
                    {
                        color = COLOR.yellow;
                        TextureDir = "cannon/canon-original-yellow";
                        _texture = _textures[6];
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
                        _texture = _textures[0];
                        SetBallColor(color);
                        this.color_ = 0;
                        break;
                    }
                case COLOR.magenta:
                    {
                        TextureDir = "cannon/canon-original-magen";
                        _texture = _textures[1];
                        SetBallColor(color);
                        this.color_ = 1;
                        break;
                    }
                case COLOR.orange:
                    {
                        TextureDir = "cannon/canon-original-orange";
                        _texture = _textures[2];
                        SetBallColor(color);
                        this.color_ = 2;
                        break;
                    }
                case COLOR.pink:
                    {
                        TextureDir = "cannon/canon-original-pink";
                        _texture = _textures[3];
                        SetBallColor(color);
                        this.color_ = 3;
                        break;
                    }
                case COLOR.red:
                    {
                        TextureDir = "cannon/canon-original-red";
                        _texture = _textures[4];
                        SetBallColor(color);
                        this.color_ = 4;
                        break;
                    }
                case COLOR.yellow:
                    {
                        TextureDir = "cannon/canon-original-yellow";
                        _texture = _textures[5];
                        SetBallColor(color);
                        this.color_ = 5;
                        break;
                    }
            }
        }

        public float GetAngle()
        {
            return angle;
        }

        protected void SetBallColor(COLOR color)
        {
            ObjInstances.movingball.SetColor((GameObjs.Ball.COLOR)color);
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
