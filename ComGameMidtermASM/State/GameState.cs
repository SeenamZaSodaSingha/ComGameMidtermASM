﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using ComGameMidtermASM.GameObjs;
using ComGameMidtermASM.Control;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using System;


namespace ComGameMidtermASM.State
{
    public class GameState : State
    {
        private GraphicsDevice _graphics;
        private GraphicsDeviceManager graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;
        public static List<GameObj> gameobjs = ObjInstances.gameobjs;
        private bool activate = false;


        private int x, y;
        Random rand = new Random();
        int colorID;
        private int count;
        private Texture2D background, losescreen, restartIdle, restartHover;
        List<Texture2D> ball_textures;
        List<Texture2D> gun_textures;
        List<Texture2D> pac_texturesL;
        List<Texture2D> pac_texturesR;
        private SoundEffectInstance bounce, click, boom, moving, hit;
        private int turn;
        Vector2 pos;

        int increase;

        int width = 60;
        int height = 60;

        public GameState(Game1 game, GraphicsDevice _graphicsDevice, ContentManager Content) : base(game, _graphicsDevice, Content)
        {
            turn = 0;
            Singleton.CurrentGameState = Singleton.GameState.GamePlaying;
            //graphics = new GraphicsDeviceManager(this);


            // TODO extrac medthoid loader
            ball_textures = new List<Texture2D>
            {
                _content.Load<Texture2D>("ghost/blue_ghost"),
                _content.Load<Texture2D>("ghost/cyan_ghost"),
                _content.Load<Texture2D>("ghost/magen_ghost"),
                _content.Load<Texture2D>("ghost/orage_ghost"),
                _content.Load<Texture2D>("ghost/pink_ghost"),
                _content.Load<Texture2D>("ghost/red_ghost"),
                _content.Load<Texture2D>("ghost/yellow_ghost")
            };

            gun_textures = new List<Texture2D>
            {
                _content.Load<Texture2D>("cannon/base-transparent"),
                _content.Load<Texture2D>("cannon/canon-original-cyan"),
                _content.Load<Texture2D>("cannon/canon-original-magen"),
                _content.Load<Texture2D>("cannon/canon-original-orange"),
                _content.Load<Texture2D>("cannon/canon-original-pink"),
                _content.Load<Texture2D>("cannon/canon-original-red"),
                _content.Load<Texture2D>("cannon/canon-original-yellow")
            };

            pac_texturesL = new List<Texture2D>
            {
                _content.Load<Texture2D>("aim guide line/dot"),
                _content.Load<Texture2D>("pacman-left-color/cyan-pac/1"),
                _content.Load<Texture2D>("pacman-left-color/magen-pac/1"),
                _content.Load<Texture2D>("pacman-left-color/orange-pac/1"),
                _content.Load<Texture2D>("pacman-left-color/pink-pac/1"),
                _content.Load<Texture2D>("pacman-left-color/red-pac/1"),
                _content.Load<Texture2D>("pacman-left-color/yellow-pac/1"),

                _content.Load<Texture2D>("pacman-left-color/cyan-pac/2"),
                _content.Load<Texture2D>("pacman-left-color/magen-pac/2"),
                _content.Load<Texture2D>("pacman-left-color/orange-pac/2"),
                _content.Load<Texture2D>("pacman-left-color/pink-pac/2"),
                _content.Load<Texture2D>("pacman-left-color/red-pac/2"),
                _content.Load<Texture2D>("pacman-left-color/yellow-pac/2"),

                _content.Load<Texture2D>("pacman-left-color/cyan-pac/3"),
                _content.Load<Texture2D>("pacman-left-color/magen-pac/3"),
                _content.Load<Texture2D>("pacman-left-color/orange-pac/3"),
                _content.Load<Texture2D>("pacman-left-color/pink-pac/3"),
                _content.Load<Texture2D>("pacman-left-color/red-pac/3"),
                _content.Load<Texture2D>("pacman-left-color/yellow-pac/3"),
            };

            pac_texturesR = new List<Texture2D>
            {
                _content.Load<Texture2D>("aim guide line/dot"),
                _content.Load<Texture2D>("pacman-right-color/cyan-pac/1"),
                _content.Load<Texture2D>("pacman-right-color/magen-pac/1"),
                _content.Load<Texture2D>("pacman-right-color/orange-pac/1"),
                _content.Load<Texture2D>("pacman-right-color/pink-pac/1"),
                _content.Load<Texture2D>("pacman-right-color/red-pac/1"),
                _content.Load<Texture2D>("pacman-right-color/yellow-pac/1"),

                _content.Load<Texture2D>("pacman-right-color/cyan-pac/2"),
                _content.Load<Texture2D>("pacman-right-color/magen-pac/2"),
                _content.Load<Texture2D>("pacman-right-color/orange-pac/2"),
                _content.Load<Texture2D>("pacman-right-color/pink-pac/2"),
                _content.Load<Texture2D>("pacman-right-color/red-pac/2"),
                _content.Load<Texture2D>("pacman-right-color/yellow-pac/2"),

                _content.Load<Texture2D>("pacman-right-color/cyan-pac/3"),
                _content.Load<Texture2D>("pacman-right-color/magen-pac/3"),
                _content.Load<Texture2D>("pacman-right-color/orange-pac/3"),
                _content.Load<Texture2D>("pacman-right-color/pink-pac/3"),
                _content.Load<Texture2D>("pacman-right-color/red-pac/3"),
                _content.Load<Texture2D>("pacman-right-color/yellow-pac/3"),
            };

            restartIdle = _content.Load<Texture2D>("Control/restart_idle");
            restartHover = _content.Load<Texture2D>("Control/restart_hover");

            //ObjInstances.restartGameButton = new Button(restartIdle, restartHover, _spriteFont)
            //{
            //    Position = new Vector2(39, 459),
            //};

            //ObjInstances.restartGameButton.Click += restartGameButton_Click;

            background = _content.Load<Texture2D>("Raccoon_norm");
            losescreen = _content.Load<Texture2D>("Gameover3");

            boom = _content.Load<SoundEffect>("Witcher 3 Quest completed - [HQ] Sound Effect").CreateInstance();
            bounce = _content.Load<SoundEffect>("MARIO JUMP SOUND EFFECT (FREE DOWNLOAD)").CreateInstance();
            click = _content.Load<SoundEffect>("Project-nebula_bullet").CreateInstance();
            moving = _content.Load<SoundEffect>("pacman_chomp").CreateInstance();
            hit = _content.Load<SoundEffect>("Project-Nenula_Click").CreateInstance();


            // assign volume
            bounce.Volume = 0.4f * Singleton.MAINVOLUME;
            click.Volume = 1.0f * Singleton.MAINVOLUME;
            boom.Volume = 0.7f * Singleton.MAINVOLUME;
            moving.Volume = 0.5f * Singleton.MAINVOLUME;
            hit.Volume = 0.8f * Singleton.MAINVOLUME;

            //FIXME
            _spriteBatch = new SpriteBatch(_graphicsDevice);
            _spriteFont = _content.Load<SpriteFont>("fonts/GameText");


            Singleton.Instance.sound = moving;
            //load boarder
            ObjInstances.boarder = new GameObjs.Boarder(new Texture2D(_spriteBatch.GraphicsDevice, 1, 1));

            //load and set nextball indicater
            ObjInstances.nextball = new GameObjs.Ball(ball_textures);
            colorID = rand.Next(1, 7);
            ObjInstances.nextball.SetColor(colorID);
            ObjInstances.nextball.Position = new Vector2(190, 570);

            //load and set gun and ball
            ObjInstances.gun = new GameObjs.Gun(gun_textures, _content.Load<Texture2D>("aim guide line/dot"), pac_texturesR, pac_texturesL, click, bounce);
            ObjInstances.gun.SetColor(ObjInstances.nextball.color_);

            //load ghost 
            //var crosshairTexture = _content.Load<Texture2D>("ghost/crosshairs");
            //background = _content.Load<Texture2D>("ghost/Raccoon_norm");


            //TODO: extract medthoid here
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
                            Position = new Vector2((j * width) + width + Singleton.GAMEPANELLOCX, i * height + height / 2 + Singleton.GAMEPANELLOCY),
                            //color = GetColor(color)
                        };
                    }
                    else
                    {
                        ObjInstances.ball[i, j] = new GameObjs.Ball(ball_textures)
                        {
                            Position = new Vector2(j * width + width / 2 + Singleton.GAMEPANELLOCX, i * height + height / 2 + Singleton.GAMEPANELLOCY)
                        };
                    }

                    colorID = rand.Next(1, 7);

                    ObjInstances.ball[i, j].SetColor(colorID);

                }
            }

            gameobjs.Add(ObjInstances.boarder);
            gameobjs.Add(ObjInstances.gun);
            gameobjs.Add(ObjInstances.movingball);

        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //FIXME
                _game.Exit();

            //TODO: extract medthoid here
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

            //ObjInstances.restartGameButton.Update(gameTime);

            //update next ball indicator
            ObjInstances.nextball.Update(gameTime, gameobjs);


            //TODO: extract medthoid here
            //sucking balls
            if (ObjInstances.movingball.IsActive)
            {
                Play(moving);
                pos = ObjInstances.movingball.Position;
                activate = true;
                count = 0;
            }
            if (!ObjInstances.movingball.IsActive && activate)
            {
                Play(hit);
                turn++;
                x = (int)Math.Ceiling((pos.X - Singleton.GAMEPANELLOCX) / width - 1);
                y = (int)Math.Ceiling((pos.Y - Singleton.GAMEPANELLOCY) / height - 1);

                if (y < 8)
                {
                    if ((y + increase) % 2 == 1)
                    {
                        if (y < 0) y = 0;
                        ObjInstances.ball[y, x] = new GameObjs.Ball(ball_textures)
                        {
                            Position = new Vector2((x * width) + width + Singleton.GAMEPANELLOCX, (y * height) + height / 2 + Singleton.GAMEPANELLOCY),
                            visit = false,
                            Destroy = false,
                        };
                        ObjInstances.ball[y, x].SetColor(ObjInstances.movingball.color_);
                        activate = false;
                    }
                    else
                    {
                        if (y < 0) y = 0;
                        ObjInstances.ball[y, x] = new GameObjs.Ball(ball_textures)
                        {
                            Position = new Vector2((x * width) + width / 2 + Singleton.GAMEPANELLOCX, (y * height) + height / 2 + Singleton.GAMEPANELLOCY),
                        };
                        ObjInstances.ball[y, x].SetColor(ObjInstances.movingball.color_);
                        activate = false;
                    }



                    //set gun color
                    ObjInstances.gun.SetColor(ObjInstances.nextball.color_);

                    colorID = rand.Next(1, 7);
                    ObjInstances.nextball.SetColor(colorID);


                    CheckBall(ObjInstances.ball, ObjInstances.ball[y, x].color_, y, x);
                }
                // cheak if lose
                else
                { 
                    Singleton.CurrentGameState = Singleton.GameState.GameLose;
                }


                //TODO extract med
                //Logic of deleting balls
                //How to Remove
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (ObjInstances.ball[i, j] != null)
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
                                    //ObjInstances.ball[i, j].Position = new Vector2(0, 0);
                                    //ObjInstances.ball[i, j].color_ = -1;
                                    //ObjInstances.ball[i, j].Destroy = false;
                                    ObjInstances.ball[i, j] = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (ObjInstances.ball[i, j] != null)
                            {
                                ObjInstances.ball[i, j].Destroy = false;
                            }
                        }
                    }
                }
                if (turn == 2)
                {
                    boom.Play();
                    increase++;
                    for (int i = 8; i >= 0; i--)
                    {
                        for (int j = 7; j >= 0; j--)
                        {
                            if (ObjInstances.ball[i, j] != null && i + 1 <= 7)
                            {
                                ObjInstances.ball[i + 1, j] = ObjInstances.ball[i, j];
                                ObjInstances.ball[i, j].Position += new Vector2(0, height);

                            }
                            if (i == 0)
                            {
                                ObjInstances.ball[i, j] = null;
                            }
                        }
                    }
                    turn = 0;
                }


            }

            //FIXME
            //_game.Update(gameTime);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //FIXME
            _graphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, new Vector2(Singleton.GAMEPANELLOCX, Singleton.GAMEPANELLOCY), null, Color.White);


            //TODO: extract medthoid here
            //draw gun balls barder
            foreach (GameObjs.GameObj obj in gameobjs)
            {
                obj.Draw(_spriteBatch);
            }
            
            //ObjInstances.restartGameButton.Draw(gameTime ,_spriteBatch);

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


            //for debug
            print<Vector2>(pos, 0, 0);
            print<String>((y.ToString() + " " + x.ToString()), 0, 100);
            //print<Vector2>(ObjInstances.ball[y, x].Position - new Vector2(240, 40), 0, 150);
            print<String>(ObjInstances.movingball.color_.ToString(), 200, 150);


            //TODO: extract medthoid here
            //draw lose screen.
            if (Singleton.CurrentGameState == Singleton.GameState.GameLose)
            {
                _spriteBatch.Draw(losescreen, new Vector2(0, 0), null, Color.White);
            }

            _spriteBatch.End();
        }

        private void restartGameButton_Click(object sender, EventArgs e)
        {
            //do restart
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public void print<T>(T stringable, int x, int y)
        {
            _spriteBatch.DrawString(_spriteFont, stringable.ToString(), new Vector2(x, y), Color.Black);
        }



        public void CheckBall(GameObjs.Ball[,] ball, int color, int x, int y)
        {
            //if ((me.X >= 0 && me.Y >= 0) && (me.X <= 7 && me.Y <= 8) && (gameObjects[(int)me.Y, (int)me.X] != null && gameObjects[(int)me.Y, (int)me.X].color == ColorTarget))
            if (((x >= 0 && y >= 0) && (x <= 7 && y < 8)))
                if ((ball[x, y] != null) && (!ball[x, y].visit) && (ball[x, y].color_ == color))
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
        void Play(SoundEffectInstance ins)
        {
            if (Singleton.CurrentGameState == Singleton.GameState.GamePlaying)
            {
                ins.Play();
            }
        }
    }
}
