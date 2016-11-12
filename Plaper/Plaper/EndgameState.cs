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

        static int deathCounter = 0;

        Button[] buttonArr = new Button[4];

        public EndgameState(GraphicsDeviceManager graphics, Game1 game, int score) : base(graphics, game) {

            nButtons = 0;

            buttonSpacing = Plaper.windowHeight / buttonArr.Length;
            double buttonHeight = buttonSpacing * 2 / 3;

            Rectangle newButtonPosRect = new Rectangle(0, (buttonSpacing * nButtons) + ((int)buttonHeight / 4), Plaper.playWidth, (int)buttonHeight);
            buttonArr[0] = new Button("Score: " + score.ToString(), buttonTexture, newButtonPosRect, false);
            nButtons++;

            newButtonPosRect = new Rectangle(0, (buttonSpacing * nButtons) + ((int)buttonHeight / 4), Plaper.playWidth, (int)buttonHeight);
            buttonArr[1] = new Button("Max: " + Plaper.highScore.ToString(), buttonTexture, newButtonPosRect, false);
            nButtons++;

            newButtonPosRect = new Rectangle(0, (buttonSpacing * nButtons) + ((int)buttonHeight / 4), Plaper.playWidth, (int)buttonHeight);
            buttonArr[2] = new Button(RETRY_TEXT, buttonTexture, newButtonPosRect);
            nButtons++;

            newButtonPosRect = new Rectangle(0, (buttonSpacing * nButtons) + ((int)buttonHeight / 4), Plaper.playWidth, (int)buttonHeight);
            buttonArr[3] = new Button(MENU_TEXT, buttonTexture, newButtonPosRect);

            ++deathCounter;
        }

        public override void Update(GameTime gameTime, Game1 game) {

            foreach(Button button in buttonArr) {
                button.Update(gameTime);
            }

            if(buttonArr[2].Clicked()) {
                State.setState(new GameState(graphics, game));
            }
            if(buttonArr[3].Clicked()) {
                State.setState(new MenuState(graphics, game));
            }

            if(!Input.keyboardState.IsKeyDown(Keys.Space) && Input.lastKeyboardState.IsKeyDown(Keys.Space)) {
                State.setState(new GameState(graphics, game));
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

        }

        public override void Draw(SpriteBatch spriteBatch) {
            foreach (Button button in buttonArr) {
                button.Draw(spriteBatch);
            }
        }
    }
}