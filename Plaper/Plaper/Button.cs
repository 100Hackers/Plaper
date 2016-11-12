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
        float  textScale;
        Vector2 textSize;
        Rectangle buttonRect;
        Boolean isClickable;

        public Button(string name, Texture2D texture, Rectangle buttonrect, Boolean clickable = true) {
            this.name = name;
            this.texture = texture;
            //this.buttonRect = new Rectangle(0, buttonrect.Y, Plaper.playWidth, (int)(buttonrect.Height * Plaper.BUTTON_HEIGHT * Plaper.playHeight));
            this.buttonRect = buttonrect;
            /*this.texture = new Texture2D(Output.graphics.GraphicsDevice  , buttonRect.Width, buttonRect.Height);
            Color[] data = new Color[buttonRect.Width * buttonRect.Height];
            for (int i = 0; i < data.Length; ++i) {
                data[i] = Color.Crimson;
            }
            this.texture.SetData(data);*/

            //if (Plaper.screenHeight > 1500) {
                this.font = Output.font108;
            //} else {
            //    this.font = Output.font36;
            //}

            this.isClickable = clickable;

            textSize = this.font.MeasureString(name);
            textScale = buttonRect.Height / textSize.Y;
            //textScale = Math.Min(textScale, buttonRect.Width/textSize.X);
            textScale *= 0.6f;

            //textScale = (int)(textScale *1.5);

            int stringHeight = (int) Math.Round(textSize.Y * textScale);
            int stringWidth  = (int) Math.Round(textSize.X * textScale);

            textPosition.Y = (buttonRect.Height - stringHeight)/2 + buttonRect.Y;
            //textPosition.Y = buttonRect.Height + buttonRect.Y;
            textPosition.X = (buttonRect.Width  - stringWidth) /2 + buttonRect.X;
            Console.WriteLine("but: " + buttonRect.Y);
            Console.WriteLine("but: " + buttonRect.Height);
            Console.WriteLine("txt: " + textPosition.Y);
            Console.WriteLine("txt: " + textSize.Y);
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

           // spriteBatch.Draw(texture, buttonRect, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            spriteBatch.Draw(texture, buttonRect, Color.White);

            if (isClickable)
            {
                //spriteBatch.DrawString(font, name, textPosition, isInBounds(Input.mouse) ? HOVER_COLOR : TEXT_COLOR);
                spriteBatch.DrawString(font, name, textPosition, isInBounds(Input.mouse) ? HOVER_COLOR : TEXT_COLOR, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
            } else
            {
                spriteBatch.DrawString(font, name, textPosition, Color.Black, 0f, Vector2.Zero, textScale, SpriteEffects.None, 0f);
                //spriteBatch.DrawString(font, name, textPosition, TEXT_COLOR);
            }
            spriteBatch.End();
        }
    }
}