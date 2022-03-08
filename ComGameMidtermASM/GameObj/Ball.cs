using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComGameMidtermASM.GameObj
{

    class Ball : GameObj
    {
        float origin;
        Texture2D ballTexture;
        public Ball(Texture2D texture, int color)
            :base(texture)
        {
            ballTexture = texture;
            switch(color)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4:
                    {
                        break;
                    }
                case 5:
                    {
                        break;
                    }
            }
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTexture, Position, null, Color.White);
            
            base.Draw(spriteBatch);
        }
    }
}
