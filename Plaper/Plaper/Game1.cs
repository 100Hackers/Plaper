using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System;
using System.IO.IsolatedStorage;
using System.IO;

namespace Plaper {
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const bool makeFullscreen = false;

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

        public Game1() {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Set screen height and width
            graphics.PreferredBackBufferHeight = Plaper.SCREEN_HEIGHT;
            graphics.PreferredBackBufferWidth = Plaper.SCREEN_HEIGHT / 2;
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

        //public SoundEffect LosingSound
        //{
        //    get { return Output.losingSound; }
        //}

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {

            //loadHighScoreWindows();
            loadHighScoreAndroid();

            base.Initialize();

            Output.Initialize(graphics);

            //create state and pass player object
            currentState = new MenuState(graphics, this);

            //set state to game
            State.setState(currentState);

            this.IsMouseVisible = true;

            Input.Initialize();
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
            Output.font12 = Content.Load<SpriteFont>("joystik");
            Output.font24 = Content.Load<SpriteFont>("joystik24");
            Output.font36 = Content.Load<SpriteFont>("joystik36");
            Output.font72 = Content.Load<SpriteFont>("joystik72");
            Output.font108 = Content.Load<SpriteFont>("joystik108");
            Output.font144 = Content.Load<SpriteFont>("joystik144");

			Output.font10 = Content.Load<SpriteFont>("ScoreFont");

            Output.font = Output.font24;

            //Sounds
            Output.jump = Content.Load<SoundEffect>("Jump");
            Output.losingSound = Content.Load<SoundEffect>("Losing Sound");
            Output.wallHits[0] = Content.Load<SoundEffect>("Thud1");
            Output.wallHits[1] = Content.Load<SoundEffect>("Thud2");
            SoundEffect.MasterVolume = 0.1f;

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }
        
        private void loadHighScoreWindows() {
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForDomain();
            if(savegameStorage.FileExists("high_score.txt")) {
                IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("high_score.txt", FileMode.OpenOrCreate, FileAccess.Read);
                using(StreamReader sr = new StreamReader(isoStream)) {
                    Plaper.highScore = int.Parse(sr.ReadLine());
                }
            } else {
                Plaper.highScore = 0;
            }
        }

        private void loadHighScoreAndroid() {
            IsolatedStorageFile savegameStorage = IsolatedStorageFile.GetUserStoreForApplication();
            if(savegameStorage.FileExists("high_score.txt")) {
                IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("high_score.txt", FileMode.OpenOrCreate, FileAccess.Read);
                using(StreamReader sr = new StreamReader(isoStream)) {
                    Plaper.highScore = int.Parse(sr.ReadLine());
                }
            } else {
                Plaper.highScore = 0;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {

            Plaper.gameTime = gameTime;
            Input.Update();
            //exit if esc is pressed
            if(GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
                Exit();
            }

            //check that state is initalized then call current state's update
            State.getState()?.Update(gameTime, this);

            if(!Input.keyboardState.IsKeyDown(Keys.D) && Input.lastKeyboardState.IsKeyDown(Keys.D)) {
                Plaper.debugMode = !Plaper.debugMode;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {

            GraphicsDevice.SetRenderTarget(Output.playArea);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //check that state is initalized then call current state's draw
            State.getState()?.Draw(spriteBatch);

            GraphicsDevice.SetRenderTarget(null);
            GraphicsDevice.Clear(Color.DarkOrange);
            spriteBatch.Begin();
            spriteBatch.Draw(Output.playArea, Output.playRectangle, Color.White);
            spriteBatch.End();
            //display framerate in title bar
            Window.Title = "Plaper - " + Math.Round((1 / gameTime.ElapsedGameTime.TotalSeconds), 2).ToString() + " FPS";

            base.Draw(gameTime);
        }
    }
}