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

        //resolution
        public const int SCREEN_WIDTH = 400;
        public const int SCREEN_HEIGHT = 600;

        //unused
        Texture2D platform;
        Rectangle platformPos;

        const double PLAT_SCALE = 0.5;
        const int PLAT_H = (int) (50 * PLAT_SCALE);
        const int PLAT_W = (int) (500 * PLAT_SCALE);

        Rectangle screenRectangle;

        //used for generating platform
        Random rand = new Random();

        Player player;

        State currentState;

        public Game1() {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;

            screenRectangle = new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT);

            // unrestricted framerate
            //this.IsFixedTimeStep = false;
            //this.graphics.SynchronizeWithVerticalRetrace = false;

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
            currentState = new MenuState(player, graphics);
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
            Texture2D arrow  = Content.Load<Texture2D>("arrow");
            Texture2D arrowFill = Content.Load<Texture2D>("arrow_fill");

            //player
            Texture2D sprite = Content.Load<Texture2D>("blocky_all");
            player = new Player(sprite, arrow, arrowFill, screenRectangle);

            //platform
            platform = Content.Load<Texture2D>("platform");

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

            //exit if back or esc is pressed
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
                Exit();
            }
            if(Keyboard.GetState().IsKeyDown(Keys.LeftAlt) && Keyboard.GetState().IsKeyDown(Keys.F4)) {
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