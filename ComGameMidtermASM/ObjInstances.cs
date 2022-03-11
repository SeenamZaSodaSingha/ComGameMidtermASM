//notused
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;

namespace ComGameMidtermASM
{
    static class ObjInstances
    {
        public static List<GameObjs.GameObj> gameobjs = new List<GameObjs.GameObj>();
        public static GameObjs.Ball[,] ball;
        // next ball indicator.
        public static GameObjs.Ball nextball;
        public static GameObjs.MovingBall movingball;
        public static GameObjs.Gun gun;
        public static GameObjs.Boarder boarder;
        public static Control.Button restartGameButton;
    }
}
