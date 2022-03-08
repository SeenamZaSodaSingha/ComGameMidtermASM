using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

            // determine window size. from Singleton class
            _graphics.PreferredBackBufferHeight = Singleton.SCREENHEIGHT;
            _graphics.PreferredBackBufferWidth = Singleton.SCREENWIDTH;


            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.ApplyChanges();

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // shoot a ball when mouse is click.
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                movingball = new GameObjs.MovingBall(Content.Load<Texture2D>("ghost/cyan_ghost"), gun.Position, gun.GetAngle());
            }

            //
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //test 
            gun = new GameObjs.BallShooter(Content.Load<Texture2D>("cannon/cannon-original-cyan"), Content.Load <Texture2D>("cannon/cannon-original-cyan"));
            movingball = new GameObjs.MovingBall(Content.Load<Texture2D>("ghost/cyan_ghost"), gun.Position, gun.GetAngle());

            gameobjs = new List<GameObjs.GameObj>()
            {
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
    }
}
