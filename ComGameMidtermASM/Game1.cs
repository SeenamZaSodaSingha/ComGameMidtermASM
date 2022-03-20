using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ComGameMidtermASM.State;
using Microsoft.Xna.Framework.Audio;


namespace ComGameMidtermASM
{
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        //seenams work
        private State.State _currentState;

        private State.State _nextState;

        private SoundEffectInstance bgmusic;
        //end of seenam work

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //seenams work

        public void ChangeState(State.State state)
        {
            _nextState = state;
        }



        //end seenams work
        
        protected override void Initialize()
        {
            IsMouseVisible = true;

            _graphics.PreferredBackBufferHeight = 680;
            _graphics.PreferredBackBufferWidth = 800;

            IsMouseVisible = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);
            bgmusic = Content.Load<SoundEffect>("BGMusic/Delfino Plaza - Super Mario Sunshine [OST]").CreateInstance();
            
            bgmusic.IsLooped = true;
            bgmusic.Volume = 0.5f;
            bgmusic.Play();
        }

        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            
            if(_nextState != null)
            {
                bgmusic.Stop();
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            if(Singleton.CurrentGameState == Singleton.GameState.GameLose)
            {
                _graphics.IsFullScreen = true;
                _graphics.PreferredBackBufferWidth = 1920;
                _graphics.PreferredBackBufferHeight = 1080;
                _graphics.ApplyChanges();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _currentState.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }
    }
}
