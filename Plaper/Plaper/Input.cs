using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Input {

        public static KeyboardState lastKeyboardState, keyboardState;
        public static bool jumpPressed;

        public static bool lastMouseClicked, mouseClicked;
        public static Vector2 lastMouse, mouse;

        public static void Initialize() {
            jumpPressed = false;
            lastKeyboardState = keyboardState = Keyboard.GetState();
            
            mouseClicked = false;
            lastMouse = mouse = new Vector2(-1, -1);
        }

        public static void Update() {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            jumpPressed = keyboardState.IsKeyDown(Keys.Space);

            var tMouseState = Mouse.GetState();
            var tPoint = tMouseState.Position;

            lastMouseClicked = mouseClicked;
            mouseClicked = tMouseState.LeftButton == ButtonState.Pressed;

            lastMouse = mouse;
            mouse.X = tPoint.X;
            mouse.Y = tPoint.Y;
        }
    }
}
