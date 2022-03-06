using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ComGameMidtermASM.GameObjs
{
    public class Ball : GameObj
    {
        public enum COLOR
        {
            red,
            yellow,
            purple,
            cyan
        }
        
        public COLOR color;

        public Ball(Texture2D texture, int color_) : base(texture)
        {
            switch (color_)
            {
                case 0:
                    {
                        color = COLOR.red;
                        break;
                    }
                case 1:
                    {
                        color = COLOR.yellow;
                        break;
                    }
                case 2:
                    {
                        color = COLOR.purple;
                        break;
                    }
                case 3:
                    {
                        color = COLOR.cyan;
                        break;
                    }
            }
        }

    }
}
