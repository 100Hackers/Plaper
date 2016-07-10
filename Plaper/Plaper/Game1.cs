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

        const int SCREEN_WIDTH = 400;
        const int SCREEN_HEIGHT = 600;

        Texture2D[] sprites;

        Texture2D platform;
        Rectangle platformPos;
        const double PLAT_SCALE = 0.5;
        const int PLAT_H = (int) (50 * PLAT_SCALE);
        const int PLAT_W = (int) (500 * PLAT_SCALE);

        Rectangle screenRectangle;

        Random rand = new Random();

        Player player;


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
            // TODO: Add your initialization logic here

            //platformPos = new Rectangle(rand.Next(0, 400 - PLAT_W), rand.Next(200, 400 - PLAT_H), PLAT_W, PLAT_H);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            Texture2D blocky = Content.Load<Texture2D>("blocky");
            Texture2D arrow  = Content.Load<Texture2D>("arrow");
            player = new Player(blocky, arrow, screenRectangle);


            /*sprites = new Texture2D[3];
            sprites[0] = Content.Load<Texture2D>("blocky_left");
            sprites[1] = Content.Load<Texture2D>("blocky");
            sprites[2] = Content.Load<Texture2D>("blocky_right");

            platform = Content.Load<Texture2D>("platform");*/
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            player.Update(gameTime);


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Window.Title = (1 / gameTime.ElapsedGameTime.TotalSeconds).ToString();

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            player.Draw(spriteBatch);
            //spriteBatch.Draw(platform, platformPos, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
