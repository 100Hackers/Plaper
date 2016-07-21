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

        Texture2D buttonTexture;
        int nButtons = 0;

        const float TEXT_SCALE = 2.5f;
        static Color HOVER_COLOR = Color.White;
        static Color TEXT_COLOR = Color.Black;

        //ctor
        public SettingsState(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;

            //stuff for button texture
            buttonTexture = new Texture2D(graphics.GraphicsDevice, 20, 10);
            Color[] startTextureData = new Color[Game1.buttonHeight * Game1.SCREEN_WIDTH];
            for(int i = 0; i < startTextureData.Length; i++) {
                startTextureData[i] = Color.Chocolate;
            }
            buttonTexture.SetData(startTextureData);

            backButton = new Button("BACK", buttonTexture, 
                new Rectangle(0, (Game1.buttonHeight/2) + (nButtons * Game1.buttonHeight), 
                Game1.SCREEN_WIDTH, Game1.buttonHeight));
            nButtons++;
        }

        //update for game logic
        public override void Update(GameTime gameTime) {

            backButton.Update(gameTime);

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


            spriteBatch.End();

        }
    }
}