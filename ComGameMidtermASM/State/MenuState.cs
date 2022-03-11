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

        protected Texture2D newGameIdle, newGameHover, quitIdle, quitHover, restartIdle, restartHover, _backgroundTexturee;
        protected SpriteFont buttonFont;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content) : base(game, graphicsDevice, content)
        {   
            newGameIdle = _content.Load<Texture2D>("Control/start_idle");
            newGameHover = _content.Load<Texture2D>("Control/start_hover_ori");

            
            // NOTE HOT FIX
            //FIXME need exit texture
            //quitIdle = _content.Load<Texture2D>("Control/quit_idle");
            //quitHover = _content.Load<Texture2D>("Control/quit_hover");

            quitIdle = _content.Load<Texture2D>("Control/restart_idle");
            quitHover = _content.Load<Texture2D>("Control/restart_hover");


            restartIdle = _content.Load<Texture2D>("Control/restart_idle");
            restartHover = _content.Load<Texture2D>("Control/restart_hover");

            buttonFont = _content.Load<SpriteFont>("Font/font");
            _backgroundTexturee = _content.Load<Texture2D>("Background/unicorn_cat");
            //var bg = _content.Load<>
            var newGameButton = new Button(newGameIdle, newGameHover, buttonFont)
            {
                Position = new Vector2(305, 272),
                //Text = "New Game",
            };

            newGameButton.Click += newGameButton_Click;

            
            var restartGameButton = new Button(restartIdle, restartHover, buttonFont)
            {
                Position = new Vector2(39, 459),
                //Text = "Load Game",
            };

            restartGameButton.Click += restartGameButton_Click;

            var quitGameButton = new Button(quitIdle, quitHover, buttonFont)
            {
                Position = new Vector2(305, 363),
                //Text = "Quit Game",
            };

            quitGameButton.Click += quitGameButton_Click;

            _components = new List<Component>
            {
                newGameButton,
                //restartGameButton,
                quitGameButton
            };
        }

        private void newGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
        
        //restart
        private void restartGameButton_Click(object sender, EventArgs e)
        {
            //do restart
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
