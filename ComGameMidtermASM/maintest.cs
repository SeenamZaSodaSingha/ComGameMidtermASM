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
        private int x, y;
        Random rand = new Random();
        int colorID;
        private Texture2D background;
        List<Texture2D> ball_textures;
        List<Texture2D> gun_textures;
        
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
            ball_textures = new List<Texture2D>
            {
                Content.Load<Texture2D>("ghost/blue_ghost"),
                Content.Load<Texture2D>("ghost/cyan_ghost"),
                Content.Load<Texture2D>("ghost/magen_ghost"),
                Content.Load<Texture2D>("ghost/orage_ghost"),
                Content.Load<Texture2D>("ghost/pink_ghost"),
                Content.Load<Texture2D>("ghost/red_ghost"),
                Content.Load<Texture2D>("ghost/yellow_ghost")
            };

            gun_textures = new List<Texture2D>
            {
                Content.Load<Texture2D>("cannon/canon-original-cyan"),
                Content.Load<Texture2D>("cannon/canon-original-magen"),
                Content.Load<Texture2D>("cannon/canon-original-orange"),
                Content.Load<Texture2D>("cannon/canon-original-pink"),
                Content.Load<Texture2D>("cannon/canon-original-red"),
                Content.Load<Texture2D>("cannon/canon-original-yellow")
            };


            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("fonts/GameText");

            ball_textures[0] = Content.Load<Texture2D>("ghost/blue_ghost");
            background = Content.Load<Texture2D>("Raccoon_norm");

            //load boarder
            ObjInstances.boarder = new GameObjs.Boarder(new Texture2D(_spriteBatch.GraphicsDevice, 1, 1));

            //load and set nextball indicater
            ObjInstances.nextball = new GameObjs.Ball(ball_textures);
            colorID = rand.Next(1, 7);
            ObjInstances.nextball.SetColor(colorID);
            ObjInstances.nextball.Position = new Vector2(190, 570);

            //load and set gun and ball
            ObjInstances.gun = new GameObjs.BallShooter(gun_textures, Content.Load<Texture2D>("aim guide line/dot"), ball_textures);
            ObjInstances.gun.SetColor((GameObjs.BallShooter.COLOR)ObjInstances.nextball.color);

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
                        ObjInstances.ball[i, j] = new GameObjs.Ball(ball_textures)
                        {
                            Position = new Vector2((j * ball_textures[0].Width) + ball_textures[0].Width + Singleton.GAMEPANELLOCX, i * ball_textures[0].Height + ball_textures[0].Height / 2 + Singleton.GAMEPANELLOCY),
                            //color = GetColor(color)
                        };
                    }
                    else
                    {
                        ObjInstances.ball[i, j] = new GameObjs.Ball(ball_textures)
                        {
                            Position = new Vector2(j * ball_textures[0].Width + ball_textures[0].Width / 2 + Singleton.GAMEPANELLOCX, i * ball_textures[0].Height + ball_textures[0].Height / 2 + Singleton.GAMEPANELLOCY)
                        };
                    }

                    colorID = rand.Next(1, 7);

                    ObjInstances.ball[i, j].SetColor(colorID);

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
                x = (int)Math.Ceiling((pos.X - Singleton.GAMEPANELLOCX) / ball_textures[0].Width - 1);
                y = (int)Math.Ceiling((pos.Y - Singleton.GAMEPANELLOCY) / ball_textures[0].Height - 1);
                if (y % 2 == 1)
                {
                    if (y < 0) y = 0;
                    if (y > 8) y = 8;
                    ObjInstances.ball[y, x] = new GameObjs.Ball(ball_textures)
                    {
                        Position = new Vector2((x * ball_textures[0].Width) + ball_textures[0].Width + Singleton.GAMEPANELLOCX, (y * ball_textures[0].Height) + ball_textures[0].Height / 2 + Singleton.GAMEPANELLOCY),
                    };
                    ObjInstances.ball[y, x].SetColor(ObjInstances.movingball.color_);
                    activate = false;
                }
                else
                {
                    if (y < 0) y = 0;
                    if (y > 8) y = 8;
                    ObjInstances.ball[y, x] = new GameObjs.Ball(ball_textures)
                    {
                        Position = new Vector2((x * ball_textures[0].Width) + ball_textures[0].Width / 2 + Singleton.GAMEPANELLOCX, (y * ball_textures[0].Height) + ball_textures[0].Height / 2 + Singleton.GAMEPANELLOCY),
                    };
                    ObjInstances.ball[y, x].SetColor(ObjInstances.movingball.color_);
                    activate = false;
                }


                //set gun color
                ObjInstances.gun.SetColor((GameObjs.BallShooter.COLOR)ObjInstances.nextball.color);
                ObjInstances.gun.Reset(Content.Load<Texture2D>(ObjInstances.gun.TextureDir));

                colorID = rand.Next(1, 7);
                ObjInstances.nextball.SetColor(colorID);


                //Logic of deleting balls
                //How to Remove
                bool checking = true;
                while (checking)
                {
                    if (x % 2 == 0)
                    {
                        //top
                        if (x - 1 >= 0 && y - 1 >= 0 && ObjInstances.ball[y - 1, x - 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x - 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                        if (y - 1 >= 0 && ObjInstances.ball[y - 1, x] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }

                        //side
                        if (x + 1 <= 6 && ObjInstances.ball[y, x + 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y, x + 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                        if (x - 1 >= 0 && ObjInstances.ball[y, x - 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y, x - 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }

                        //bottom
                        if (y + 1 <= 9 && x - 1 >= 0 && ObjInstances.ball[y + 1, x - 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x - 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                        if (y + 1 <= 9 && ObjInstances.ball[y + 1, x] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                    }
                    else
                    {
                        //top
                        if (ObjInstances.ball[y - 1, x + 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x + 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                        if (ObjInstances.ball[y - 1, x] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }

                        //side
                        if (ObjInstances.ball[y, x + 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y, x + 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                        if (ObjInstances.ball[y, x - 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y, x - 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }

                        //bottom
                        if (ObjInstances.ball[y + 1, x + 1] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x + 1].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                        if (ObjInstances.ball[y + 1, x] != null && ObjInstances.ball[y, x].color_ == ObjInstances.ball[y - 1, x].color_)
                        {
                            ObjInstances.ball[y, x].SetColor(0);
                        }
                    }
                    checking = false;
                }

            }


            base.Update(gameTime);
        }

        Vector2 pos;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(Singleton.GAMEPANELLOCX, Singleton.GAMEPANELLOCY), null, Color.White);
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
            print<String>(ObjInstances.movingball.color_.ToString(), 200, 150);
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
