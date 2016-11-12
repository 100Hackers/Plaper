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
            SCREEN_WIDTH  = 600,
            SCREEN_HEIGHT = 800;
        
        public static int
            screenHeight,
            screenWidth,
            windowHeight,
            windowWidth,
            playHeight,
            playWidth;

        public static int highScore = 0;

        // Sprite ratios, should be height / width
        public const double PLAYER_RATIO = 17.0 /  14.0; // remember there are actually three sprites in this file
        public const double PLAT_RATIO   = 50.0 / 250.0;

        // Sprite scales
        public const double PLAYER_SCALE  = 0.06;
        public const double PLAT_SCALE    = 0.13;
        public const double BUTTON_HEIGHT = 0.0012;
        public const double START_HEIGHT  = 0.09;

        // Physics
        public const double GRAVITY      =   0.7;
        public const double JUMP_SPEED   =   0.011;
        public const double ARROW_SPEED  =   2.5;
        public const double FILL_SPEED   = 150.0;
        public const double SCROLL_SPEED =   0.5;

        // Slide amout (when player lands on edge of platform)
        public const int SLIDE_SCALE = 300;

        public static bool debugMode = false;

        public static GameTime gameTime;
    }
}