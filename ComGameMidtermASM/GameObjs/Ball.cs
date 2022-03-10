using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM.GameObjs
{
    public class Ball : GameObj
    {
        public bool Destroy;
        public bool visit;
        List<Texture2D> textures;
        public Ball(List<Texture2D> texture) : base(texture[0]) 
        {
            Destroy = false;
            visit = false;
            textures = texture;
        }

        public void SetColor(int color_)
        {
            this.color_ = color_;
            _texture = textures[color_];
        }

        //NOTE not used this.
        #region
        //public void SetColor(COLOR color_)
        //{
        //    color = color_;
        //    switch (color_)
        //    {
        //        case COLOR.cyan:
        //            {
        //                TextureDir = "ghost/cyan_ghost";
        //                _texture = textures[1];
        //                break;
        //            }
        //        case COLOR.magenta:
        //            {
        //                TextureDir = "ghost/magen_ghost";
        //                _texture = textures[2];
        //                break;
        //            }
        //        case COLOR.orange:
        //            {
        //                TextureDir = "ghost/orage_ghost";
        //                _texture = textures[3];
        //                break;
        //            }
        //        case COLOR.pink:
        //            {
        //                TextureDir = "ghost/pink_ghost";
        //                _texture = textures[4];
        //                break;
        //            }
        //        case COLOR.red:
        //            {
        //                TextureDir = "ghost/red_ghost";
        //                _texture = textures[5];
        //                break;
        //            }
        //        case COLOR.yellow:
        //            {
        //                TextureDir = "ghost/yellow_ghost";
        //                _texture = textures[6];
        //                break;
        //            }
        //    }
        //}
        //
        #endregion

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            HitboxX = (int)(Position.X - _texture.Width / 2);
            HitboxY = (int)(Position.Y - _texture.Height / 2);
            HitboxDX = (int)(_texture.Width);
            HitboxDY = (int)(_texture.Height);

            base.Update(gameTime, GameObjs);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), 1, SpriteEffects.None, 0f);
            Reset();
            base.Draw(spriteBatch);
        }

        public override void Reset()
        {
            base.Reset();
        }

    }
}
