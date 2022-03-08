// class contain constant value.
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using ComGameMidtermASM.GameObj;

namespace ComGameMidtermASM
{
    class Singleton
    {
        // windows size.
        public const int SCREENWIDTH = 480; //ชนาดจอ
        public const int SCREENHEIGHT = 600;

        //TODO: Game State Machine

        public enum GameState
        {
            GameMain,
            GameStart,
            GamePlaying,
            GameWin,
            GameLose,
            GameEnded,
        }
 
        public GameState CurrentGameState;
        

        public enum GameResult
        {
            Win,
            Lose
        }

        public GameResult CurrentGameResult;

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
