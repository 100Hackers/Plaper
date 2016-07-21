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
    class MenuState : State {

        GraphicsDeviceManager graphics;
        Game1 game;

        Texture2D buttonTexture;
        const string START_TEXT = "START";
        const string SETTINGS_TEXT = "SETTINGS";

        Button startButton;
        Button settingsButton;

        int nButtons = 1;

        const  float TEXT_SCALE  = 2.5f;
        static Color HOVER_COLOR = Color.White;
        static Color TEXT_COLOR  = Color.Black;


        //ctor
        public MenuState(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;

            //stuff for button texture
            buttonTexture = new Texture2D(graphics.GraphicsDevice, 20, 10);
            Color[] startTextureData = new Color[Game1.buttonHeight * Game1.SCREEN_WIDTH];
            for (int i = 0; i < startTextureData.Length; i++) {
                startTextureData[i] = Color.Chocolate;
            }
            buttonTexture.SetData(startTextureData);

            startButton = new Button("START", buttonTexture,
                new Rectangle(0, (Game1.buttonHeight + Game1.buttonSpacing*3) * nButtons,
                Game1.SCREEN_WIDTH, Game1.buttonHeight));
            nButtons++;
            settingsButton = new Button("SETTINGS", buttonTexture,
                new Rectangle(0, (Game1.buttonHeight + Game1.buttonSpacing*2) * nButtons,
                Game1.SCREEN_WIDTH, Game1.buttonHeight));
        }

        //update game logic
        public override void Update(GameTime gameTime) {

            startButton.Update(gameTime);
            settingsButton.Update(gameTime);

            if(startButton.Clicked()) {
                State.setState(new GameState(graphics, game));
            }
            if(settingsButton.Clicked()) {
                State.setState(new SettingsState(graphics, game));
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            startButton.Draw(spriteBatch);
            settingsButton.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}