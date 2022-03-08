using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ComGameMidtermASM.GameObj;
using System;
using System.Collections.Generic;



namespace ComGameMidtermASM
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BallShooter gun;
        List<GameObj.GameObj> _gameObj;
        Ball[,] ball;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            // determine window size. from Singleton class
            _graphics.PreferredBackBufferHeight = Singleton.SCREENHEIGHT;
            _graphics.PreferredBackBufferWidth = Singleton.SCREENWIDTH;
            _graphics.ApplyChanges();
            //gun = new BallShooter(gunTexture, crosshairTexture);
            //table = new int[8, 8];
            ball = new Ball[8, 8];
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //gun = new BallShooter(this.Content.Load<Texture2D>("gun"), this.Content.Load<Texture2D>("crosshairs"));
            var gunTexture = Content.Load<Texture2D>("gun");
            var crosshairTexture = Content.Load<Texture2D>("crosshairs");
            var ghost = Content.Load<Texture2D>("blue_ghost");
            // TODO: use this.Content to load your game content here
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if(i % 2 == 1)
                    {
                        ball[i, j] = new Ball(ghost, 0)
                        {
                            Position = new Vector2((j * ghost.Width) + ghost.Width / 2, i * ghost.Height)
                        };
                    }
                    else
                    {
                        ball[i, j] = new Ball(ghost, 0)
                        {
                            Position = new Vector2(j * ghost.Width, i * ghost.Height)
                        };
                    }
                }
            }
            _gameObj = new List<GameObj.GameObj>()
            {
                new BallShooter(gunTexture, crosshairTexture)
                {
                    Position = new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT - gunTexture.Height)
                }
            };
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (var sprite in _gameObj)
            {
                sprite.Update(gameTime, _gameObj);
            }
            //gun.Update(gameTime, null);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if(ball[i, j] != null)
                    {
                        ball[i, j].Draw(_spriteBatch);
                    }
                }
            }
            foreach(var sprite in _gameObj)
            {
                sprite.Draw(_spriteBatch);
            }
            //gun.Draw(_spriteBatch);
            // TODO: Add your drawing code here
            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
