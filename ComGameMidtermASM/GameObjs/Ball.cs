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
        public enum COLOR
        {
            blue,
            cyan,
            magenta,
            orange,
            pink,
            red,
            yellow
        }

        public COLOR color;

        public Ball(List<Texture2D> _textures) : base(_textures)
        {
            TextureDir = "ghost/blue_ghost";
        }

        public void SetColor(int color_)
        {
            this.color_ = color_;
            switch (color_)
            {
                case 0:
                    {
                        color = COLOR.blue;
                        _texture = _textures[0];
                        TextureDir = "ghost/blue_ghost";
                        break;
                    }
                case 1:
                    {
                        color = COLOR.cyan;
                        _texture = _textures[1];
                        TextureDir = "ghost/cyan_ghost";
                        break;
                    }
                case 2:
                    {
                        color = COLOR.magenta;
                        _texture = _textures[2];
                        TextureDir = "ghost/magen_ghost";
                        break;
                    }
                case 3:
                    {
                        color = COLOR.orange;
                        _texture = _textures[3];
                        TextureDir = "ghost/orage_ghost";
                        break;
                    }
                case 4:
                    {
                        color = COLOR.pink;
                        _texture = _textures[4];
                        TextureDir = "ghost/pink_ghost";
                        break;
                    }
                case 5:
                    {
                        color = COLOR.red;
                        _texture = _textures[5];
                        TextureDir = "ghost/red_ghost";
                        break;
                    }
                case 6:
                    {
                        color = COLOR.yellow;
                        _texture = _textures[6];
                        TextureDir = "ghost/yellow_ghost";
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
                        TextureDir = "ghost/cyan_ghost";
                        _texture = _textures[1];
                        break;
                    }
                case COLOR.magenta:
                    {
                        TextureDir = "ghost/magen_ghost";
                        _texture = _textures[2];
                        break;
                    }
                case COLOR.orange:
                    {
                        TextureDir = "ghost/orage_ghost";
                        _texture = _textures[3];
                        break;
                    }
                case COLOR.pink:
                    {
                        TextureDir = "ghost/pink_ghost";
                        _texture = _textures[4];
                        break;
                    }
                case COLOR.red:
                    {
                        TextureDir = "ghost/red_ghost";
                        _texture = _textures[5];
                        break;
                    }
                case COLOR.yellow:
                    {
                        TextureDir = "ghost/yellow_ghost";
                        _texture = _textures[6];
                        break;
                    }
            }
        }

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
