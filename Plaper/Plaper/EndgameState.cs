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

        public EndgameState(GraphicsDeviceManager graphics, Game1 game) : base(graphics, game) {

            buttonSpacing = Game1.buttonHeight * 3 / 2;

            buttonArr[0] = new Button("Score: " + Plaper.score.ToString(), buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight/2,
                Plaper.playWidth, Game1.buttonHeight), 3);
            nButtons++;
            buttonArr[1] = new Button(RETRY_TEXT, buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.playWidth, Game1.buttonHeight), 1);

            nButtons++;
            buttonArr[2] = new Button(MENU_TEXT, buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.playWidth, Game1.buttonHeight), 0);
        }

        public override void Update(GameTime gameTime, Game1 game) {

            foreach(Button button in buttonArr) {
                button.Update(gameTime);
            }

            if(!Input.keyboardState.IsKeyDown(Keys.Space) && Input.lastKeyboardState.IsKeyDown(Keys.Space)) {
                State.setState(new GameState(graphics, game));
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

        }

        public override void Draw(SpriteBatch spriteBatch) {
            foreach(Button button in buttonArr) {
                button.Draw(spriteBatch);
            }
        }
    }
}