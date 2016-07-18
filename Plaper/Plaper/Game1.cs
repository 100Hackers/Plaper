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

        // Height and Width to make screen
        public const int SCREEN_WIDTH = 400;
        public const int SCREEN_HEIGHT = 600;

        //unused
        Texture2D platform;
        Rectangle platformPos;

        // Textures for Player
        Texture2D sprite;
        Texture2D arrow;
        Texture2D arrowFill;

        // Platform constants
        const double PLAT_SCALE = 0.5;
        const int PLAT_H = (int) (50 * PLAT_SCALE);
        const int PLAT_W = (int) (500 * PLAT_SCALE);

        //used for generating platform
        Random rand = new Random();

        Player player;

        State currentState;

        public static SpriteFont font;
        public static SpriteFont font12;
        public static SpriteFont font24;
        public static SpriteFont font36;

        public Game1() {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set screen height and width
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            

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

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            base.Initialize();

            //create state and pass player object
            currentState = new MenuState(graphics, this);

            //set state to game
            State.setState(currentState);

            this.IsMouseVisible = true;


            //platformPos = new Rectangle(rand.Next(0, 400 - PLAT_W), rand.Next(200, 400 - PLAT_H), PLAT_W, PLAT_H);
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
            sprite = Content.Load<Texture2D>("blocky_all");

            //platform
            platform = Content.Load<Texture2D>("platform");

            font12 = Content.Load<SpriteFont>("joystik");
            font24 = Content.Load<SpriteFont>("joystik24");
            font36 = Content.Load<SpriteFont>("joystik36");

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

            //exit if esc is pressed
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
                Exit();
            }

            //check that state is initalized then call current state's update
            if(State.getState() != null) {
                State.getState().Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            //check that state is initalized then call current state's draw
            if(State.getState() != null) {
                State.getState().Draw(spriteBatch);
            }

            //display framerate in title bar
            Window.Title = "Plaper - " + Math.Round((1 / gameTime.ElapsedGameTime.TotalSeconds), 2).ToString() + " FPS";

            base.Draw(gameTime);
        }
    }
}