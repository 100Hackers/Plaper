using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Plaper {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const bool makeFullscreen = false;

        public const int buttonHeight = Plaper.SCREEN_HEIGHT / 5;
        public const int buttonSpacing = buttonHeight / 3;

        // Textures for Player
        Texture2D sprite;
        Texture2D platformTex;
        Texture2D arrow;
        Texture2D arrowFill;

        // Platform constants
        const double PLAT_SCALE = 0.5;
        const int PLAT_H = (int) (50 * PLAT_SCALE);
        const int PLAT_W = (int) (500 * PLAT_SCALE);

        //used for generating platform
        Random rand = new Random();

        State currentState;

        public static SpriteFont font;
        public static SpriteFont font12;
        public static SpriteFont font24;
        public static SpriteFont font36;
		public /*static*/ SpriteFont font10;

        RenderTarget2D playArea;

        public Game1() {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set screen height and width
            graphics.PreferredBackBufferHeight = Plaper.SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = Plaper.SCREEN_HEIGHT / 2;

            // unrestricted framerate
            //this.IsFixedTimeStep = false;
            //this.graphics.SynchronizeWithVerticalRetrace = false;
        }

        public Texture2D Sprite {
            get { return sprite; }
        }

        public Texture2D Arrow {
            get { return arrow; }
        }

        public Texture2D ArrowFill {
            get { return arrowFill; }
        }

        public Texture2D PlatformTex {
            get { return platformTex; }
        }

        public GraphicsDeviceManager Graphics {
            get { return graphics; }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            base.Initialize();

            
            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;

            Plaper.windowHeight = Plaper.SCREEN_HEIGHT;
            Plaper.windowWidth  = Plaper.SCREEN_WIDTH;

            Plaper.screenHeight = displayMode.Height;
            Plaper.screenWidth  = displayMode.Width;

            Plaper.playHeight = Plaper.SCREEN_HEIGHT;
            Plaper.playWidth  = Plaper.SCREEN_HEIGHT / 2;

            if (makeFullscreen) {
                Plaper.playHeight = Plaper.screenHeight;
                Plaper.playWidth  = Plaper.screenHeight / 2;
                Plaper.windowHeight = Plaper.screenHeight;
                Plaper.windowWidth = Plaper.screenWidth;
                graphics.IsFullScreen = !graphics.IsFullScreen;
            }

            graphics.PreferredBackBufferHeight = Plaper.windowHeight;
            graphics.PreferredBackBufferWidth  = Plaper.windowWidth;
            graphics.ApplyChanges();

            playArea = new RenderTarget2D(graphics.GraphicsDevice, Plaper.playWidth, Plaper.playHeight, false, SurfaceFormat.Color, DepthFormat.None, 2, RenderTargetUsage.DiscardContents);

            //create state and pass player object
            currentState = new MenuState(graphics, this);

            //set state to game
            State.setState(currentState);

            this.IsMouseVisible = true;

            Plaper.lastKeyboardState = Keyboard.GetState();
            Plaper.keyboardState     = Keyboard.GetState();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //arrow
            arrow  = Content.Load<Texture2D>("arrow");
            arrowFill = Content.Load<Texture2D>("arrow_fill");

            //player
            sprite = Content.Load<Texture2D>("bouncer_all");

            //platform
            platformTex = Content.Load<Texture2D>("platform");

            //fonts
            font12 = Content.Load<SpriteFont>("joystik");
            font24 = Content.Load<SpriteFont>("joystik24");
            font36 = Content.Load<SpriteFont>("joystik36");
			font10 = Content.Load<SpriteFont>("ScoreFont");

            font = font24;
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {

            Plaper.keyboardState = Keyboard.GetState();
            Plaper.gameTime = gameTime;
            Input.Update();
            //exit if esc is pressed
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
                Exit();
            }

            //check that state is initalized then call current state's update
            if(State.getState() != null) {
                State.getState().Update(gameTime);
            }

            base.Update(gameTime);

            Plaper.lastKeyboardState = Plaper.keyboardState;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.SetRenderTarget(playArea);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //check that state is initalized then call current state's draw
            State.getState()?.Draw(spriteBatch);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.DarkOrange);
            spriteBatch.Begin();
            spriteBatch.Draw(playArea, new Rectangle((Plaper.windowWidth - Plaper.playWidth) / 2, 0, Plaper.playWidth, Plaper.playHeight), Color.White);
            spriteBatch.End();
            //display framerate in title bar
            Window.Title = "Plaper - " + Math.Round((1 / gameTime.ElapsedGameTime.TotalSeconds), 2).ToString() + " FPS";

            base.Draw(gameTime);
        }
    }
}