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

        const string START_TEXT = "START";
        const string SETTINGS_TEXT = "SETTINGS";

        Rectangle spritePreviewRectangle;

        Button[] buttonArr = new Button[2];

        //ctor
        public MenuState(GraphicsDeviceManager graphics, Game1 game) : base(graphics, game) {

            spritePreviewRectangle = new Rectangle(Plaper.SCREEN_WIDTH / 4, Plaper.SCREEN_HEIGHT / 50, Plaper.SCREEN_WIDTH/2, (int)(Plaper.SCREEN_WIDTH/2 * 17.0 / 14.0));

            nButtons++;
            buttonArr[0] = new Button(START_TEXT, buttonTexture,
                new Rectangle(0, (int)(Game1.buttonHeight + Game1.buttonSpacing*3.9) * nButtons,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));
            nButtons++;
            buttonArr[1] = new Button(SETTINGS_TEXT, buttonTexture,
                new Rectangle(0, (int)(Game1.buttonHeight + Game1.buttonSpacing*2.4) * nButtons,
                Plaper.SCREEN_WIDTH, Game1.buttonHeight));
        }

        //update game logic
        public override void Update(GameTime gameTime) {

            foreach(Button button in buttonArr) {
                button.Update(gameTime);
            }

            if(buttonArr[0].Clicked()) {
                State.setState(new GameState(graphics, game));
            }
            if(buttonArr[1].Clicked()) {
                State.setState(new SettingsState(graphics, game));
            }

            if(!Keyboard.GetState().IsKeyDown(Keys.Space) && Plaper.lastKeyboardState.IsKeyDown(Keys.Space)) {
                State.setState(new GameState(graphics, game));
            }

            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                //game.Exit();
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(game.Sprite, spritePreviewRectangle, new Rectangle(14, 0, 14, 17), Color.White);

            foreach (Button button in buttonArr) {
                button.Draw(spriteBatch);
            }
        }
    }
}