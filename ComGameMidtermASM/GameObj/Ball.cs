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
        public int colorID;
        public Color color;
        public bool IsActive;
        public float speed;
        public float angle;
        private bool reverseForward;
        private bool reverseBackward;
        SpriteFont gameFont;
        public Ball(Texture2D texture, int color)
            :base(texture)
        {
            colorID = color;
            switch(colorID)
            {
                case 0:
                    {
                        ballTexture = texture;
                        break;
                    }
                case 1:
                    {
                        ballTexture = texture;
                        break;
                    }
                case 2:
                    {
                        ballTexture = texture;
                        break;
                    }
                case 3:
                    {
                        ballTexture = texture;
                        break;
                    }
                case 4:
                    {
                        ballTexture = texture;
                        break;
                    }
                case 5:
                    {
                        ballTexture = texture;
                        break;
                    }
                case 6:
                    {
                        ballTexture = texture;
                        break;
                    }
            }
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            if (IsActive)
            {
                Velocity.X = speed * (float)Math.Cos(angle);
                Velocity.Y = speed * (float)Math.Sin(angle);
                Position.X += Velocity.X;
                Position.Y += Velocity.Y;
                if(Position.X + ballTexture.Width > Singleton.SCREENWIDTH)
                {
                    Velocity.X = -Velocity.X;
                }
                else if(Position.X < 0)
                {
                    Velocity.X = -Velocity.X;
                }

            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(ballTexture, Position, null, Color.White);
            //spriteBatch.Draw(gameFont, Position.ToString(), new Vector2(0, 0), Color.Black);
            base.Draw(spriteBatch);
        }
    }
}
