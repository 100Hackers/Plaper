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

        Texture2D[] sprites;
        Rectangle playerPos;
        const double PLAYER_SCALE = 3.0;
        const int PLAYER_H = (int) (17 * PLAYER_SCALE);
        const int PLAYER_W = (int) (14 * PLAYER_SCALE);

        Texture2D platform;
        Rectangle platformPos;
        const double PLAT_SCALE = 0.5;
        const int PLAT_H = (int) (50 * PLAT_SCALE);
        const int PLAT_W = (int) (500 * PLAT_SCALE);

        Random rand = new Random();

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 400;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            playerPos = new Rectangle(rand.Next(0, 400 - PLAYER_W), 600 - PLAYER_H, PLAYER_W, PLAYER_H);
            platformPos = new Rectangle(rand.Next(0, 400 - PLAT_W), rand.Next(200, 400 - PLAT_H), PLAT_W, PLAT_H);


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
            sprites = new Texture2D[3];
            sprites[0] = Content.Load<Texture2D>("blocky_left");
            sprites[1] = Content.Load<Texture2D>("blocky");
            sprites[2] = Content.Load<Texture2D>("blocky_right");

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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Space)) {
                playerPos = new Rectangle(rand.Next(0, 400 - PLAYER_W), 600 - PLAYER_H, PLAYER_W, PLAYER_H);
                platformPos = new Rectangle(rand.Next(0, 400 - PLAT_W), rand.Next(200, 400 - PLAT_H), PLAT_W, PLAT_H);
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            spriteBatch.Draw(sprites[1], playerPos, Color.White);
            spriteBatch.Draw(platform, platformPos, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
