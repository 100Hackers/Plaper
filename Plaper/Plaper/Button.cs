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
        string name;
        Texture2D texture;

        public int X {
            get { return x; }
        }
        public int Y {
            get { return y; }
        }

        public Button(string name, Texture2D texture, int buttonX, int buttonY) {
            this.name = name;
            this.texture = texture;
            this.x = buttonX;
            this.y = buttonY;
        }

        public bool enterButton() {
            var mouseState = Mouse.GetState();

            if(mouseState.X <  + texture.Width &&
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

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);

            spriteBatch.Draw(texture, new Rectangle((int)X, (int)Y, texture.Width, texture.Height), Color.White);

            spriteBatch.End();

        }
    }
}
