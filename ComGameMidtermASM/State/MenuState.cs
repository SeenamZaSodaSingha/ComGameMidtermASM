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

        protected Texture2D buttonTexture, _backgroundTexturee;
        protected SpriteFont buttonFont;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {
            buttonTexture = _content.Load<Texture2D>("Control/start_idle");
            buttonFont = _content.Load<SpriteFont>("Font/font");
            _backgroundTexturee = _content.Load<Texture2D>("Background/unicorn_cat");
            //var bg = _content.Load<>
            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(0, 0),
                //Text = "New Game",
            };

            newGameButton.Click += newGameButton_Click;

            var loadGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 250),
                //Text = "Load Game",
            };

            loadGameButton.Click += loadGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(300, 300),
                //Text = "Quit Game",
            };

            quitGameButton.Click += quitGameButton_Click;

            _components = new List<Component>
            {
                newGameButton,
                loadGameButton,
                quitGameButton
            };
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
        

        private void loadGameButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Load Game");
        }
        
        private void quitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }



        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(_backgroundTexturee, new Vector2(0, 0), Color.White);

            foreach (var component in _components)
            {
                component.Draw(gameTime, spriteBatch);
            }

            spriteBatch.End();
        }

        public override void PostUpdate(GameTime gameTime)
        {
            
        }

        public override void Update(GameTime gameTime)
        {

            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }
    }
}
