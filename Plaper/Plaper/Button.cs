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
        Rectangle buttonRect;
        Boolean isClickable;

        public Button(string name, Texture2D texture, Rectangle buttonRect, Boolean clickable = true) {
            this.name = name;
            this.texture = texture;
            this.buttonRect = new Rectangle(0, buttonRect.Y, Plaper.playWidth, (int) (buttonRect.Height * Plaper.BUTTON_HEIGHT * Plaper.playHeight));
            this.font = Output.font36;
            this.isClickable = clickable;

            Vector2 textSize = this.font.MeasureString(name);
            textPosition.Y = (buttonRect.Height - textSize.Y) / 2 + buttonRect.Y;
            textPosition.X = (buttonRect.Width - textSize.X) / 2 + buttonRect.X;
        }

        public Boolean Clicked() {
            return !Input.mouseClicked && Input.lastMouseClicked && this.isInBounds(Input.lastMouse);
        }

        public Boolean isInBounds(Vector2 mouse) {
            if(mouse.X < buttonRect.X + buttonRect.Width &&
               mouse.X > buttonRect.X &&
               mouse.Y < buttonRect.Y + buttonRect.Height &&
               mouse.Y > buttonRect.Y) {
                return true;
            }
            return false;
        }

        public void Update(GameTime gameTime) {}

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            spriteBatch.Draw(texture, buttonRect, Color.White);

            if (isClickable)
            {
                spriteBatch.DrawString(font, name, textPosition, isInBounds(Input.mouse) ? HOVER_COLOR : TEXT_COLOR);
            } else
            {
                spriteBatch.DrawString(font, name, textPosition, TEXT_COLOR);
            }
            spriteBatch.End();
        }
    }
}