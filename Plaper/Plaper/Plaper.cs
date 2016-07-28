using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Plaper {

        // Screen variables
        public const int 
            SCREEN_WIDTH  = 400,
            SCREEN_HEIGHT = 600,
            SHIFT_SPEED   = 300;
        
        public static int
            screenHeight,
            screenWidth,
            playHeight,
            playWidth;

        // Sprite ratios, should be height / width
        public const double PLAYER_RATIO = 17.0 /  14.0; // remember there are actually three sprites in this file
        public const double PLAT_RATIO   = 50.0 / 250.0;

        // Sprite scales
        public const double PLAYER_SCALE  = 0.07;
        public const double PLAT_SCALE    = 0.17;
        public const double BUTTON_HEIGHT = 0.1;
        public const double START_HEIGHT  = 0.09;

        // Physics
        public const double GRAVITY    = 1.2;
        public const double JUMP_SPEED = 0.015;
        

        public static KeyboardState keyboardState, lastKeyboardState;

        public static GameTime gameTime;
    }
}
