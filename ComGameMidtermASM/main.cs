using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM
{
    public class main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D red_ball;
        Texture2D ball;
        Ball ball_;

        public main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // determine window size. from Singleton class
            _graphics.PreferredBackBufferHeight = Singleton.SCREENHEIGHT;
            _graphics.PreferredBackBufferWidth = Singleton.SCREENWIDTH;
            _graphics.ApplyChanges();
            //
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            red_ball = Content.Load<Texture2D>("picture/red_ball");
            ball = Content.Load<Texture2D>("picture/ball");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _spriteBatch.Draw(ball, new Vector2(100, 700), null, Color.Brown, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
