using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Button {

        string name;
        Texture2D texture;
        static Color HOVER_COLOR = Color.White;
        static Color TEXT_COLOR = Color.Black;
        SpriteFont font;
        Vector2 textPosition;
        ButtonState lastMouseButton;
        Rectangle buttonRect;

        public Button(string name, Texture2D texture, Rectangle buttonRect, ContentManager content) {
            this.name = name;
            this.texture = texture;
            this.buttonRect = buttonRect;
            this.font = content.Load<SpriteFont>("joystik24");

            Vector2 textSize = this.font.MeasureString(name);
            textPosition.Y = (buttonRect.Height - textSize.Y) / 2 + buttonRect.Y;
            textPosition.X = (buttonRect.Width - textSize.X) / 2 + buttonRect.X;
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

            if(mouseState.X < buttonRect.X + buttonRect.Width &&
               mouseState.X > buttonRect.X &&
               mouseState.Y < buttonRect.Y + buttonRect.Height &&
               mouseState.Y > buttonRect.Y) {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime) {
            
        }
        public void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Draw(texture, buttonRect, Color.White);

            spriteBatch.DrawString(font, name, textPosition, isInBounds() ? HOVER_COLOR : TEXT_COLOR);

        }
    }
}
