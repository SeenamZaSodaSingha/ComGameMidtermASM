using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ComGameMidtermASM.GameObjs
{
    public class Boarder : GameObj
    {
        protected int LineWidth;
        Vector2 Dimentions;

        public Boarder(Texture2D texture, Vector2 Position, Vector2 Dimentions, int LineWidth) : base(texture)
        {
            _texture = texture;
            this.Position = Position;
            this.LineWidth = LineWidth;
            this.Dimentions = Dimentions;
            Name = "boarder";
            Color = Color.Black;

            HitboxX = (int)Position.X;
            HitboxY = (int)Position.Y;
            HitboxDX = (int)Dimentions.X;
            HitboxDY = (int)Dimentions.Y;

        }

        public Boarder(Texture2D texture) : base(texture)
        {
            _texture = texture;
            this.Position = new Vector2(Singleton.GAMEPANELLOCX, Singleton.GAMEPANELLOCY);
            this.LineWidth = Singleton.BOARDERWIDTH;
            this.Dimentions = new Vector2(Singleton.GAMEPANELWIDTH, Singleton.GAMEPANELHEIGHT);
            Name = "boarder";
            Color = Color.Black;

            HitboxX = (int)Position.X;
            HitboxY = (int)Position.Y;
            HitboxDX = (int)Singleton.GAMEPANELWIDTH;
            HitboxDY = (int)Singleton.GAMEPANELHEIGHT;

        }

        public void SetColor(Color color)
        {
            Color = color;
        }

        public override void Update(GameTime gameTime, List<GameObj> GameObjs)
        {
            base.Update(gameTime, GameObjs);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _texture.SetData<Color>(new Color[] { Color.White });

            spriteBatch.Draw(_texture, new Rectangle((int) Position.X, (int)Position.Y, LineWidth, HitboxDY + LineWidth), Color);
            spriteBatch.Draw(_texture, new Rectangle((int) Position.X, (int)Position.Y, HitboxDX + LineWidth, LineWidth), Color);
            spriteBatch.Draw(_texture, new Rectangle((int) Position.X + HitboxDX, (int)Position.Y, LineWidth, HitboxDY + LineWidth), Color);
            spriteBatch.Draw(_texture, new Rectangle((int) Position.X, (int)Position.Y + HitboxDY, HitboxDX + LineWidth, LineWidth), Color);
        }
        public override void Reset()
        {
            base.Reset();
        }
    }
}