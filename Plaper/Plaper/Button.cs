using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plaper {
    class Button {

        public string name;
        Texture2D texture;
        static Color HOVER_COLOR = Color.White;
        static Color TEXT_COLOR = Color.Black;
        SpriteFont font;
        Vector2 textPosition;
        ButtonState lastMouseButton;
        Rectangle buttonRect;

        public Button(string name, Texture2D texture, Rectangle buttonRect) {
            this.name = name;
            this.texture = texture;
            this.buttonRect = new Rectangle(0, buttonRect.Y, Plaper.playWidth, (int) (buttonRect.Height * Plaper.BUTTON_HEIGHT * Plaper.playHeight));
            this.font = Output.font36;

            Vector2 textSize = this.font.MeasureString(name);
            textPosition.Y = (buttonRect.Height - textSize.Y) / 2 + buttonRect.Y;
            textPosition.X = (buttonRect.Width - textSize.X) / 2 + buttonRect.X;
        }

        public Boolean Clicked() {
            return Input.mouseClicked && !Input.lastMouseClicked && this.isInBounds();
        }

        public Boolean isInBounds() {
            //var mouseState = Mouse.GetState();

            if(Input.mouse.X < buttonRect.X + buttonRect.Width &&
               Input.mouse.X > buttonRect.X &&
               Input.mouse.Y < buttonRect.Y + buttonRect.Height &&
               Input.mouse.Y > buttonRect.Y) {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime) {
            
        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            spriteBatch.Draw(texture, buttonRect, Color.White);

            spriteBatch.DrawString(font, name, textPosition, isInBounds() ? HOVER_COLOR : TEXT_COLOR);
            spriteBatch.End();
        }
    }
}
