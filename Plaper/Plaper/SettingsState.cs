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
        Button backButton;

        Rectangle backButtonPosition;
        Texture2D buttonTexture;
        int nButtons = 0;
        int buttonHeight;
        ButtonState lastMouseButton;

        //ctor
        public SettingsState(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;
            buttonHeight = Game1.SCREEN_HEIGHT / 5;

            //stuff for button texture
            buttonTexture = new Texture2D(graphics.GraphicsDevice, 20, 10);
            Color[] startTextureData = new Color[buttonHeight * Game1.SCREEN_WIDTH];
            for(int i = 0; i < startTextureData.Length; i++) {
                startTextureData[i] = Color.Chocolate;
            }
            buttonTexture.SetData(startTextureData);

            //(string, texture, right, down, right, down)
            backButton = new Plaper.Button("BACK", buttonTexture, 0, (buttonHeight/2) + (nButtons * buttonHeight), Game1.SCREEN_WIDTH, buttonHeight);
            nButtons++;
        }

        //update for game logic
        public override void Update(GameTime gameTime) {

            //check where mouse is and do stuff if it's clicked
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

            //check if mouse button has just been depressed
            if(backButton.Clicked()) {
                State.setState(new MenuState(graphics, game));
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            //back button
            backButton.Draw(spriteBatch);

            //TODO: add text for back button

            spriteBatch.End();

        }
    }
}