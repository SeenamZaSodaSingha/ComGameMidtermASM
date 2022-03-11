using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;



namespace ComGameMidtermASM.GameObjs
{
    class Gun : GameObj
    {
        float angle;
        Vector2 Distance;
        Vector2 crosshairPosition;
        Texture2D crosshairTexture;
        SoundEffectInstance click, bounce;

        public Gun(List<Texture2D> _textures, Texture2D crosshairtexture, List<Texture2D> balltextures, SoundEffectInstance click, SoundEffectInstance bounce) : base(_textures)
        {
            ObjInstances.movingball = new MovingBall(balltextures);
            SetBallColor(0);
            crosshairTexture = crosshairtexture;
            Position.X = Singleton.GUNPOSITIONX;
            Position.Y = Singleton.GUNPOSITIONY;
            this.click = click;
            this.bounce = bounce;
        }

        public Gun(List<Texture2D> _textures, Texture2D crosshairtexture, List<Texture2D> balltexturesR, List<Texture2D> balltexturesL, SoundEffectInstance click, SoundEffectInstance bounce) : base(_textures)
        {
            ObjInstances.movingball = new MovingBall(balltexturesR, balltexturesL);
            SetBallColor(0);
            crosshairTexture = crosshairtexture;
            Position.X = Singleton.GUNPOSITIONX;
            Position.Y = Singleton.GUNPOSITIONY;
            this.click = click;
            this.bounce = bounce;
        }


        public void SetColor(int color_)
        {
            this.color_ = color_;
            _texture = _textures[this.color_];
            SetBallColor(this.color_);
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
            ObjInstances.movingball.Update(gameTime, GameObjs, click, bounce);
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
