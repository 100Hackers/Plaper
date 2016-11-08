using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Plaper {
    class Output {
        public static SpriteFont font;
        public static SpriteFont font12;
        public static SpriteFont font24;
        public static SpriteFont font36;
        public static SpriteFont font72;
        public static SpriteFont font108;
        public static SpriteFont font144;
		public static SpriteFont font10;

        public static RenderTarget2D playArea;
        public static SoundEffect jump, losingSound;
        public static SoundEffect[] wallHits = new SoundEffect[2];

        public static Rectangle playRectangle;
        public static GraphicsDeviceManager graphics;


        public static void Initialize(GraphicsDeviceManager graphics) {

            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            var screenSize = graphics.GraphicsDevice.Viewport.Bounds;
            Plaper.screenHeight = screenSize.Height;
            Plaper.screenWidth  = screenSize.Width;

            Plaper.windowHeight = Plaper.screenHeight;
            Plaper.windowWidth = Plaper.screenWidth;

            Plaper.playHeight = Plaper.screenHeight;
            Plaper.playWidth  = Plaper.screenHeight / 2;

            int H = displayMode.Width;

            graphics.IsFullScreen = false;

            Output.graphics = graphics;

            graphics.PreferredBackBufferHeight = Plaper.windowHeight;
            graphics.PreferredBackBufferWidth  = H;
            graphics.ApplyChanges();
            //graphics.GraphicsDevice.Viewport.

            playRectangle = new Rectangle((H - Plaper.playWidth) / 2, 0, Plaper.playWidth, Plaper.playHeight);

            Plaper.playWidth *= 1;
            Plaper.playHeight *= 1;
            playArea = new RenderTarget2D(graphics.GraphicsDevice, Plaper.playWidth, Plaper.playHeight, false, SurfaceFormat.Color, DepthFormat.None, 2, RenderTargetUsage.DiscardContents);
        }
    }
}