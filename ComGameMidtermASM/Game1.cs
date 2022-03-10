﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using ComGameMidtermASM.Control;
using ComGameMidtermASM.State;


namespace ComGameMidtermASM
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //private Color _backgroundColour = Color.CornflowerBlue;

        //private List<Component> _gameComponents;

        //seenams work
        private State.State _currentState;

        private State.State _nextState;

        //end of seenam work

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //seenams work

        public void ChangeState(State.State state)
        {
            _nextState = state;
        }



        //end seenams work
        
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 680;
            graphics.PreferredBackBufferWidth = 800;

            IsMouseVisible = true;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);
            /*
            var randomButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 200),
                Text = "Random",
            };

            randomButton.Click += RandomButton_Click;

            var quitButton = new Button(Content.Load<Texture2D>("Controls/Button"), Content.Load<SpriteFont>("Fonts/Font"))
            {
                Position = new Vector2(350, 250),
                Text = "Quit",
            };

            quitButton.Click += QuitButton_Click;
            
            _gameComponents = new List<Component>()
      {
        randomButton,
        quitButton,
      };*/
        }

        /*private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }*/

        /*
        private void RandomButton_Click(object sender, System.EventArgs e)
        {
            var random = new Random();

            _backgroundColour = new Color(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
        }*/

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }

            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            //foreach (var component in _gameComponents)
                //component.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _currentState.Draw(gameTime, spriteBatch);

            base.Draw(gameTime);
        }
    }
}
