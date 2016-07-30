using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Plaper {
    class Input {

        public static KeyboardState lastKeyboardState, keyboardState;
        public static bool jumpPressed;

        public static bool mouseClicked;
        public static Vector2 prevMouse, curMouse;

        public static void Initialize() {
            jumpPressed = false;
            lastKeyboardState = keyboardState = Keyboard.GetState();
            
            mouseClicked = false;
            prevMouse = curMouse = new Vector2(-1, -1);
        }

        public static void Update() {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            jumpPressed = keyboardState.IsKeyDown(Keys.Space);

            prevMouse = curMouse;
            var tMouseState = Mouse.GetState();
            var tPoint = tMouseState.Position;
            mouseClicked = tMouseState.MiddleButton == ButtonState.Pressed;
            curMouse.X = tPoint.X;
            curMouse.Y = tPoint.Y;
        }
    }
}