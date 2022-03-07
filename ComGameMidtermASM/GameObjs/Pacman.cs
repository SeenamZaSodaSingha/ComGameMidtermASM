using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM.GameObjs
{
    class Pacman : GameObj
    {
        public enum COLOR
        {
            cyan,
            magenta,
            orange,
            pink,
            red,
            yellow
        }

        public COLOR color;

        public Pacman(Texture2D texture, int color_) : base(texture)
        {
            switch (color_)
            {
                case 0:
                    {
                        color = COLOR.cyan;
                        break;
                    }
                case 1:
                    {
                        color = COLOR.magenta;
                        break;
                    }
                case 2:
                    {
                        color = COLOR.orange;
                        break;
                    }
                case 3:
                    {
                        color = COLOR.pink;
                        break;
                    }
                case 4:
                    {
                        color = COLOR.red;
                        break;
                    }
                case 5:
                    {
                        color = COLOR.yellow;
                        break;
                    }
            }
        }

    }
}

