using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Output {
        public static SpriteFont font;
        public static SpriteFont font12;
        public static SpriteFont font24;
        public static SpriteFont font36;
		public static SpriteFont font10;
        public static SpriteFont font72;
        public static SpriteFont font144;
        public static SpriteFont font108;

        public static RenderTarget2D playArea;
        public static SoundEffect jump, losingSound;
        public static SoundEffect[] wallHits = new SoundEffect[2];

        public static Rectangle playRectangle;


        public static void Initialize(GraphicsDeviceManager graphics) {

            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            Plaper.windowHeight = Plaper.SCREEN_HEIGHT;
            Plaper.windowWidth  = Plaper.SCREEN_WIDTH;

            Plaper.screenHeight = displayMode.Height;
            Plaper.screenWidth  = displayMode.Width;

            Plaper.playHeight = Plaper.SCREEN_HEIGHT;
            Plaper.playWidth  = Plaper.SCREEN_HEIGHT / 2;

            if (Game1.makeFullscreen) {
                Plaper.playHeight = Plaper.screenHeight;
                Plaper.playWidth  = Plaper.screenHeight / 2;
                Plaper.windowHeight = Plaper.screenHeight;
                Plaper.windowWidth = Plaper.screenWidth;
                graphics.IsFullScreen = !graphics.IsFullScreen;
            }

            graphics.PreferredBackBufferHeight = Plaper.windowHeight;
            graphics.PreferredBackBufferWidth  = Plaper.windowWidth;
            graphics.ApplyChanges();

            playRectangle = new Rectangle((Plaper.windowWidth - Plaper.playWidth) / 2, 0, Plaper.playWidth, Plaper.playHeight);

            Plaper.playWidth *= 1;
            Plaper.playHeight *= 1;
            playArea = new RenderTarget2D(graphics.GraphicsDevice, Plaper.playWidth, Plaper.playHeight, false, SurfaceFormat.Color, DepthFormat.None, 2, RenderTargetUsage.DiscardContents);
        }
    }
}
