#if __ANDROID__
using AdBuddiz.Xamarin;
#endif

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
        bool adShown;

        Button[] buttonArr = new Button[3];

        public EndgameState(GraphicsDeviceManager graphics, Game1 game, int score) : base(graphics, game) {

            buttonSpacing = Game1.buttonHeight * 3 / 2;
            
            buttonArr[0] = new Button("Score: " + score.ToString(), buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight/2,
                Plaper.playWidth, Game1.buttonHeight), false);
            nButtons++;
            buttonArr[1] = new Button(RETRY_TEXT, buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.playWidth, Game1.buttonHeight));

            nButtons++;
            buttonArr[2] = new Button(MENU_TEXT, buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.playWidth, Game1.buttonHeight));

            ++deathCounter;
            adShown = false;
        }

        public override void Update(GameTime gameTime, Game1 game) {

#if __ANDROID__
            if (!adShown && deathCounter % 5 == 4 && Activity1.TimesPlayed > 2) {
                AdBuddizHandler.Instance.ShowAd();
                adShown = true;
            }
#endif

            foreach(Button button in buttonArr) {
                button.Update(gameTime);
            }

            if(buttonArr[1].Clicked()) {
                State.setState(new GameState(graphics, game));
            }
            if(buttonArr[2].Clicked()) {
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