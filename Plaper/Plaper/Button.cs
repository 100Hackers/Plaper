using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
Largly based off the example code here:
http://stackoverflow.com/questions/32430598/making-a-button-xna-monogame
*/

namespace Plaper {
    class Button {

        int x;
        int y;
        int width;
        int height;
        string name;
        Texture2D texture;
        static Color HOVER_COLOR = Color.White;
        static Color TEXT_COLOR = Color.Black;
        SpriteFont font;
        Vector2 textPosition;
        ButtonState lastMouseButton;

        public int X {
            get { return x; }
        }
        public int Y {
            get { return y; }
        }

        public Button(string name, Texture2D texture, int buttonX, int buttonY, int width, int height) {
            this.name = name;
            this.texture = texture;
            this.x = buttonX;
            this.y = buttonY;
            this.width = width;
            this.height = height;
            this.font = Game1.font36;

            Vector2 textSize = this.font.MeasureString(name);
            textPosition.Y = (height - textSize.Y) / 2 + y;
            textPosition.X = (width - textSize.X) / 2 + x;
        }

        public Boolean Clicked() {

            if(Mouse.GetState().LeftButton == ButtonState.Released && lastMouseButton == ButtonState.Pressed) {
                if(this.isInBounds()) {
                    return true;
                }
            }
            lastMouseButton = Mouse.GetState().LeftButton;
            return false;            
        }

        public Boolean isInBounds() {
            var mouseState = Mouse.GetState();

            if(mouseState.X < X + texture.Width &&
                    mouseState.X > X &&
                    mouseState.Y < Y + texture.Height &&
                    mouseState.Y > Y) {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime) {
            
        }
        public void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Draw(texture, new Rectangle((int)X, (int)Y, width, height), Color.White);

            spriteBatch.DrawString(font, name, textPosition, isInBounds() ? HOVER_COLOR : TEXT_COLOR);

        }
    }
}
