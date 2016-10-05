using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {

    //game state class
    class SettingsState : State {

        Button[] buttonArr = new Button[2];

        const string BACK_TEXT = "BACK";

        //ctor
        public SettingsState(GraphicsDeviceManager graphics, Game1 game) : base(graphics, game) {

            buttonSpacing = Game1.buttonHeight * 3 / 2;
            
            buttonArr[0] = new Button("VOLUME: " + Math.Round(SoundEffect.MasterVolume * 100, 1).ToString() + "%", buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.playWidth, Game1.buttonHeight));
            nButtons++;
            buttonArr[1] = new Button(BACK_TEXT, buttonTexture,
                new Rectangle(0, buttonSpacing * nButtons + Game1.buttonHeight / 2,
                Plaper.playWidth, Game1.buttonHeight));
            nButtons++;
        }

        //update for game logic
        public override void Update(GameTime gameTime, Game1 game) {

            for (int i = 0; i < nButtons; ++i) {
                buttonArr[i].Update(gameTime);
            }

            //check where mouse is and do stuff if it's clicked
            if(Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                State.setState(new MenuState(graphics, game));
            }

            if(buttonArr[0].Clicked()) {
                if(SoundEffect.MasterVolume < 0.9f) {
                    SoundEffect.MasterVolume += 0.1f;
                } else {
                    SoundEffect.MasterVolume = 0.0f;
                }
                buttonArr[0].name = "VOLUME: " + Math.Round(SoundEffect.MasterVolume * 100, 1).ToString() + "%";
            }
            
            if(buttonArr[1].Clicked()) {
                State.setState(new MenuState(graphics, game));
            }

        }

        //update graphics
        public override void Draw(SpriteBatch spriteBatch) {
            for (int i = 0; i < nButtons; ++i) {
                buttonArr[i].Draw(spriteBatch);
            }
        }
    }
}