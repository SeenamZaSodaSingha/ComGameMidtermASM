using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace ComGameMidtermASM
{
    public class maintest : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        private GameObjs.BallShooter gun;
        private GameObjs.MovingBall movingball;
        List<GameObjs.GameObj> gameobjs;

        public maintest()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";



        }

        protected override void Initialize()
        {
            // determine window size. from Singleton class
            _graphics.PreferredBackBufferHeight = Singleton.SCREENHEIGHT;
            _graphics.PreferredBackBufferWidth = Singleton.SCREENWIDTH;

            IsMouseVisible = true;
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("fonts/GameText");
            //test 
            gun = new GameObjs.BallShooter(Content.Load<Texture2D>("cannon/base-transparent"), Content.Load <Texture2D>("aim guide line/dot"), Content.Load<Texture2D>("ghost/cyan_ghost"));
            movingball = new GameObjs.MovingBall(Content.Load<Texture2D>("ghost/cyan_ghost"));

            gameobjs = new List<GameObjs.GameObj>()
            {
                new GameObjs.Boarder(new Texture2D(_spriteBatch.GraphicsDevice, 1, 1)),
                gun,
                movingball
            
            };

        }
    

        // TODO: use this.Content to load your game content here


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            foreach (GameObjs.GameObj obj in gameobjs)
            {
                obj.Update(gameTime, gameobjs);

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            foreach (GameObjs.GameObj obj in gameobjs)
            {
                obj.Draw(_spriteBatch);
            }


            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void print<T>(T stringable,int y)
        {
            _spriteBatch.DrawString(_spriteFont, stringable.ToString(), new Vector2(0,y), Color.Black);
        }
    }
}
