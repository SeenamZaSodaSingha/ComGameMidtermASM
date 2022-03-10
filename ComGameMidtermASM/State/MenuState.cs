using ComGameMidtermASM.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComGameMidtermASM.State
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            var buttonTextuere = _content.Load<Texture2D>("Control/start_idle");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            var newGameButton = new Button(buttonTextuere, buttonFont)
            {
                Position = new Vector2(300, 200),
                Text = "New Game",
            };

            newGameButton.Click += newGameButton_Click;

            var loadGameButton = new Button(buttonTextuere, buttonFont)
            {
                Position = new Vector2(300, 250),
                Text = "Load Game",
            };

            loadGameButton.Click += loadGameButton_Click;

            var quitGameButton = new Button(buttonTextuere, buttonFont)
            {
                Position = new Vector2(300, 300),
                Text = "Quit Game",
            };

            quitGameButton.Click += quitGameButton_Click;

            _components = new List<Component>
            {

            };
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        

        private void loadGameButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        
        private void quitGameButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
