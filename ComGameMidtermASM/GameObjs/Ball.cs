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
            cyan,
            magenta,
            orange,
            pink,
            red,
            yellow,
            blue
        }

        public COLOR color;

        public Ball(Texture2D texture) : base(texture) 
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
                        color = COLOR.cyan;
                        TextureDir = "ghost/cyan_ghost";
                        break;
                    }
                case 1:
                    {
                        color = COLOR.magenta;
                        TextureDir = "ghost/magen_ghost";
                        break;
                    }
                case 2:
                    {
                        color = COLOR.orange;
                        TextureDir = "ghost/orage_ghost";
                        break;
                    }
                case 3:
                    {
                        color = COLOR.pink;
                        TextureDir = "ghost/pink_ghost";
                        break;
                    }
                case 4:
                    {
                        color = COLOR.red;
                        TextureDir = "ghost/red_ghost";
                        break;
                    }
                case 5:
                    {
                        color = COLOR.yellow;
                        TextureDir = "ghost/yellow_ghost";
                        break;
                    }
                case 6:
                    {
                        color = COLOR.yellow;
                        TextureDir = "ghost/blue_ghost";
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
                        break;
                    }
                case COLOR.magenta:
                    {
                        TextureDir = "ghost/magen_ghost";
                        break;
                    }
                case COLOR.orange:
                    {
                        TextureDir = "ghost/orage_ghost";
                        break;
                    }
                case COLOR.pink:
                    {
                        TextureDir = "ghost/pink_ghost";
                        break;
                    }
                case COLOR.red:
                    {
                        TextureDir = "ghost/red_ghost";
                        break;
                    }
                case COLOR.yellow:
                    {
                        TextureDir = "ghost/yellow_ghost";
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
