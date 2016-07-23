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
    class EndgameState : State {

        GraphicsDeviceManager graphics;
        Game1 game;

        Texture2D buttonTexture;
        const string START_TEXT = "START";
        const string SETTINGS_TEXT = "SETTINGS";

        Button retryButton;
        Button menuButton;

        int nButtons = 1;

        const float TEXT_SCALE = 2.5f;
        static Color HOVER_COLOR = Color.White;
        static Color TEXT_COLOR = Color.Black;


        //ctor
        public EndgameState(GraphicsDeviceManager graphics, Game1 game) {
            this.graphics = graphics;
            this.game = game;

            //stuff for button texture
            buttonTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            buttonTexture.SetData(new Color[] { Color.Chocolate });

            retryButton = new Button("RETRY", buttonTexture,
                new Rectangle(0, (Game1.buttonHeight + Game1.buttonSpacing * 3) * nButtons,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));
            nButtons++;
            menuButton = new Button("MENU", buttonTexture,
                new Rectangle(0, (Game1.buttonHeight + Game1.buttonSpacing * 2) * nButtons,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));
        }

        //update game logic
        public override void Update(GameTime gameTime) {

            retryButton.Update(gameTime);
            menuButton.Update(gameTime);

            if(retryButton.Clicked()) {
                State.setState(new GameState(graphics, game));
            }
            if(menuButton.Clicked()) {
                State.setState(new MenuState(graphics, game));
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Space)) {
                State.setState(new GameState(graphics, game));
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            retryButton.Draw(spriteBatch);
            menuButton.Draw(spriteBatch);

            spriteBatch.End();

        }
    }
}