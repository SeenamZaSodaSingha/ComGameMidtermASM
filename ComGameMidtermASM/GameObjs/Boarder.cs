using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM.GameObjs
{
    public class Boarder : GameObj
    {
        protected int LineWidth;

        //texture = Texture2D(_spriteBatch.GraphicsDevice, 1, 1);
        public Boarder(Texture2D texture, Vector2 Position, Vector2 Dimentions, int LineWidth) : base(texture)
        {
            _texture = texture;
            this.Position = Position;
            this.LineWidth = LineWidth;
            Name = "boarder";
            Color = Color.Black;
            Viewport = new Rectangle((int)Position.X, (int)Position.Y, (int)Dimentions.X, (int)Dimentions.Y);
        }

        public Boarder(Texture2D texture) : base(texture)
        {
            _texture = texture;
            this.Position = new Vector2(Singleton.GAMEPANELLOCX, Singleton.GAMEPANELLOCY);
            this.LineWidth = Singleton.BOARDERWIDTH;
            Name = "boarder";
            Color = Color.Black;
            Viewport = new Rectangle((int)Position.X, (int)Position.Y, (int)Singleton.GAMEPANELWIDTH, (int)Singleton.GAMEPANELHEIGHT);
        }

        public void SetColor(Color color)
        {
            this.Color = Color;
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            base.Update(gameTime, GameObjs);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _texture.SetData<Color>(new Color[] { Color.White });

            spriteBatch.Draw(_texture, new Rectangle((int) Position.X, (int)Position.Y, LineWidth, Viewport.Height + LineWidth), Color);
            spriteBatch.Draw(_texture, new Rectangle((int) Position.X, (int)Position.Y, Viewport.Width + LineWidth, LineWidth), Color);
            spriteBatch.Draw(_texture, new Rectangle((int) Position.X + Viewport.Width, (int)Position.Y, LineWidth, Viewport.Height + LineWidth), Color);
            spriteBatch.Draw(_texture, new Rectangle((int) Position.X, (int)Position.Y + Viewport.Height, Viewport.Width + LineWidth, LineWidth), Color);
        }
        public override void Reset()
        {
            base.Reset();
        }
    }
}