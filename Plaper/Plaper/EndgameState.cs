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

        const string RETRY_TEXT = "RETRY";
        const string MENU_TEXT = "MENU";

        Button retryButton;
        Button menuButton;

        //ctor
        public EndgameState(GraphicsDeviceManager graphics, Game1 game) : base(graphics, game) {

            nButtons++;
            retryButton = new Button(RETRY_TEXT, buttonTexture,
                new Rectangle(0, (Game1.buttonHeight + Game1.buttonSpacing * 3) * nButtons,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));

            nButtons++;
            menuButton = new Button(MENU_TEXT, buttonTexture,
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