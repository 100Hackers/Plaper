using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {

    //game state class
    class SettingsState : State {

        GraphicsDeviceManager graphics;
        Game1 game;

        SpriteFont font;
        bool isMouseOverBack;
        const string BACK_TEXT = "BACK";
        Vector2 backTextPosition;


        Rectangle backButtonPosition;
        Texture2D menuButtonTexture;
        //Rectangle newButtonPosition;

        Boolean lastMouseState = false;

        const float TEXT_SCALE = 2.5f;
        static Color HOVER_COLOR = Color.White;
        static Color TEXT_COLOR = Color.Black;

        //ctor
        public SettingsState(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.font = Game1.font36;
            this.game = game;

            //stuff for button texture
            menuButtonTexture = new Texture2D(graphics.GraphicsDevice, 20, 10);
            Color[] startTextureData = new Color[20 * 10];
            for(int i = 0; i < startTextureData.Length; i++) {
                startTextureData[i] = Color.Chocolate;
            }
            menuButtonTexture.SetData(startTextureData);

            //setting where the buttons will be
            backButtonPosition = new Rectangle(0, Game1.SCREEN_HEIGHT - (Game1.SCREEN_HEIGHT / 10) - Game1.SCREEN_HEIGHT / 5, Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT / 5);
            //newButtonPosition = new Rectangle(0, settingsButtonPosition.Y - settingsButtonPosition.Height - (Game1.SCREEN_HEIGHT / 10), Game1.SCREEN_WIDTH, Game1.SCREEN_HEIGHT / 5);

            Vector2 backTextSize = this.font.MeasureString(BACK_TEXT);
            backTextPosition.Y = (backButtonPosition.Height - backTextSize.Y) / 2 + backButtonPosition.Y;
            backTextPosition.X = (backButtonPosition.Width - backTextSize.X) / 2 + backButtonPosition.X;
        }

        //update for game logic
        public override void Update(GameTime gameTime) {

            //check where mouse is and do stuff if it's clicked
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }
            var mouseState = Mouse.GetState();
            var mousePosition = new Point(mouseState.X, mouseState.Y);

            isMouseOverBack = backButtonPosition.Contains(mousePosition);

            //check if mouse button has just been depressed
            if(mouseState.LeftButton != ButtonState.Pressed && lastMouseState == true) {
                if(backButtonPosition.Contains(mousePosition)) {
                    State.setState(new MenuState(graphics, game));
                }
            }

            //set last mouse state for betterClick
            //this code also seen in MenuState
            if(mouseState.LeftButton == ButtonState.Pressed) {
                lastMouseState = true;
            } else {
                lastMouseState = false;
            }
        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            //back button
            spriteBatch.Draw(menuButtonTexture, backButtonPosition, Color.White);

            //TODO: add text for back button
            spriteBatch.DrawString(font, BACK_TEXT, backTextPosition, isMouseOverBack ? HOVER_COLOR : TEXT_COLOR);


            spriteBatch.End();

        }
    }
}