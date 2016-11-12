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

        Button[] buttonArr = new Button[1];

        //ctor
        public MenuState(GraphicsDeviceManager graphics, Game1 game) : base(graphics, game) {

            spritePreviewRectangle = new Rectangle(Plaper.playWidth / 4, Plaper.playHeight / 50, Plaper.playWidth/2, (int)(Plaper.playWidth/2 * 17.0 / 14.0));

            nButtons = 2;

            buttonSpacing = Plaper.windowHeight / 4;
            double buttonHeight = buttonSpacing * 2 / 3;

            Rectangle newButtonPosRect = new Rectangle(0, (buttonSpacing * nButtons), Plaper.playWidth, (int)buttonHeight);
            buttonArr[0] = new Button(START_TEXT, buttonTexture, newButtonPosRect, true);

            //nButtons++;

            //buttonArr[1] = new Button(SETTINGS_TEXT, buttonTexture,
            //    new Rectangle(0, Game1.buttonHeight*10 + buttonSpacing*nButtons,
            //    Plaper.playWidth, Game1.buttonHeight));
        }

        //update game logic
        public override void Update(GameTime gameTime, Game1 game) {

            foreach(Button button in buttonArr) {
                button.Update(gameTime);
            }

            if(buttonArr[0].Clicked()) {
                State.setState(new GameState(graphics, game));
            }
            /*if(buttonArr[1].Clicked()) {
                State.setState(new SettingsState(graphics, game));
            }*/

            if(!Input.keyboardState.IsKeyDown(Keys.Space) && Input.lastKeyboardState.IsKeyDown(Keys.Space)) {
                State.setState(new GameState(graphics, game));
            }

            if(Input.keyboardState.IsKeyDown(Keys.Escape)) {
                //game.Exit();
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
            spriteBatch.Draw(game.Sprite, spritePreviewRectangle, new Rectangle(14, 0, 14, 17), Color.White);
            spriteBatch.End();

            foreach (Button button in buttonArr) {
                button.Draw(spriteBatch);
            }
        }
    }
}