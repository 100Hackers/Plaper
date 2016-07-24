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

        Button[] buttonArr = new Button[3];

        public EndgameState(GraphicsDeviceManager graphics, Game1 game, int score) : base(graphics, game) {

            buttonSpacing = Game1.buttonHeight * 3 / 2;

            buttonArr[0] = new Button("Score: " + score.ToString(), buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight/2,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));
            nButtons++;
            buttonArr[1] = new Button(RETRY_TEXT, buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));

            nButtons++;
            buttonArr[2] = new Button(MENU_TEXT, buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));
        }

        public override void Update(GameTime gameTime) {

            foreach(Button button in buttonArr) {
                button.Update(gameTime);
            }

            if(buttonArr[1].Clicked()) {
                State.setState(new GameState(graphics, game));
            }
            if(buttonArr[2].Clicked()) {
                State.setState(new MenuState(graphics, game));
            }

            if(!Keyboard.GetState().IsKeyDown(Keys.Space) && Plaper.lastKeyboardState.IsKeyDown(Keys.Space)) {
                State.setState(new GameState(graphics, game));
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

        }

        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            foreach(Button button in buttonArr) {
                button.Draw(spriteBatch);
            }

            spriteBatch.End();

        }
    }
}