﻿// class contain constant value.
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
//using ComGameMidtermASM.GameObj;

namespace ComGameMidtermASM
{
    class Singleton
    {
        // windows size.
        public const int SCREENWIDTH = 800; //ชนาดจอ
        public const int SCREENHEIGHT = 680;

        // game panel size.
        public const int GAMEPANELWIDTH = 480;
        public const int GAMEPANELHEIGHT = 600;

        // game panel location.
        public const int GAMEPANELLOCX = 280;
        public const int GAMEPANELLOCY = 40;

        // game panel border width.
        public const int BOARDERWIDTH = 1;


        public const int BALLSPEED = 5;

        public const int GUNPOSITIONX = GAMEPANELLOCX + (GAMEPANELWIDTH / 2);
        public const int GUNPOSITIONY = GAMEPANELLOCY + (GAMEPANELHEIGHT) - 50;


        public enum GameState
        {
            GamePlaying,
            GameWin,
            GameLose,
        }

        public static GameState CurrentGameState;

        //NOTE: may not use this.
        public enum GameResult
        {
            Win,
            Lose
        }

        public GameResult CurrentGameResult;
        //
        
        public KeyboardState PreviousKey, CurrentKey;
        public MouseState CurrentMouse;

        private static Singleton instance;

        private Singleton()
        {

        }
        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Singleton();
                }
                return instance;
            }
        }

    }
}
