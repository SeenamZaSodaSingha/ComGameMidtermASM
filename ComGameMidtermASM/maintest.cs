﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ComGameMidtermASM.GameObjs;
using System;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace ComGameMidtermASM
{
    public class maintest : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        public static List<GameObjs.GameObj> gameobjs = ObjInstances.gameobjs;
        private bool activate = false;
        List<GameObj> _gameObj;
        
        private Texture2D DefaultTexture;
        private int x, y;
        Random rand = new Random();
        int colorID;
        private int count;
        private Texture2D background;
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
            _gameObj = new List<GameObj>();
            _graphics.ApplyChanges();

            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _spriteFont = Content.Load<SpriteFont>("fonts/GameText");

            DefaultTexture = Content.Load<Texture2D>("ghost/blue_ghost");
            background = Content.Load<Texture2D>("Raccoon_norm");

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
                count = 0;
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
                        visit = false,
                        Destroy = false,
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
                    ObjInstances.ball[y, x].SetColor(ObjInstances.gun.color_);
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


                //Logic of deleting balls
                //How to Remove
                CheckBall(ObjInstances.ball, ObjInstances.ball[y, x].color_, y, x);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if(ObjInstances.ball[i, j] != null)
                        {
                            ObjInstances.ball[i, j].visit = false;
                            if (ObjInstances.ball[i, j].Destroy == true)
                            {
                                count++;
                            }
                        }
                    }
                }
                if (count >= 3)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (ObjInstances.ball[i, j] != null)
                            {
                                if (ObjInstances.ball[i, j].Destroy == true)
                                {
                                    ObjInstances.ball[i, j].Position = new Vector2(0, 0);
                                    ObjInstances.ball[i, j].color_ = -1;
                                }
                            }
                        }
                    }
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
            //print<Vector2>(ObjInstances.ball[y, x].Position - new Vector2(240, 40), 0, 150);
            print<String>(ObjInstances.movingball.color_.ToString(), 200, 150);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void print<T>(T stringable, int x, int y)
        {
            _spriteBatch.DrawString(_spriteFont, stringable.ToString(), new Vector2(x, y), Color.Black);
        }

        public void CheckBall(GameObjs.Ball[,] ball, int color, int x, int y)
        {
            //if ((me.X >= 0 && me.Y >= 0) && (me.X <= 7 && me.Y <= 8) && (gameObjects[(int)me.Y, (int)me.X] != null && gameObjects[(int)me.Y, (int)me.X].color == ColorTarget))
            if(((x >= 0 && y >= 0) && (x <= 8 && y <= 7)) && (ball[x, y] != null) && (!ball[x, y].visit) &&  (ball[x, y].color_ == color))
            {
                ball[x, y].visit = true;
                ball[x, y].Destroy = true;
                //ball[x, y] = null;
                CheckBall(ball, color, x - 1, y);//Left
                CheckBall(ball, color, x + 1, y); // Right
                if (y % 2 == 0)
                {
                    CheckBall(ball, color, x, y - 1); // Top Right
                    CheckBall(ball, color, x - 1, y - 1); // Top Left
                    CheckBall(ball, color, x, y + 1); // Bot Right
                    CheckBall(ball, color, x - 1, y + 1); // Bot Left
                }
                else
                {
                    CheckBall(ball, color, x + 1, y - 1); // Top Right
                    CheckBall(ball, color, x, y - 1); // Top Left
                    CheckBall(ball, color, x + 1, y + 1); // Bot Right
                    CheckBall(ball, color, x, y + 1); // Bot Left	
                }
            }
            else
            {
                return;
            }
        }
    }
}