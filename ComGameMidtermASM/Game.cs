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
        private Texture2D Texture;
        private Texture2D background;
        List<GameObj.GameObj> _gameObj;
        Ball[,] ball;
        private int colorID;
        Random rand = new Random();
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
            var gunTexture = Content.Load<Texture2D>("base-transparent");
            var crosshairTexture = Content.Load<Texture2D>("crosshairs");
            var blue_ghost = Content.Load<Texture2D>("blue_ghost");
            var cyan_ghost = Content.Load<Texture2D>("cyan_ghost");
            var magen_ghost = Content.Load<Texture2D>("magen_ghost");
            var red_ghost = Content.Load<Texture2D>("red_ghost");
            var orange_ghost = Content.Load<Texture2D>("orage_ghost");
            var pink_ghost = Content.Load<Texture2D>("pink_ghost");
            var yellow_ghost = Content.Load<Texture2D>("yellow_ghost");
            background = Content.Load<Texture2D>("Raccoon_norm");
            // TODO: use this.Content to load your game content here
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    colorID = rand.Next(0, 7);
                    switch (colorID)
                    {
                        case 0:
                            Texture = blue_ghost;
                            break;
                        case 1:
                            Texture = cyan_ghost;
                            break;
                        case 2:
                            Texture = magen_ghost;
                            break;
                        case 3:
                            Texture = orange_ghost;
                            break;
                        case 4:
                            Texture = red_ghost;
                            break;
                        case 5:
                            Texture = pink_ghost;
                            break;
                        case 6:
                            Texture = yellow_ghost;
                            break;
                    }
                    if (i % 2 == 1)
                    {
                        ball[i, j] = new Ball(Texture, colorID)
                        {
                            Position = new Vector2((j * Texture.Width) + Texture.Width / 2, i * Texture.Height),
                            //color = GetColor(color)
                        };
                    }
                    else
                    {
                        ball[i, j] = new Ball(Texture, colorID)
                        {
                            Position = new Vector2(j * Texture.Width, i * Texture.Height)
                        };
                    }
                }
            }
            _gameObj = new List<GameObj.GameObj>()
            {
                new BallShooter(gunTexture, crosshairTexture, blue_ghost, 0)
                {
                    Position = new Vector2(Singleton.SCREENWIDTH / 2, Singleton.SCREENHEIGHT - gunTexture.Height / 2)
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
            //for(int i = 0; i < )
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
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
