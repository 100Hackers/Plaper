using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Plaper {
        public const int 
            GRAVITY = 400,
            SCREEN_WIDTH  = 400,
            SCREEN_HEIGHT = 600,
            SHIFT_SPEED   = 300;


        public static int
            screenHeight,
            screenWidth,
            height,
            width;

        public const double PLAYER_SCALE = 3.0;
        public const double PLAT_SCALE   = 1.0;
        

        public static KeyboardState keyboardState, lastKeyboardState;

        public static GameTime gameTime;
    }
}
