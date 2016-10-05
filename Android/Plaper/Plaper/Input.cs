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
using Microsoft.Xna.Framework.Input.Touch;

namespace Plaper {
    class Input {

        public static KeyboardState lastKeyboardState, keyboardState;
        public static bool jumpPressed;

        public static bool lastMouseClicked, mouseClicked;
        public static Vector2 lastMouse, mouse;

        static TouchCollection touches;
        static TouchLocation   touch;

        public static void Initialize() {
            jumpPressed = false;
            lastKeyboardState = keyboardState = Keyboard.GetState();
            
            mouseClicked = lastMouseClicked = false;
            lastMouse = mouse = new Vector2(-1, -1);
        }

        public static void Update() {
            lastKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            touches = TouchPanel.GetState();
            bool oneTouchOnScreen = touches.Count == 1;

            jumpPressed = oneTouchOnScreen;

            lastMouseClicked = mouseClicked;
            lastMouse = mouse;

            if (oneTouchOnScreen) {
                mouseClicked = true;
                mouse = touches.ToArray()[0].Position;
            } else {
                mouseClicked = false;
                mouse.X = -1;
                mouse.Y = -1;
            }
        }
    }
}