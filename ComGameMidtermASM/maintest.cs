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
        public static List<GameObjs.GameObj> gameobjs = ObjInstances.gameobjs;
        private bool activate = false;
        private Texture2D DefaultTexture;
        private int x, y;
        Random rand = new Random();
        int colorID;

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

            DefaultTexture = Content.Load<Texture2D>("ghost/blue_ghost");

            //load boarder
            ObjInstances.boarder = new GameObjs.Boarder(new Texture2D(_spriteBatch.GraphicsDevice, 1, 1));
            
            //load and set nextball indicater
            ObjInstances.nextball = new GameObjs.Ball(DefaultTexture);
            colorID = rand.Next(0, 6);
            ObjInstances.nextball.SetColor(colorID);
            ObjInstances.nextball._texture = Content.Load<Texture2D>(ObjInstances.nextball.TextureDir);
            ObjInstances.nextball.Position = new Vector2(190, 570);

            //load and set gun and ball
            ObjInstances.gun = new GameObjs.BallShooter(Content.Load<Texture2D>("cannon/base-transparent"), Content.Load <Texture2D>("aim guide line/dot"), DefaultTexture);
            ObjInstances.gun.SetColor((GameObjs.BallShooter.COLOR)ObjInstances.nextball.color);
            ObjInstances.gun.Reset(Content.Load<Texture2D>(ObjInstances.gun.TextureDir));
            ObjInstances.movingball._texture = Content.Load<Texture2D>(ObjInstances.movingball.TextureDir);

            //load ghost 
            //var crosshairTexture = Content.Load<Texture2D>("ghost/crosshairs");
            //background = Content.Load<Texture2D>("ghost/Raccoon_norm");

            // Random ghost color
            ObjInstances.ball = new GameObjs.Ball[9, 8];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if (i % 2 == 1)
                    {
                        ObjInstances.ball[i, j] = new GameObjs.Ball(DefaultTexture)
                        {
                            Position = new Vector2((j * DefaultTexture.Width) + DefaultTexture.Width + Singleton.GAMEPANELLOCX, i * DefaultTexture.Height + DefaultTexture.Height / 2 + Singleton.GAMEPANELLOCY),
                            //color = GetColor(color)
                        };
                    }
                    else
                    {
                        ObjInstances.ball[i, j] = new GameObjs.Ball(DefaultTexture)
                        {
                            Position = new Vector2(j * DefaultTexture.Width + DefaultTexture.Width / 2 + Singleton.GAMEPANELLOCX, i * DefaultTexture.Height + DefaultTexture.Height / 2 + Singleton.GAMEPANELLOCY)
                        };
                    }

                    colorID = rand.Next(0, 6);

                    ObjInstances.ball[i, j].SetColor(colorID);
                    ObjInstances.ball[i, j]._texture = Content.Load<Texture2D>(ObjInstances.ball[i, j].TextureDir);

                }
            }
            //


            gameobjs.Add(ObjInstances.boarder);
            gameobjs.Add(ObjInstances.gun);
            gameobjs.Add(ObjInstances.movingball);

        }


        // TODO: use this.Content to load your game content here


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // update
            foreach (GameObjs.GameObj obj in gameobjs)
            {
                obj.Update(gameTime, gameobjs);

            }

            //update ghost
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if (ObjInstances.ball[i, j] != null)
                    {
                        ObjInstances.ball[i, j].Update(gameTime, gameobjs);
                    }
                }
            }

            //update next ball indicator
            ObjInstances.nextball.Update(gameTime, gameobjs);


            //sucking balls
            if (ObjInstances.movingball.IsActive)
            {
                pos = ObjInstances.movingball.Position;
                activate = true;
            }
            if (!ObjInstances.movingball.IsActive && activate)
            {
                x = (int)Math.Ceiling((pos.X - Singleton.GAMEPANELLOCX) / DefaultTexture.Width - 1);
                y = (int)Math.Ceiling((pos.Y - Singleton.GAMEPANELLOCY)/ DefaultTexture.Height - 1);
                if(y % 2 == 1)
                {
                    if (y < 0) y = 0;
                    if (y > 8) y = 8;
                    ObjInstances.ball[y, x] = new GameObjs.Ball(DefaultTexture)
                    {
                        Position = new Vector2((x * DefaultTexture.Width) + DefaultTexture.Width + Singleton.GAMEPANELLOCX, (y * DefaultTexture.Height) + DefaultTexture.Height / 2 + Singleton.GAMEPANELLOCY),
                    };
                    ObjInstances.ball[y, x].SetColor(ObjInstances.gun.color_);
                    ObjInstances.ball[y, x]._texture = Content.Load<Texture2D>(ObjInstances.ball[y, x].TextureDir);
                    activate = false;
                }
                else
                {
                    if (y < 0) y = 0;
                    if (y > 8) y = 8;
                    ObjInstances.ball[y, x] = new GameObjs.Ball(DefaultTexture)
                    {
                        Position = new Vector2((x * DefaultTexture.Width) + DefaultTexture.Width / 2 + Singleton.GAMEPANELLOCX, (y * DefaultTexture.Height) + DefaultTexture.Height / 2 + Singleton.GAMEPANELLOCY),
                    };
                    ObjInstances.ball[y, x].SetColor(ObjInstances.movingball.color);
                    ObjInstances.ball[y, x]._texture = Content.Load<Texture2D>(ObjInstances.ball[y, x].TextureDir);
                    activate = false;
                }


                //set gun color
                ObjInstances.gun.SetColor((GameObjs.BallShooter.COLOR)ObjInstances.nextball.color);
                ObjInstances.gun.Reset(Content.Load<Texture2D>(ObjInstances.gun.TextureDir));
                ObjInstances.movingball._texture = Content.Load<Texture2D>(ObjInstances.movingball.TextureDir);

                colorID = rand.Next(0, 6);
                ObjInstances.nextball.SetColor(colorID);
                ObjInstances.nextball._texture = Content.Load<Texture2D>(ObjInstances.nextball.TextureDir);

            }


            base.Update(gameTime);
        }
        Vector2 pos;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            foreach (GameObjs.GameObj obj in gameobjs)
            {
                obj.Draw(_spriteBatch);
            }

            //draw ghost
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8 - (i % 2); j++)
                {
                    if (ObjInstances.ball[i, j] != null)
                    {
                        ObjInstances.ball[i, j].Draw(_spriteBatch);
                        print<String>(ObjInstances.ball[i, j].color_.ToString(), j * 25, i * 25 + 200);
                    }
                }
            }


            //draw next ball indicator
            ObjInstances.nextball.Draw(_spriteBatch);

            print<Vector2>(pos, 0, 0);
            print<String>((y.ToString() + " " + x.ToString()), 0, 100);
            print<Vector2>(ObjInstances.ball[y, x].Position - new Vector2(240, 40), 0, 150);

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void print<T>(T stringable, int x, int y)
        {
            _spriteBatch.DrawString(_spriteFont, stringable.ToString(), new Vector2(x, y), Color.Black);
        }
    }
}
