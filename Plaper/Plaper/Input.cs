using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Input {
        public static void Update(GameTime gameTime) {
            Plaper.lastKeyboardState = Plaper.keyboardState;
            MouseState state = Mouse.GetState();
            Plaper.keyboardState = Keyboard.GetState();


            Plaper.prevMouse = Plaper.curMouse;
            Plaper.curMouse = state.LeftButton == ButtonState.Pressed;
            Plaper.mouse = new Vector2(state.X, state.Y);

            Plaper.jumpPressed = Plaper.keyboardState.IsKeyDown(Keys.Space);

        }
    }
}
