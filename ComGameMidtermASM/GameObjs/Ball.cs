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
            yellow
        }

        public COLOR color;

        public Ball(Texture2D texture) : base(texture) { }

        public void SetColor(int color_)
        {
            switch (color_)
            {
                case 0:
                    {
                        color = COLOR.cyan;
                        TextureDir = "canon/canon-original-cyan";
                        break;
                    }
                case 1:
                    {
                        color = COLOR.magenta;
                        TextureDir = "canon/canon-original-magenta";
                        break;
                    }
                case 2:
                    {
                        color = COLOR.orange;
                        TextureDir = "canon/canon-original-orange";
                        break;
                    }
                case 3:
                    {
                        color = COLOR.pink;
                        TextureDir = "canon/canon-original-pink";
                        break;
                    }
                case 4:
                    {
                        color = COLOR.red;
                        TextureDir = "canon/canon-original-red";
                        break;
                    }
                case 5:
                    {
                        color = COLOR.yellow;
                        TextureDir = "canon/canon-original-yellow";
                        break;
                    }
            }
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
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
